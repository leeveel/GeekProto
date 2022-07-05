using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;

namespace Geek.Server.Proto
{
	[IsState]
    [SClass(SID._111101, false)]
	public class Test1
	{
		[SProperty(0)]
		public long Id { get; set; }

		[SProperty(1)]
		public string S1 { get; set; }

		[SProperty(2)]
		public int I1 { get; set; }

		[SProperty(3)]
		public bool B1 { get; set; }

		[SProperty(4)]
		public float F1 { get; set; }

		[SProperty(5)]
		public short S2 { get; set; }

		[SProperty(6)]
		public double D1 { get; set; }

        [SProperty(7)]
        public byte[] B2 { get; set; }

        [SProperty(8, SO.Optional)]
		public string O1 { get; set; }
	}

    [SClass(SID._111102)]
    public class Test2 : Test1
    {
        [SProperty(0)]
        public long L1 { get; set; }

		[SProperty(1)]
		public List<string> L2 { get; set; }

		[SProperty(2)]
		public List<float> L3 { get; set; } 

		[SProperty(3)]
		public List<Test1> L4 { get; set; }

        [SProperty(4)]
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]//only for bosn test
		public Dictionary<long, string> M1 { get; set; }

        [SProperty(5)]
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)] //only for bosn test
		public Dictionary<int, Test1> M2 { get; set; }

        [SProperty(6, SO.Optional)]
        public long L5 { get; set; }
    }

	[SClass(SID.Enum)]
	public enum TestEnum
	{
		A = 100, 
		B, 
		C, 
		D, 
		E, 
		F, 
		G
	}


	[SClass(SID._111103)]
	public class Test3 : Serializable
	{
		[SProperty(0)]
		public string UserId { get; set; }
		[SProperty(1)]
		public string Platform { get; set; }
		[SProperty(2)]
		public List<Test1> List { get; set; }
		[SProperty(3)]
		public Dictionary<int, int> Map { get; set; }
		[SProperty(4)]
		public Dictionary<int, Test1> Map2 { get; set; }
		[SProperty(5)]
		public Dictionary<int, List<Test1>> Map3 { get; set; } 
		[SProperty(6)]
		public Dictionary<int, HashSet<Test1>> Map4 { get; set; }
		[SProperty(7)]
		public Dictionary<int, Dictionary<long, Test1>> Map5 { get; set; }
		[SProperty(8, SO.Optional)]
		public Test1 T1 { get; set; }
		[SProperty(9)]
		public Dictionary<string, string> Map6 { get; set; }
		//[SProperty(9)]
		//public TestEnum E1 { get; set; } = TestEnum.A;
		//[SProperty(10)]
		//public Dictionary<int, Dictionary<long, TestEnum>> Map6 { get; set; }

		//[SProperty(11)]
		//public Dictionary<int, TestEnum> Map7 { get; set; }
		//[SProperty(11)]
		//public Dictionary<string, TestEnum> Map8 { get; set; }
		[SProperty(10, SO.Optional)]
		public Test1 T2 { get; set; }
	}

	[SClass(SID._111104)]
	public class Test4 : Serializable
	{
		[SProperty(0, SO.Optional)]
		public Test1 T1 { get; set; }
		[SProperty(1, SO.Optional)]
		public Test2 T2 { get; set; }
		[SProperty(2)]
		public Dictionary<int, string> Map { get; set; }
	}

    [SClass(SID._111105, SO.Msg)]
    public class ReqTest : BaseMessage
    {
        [SProperty(0)]
        public string UserId { get; set; }
        public string Platform { get; set; }
    }

	[SClass(SID._9998)]
	public class My1
	{
		[SProperty(0)]
		public long Id { get; set; }

		[SProperty(1)]
		public string S1 { get; set; }

		[SProperty(2)]
		public int I1 { get; set; }

		[SProperty(3)]
		public bool B1 { get; set; }

		[SProperty(4)]
		public float F1 { get; set; }

		[SProperty(5)]
		public short S2 { get; set; }

		[SProperty(6)]
		public double D1 { get; set; }

		[SProperty(7)]
		public byte[] B2 { get; set; }

		[SProperty(8)]
		public string O1 { get; set; }
	}

	/// <summary>
	/// 请求登录
	/// </summary>
	//[IsState]
	[SClass(SID._9999)]
	public class ReqLogin
	{
		[SProperty(0)]
		public string UserName { get; set; }
		[SProperty(1)]
		public string Platform { get; set; }
		[SProperty(2)]
		public int SdkType { get; set; }
		[SProperty(3)]
		public string SdkToken { get; set; }
		[SProperty(4)]
		public string Device { get; set; }
		[SProperty(5)]
		public List<int> List { get; set; }
		[SProperty(6)]
		public Dictionary<int, int> Map1 { get; set; }
		//[SProperty(5)]
		//public Dictionary<int,string> Map { get; set; }
		//[SProperty(6)]
		//public Dictionary<int, List<int>> Map2 { get; set; }
		//[SProperty(7)]
		//public Dictionary<int, List<List<int>>> Map3 { get; set; }
		//[SProperty(8)]
		//public Dictionary<int, Test1> Map4 { get; set; }
		//[SProperty(9)]
		//public Test1 T1 { get; set; }
	}

	[SClass(SID._200001, false)]
	public class Test5
	{
		[SProperty(0)]
		public long Id { get; set; }

		[SProperty(1)]
		public string S1 { get; set; }

		[SProperty(2)]
		public int I1 { get; set; }

		[SProperty(3)]
		public TestEnum Enum1 { get; set; } = TestEnum.A;

		[SProperty(4)]
		public List<TestEnum> Enum2 { get; set; }

		[SProperty(5)]
		public Dictionary<TestEnum, int> Enum3 { get; set; }

		[SProperty(6)]
		public Dictionary<TestEnum, TestEnum> Enum4 { get; set; }

		[SProperty(7)]
		public Test1 Test1 { get; set; }
		[SProperty(8)]
		public Dictionary<TestEnum, string> Dic1 { get; set; }

		[SProperty(9)]
		public DateTime Time { get; set; }

		[SProperty(10)]
		public List<DateTime> Time1 { get; set; }

		[SProperty(11)]
		public Dictionary<DateTime, DateTime> Time2 { get; set; }
		[SProperty(12)]
		public List<List<DateTime>> Time3 { get; set; }
	}

}
