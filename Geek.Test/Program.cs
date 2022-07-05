namespace Geek.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请按需输入指令:");
            Console.WriteLine("1.测试1");
            Console.WriteLine("2.测试2");
            Console.WriteLine("3.测试3");
            Console.WriteLine("4.测试4");
            Console.WriteLine("5.测试5");
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
                        test.TestSpan();
                        break;
                    case '3':
                        test.TestEnum();
                        break;
                    case '4':
                        test.TestAs();
                        break;
                    case '5':
                        break;
                }
            }
        }
    }
}