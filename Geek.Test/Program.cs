namespace Geek.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请按需输入指令:");
            Console.WriteLine("1.测试序列化");
            Console.WriteLine("2.性能测试");
            Console.WriteLine("3.导出协议代码");
            Console.WriteLine("4.导出Resolver");
            Console.WriteLine("5.导出工具类");
            Console.WriteLine("-------------------------------------------");
            Test002 test = new Test002();
            while (true)
            {
                var key = Console.ReadKey().KeyChar;
                Console.WriteLine("你输入了:" + key.ToString());
                switch (key)
                {
                    case '1':
                        test.TestPerformance();
                        break;
                    case '2':
                        test.TestFor();
                        break;
                    case '3':
                        test.TestEnum();
                        break;
                    case '4':
                        test.GetEnumNames();
                        break;
                    case '5':
                        break;
                }
            }
        }
    }
}