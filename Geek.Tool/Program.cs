using Scriban;
using System;
using System.IO;
using Tool.Logic;
using Tool.Test;

namespace Geek.Tool
{
    class Program
    {

        static bool isDebug = false;
        static void Main(string[] args)
        {
            //初始化配置信息
            if (!Setting.Init())
            {
                Console.WriteLine("----配置错误，启动失败----");
                return;
            }

            if (!isDebug)
            {
                Console.WriteLine("请按需输入指令:");
                Console.WriteLine("1.导出服务器");
                Console.WriteLine("2.导出客户端");
                Console.WriteLine("3.导出服务器+客户端");
                while (true)
                {
                    var key = Console.ReadKey().Key;
                    Console.WriteLine("你输入了:" + key.ToString());
                    switch (key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            new ProtoGen(1).Gen();
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            new ProtoGen(2).Gen();
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            new ProtoGen(3).Gen();
                            break;
                        default:
                            Console.WriteLine("输入指令错误");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("请按需输入指令:");
                Console.WriteLine("1.测试序列化");
                Console.WriteLine("2.性能测试");
                Console.WriteLine("3.导出协议代码");
                Console.WriteLine("-------------------------------------------");
                Test001 test = new Test001();
                while (true)
                {
                    var key = Console.ReadKey().Key;
                    Console.WriteLine("你输入了:" + key.ToString());
                    switch (key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            test.Test();
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            test.TestPerformance1();
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            new ProtoGen(1).Gen();
                            break;
                    }
                }
            }
        }
    }
}
