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
        public static string FactoryTemplatePath { get; set; }
        public static string ClientFactoryTemplatePath { get; set; }

        /// <summary>
        /// 消息模板路径
        /// </summary>
        public static string MessageTemplatePath { get; set; }
        public static string ClientMessageTemplatePath { get; set; }

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
        /// 是否支持枚举
        /// </summary>
        public static bool SupportEnum { private set; get; }


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
                        case "message-template-path":
                            MessageTemplatePath = node.InnerText;
                            break;
                        case "client-message-template-path":
                            ClientMessageTemplatePath = node.InnerText;
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
                        case "support-enum":
                            SupportEnum = bool.Parse(node.InnerText);
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
