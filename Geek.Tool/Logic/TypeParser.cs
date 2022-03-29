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

        public const string BaseMessage = "BaseMessage";
        public const string BaseSerializable = "Serializable";
        public const string BaseInterface = "Geek.Server.ISerializable";

        public static ClassTemplate ToTemplate(Type type)
        {
            //只有标记了SClass属性的才会被序列化
            var catt = GetClassAttribute(type);
            if (catt == null)
                return null;

            var ct = new ClassTemplate();
            ct.space = type.Namespace;
            ct.name = type.Name;
            ct.fullname = type.FullName;
            ct.super = GetSuperName(type, ct);
            ct.sid = GetPropertyValue<int>(catt, "Id");
            if (ct.super.Equals(BaseMessage))
                ct.msgid = ct.sid;

            var satt = GetClassAttribute(type, "IsStateAttribute");
            if(satt != null)
                ct.isstate = true;
            if (ct.isstate)
            {
                ct.listname = "StateList";
                ct.linklistname = "StateLinkedList";
                ct.setname = "StateSet";
                ct.mapname = "StateMap";
            }
            else 
            {
                ct.listname = "List";
                ct.linklistname = "LinkedList";
                ct.setname = "HashSet";
                ct.mapname = "Dictionary";
            }

            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            List<PropTemplate> propList = new List<PropTemplate>();
            foreach (var item in pros)
            {
                //只有标记SField属性的才会被序列化
                var fieldAtt = GetPropertyAttribute(item);
                if (fieldAtt == null)
                    continue;

                if (item.PropertyType.IsEnum && !Setting.SupportEnum)
                    throw new Exception($"工具配置为，不支持枚举类型:{item.PropertyType.FullName}");

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
                    prop.clsname1 = GetClassName(args[0]);
                    prop.isenum1 = args[0].IsEnum;

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
                                if (!Setting.SupportEnum)
                                    throw new Exception($"工具配置为，不支持枚举类型:{item.PropertyType.FullName}");
                                prop.isenum3 = true;
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
                            if (!IsKeyLegal(dicArgs[0]))
                                throw new Exception($"Map Key 不合法{dicArgs[0].FullName}，只能为基础类型");
                            prop.clsname3 = GetClassName(dicArgs[0]);
                            prop.isenum3 = dicArgs[0].IsEnum;
                            if (!IsPrimitive(dicArgs[1]))
                            {
                                prop.clsname4 = GetClassName(dicArgs[1]);
                                prop.nestmodel = (int)NestModel.Dictionary;
                                if (dicArgs[1].IsEnum)
                                {
                                    if (!Setting.SupportEnum)
                                        throw new Exception($"工具配置为，不支持枚举类型:{item.PropertyType.FullName}");
                                    prop.isenum4 = true;
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
                                prop.clsname4 = GetClassName(dicArgs[1]);
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
                                if (!Setting.SupportEnum)
                                    throw new Exception($"工具配置为，不支持枚举类型:{item.PropertyType.FullName}");
                                prop.isenum3 = true;
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
                            prop.clsname2 = GetClassName(param2);
                            prop.nestmodel = 0;
                            if (param2.IsEnum)
                            {
                                if (!Setting.SupportEnum)
                                    throw new Exception($"工具配置为，不支持枚举类型:{item.PropertyType.FullName}");
                                prop.isenum2 = param2.IsEnum;
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
                        prop.clsname2 = GetClassName(param2);
                        prop.isenum2 = param2.IsEnum;
                        if (prop.isenum2 && !Setting.SupportEnum)
                            throw new Exception($"工具配置为，不支持枚举类型:{item.PropertyType.FullName}");
                    }
                }
                else if (prop.type.Equals("list") || prop.type.Equals("set")) // list - set
                {
                    var listType = item.PropertyType.GenericTypeArguments[0];
                    if (IsPrimitive(listType))
                    {
                        prop.clsname1 = GetClassName(listType);
                        prop.isenum1 = listType.IsEnum;
                        if (prop.isenum1 && !Setting.SupportEnum)
                            throw new Exception($"工具配置为，不支持枚举类型:{item.PropertyType.FullName}");
                    }
                    else
                    {
                        var att = GetClassAttribute(listType);
                        if (att == null)
                            throw new Exception($"此类型不可序列化{listType.FullName}");
                        prop.clsname1 = GetClassName(listType);
                        prop.clsid = GetPropertyValue<int>(att, "Id");
                    }
                }
                else //普通属性
                {
                    prop.clsname1 = GetClassName(item.PropertyType);
                    prop.isenum1 = item.PropertyType.IsEnum;
                    if (prop.isenum1 && !Setting.SupportEnum)
                        throw new Exception($"工具配置为，不支持枚举类型:{item.PropertyType.FullName}");
                }
            }
            ct.fields.AddRange(propList.OrderBy(prop => prop.idx));
            return ct;
        }


        public static bool IsKeyLegal(Type type)
        {
            if (type.IsEnum)
                return Setting.SupportEnum;
            return type.IsPrimitive || type.Equals(typeof(string));
        }

        public static bool IsPrimitive(Type type)
        {
            return  type.IsPrimitive || type.Equals(typeof(string));
        }


        public static string GetClassName(Type propType)
        {
            switch (propType.Name)
            {
                case "Byte":
                    return "byte";
                case "SByte":
                    return "sbyte";
                case "Boolean":
                    return "bool";
                case "Int32":
                    return "int";
                case "Int64":
                    return "long";
                case "Single":
                    return "float";
                case "Int16":
                    return "short";
                case "Double":
                    return "double";
                case "Byte[]":
                    return "byte[]";
                case "String":
                    return "string";
                default:
                    break;
            }
            return propType.FullName;
        }

        public static string GetTypeName(Type propType)
        {

            if (IsPrimitive(propType))
                return "field";

            else if (propType.Name.Contains("Dictionary"))
                return "map";
            else if (propType.Name.Contains("List"))
                return "list";
            else if (propType.Name.Contains("HashSet"))
                return "set";
            else if (propType.Name.Contains("StateMap"))
                return "statemap";
            else if (propType.Name.Contains("StateList"))
                return "statelist";
            else if (propType.Name.Contains("StateSet"))
                return "stateset";

            return propType.FullName;
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
