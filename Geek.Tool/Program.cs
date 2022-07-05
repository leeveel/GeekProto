using NLog;
using NLog.Config;
using System;
using Tool.Logic;

namespace Geek.Tool
{
    class Program
    {
        static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            LogManager.Configuration = new XmlLoggingConfiguration("Configs/NLog.config");
            LOGGER.Info("Geek.Proto.Tool start....");

            //初始化配置信息
            if (!Setting.Init())
            {
                Console.WriteLine("----配置错误，启动失败----");
                return;
            }

            Console.WriteLine("请按需输入指令:");
            Console.WriteLine("1.导出服务器");
            Console.WriteLine("2.导出客户端");
            Console.WriteLine("3.导出服务器+客户端");
            while (true)
            {
                var key = Console.ReadKey().KeyChar;
                Console.WriteLine("你输入了:" + key.ToString());
                switch (key)
                {
                    case '1':
                        new ProtoGen(1).Gen();
                        break;
                    case '2':
                        new ProtoGen(2).Gen();
                        break;
                    case '3':
                        new ProtoGen(3).Gen();
                        break;
                    default:
                        Console.WriteLine("输入指令错误");
                        break;
                }
            }

        }
    }
}
