using System.Collections.Generic;

//https://github.com/lunet-io/scriban/blob/master/doc/language.md
namespace Tool.Logic
{

    public class FactoryTemplate
    {
        public List<ClassTemplate> sclasses { get; set; } = new List<ClassTemplate>();
    }


    public enum NestModel
    {
        None = 0,
        List = 1,
        Set = 2,
        Dictionary = 3
    }

    public class PropTemplate
    {
        public int idx { get; set; }
        public string name { get; set; }
        public string clsname1 { get; set; }
        public string clsname2 { get; set; }  //KeyValue中value的类型
        public string clsname3 { get; set; }  //map嵌套map的key的类型
        public string clsname4 { get; set; }  //map嵌套map的value的类型
        public int clsid { get; set; }   //KeyValue中value的serialize class id
        /// list map, primitive
        public string type { get; set; }
        public bool optional { get; set; }
        public bool isenum { get; set; }
        /// <summary>
        /// 0:无嵌套 1：嵌套list 2：嵌套dictionary 3：嵌套Set
        /// </summary>
        public int nestmodel { get; set; }
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
        //有子类的类型
        public List<string> sublist = new List<string>();
    }

    public class TypeTemplate
    {
        public List<string> typelist { get; set; } = new List<string>();
    }


}
