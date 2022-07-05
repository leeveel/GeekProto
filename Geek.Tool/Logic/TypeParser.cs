using Geek.Tool;
using Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tool.Logic
{
    public static class TypeParser
    {

        public const string FieldAttributeName = "SFieldAttribute";
        public const string PropertyAttributeName = "SPropertyAttribute";
        public const string ClassAttributeName = "SClassAttribute";

        public const string BaseMessage = "BaseMessage";
        public const string BaseSerializable = "Serializable";
        public const string BaseInterface = "Geek.Server.ISerializable";

        public static ClassTemplate ToTemplate(Type type, NestTypeTemplate nestType)
        {
            //只有标记了SClass属性的才会被序列化
            var catt = GetClassAttribute(type);
            if (catt == null)
                return null;

            List<PropTemplate> propList = new List<PropTemplate>();
            var ct = new ClassTemplate();
            if (type.IsEnum)
            {
                ct.isenum = true;
                ct.space = type.Namespace;
                ct.name = type.Name;

                var enames = type.GetEnumNames();
                var evals = type.GetEnumValues();
                int i = 0;
                foreach (int v in evals)
                {
                    ct.enumvalues.Add(enames[i] + " = " + v + ",");
                    i++;
                }
            }
            else
            {
                ct.space = type.Namespace;
                ct.name = type.Name;
                ct.fullname = type.FullName;
                ct.super = GetSuperName(type, ct);
                ct.sid = GetPropertyValue<int>(catt, "Id");
                if (ct.super.Equals(BaseMessage))
                    ct.msgid = ct.sid;

                var satt = GetClassAttribute(type, "IsStateAttribute");
                if (satt != null)
                    ct.isstate = true;

                var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var item in pros)
                {
                    //只有标记SField属性的才会被序列化
                    var fieldAtt = GetPropertyAttribute(item);
                    if (fieldAtt == null)
                        continue;

                    var prop = new PropTemplate();
                    prop.idx = GetPropertyValue<int>(fieldAtt, "Id");
                    prop.optional = GetPropertyValue<bool>(fieldAtt, "Optional");
                    propList.Add(prop);
                    prop.isstate = ct.isstate;
                    prop.name = item.Name;
                    GetTypeInfo(prop, item.PropertyType); //获取type类型
                    if (prop.isdic)  // map
                    {
                        var args = item.PropertyType.GenericTypeArguments;
                        //map key只能是：基础类型或string
                        if (!IsKeyLegal(args[0]))
                            throw new Exception($"Map Key 不合法{args[0].FullName}，只能为基础类型或string或枚举");
                        prop.clsname = item.PropertyType.GetNiceName();
                        if (ct.isstate)
                            prop.clsname = ToStateName(prop.clsname);
                        AddResolverInfo(item.PropertyType, nestType, ct.isstate, prop.isdic);
                    }
                    else if (prop.iscollection) // list - set
                    {
                        prop.clsname = item.PropertyType.GetNiceName();
                        if (ct.isstate)
                            prop.clsname = ToStateName(prop.clsname);
                        AddResolverInfo(item.PropertyType, nestType, ct.isstate, prop.isdic);
                    }
                    else if (prop.iscustom)
                    {
                        prop.clsname = GetClassName(item.PropertyType);
                        prop.isenum = item.PropertyType.IsEnum;
                        AddResolverInfo(item.PropertyType, nestType, ct.isstate, prop.isdic);
                    }
                    else //普通属性
                    {
                        prop.clsname = GetClassName(item.PropertyType);
                        prop.isenum = item.PropertyType.IsEnum;
                    }
                }
            }
            if (propList.Count > 0)
            {
                ct.fields.AddRange(propList.OrderBy(prop => prop.idx));
                CheckPropId(ct.name, propList);
            }
            return ct;
        }

        /// <summary>
        /// TODO：检查id是否连贯，重复
        /// </summary>
        public static bool CheckPropId(string className, List<PropTemplate> list)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (dic.ContainsKey(list[i].idx))
                    throw new Exception($"{className}.SProperty.Id存在重复:{list[i]}");
                else
                    dic.Add(list[i].idx, list[i].idx);
                if (list[i].idx != i)
                {
                    throw new Exception($"{className}.SProperty.Id不连贯:{list[i].idx}:{i}");
                }
            }
            return true;
        }


        public static void AddResolverInfo(Type type, NestTypeTemplate nestType, bool isState, bool isdic)
        {
            if (type.IsGenericType)
            {
                var info = new GenericInfo
                {
                    fullname = GetTrueName(type, isState),
                    formatter = GetFormatterName(type, isState)
                };
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    info.isdic = true;
                    var args = type.GenericTypeArguments;
                    info.arg0 = GetTrueName(args[0], isState);
                    info.arg1 = GetTrueName(args[1], isState);
                }
                else
                {
                    info.iscollection = true;
                    var args = type.GenericTypeArguments;
                    info.arg0 = GetTrueName(args[0], isState);
                }

                if (nestType.ResolverDic.TryAdd(info, true))
                    nestType.genericinfos.Add(info);

                foreach (var typeArgument in type.GenericTypeArguments)
                {
                    AddResolverInfo(typeArgument, nestType, isState, isdic);
                }
            }
            else if (IsCustom(type))
            {
                var info = new GenericInfo
                {
                    fullname = type.GetNiceName(true),
                    formatter = "CustomFormatter",
                    iscustom = true
                };
                if (nestType.ResolverDic.TryAdd(info, true))
                    nestType.genericinfos.Add(info);
            }
        }

        public static string GetTrueName(Type type, bool isState)
        {
            bool iscustom = IsCustom(type);
            var fullname = type.GetNiceName();
            if (isState && !iscustom)
                fullname = ToStateName(fullname);
            return fullname;
        }

        public static string ToStateName(string org)
        {
            return org.Replace("List", "StateList")
                    .Replace("LinkedList", "StateLinkedList")
                    .Replace("HashSet", "StateSet")
                    .Replace("Dictionary", "StateMap");
        }

        public static string GetFormatterName(Type type, bool isState)
        {
            if (type.Name.StartsWith("Dictionary`2"))
            {
                if (isState)
                    return "StateMapFormatter";
                else
                    return "DictionaryFormatter";
            }
            else if (type.Name.StartsWith("List`1"))
            {
                if (isState)
                    return "StateListFormatter";
                else
                    return "ListFormatter";
            }
            else if (type.Name.StartsWith("LinkedList`1"))
            {
                if (isState)
                    return "StateLinkedListFormatter";
                else
                    return "LinkedListFormatter";
            }
            else if (type.Name.StartsWith("HashSet`1"))
            {
                if (isState)
                    return "StateSetFormatter";
                else
                    return "HasSetFormatter";
            }

            if (IsCustom(type))
                return "CustomFormatter";

            throw new Exception($"未知的数据类型：{type}");
        }

        public static bool IsCustom(Type type)
        {
            if (type.IsEnum)
                return false;
            var catt = GetClassAttribute(type);
            if (catt != null)
                return true;
            return false;
        }

        public static bool IsKeyLegal(Type type)
        {
            return type.IsPrimitive || type.IsEnum || type.Equals(typeof(string)) || type.Equals(typeof(System.DateTime));
        }

        public static bool IsPrimitive(Type type)
        {
            return  type.IsPrimitive || type.IsEnum 
                || type.Equals(typeof(string)) 
                || type.Equals(typeof(System.DateTime))
                || type.Equals(typeof(byte[]));
        }


        public static string GetClassName(Type propType)
        {
            switch (propType.Name)
            {
                case "Boolean":
                    return "bool";
                case "SByte":
                    return "sbyte";
                case "Byte":
                    return "byte";
                case "Char":
                    return "char";
                case "Int16":
                    return "short";
                case "UInt16":
                    return "ushort";
                case "Int32":
                    return "int";
                case "UInt32":
                    return "uint";
                case "Int64":
                    return "long";
                case "UInt64":
                    return "ulong";
                case "Single":
                    return "float";
                case "Double":
                    return "double";
                //case "DateTime":
                //    return "time";
                case "String":
                    return "string";
                case "Byte[]":
                    return "byte[]";
                default:
                    break;
            }
            return propType.FullName;
        }

        public static void GetTypeInfo(PropTemplate prop, Type propType)
        {
            if (propType.IsPrimitive || propType.IsEnum || IsDateTime(propType))
            {
                prop.isprimitive = true;
                prop.isstrictprimitive = true;
                return;
            }
            if (IsPrimitive(propType))
            {
                prop.isprimitive = true;
                prop.istime = IsDateTime(propType);
                return;
            }
            else if (propType.Name.StartsWith("Dictionary`2"))
            {
                prop.iscollection = true;
                prop.isdic = true;

                if (propType.GenericTypeArguments[1].IsGenericType)
                {
                    prop.isnest = true;
                }
                else
                {
                    var t = propType.GenericTypeArguments[0];
                    prop.CollectionInfo.prop1.iscustom = IsCustom(t);
                    prop.CollectionInfo.prop1.isenum = t.IsEnum;
                    GetTypeInfo(prop.CollectionInfo.prop1, t);
                    if (prop.CollectionInfo.prop1.iscustom || prop.CollectionInfo.prop1.isenum)
                        prop.CollectionInfo.prop1.clsname = t.FullName;
                    else
                        prop.CollectionInfo.prop1.clsname = GetClassName(t);

                    var t2 = propType.GenericTypeArguments[1];
                    prop.CollectionInfo.prop2.iscustom = IsCustom(t2);
                    prop.CollectionInfo.prop2.isenum = t2.IsEnum;
                    GetTypeInfo(prop.CollectionInfo.prop2, t2);
                    if (prop.CollectionInfo.prop2.iscustom || prop.CollectionInfo.prop2.isenum || prop.CollectionInfo.prop2.istime)
                        prop.CollectionInfo.prop2.clsname = t2.FullName;
                    else
                        prop.CollectionInfo.prop2.clsname = GetClassName(t2);

                }
                return;
            }
            else if (propType.Name.StartsWith("List`1") 
                || propType.Name.StartsWith("LinkedList`1")
                || propType.Name.StartsWith("HashSet`1"))
            {
                prop.iscollection = true;
                if (propType.GenericTypeArguments[0].IsGenericType)
                {
                    prop.isnest = true;
                }
                else
                {
                    var t = propType.GenericTypeArguments[0];
                    prop.CollectionInfo.prop1.iscustom = IsCustom(t);
                    prop.CollectionInfo.prop1.isenum = t.IsEnum;
                    GetTypeInfo(prop.CollectionInfo.prop1, t);
                    if (prop.CollectionInfo.prop1.iscustom || prop.CollectionInfo.prop1.isenum || prop.CollectionInfo.prop2.istime)
                        prop.CollectionInfo.prop1.clsname = t.FullName;
                    else
                        prop.CollectionInfo.prop1.clsname = GetClassName(t);
                }
                return;
            }

            prop.iscustom = IsCustom(propType);
        }

        public static string GetSuperName(Type type, ClassTemplate ct)
        {
            //Message 之间不能相互继承
            var att = GetClassAttribute(type);
            if (att == null)
                throw new Exception($"此类型不可序列化{type.FullName}");
            bool isMsg = GetPropertyValue<bool>(att, "IsMsg");
            if (isMsg) return BaseMessage;

            string baseName = type.BaseType.Name;
            //没有任何基类，则默认继承Serializable
            if (baseName.Equals(typeof(object).Name))
                return BaseSerializable;

            if (baseName.Equals(BaseMessage))
                return BaseMessage;
            if (baseName.Equals(BaseSerializable))
                return BaseSerializable;


            var baseAtt = GetClassAttribute(type.BaseType);
            if (baseAtt == null)
                throw new Exception($"不合法的基类{type.BaseType.FullName},基类不可序列化");
            
            //子类
            ct.issubclass = true;
            return baseName;
        }

        public static bool IsDateTime(Type type)
        {
            return type.Equals(typeof(DateTime));
        }

        public static object GetClassAttribute(Type type, string name = ClassAttributeName)
        {
            var att = type.GetCustomAttributes();
            foreach (var item in att)
            {
                if (item.GetType().Name.Equals(name))
                    return item;
            }
            return null;
        }

        public static object GetFieldAttribute(FieldInfo field)
        {
            var att = field.GetCustomAttributes();
            foreach (var item in att)
            {
                if (item.GetType().Name.Equals(FieldAttributeName))
                    return item;
            }
            return null;
        }

        public static object GetPropertyAttribute(PropertyInfo prop)
        {
            var att = prop.GetCustomAttributes();
            foreach (var item in att)
            {
                if (item.GetType().Name.Equals(PropertyAttributeName))
                    return item;
            }
            return null;
        }

        public static T GetPropertyValue<T>(object target, string propName)
        {
            return (T)target.GetType().GetProperty(propName).GetValue(target);
        }



        public static string GetNiceName(this Type type)
        {
            if (!type.IsGenericType)
            {
                return IsCustom(type) ? type.FullName : type.Name;
            }
            var typeNameBuilder = new StringBuilder();
            GetNiceGenericName(typeNameBuilder, type);
            return typeNameBuilder.ToString();
        }

        private static void GetNiceGenericName(StringBuilder sb, Type type)
        {
            bool useFullName = IsCustom(type);
            if (!type.IsGenericType)
            {
                sb.Append(useFullName ? type.FullName : type.Name);
                return;
            }

            var typeDef = type.GetGenericTypeDefinition();
            var typeName = useFullName ? typeDef.FullName : typeDef.Name;
            sb.Append(typeName);
            sb.Length -= typeName.Length - typeName.LastIndexOf('`');
            sb.Append('<');
            foreach (var typeArgument in type.GenericTypeArguments)
            {
                GetNiceGenericName(sb, typeArgument);
                sb.Append(", ");
            }
            sb.Length -= 2;
            sb.Append('>');
        }

        private static Template readCollectionTp;
        public static string RenderReadCollection(CollectionInfo info)
        {
            if(readCollectionTp == null)
                readCollectionTp = Template.Parse(File.ReadAllText(Setting.CollectionTemplatePath + "ReadCollection.liquid"));
            return readCollectionTp.Render(info);
        }


        private static Template readMapTp;
        public static string RenderReadMap(CollectionInfo info)
        {
            if (readMapTp == null)
                readMapTp = Template.Parse(File.ReadAllText(Setting.CollectionTemplatePath + "ReadMap.liquid"));
            return readMapTp.Render(info);
        }

        private static Template writeCollectionTp;
        public static string RenderWriteCollection(CollectionInfo info)
        {
            if (writeCollectionTp == null)
                writeCollectionTp = Template.Parse(File.ReadAllText(Setting.CollectionTemplatePath + "WriteCollection.liquid"));
            return writeCollectionTp.Render(info);
        }


        private static Template writeMapTp;
        public static string RenderWriteMap(CollectionInfo info)
        {
            if (writeMapTp == null)
                writeMapTp = Template.Parse(File.ReadAllText(Setting.CollectionTemplatePath + "WriteMap.liquid"));
            return writeMapTp.Render(info);
        }


        private static Template collectionLengthTp;
        public static string RenderCollectionLength(CollectionInfo info)
        {
            if (collectionLengthTp == null)
                collectionLengthTp = Template.Parse(File.ReadAllText(Setting.CollectionTemplatePath + "CollectionLength.liquid"));
            return collectionLengthTp.Render(info);
        }

        private static Template mapLengthTp;
        public static string RenderMapLength(CollectionInfo info)
        {
            if (mapLengthTp == null)
                mapLengthTp = Template.Parse(File.ReadAllText(Setting.CollectionTemplatePath + "MapLength.liquid"));
            return mapLengthTp.Render(info);
        }


    }
}
