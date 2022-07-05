using System.IO;
using System.Xml;
using System;

namespace Tool.Logic
{
    public static class Setting
    {

        /// <summary>
        /// 工厂模板路径
        /// </summary>
        public static string ResolverTemplatePath { get; set; }
        public static string ClientResolverTemplatePath { get; set; }

        /// <summary>
        /// 工厂模板路径
        /// </summary>
        public static string FactoryTemplatePath { get; set; }
        public static string ClientFactoryTemplatePath { get; set; }

        /// <summary>
        /// 消息模板路径
        /// </summary>
        public static string MessageTemplatePath { get; set; }
        public static string ClientMessageTemplatePath { get; set; }

        public static string CollectionTemplatePath { get; set; }
        /// <summary>
        /// proto dll path
        /// </summary>
        public static string DllPath { set; get; }


        /// <summary>
        /// 服务器代码导出路径
        /// </summary>
        public static string ServerOutPath { private set; get; }

        /// <summary>
        /// 客户端代码导出路径
        /// </summary>
        public static string ClientOutPath { private set; get; }

        /// <summary>
        ///  成员变量中复杂对象和容器是否直接new好
        ///  public A 
        ///  {
        ///     public List<int> list {get;set;} = new List<int>();   ==> AutoNew = true
        ///     public List<int> list {get;set;}                              ==> AutoNew = false
        ///  }
        /// </summary>
        public static bool AutoNew { private set; get; }


        public static bool Init()
        {
            if (File.Exists("Configs/config.xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Configs/config.xml");
                XmlElement root = doc.DocumentElement;
                XmlNode listNodes = root.SelectNodes("/config").Item(0);
                foreach (XmlNode node in listNodes)
                {
                    switch (node.Name)
                    {
                        case "factory-template-path":
                            FactoryTemplatePath = node.InnerText;
                            break;
                        case "client-factory-template-path":
                            ClientFactoryTemplatePath = node.InnerText;
                            break;
                        case "resolver-template-path":
                            ResolverTemplatePath = node.InnerText;
                            break;
                        case "client-resolver-template-path":
                            ClientResolverTemplatePath = node.InnerText;
                            break;
                        case "message-template-path":
                            MessageTemplatePath = node.InnerText;
                            break;
                        case "client-message-template-path":
                            ClientMessageTemplatePath = node.InnerText;
                            break;
                        case "collection-template-path":
                            CollectionTemplatePath = node.InnerText;
                            break;
                        case "dll-path":
                            DllPath = node.InnerText;
                            DllPath = Path.GetFullPath(DllPath);
                            break;
                        case "server-out-path":
                            ServerOutPath = node.InnerText;
                            break;
                        case "client-out-path":
                            ClientOutPath = node.InnerText;
                            break;
                        case "auto-new":
                            //AutoNew = bool.Parse(node.InnerText);
                            AutoNew = false;
                            break;
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("服务器配置文件错误或不存在,启动失败!");
                return false;
            }
        }

    }
}
