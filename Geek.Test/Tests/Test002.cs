using Geek.Server.Proto;
using MessagePack;

namespace Geek.Test
{
    public class Test002
    {

        [MessagePackObject]
        public class Test1MsgPack
        {
            [Key(0)]
            public long Id { get; set; }

            [Key(1)]
            public string S1 { get; set; }

            [Key(2)]
            public int I1 { get; set; }

            [Key(3)]
            public bool B1 { get; set; }

            [Key(4)]
            public float F1 { get; set; }

            [Key(5)]
            public short S2 { get; set; }

            [Key(6)]
            public double D1 { get; set; }

            [Key(7)]
            public byte[] B2 { get; set; }

            [Key(8)]
            public string O1 { get; set; }
        }

        /// <summary>
        /// 请求登录
        /// </summary>
        [MessagePackObject]
        public class ReqLoginMsgPack
        {
            [Key(0)]
            public string UserName { get; set; }
            [Key(1)]
            public string Platform { get; set; }
            [Key(2)]
            public int SdkType { get; set; }
            [Key(3)]
            public string SdkToken { get; set; }
            [Key(4)]
            public string Device { get; set; }
            [Key(5)]
            public List<int> List { get; set; }
            [Key(6)]
            public Dictionary<int, int> Map1 { get; set; }
            //[Key(5)]
            //public Dictionary<int, string> Map { get; set; }
            //[Key(6)]
            //public Dictionary<int, List<int>> Map2 { get; set; }
            //[Key(7)]
            //public Dictionary<int, List<List<int>>> Map3 { get; set; }
            //[Key(8)]
            //public Dictionary<int, Test1MsgPack> Map4 { get; set; }
            //[Key(9)]
            //public Test1MsgPack T1 { get; set; }
        }


        public void TestPerformance()
        {
            int count = 10000;
            long start = TimeUtils.CurrentTimeMillis();
            TestMsg(count, false);
            Console.WriteLine($"{TimeUtils.CurrentTimeMillis() - start}:ms");

            start = TimeUtils.CurrentTimeMillis();
            Test1(count, false);
            Console.WriteLine($"{TimeUtils.CurrentTimeMillis() - start}:ms");
        }

        public void Test1(int count, bool map)
        {
            ReqLogin t1 = new ReqLogin
            {
                //UserName = "leeveel",
                //SdkToken = "fdsafdafdsafdsafdsa",
                //Platform = "editor",
                SdkType = 213,
                //Device = "fafdsaf"
            };

            if (map)
            {
                //t1.Map = new Dictionary<int, string>();
                //for (int i = 0; i < 10; i++)
                //{
                //    t1.Map.Add(i, i.ToString());
                //}

                //t1.Map2 = new Dictionary<int, List<int>>();
                //var list = new List<int>();
                //for (int i = 0; i < 10; i++)
                //{
                //    list.Add(i);
                //}
                //for (int i = 0; i < 2; i++)
                //{
                //    t1.Map2.Add(i, list);
                //}

                //t1.Map3 = new Dictionary<int, List<List<int>>>();
                //var list1 = new List<List<int>>();
                //list1.Add(list);
                //t1.Map3.Add(1, list1);

                //var test1 = new Test1();
                //test1.S1 = "0-0--00-0--0-0-0-0-0-0-0-";

                //t1.T1 = test1;

                //t1.Map4 = new Dictionary<int, Test1>();
                //t1.Map4.Add(1, test1);
            }
            t1.List = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                t1.List.Add((int)i);
            }
            //t1.Map1 = new Dictionary<int, int>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    t1.Map1.Add(i, i);
            //}

            var b = new byte[t1.GetSerializeLength()];
            var span = new Span<byte>(b);
            for (int i = 0; i < count; i++)
            {
                t1.Serialize(span);
                ReqLogin t2 = new ReqLogin();
                t2.Deserialize(span);
            }
            Console.WriteLine("geekproto length:" + b.Length);
        }

        public void TestMsg(int count, bool map)
        {
            ReqLoginMsgPack t1 = new ReqLoginMsgPack
            {
                //UserName = "leeveel",
                //SdkToken = "fdsafdafdsafdsafdsa",
                //Platform = "editor",
                SdkType = 213,
                //Device = "fafdsaf"
            };

            if (map)
            {
                //t1.Map = new Dictionary<int, string>();
                //for (int i = 0; i < 10; i++)
                //{
                //    t1.Map.Add(i, i.ToString());
                //}

                //t1.Map2 = new Dictionary<int, List<int>>();
                //var list = new List<int>();
                //for (int i = 0; i < 10; i++)
                //{
                //    list.Add(i);
                //}
                //for (int i = 0; i < 2; i++)
                //{
                //    t1.Map2.Add(i, list);
                //}

                //t1.Map3 = new Dictionary<int, List<List<int>>>();
                //var list1 = new List<List<int>>();
                //list1.Add(list);
                //t1.Map3.Add(1, list1);

                //var test1 = new Test1MsgPack();
                //test1.S1 = "0-0--00-0--0-0-0-0-0-0-0-";

                //t1.T1 = test1;

                //t1.Map4 = new Dictionary<int, Test1MsgPack>();
                //t1.Map4.Add(1, test1);
            }

            t1.List = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                t1.List.Add(i);
            }
            //t1.Map1 = new Dictionary<int, int>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    t1.Map1.Add(i, i);
            //}
            MessagePackSerializer.DefaultOptions.WithCompression(MessagePackCompression.None);
            int len = 0;
            for (int i = 0; i < count; i++)
            {
                var bytes1 = MessagePackSerializer.Serialize(t1, MessagePackSerializer.DefaultOptions);
                if(i==0)
                    len = bytes1.Length;
                var m = MessagePackSerializer.Deserialize<ReqLoginMsgPack>(bytes1, MessagePackSerializer.DefaultOptions);
            }
            Console.WriteLine("msgpack length:" + len);
        }


        public void TestFor()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(i);
            }

            int count = 100000;
            long start = TimeUtils.CurrentTimeMillis();
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    var item  = list[j];
                }
            }
            Console.WriteLine($"{TimeUtils.CurrentTimeMillis() - start}:ms");

            start = TimeUtils.CurrentTimeMillis();
            for (int i = 0; i < count; i++)
            {
                foreach (var item in list)
                {
                    var item1 = item;
                }
            }
            Console.WriteLine($"{TimeUtils.CurrentTimeMillis() - start}:ms");
        }


        public enum MyEnum
        {
            A=100,
            B,
            C =200,
            D,
            E
        }


        public void GetEnumNames()
        {
            Type t = typeof(MyEnum);

            Console.WriteLine(typeof(float).IsPrimitive);

            var arr = t.GetEnumNames();

            var vals = t.GetEnumValues();
            foreach (int item in vals)
            {
                Console.WriteLine(item);
            }

            var v = Enum.GetValues(t);
            foreach (int item in v)
            {
                Console.WriteLine(item);
            }
        }

        public void TestEnum()
        {
            Test5 t5 = new Test5();
            t5.I1 = 111;
            t5.Id = 222;
            t5.S1 = "fffdsafas";
            t5.Enum1 = Server.Proto.TestEnum.G;

            t5.Enum2 = new List<TestEnum>();
            for (int i = 0; i < 10; i++)
            {
                t5.Enum2.Add(Server.Proto.TestEnum.E);
            }

            t5.Enum3 = new Dictionary<TestEnum, int>();
            t5.Enum3.Add(Server.Proto.TestEnum.A, 100);
            t5.Enum3.Add(Server.Proto.TestEnum.B, 100);
            t5.Enum3.Add(Server.Proto.TestEnum.C, 100);


            t5.Enum4 = new Dictionary<TestEnum, TestEnum>();
            t5.Enum4.Add(Server.Proto.TestEnum.A, Server.Proto.TestEnum.A);
            t5.Enum4.Add(Server.Proto.TestEnum.B, Server.Proto.TestEnum.A);
            t5.Enum4.Add(Server.Proto.TestEnum.C, Server.Proto.TestEnum.A);

            t5.Time = DateTime.Now;

            t5.Time1 = new List<DateTime>();
            for (int i = 0; i < 10; i++)
            {
                t5.Time1.Add(DateTime.Now);
            }

            t5.Time2 = new Dictionary<DateTime, DateTime>();
            for (int i = 0; i < 1; i++)
            {
                t5.Time2.Add(DateTime.Now, DateTime.Now);
            }
            t5.Time3 = new List<List<DateTime>>();
            t5.Time3.Add(t5.Time1);

            var b = new byte[t5.GetSerializeLength()];
            var span = new Span<byte>(b);
            for (int i = 0; i < 1; i++)
            {
                t5.Serialize(span);
                Test5 t2 = new Test5();
                t2.Deserialize(span);
            }

        }

    }
}
