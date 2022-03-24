using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tool.Logic
{
    public class TypeParser
    {

        public const string FieldAttributeName = "SFieldAttribute";
        public const string PropertyAttributeName = "SPropertyAttribute";
        public const string ClassAttributeName = "SClassAttribute";

        public const string BaseName1 = "DBState";
        public const string BaseName2 = "BaseDBState";
        public const string BaseName3 = "Message";
        public const string BaseSerializable = "Serializable";
        public const string BaseInterface = "Core.Serialize.ISerializable";

        public static ClassTemplate ToTemplate(Type type, bool canUseId)
        {
            //只有标记了SClass属性的才会被序列化
            var catt = GetClassAttribute(type);
            if (catt == null)
                return null;

            Console.WriteLine(type.FullName);

            var ct = new ClassTemplate();
            ct.space = type.Namespace;
            ct.name = type.Name;
            ct.fullname = type.FullName;
            ct.super = GetSuperName(type);
            ct.sid = GetPropertyValue<int>(catt, "Id");
            if (ct.super.Equals(BaseName3))
                ct.msgid = ct.sid;

            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            List<PropTemplate> propList = new List<PropTemplate>();
            foreach (var item in pros)
            {
                //只有标记SField属性的才会被序列化
                var fieldAtt = GetPropertyAttribute(item);
                if (fieldAtt == null)
                    continue;

                //检查是否标记了partial


                var prop = new PropTemplate();
                prop.idx = GetPropertyValue<int>(fieldAtt, "Id");
                prop.optional = GetPropertyValue<bool>(fieldAtt, "Optional");
                propList.Add(prop);
                prop.name = item.Name;

                prop.type = GetTypeName(item.PropertyType);
                if (prop.type.Equals("map"))  // map
                {
                    var args = item.PropertyType.GenericTypeArguments;
                    //map key只能是：short, int, long, string
                    if(!IsKeyLegal(args[0]))
                        throw new Exception($"Map Key 不合法{args[0].FullName}，只能为short, int, long, string");
                    prop.clsname1 = GetTypeName(args[0]);

                    Type param2 = args[1];
                    if (!IsPrimitive(param2))
                    {
                        var paramName = GetTypeName(param2);
                        //处理嵌套list
                        if (paramName.Equals("list"))
                        {
                            var listType = param2.GenericTypeArguments[0];
                            prop.clsname2 = "list";
                            prop.clsname3 = listType.Name;
                            prop.nestmodel = (int)NestModel.List;
                            if (listType.IsEnum)
                            {
                                prop.isenum = true;
                            }
                            else
                            {
                                var att = GetClassAttribute(listType);
                                if (att == null)
                                    throw new Exception($"此类型不可序列化{listType.FullName}");
                                prop.clsid = GetPropertyValue<int>(att, "Id");
                            }
                        }
                        //处理嵌套map
                        else if (paramName.Equals("map"))
                        {
                            var dicArgs = param2.GenericTypeArguments;
                            prop.clsname2 = "map";
                            //map key只能是：short, int, long, string
                            if (!IsKeyLegal(dicArgs[0]))
                                throw new Exception($"Map Key 不合法{dicArgs[0].FullName}，只能为short, int, long, string");
                            prop.clsname3 = GetTypeName(dicArgs[0]);
                            if (!IsPrimitive(dicArgs[1]))
                            {
                                prop.clsname4 = GetTypeName(dicArgs[1]);
                                prop.nestmodel = (int)NestModel.Dictionary;
                                if (dicArgs[1].IsEnum)
                                {
                                    prop.isenum = true;
                                }
                                else
                                {
                                    var att = GetClassAttribute(dicArgs[1]);
                                    if (att == null)
                                        throw new Exception($"此类型不可序列化{dicArgs[1].FullName}");
                                    prop.clsid = GetPropertyValue<int>(att, "Id");
                                }
                            }
                            else
                            {
                                prop.clsname4 = GetTypeName(dicArgs[1]);
                                prop.nestmodel = (int)NestModel.Dictionary;
                            }
                        }
                        //处理嵌套set
                        else if (paramName.Equals("set"))
                        {
                            var listType = param2.GenericTypeArguments[0];
                            prop.clsname2 = "set";
                            prop.nestmodel = (int)NestModel.Set;
                            prop.clsname3 = listType.Name;
                            if (listType.IsEnum)
                            {
                                prop.isenum = true;
                            }
                            else
                            {
                                var att = GetClassAttribute(listType);
                                if (att == null)
                                    throw new Exception($"此类型不可序列化{listType.FullName}");
                                prop.clsid = GetPropertyValue<int>(att, "Id");
                            }
                        }
                        //无嵌套
                        else
                        {
                            prop.clsname2 = GetTypeName(param2);
                            prop.nestmodel = 0;
                            if (param2.IsEnum)
                            {
                                prop.isenum = param2.IsEnum;
                            }
                            else
                            {
                                var att = GetClassAttribute(param2);
                                if (att == null)
                                    throw new Exception($"此类型不可序列化{param2.FullName}");
                                prop.clsid = GetPropertyValue<int>(att, "Id");
                            }
                        }
                    }
                    else
                    {
                        prop.clsname2 = GetTypeName(param2);
                        prop.isenum = param2.IsEnum;
                    }
                }
                else if (prop.type.Equals("list") || prop.type.Equals("set")) // list - set
                {
                    var listType = item.PropertyType.GenericTypeArguments[0];
                    if (IsPrimitive(listType))
                    {
                        prop.clsname1 = GetTypeName(listType);
                        prop.isenum = listType.IsEnum;
                    }
                    else
                    {
                        var att = GetClassAttribute(listType);
                        if (att == null)
                            throw new Exception($"此类型不可序列化{listType.FullName}");
                        prop.clsname1 = GetTypeName(listType);
                        prop.clsid = GetPropertyValue<int>(att, "Id");
                    }
                }
                else //普通属性
                {
                    prop.clsname1 = prop.type;
                    prop.isenum = item.PropertyType.IsEnum;
                }
            }
            ct.fields.AddRange(propList.OrderBy(prop => prop.idx));
            return ct;
        }


        public static bool IsKeyLegal(Type type)
        {
            return type.Equals(typeof(int)) || type.Equals(typeof(string)) || type.Equals(typeof(long)) || type.Equals(typeof(short));
        }

        public static bool IsPrimitive(Type type)
        {
            return  type.IsPrimitive || type.Equals(typeof(string));
        }

        public static string GetTypeName(Type propType)
        {
            switch (propType.Name)
            {
                case "Int32":
                    return "int";
                case "Int64":
                    return "long";
                case "Boolean":
                    return "bool";
                case "Single":
                    return "float";
                case "Int16":
                    return "short";
                case "Double":
                    return "double";
                case "Byte":
                    return "byte";
                case "Byte[]":
                    return "byte[]";
                case "String":
                    return "string";
                default:
                    break;
            }

            if (propType.Name.Contains("Dictionary"))
                return "map";

            if (propType.Name.Contains("List"))
                return "list";

            if (propType.Name.Contains("HashSet"))
                return "set";

            return propType.FullName;
        }


        public static string GetSuperName(Type type)
        {
            string baseName = type.BaseType.Name;

            //没有任何基类，则默认继承Serializable
            if (baseName.Equals(typeof(object).Name))
                return BaseSerializable;

            if (baseName.Equals(BaseName1))
                return BaseName1;
            if (baseName.Equals(BaseName2))
                return BaseName2;
            if (baseName.Equals(BaseName3))
                return BaseName3;
            if (baseName.Equals(BaseSerializable))
                return BaseSerializable;

            string baseBaseName = type.BaseType.BaseType.Name;
            //都不符合，则检查基类的基类是否为Object
            if (baseBaseName.Equals(BaseSerializable) || baseBaseName.Equals(typeof(object).Name))
                return baseName;

            throw new Exception($"不合法的基类{type.BaseType.FullName}");
        }

        public static object GetClassAttribute(Type type)
        {
            var att = type.GetCustomAttributes();
            foreach (var item in att)
            {
                //if (item.GetType().Name.Contains("SClassAttribute"))
                if (item.GetType().Name.Equals(ClassAttributeName))
                    return item;
            }
            return null;
        }

        public static object GetFieldAttribute(FieldInfo field)
        {
            var att = field.GetCustomAttributes();
            foreach (var item in att)
            {
                //if (item.GetType().Name.Contains("SClassAttribute"))
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
                //if (item.GetType().Name.Contains("SClassAttribute"))
                if (item.GetType().Name.Equals(PropertyAttributeName))
                    return item;
            }
            return null;
        }

        public static T GetPropertyValue<T>(object target, string propName)
        {
            return (T)target.GetType().GetProperty(propName).GetValue(target);
        }

    }
}
