using Scriban;
using System;
using System.IO;
using Tool.Logic;
using Tool.Test;

namespace Geek.Tool
{
    class Program
    {
        static void Main(string[] args)
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
                    //关闭监控进程
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        test.Test();
                        break;
                    //关闭开机自启动
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        test.TestPerformance1();
                        break;
                    //关闭监控进程，并且同时关闭开机自启动
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        string basePath = AppDomain.CurrentDomain.BaseDirectory;
                        string factoryTemplatePath = @"Template\Factory.liquid";
                        string templatePath = @"Template\Message.liquid";
                        string dllPath = basePath + "Geek.Proto.dll";
                        string outputPath = @"..\Geek.Proto\ProtoGen\";
                        new ProtoGen(factoryTemplatePath, templatePath, dllPath, outputPath, false).Gen();
                        break;
                }
            }

        }
    }
}
