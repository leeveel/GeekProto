using Proto;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Tool.Test
{
    public class Test001
    {

        public Test001()
        {
            BsonSerializer.RegisterSerializer(new EnumSerializer<TestEnum>(BsonType.String));
        }

        public void Test()
        {
            List<Test1> list = new List<Test1>();
            Test1 t1 = new Test1();
            t1.B2 = new byte[4];
            t1.B2[0] = 0;
            t1.B2[1] = 1;
            t1.B2[2] = 2;
            t1.B2[3] = 3;
            t1.B1 = true;
            t1.D1 = 2.9;
            t1.F1 = 1.0f;
            t1.I1 = 1;
            t1.S1 = "hello";
            list.Add(t1);

            Test2 t2 = new Test2();
            t2.B2 = new byte[4];
            t2.B2[0] = 5;
            t2.B2[1] = 6;
            t2.B2[2] = 7;
            t2.B2[3] = 8;
            t2.B1 = true;
            t2.D1 = 3.6;
            t2.F1 = 1.3f;
            t2.I1 = 1;
            t2.S1 = "hahhahah";
            t2.L1 = 987;

            list.Add(t2);

            Dictionary<long, Test1> dic = new Dictionary<long, Test1>();
            dic.Add(500, t1);

            Test3 t3 = new Test3();
            t3.Map3.Add(100, list);
            t3.Map5.Add(600, dic);
            t3.List.Add(t1);
            t3.List.Add(t2);

            var arr = t3.Serialize();
            Test3 t33 = new Test3();
            t33.Deserialize(arr);
            Console.WriteLine(t33.Map3.ContainsKey(100));
            Console.WriteLine(t33.Map5.ContainsKey(600));

            foreach (var item in t3.List)
            {
                Console.WriteLine(item.S1);
                if (item is Test2 test2)
                    Console.WriteLine(test2.L1);
            }
        }


        public void TestPerformance()
        {
            //List<Test1> list = new List<Test1>();
            //Test1 t1 = new Test1();
            //t1.B2 = new byte[4];
            //t1.B2[0] = 0;
            //t1.B2[1] = 1;
            //t1.B2[2] = 2;
            //t1.B2[3] = 3;
            //t1.B1 = true;
            //t1.D1 = 2.9;
            //t1.F1 = 1.0f;
            //t1.I1 = 1;
            //t1.S1 = "hello";
            //list.Add(t1);

            //Test2 t2 = new Test2();
            //t2.B2 = new byte[4];
            //t2.B2[0] = 5;
            //t2.B2[1] = 6;
            //t2.B2[2] = 7;
            //t2.B2[3] = 8;
            //t2.B1 = true;
            //t2.D1 = 3.6;
            //t2.F1 = 1.3f;
            //t2.I1 = 1;
            //t2.S1 = "hahhahah";

            //t2.F2 = 3.14f;
            //list.Add(t2);

            //Dictionary<long, Test1> dic = new Dictionary<long, Test1>();
            //dic.Add(500, t1);

            //Test3 t3 = new Test3();
            //t3.Map3.Add(100, list);
            //t3.Map5.Add(600, dic);
            //t3.List.Add(t1);
            //t3.List.Add(t2);

            //for (int i = 0; i < 1000; i++)
            //{
            //    t3.Map.Add(i, i);
            //}


            //int count = 1;

            //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            //watch.Start();
            //for (int i = 0; i < count; i++)
            //{
            //    var arr = t3.Serialize();
            //    if (i == 0)
            //        Console.WriteLine(arr.Length);
            //    Test3 t33 = new Test3();
            //    t33.Deserialize(arr);
            //}
            //watch.Stop();
            //Console.WriteLine($"GeekProto：{watch.Elapsed.TotalMilliseconds}(毫秒)");


            //watch.Reset();
            //watch.Start();
            //for (int i = 0; i < count; i++)
            //{
            //    var jsonStr = JsonConvert.SerializeObject(t3);
            //    JsonConvert.DeserializeObject<Test3>(jsonStr);
            //    if (i == 0)
            //        Console.WriteLine(System.Text.Encoding.UTF8.GetBytes(jsonStr).Length);
            //}
            //watch.Stop();
            //Console.WriteLine($"NewtonSoft：{watch.Elapsed.TotalMilliseconds}(毫秒)");



            //watch.Reset();
            //watch.Start();
            //for (int i = 0; i < count; i++)
            //{
            //    var bson = t3.ToBson();
            //    if (i == 0)
            //        Console.WriteLine(bson.Length);
            //    BsonSerializer.Deserialize<Test3>(bson);
            //}
            //watch.Stop();
            //System.Diagnostics.Debug.WriteLine($"Bson：{watch.Elapsed.TotalMilliseconds}(毫秒)");


        }


        public void TestPerformance1()
        {
            Test1 t1 = new Test1();
            t1.B1 = true;
            t1.S1 = "hello";
            t1.I1 = 100;

            Test2 t2 = new Test2();
            t2.L1 = 1000;

            Test4 t4 = new Test4();
            t4.T1 = t1;
            t4.T2 = t2;
            for (int i = 0; i < 10000; i++)
            {
                t4.Map.Add(i, i.ToString());
            }

            int count = 1000;
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                var arr = t4.Serialize();
                Test4 t44 = new Test4();
                t44.Deserialize(arr);
                if (i == 0)
                {
                    Console.WriteLine(arr.Length);
                    //Console.WriteLine(t44.T1.S1);
                    //foreach (var item in t44.Map)
                    //{
                    //    Console.WriteLine(item.Value);
                    //}
                }
            }
            watch.Stop();
            Console.WriteLine($"GeekProto：{watch.Elapsed.TotalMilliseconds}(毫秒)");


            watch.Reset();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                var jsonStr = JsonConvert.SerializeObject(t4);
                var t44 = JsonConvert.DeserializeObject<Test4>(jsonStr);
                if (i == 0)
                {
                    Console.WriteLine(System.Text.Encoding.UTF8.GetBytes(jsonStr).Length);
                    //foreach (var item in t44.Map)
                    //{
                    //    Console.WriteLine(item.Value);
                    //}
                }
                    
            }
            watch.Stop();
            Console.WriteLine($"NewtonSoft：{watch.Elapsed.TotalMilliseconds}(毫秒)");



            watch.Reset();
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                var bson = t4.ToBson();
                var t44 = BsonSerializer.Deserialize<Test4>(bson);
                if (i == 0)
                {
                    Console.WriteLine(bson.Length);
                    //foreach (var item in t44.Map)
                    //{
                    //    Console.WriteLine(item.Value);
                    //}
                }
            }
            watch.Stop();
            Console.WriteLine($"Bson：{watch.Elapsed.TotalMilliseconds}(毫秒)");


        }

    }
}
