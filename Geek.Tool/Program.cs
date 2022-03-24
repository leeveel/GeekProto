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
            Console.WriteLine("4.导出序列化基类");
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
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        string serializeTemplatePath = @"Template\Serialize.liquid";
                        Template serializeTemplate = Template.Parse(File.ReadAllText(serializeTemplatePath));

                        var typeTemp = new TypeTemplate();
                        typeTemp.typelist.Add("byte");
                        typeTemp.typelist.Add("sbyte");
                        typeTemp.typelist.Add("bool");
                        typeTemp.typelist.Add("short");
                        typeTemp.typelist.Add("int");
                        typeTemp.typelist.Add("long");
                        typeTemp.typelist.Add("float");
                        typeTemp.typelist.Add("double");
                        typeTemp.typelist.Add("string");
                        //typeTemp.typelist.Add("byte[]");

                        string outPath = @"..\Geek.Proto\ProtoGen\SerializeTool.cs";
                        var str = serializeTemplate.Render(typeTemp);
                        File.WriteAllText(outPath, str);

                        break;
                }
            }

        }
    }
}
