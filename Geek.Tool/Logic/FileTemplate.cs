using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//https://github.com/lunet-io/scriban/blob/master/doc/language.md
namespace Tool.Logic
{

    public class FactoryTemplate
    {
        public List<ClassTemplate> sclasses { get; set; } = new List<ClassTemplate>();
    }

    public class PropTemplate
    {
        private static Dictionary<string, string> readDic = new Dictionary<string, string>()
        {
            { "bool","XBuffer.ReadBool(_buffer_, ref _offset_)"},
            { "sbyte","XBuffer.ReadSByte(_buffer_, ref _offset_)"},
            { "byte","XBuffer.ReadByte(_buffer_, ref _offset_)"},
            { "char","XBuffer.ReadChar(_buffer_, ref _offset_)"},
            { "short","XBuffer.ReadShort(_buffer_, ref _offset_)"},
            { "ushort","XBuffer.ReadUShort(_buffer_, ref _offset_)"},
            { "int", "XBuffer.ReadInt(_buffer_, ref _offset_)"},
            { "uint", "XBuffer.ReadUInt(_buffer_, ref _offset_)"},
            { "long","XBuffer.ReadLong(_buffer_, ref _offset_)"},
            { "ulong","XBuffer.ReadULong(_buffer_, ref _offset_)"},
            { "float","XBuffer.ReadFloat(_buffer_, ref _offset_)"},
            { "double","XBuffer.ReadDouble(_buffer_, ref _offset_)"},
            { "System.DateTime","XBuffer.ReadDateTime(_buffer_, ref _offset_)"},
            { "string","XBuffer.ReadString(_buffer_, ref _offset_)"},
            { "byte[]","XBuffer.ReadBytes(_buffer_, ref _offset_)"}
        };

        private static Dictionary<string, string> writeDic = new Dictionary<string, string>()
        {
            { "bool","XBuffer.WriteBool(%s, _buffer_, ref _offset_)"},
            { "sbyte","XBuffer.WriteSByte(%s, _buffer_, ref _offset_)"},
            { "byte","XBuffer.WriteByte(%s, _buffer_, ref _offset_)"},
            { "char","XBuffer.WriteChar(%s, _buffer_, ref _offset_)"},
            { "short","XBuffer.WriteShort(%s, _buffer_, ref _offset_)"},
            { "ushort","XBuffer.WriteUShort(%s, _buffer_, ref _offset_)"},
            { "int", "XBuffer.WriteInt(%s, _buffer_, ref _offset_)"},
            { "uint", "XBuffer.WriteUInt(%s, _buffer_, ref _offset_)"},
            { "long","XBuffer.WriteLong(%s, _buffer_, ref _offset_)"},
            { "ulong","XBuffer.WriteULong(%s, _buffer_, ref _offset_)"},
            { "float","XBuffer.WriteFloat(%s, _buffer_, ref _offset_)"},
            { "double","XBuffer.WriteDouble(%s, _buffer_, ref _offset_)"},
            { "System.DateTime","XBuffer.WriteDateTime(%s, _buffer_, ref _offset_)"},
            { "string","XBuffer.WriteString(%s, _buffer_, ref _offset_)"},
            { "byte[]","XBuffer.WriteBytes(%s, _buffer_, ref _offset_)"}
        };

        private static Dictionary<string, string> lengthDic = new Dictionary<string, string>()
        {
            { "bool","XBuffer.BoolSize"},
            { "sbyte","XBuffer.SByteSize"},
            { "byte","XBuffer.ByteSize"},
            { "char","XBuffer.CharSize"},
            { "short","XBuffer.ShortSize"},
            { "ushort","XBuffer.UShortSize"},
            { "int", "XBuffer.IntSize"},
            { "uint", "XBuffer.UIntSize"},
            { "long","XBuffer.LongSize"},
            { "ulong","XBuffer.ULongSize"},
            { "float","XBuffer.FloatSize"},
            { "double","XBuffer.DoubleSize"},
            { "System.DateTime","XBuffer.LongSize"},
        };

        public int idx { get; set; }
        public string name { get; set; }

        private string _clsname;
        public string clsname 
        {
            get { return _clsname; }
            set
            {
                _clsname = value;
                if(cinfo != null)
                    cinfo.clsname = value;
            }
        }


        public bool isenum { get; set; }
        public bool iscollection { get; set; }
        public bool isdic { get; set; }
        public bool iscustom { get; set; }
        public bool isprimitive { get; set; }
        public bool isstrictprimitive { get; set; }

        public bool istime { get; set; }

        public bool isnest { get; set; }

        public bool isstring { get { return clsname == "string"; } }
        public bool isbytearray { get { return clsname == "byte[]"; } }

        /// <summary>
        /// KeyValue中value的serialize class id
        /// </summary>
        public int clsid { get; set; }   

        public bool optional { get; set; }

        //基础类型有效
        public string readcode
        { 
            get 
            {
                if (isenum)
                    return $"({clsname})" + readDic["int"]; 
                else
                    return readDic[clsname];
            } 
        }

        //基础类型有效
        public string writecode 
        { 
            get 
            {
                if (isenum)
                    return writeDic["int"].Replace("%s", "(int)"+name);
                else
                    return writeDic[clsname].Replace("%s", name); 
            } 
        }

        //严格基础类型有效
        public string primitivelengthcode
        {
            get
            {
                if (isenum)
                    return "XBuffer.IntSize";
                else
                    return lengthDic[clsname];
            }
        }

        public string lengthcode
        {
            get
            {
                if (iscollection)
                {
                    if (isnest)
                        return $"len += GetCollectionLength({name});";
                    else
                    {
                        if (isdic)
                            return TypeParser.RenderMapLength(CollectionInfo);
                        else if (iscollection)
                            return TypeParser.RenderCollectionLength(CollectionInfo);
                    }
                }
                else if (iscustom)
                {
                    return $"len += GetCustomLength<{clsname}>({name});";
                }
                else if (isstring)
                {
                    return $"len += XBuffer.GetStringSerializeLength({name});"; 
                }
                else if (isbytearray)
                {
                    return $"len += XBuffer.GetByteArraySerializeLength({name});";
                }
                else if (isprimitive)
                {
                    if (isenum)
                        return "len += XBuffer.IntSize;";
                    else
                        return "len += " + lengthDic[clsname] + ";";
                }
                throw new Exception($"未知得数据类型:{clsname}");
            }
        }


        public bool isstate { get; set; }

        public string definecode
        { 
            get 
            {
                if (!Setting.AutoNew)
                    return "";
                if (iscollection || iscustom)
                    return isstate ? $"= new {clsname}()" : $"= new {clsname}();";
                else
                    return "";
            } 
        }


        #region 单层嵌套优化
        /// <summary>
        /// 用于优化单层嵌套模式，记录容器类型名
        /// </summary>
        private CollectionInfo cinfo;
        public CollectionInfo CollectionInfo
        {
            get 
            {
                if (cinfo == null)
                {
                    cinfo = new CollectionInfo();
                    cinfo.idx = idx;
                    cinfo.name = name;
                    cinfo.clsname = clsname;
                    cinfo.prop1.name = name;
                    cinfo.prop2.name = name;
                }
                return cinfo;
            }
        }

        public string readcollectioncode 
        {
            get 
            {
                if (isnest)
                    return null;
                if (isdic)
                {
                    return TypeParser.RenderReadMap(CollectionInfo);
                }
                else if (iscollection)
                {
                    return TypeParser.RenderReadCollection(CollectionInfo);
                }
                return null;
            }
        }

        public string writecollectioncode
        {
            get
            {
                if (isnest)
                    return null;
                if (isdic)
                {
                    return TypeParser.RenderWriteMap(CollectionInfo);
                }
                else if (iscollection)
                {
                    return TypeParser.RenderWriteCollection(CollectionInfo);
                }
                return null;
            }
        }

        public string listwritecode 
        { 
            get 
            { 
                if(isenum)
                    return writeDic["int"].Replace("%s", "(int)item"); 
                else
                    return writeDic[clsname].Replace("%s", "item");
            } 
        }
        public string mapkeywritecode 
        { 
            get 
            {
                if (isenum)
                    return writeDic["int"].Replace("%s", "(int)kv.Key");
                else
                    return writeDic[clsname].Replace("%s", "kv.Key"); 
            } 
        }
        public string mapvaluewritecode 
        { 
            get
            {
                if (isenum)
                    return writeDic["int"].Replace("%s", "(int)kv.Value"); 
                else
                    return writeDic[clsname].Replace("%s", "kv.Value");
            } 
        }
        #endregion  

    }

    public class CollectionInfo
    {
        public int idx { get; set; }
        public string name { get; set; }
        public string clsname { get; set; }
        public PropTemplate prop1 { get; set; } = new PropTemplate() {};
        public PropTemplate prop2 { get; set; } = new PropTemplate();
    }


    public class ClassTemplate
    {
        public int sid { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public string space { get; set; }
        /// <summary>
        /// Message:BaseMessage
        /// RPC:BaseMessage
        /// State:DBState BaseDBState
        /// </summary>
        public string super { get; set; }
        public int msgid { get; set; }
        //包含基类的属性
        public List<PropTemplate> fields { get; set; } = new List<PropTemplate>();
        public List<string> enumvalues { get; set; } = new List<string>();

        public bool isstate { get; set; }

        /// <summary>
        /// 是否为子类
        /// </summary>
        public bool issubclass { get; set; }

        public bool isenum { get; set; }
    }

    public class GenericInfo
    {
        public string formatter { get; set; }
        public string fullname { get; set; }
        public string arg0 { get; set; }
        public string arg1 { get; set; }
        public bool isdic { get; set; }

        public bool iscollection { get; set; }
        public bool iscustom { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is GenericInfo info)
            {
                return info.formatter == formatter
                        && info.fullname == fullname 
                        && info.arg0 == arg0
                        && info.arg1 == arg1;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            int a0 = arg0 == null ? 100 : arg0.GetHashCode();
            int a1 = arg1 == null ? 1000 : arg1.GetHashCode();
            return formatter.GetHashCode() ^ fullname.GetHashCode() ^ a0.GetHashCode() ^ a1.GetHashCode();
        }
    }

    /// <summary>
    /// 嵌套类型
    /// </summary>
    public class NestTypeTemplate
    {

        public List<GenericInfo> genericinfos = new List<GenericInfo>();
        public int count { get { return genericinfos.Count; } }

        public Dictionary<GenericInfo, bool> ResolverDic = new Dictionary<GenericInfo, bool>();

    }

}
