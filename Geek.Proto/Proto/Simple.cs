using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;
using Geek.Server;

namespace Proto
{
    [SClass(SID._111101)]
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

        [SProperty(7, SO.Optional)]
        public Test1 T1 { get; set; }
    }

    public enum TestEnum
	{
		A, B, C
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
		//[SProperty(9)]
		//public TestEnum E1 { get; set; } = TestEnum.A;
		//[SProperty(10)]
		//public Dictionary<int, Dictionary<long, TestEnum>> Map6 { get; set; }

		//[SProperty(11)]
		//public Dictionary<int, TestEnum> Map7 { get; set; }
		//[SProperty(11)]
		//public Dictionary<string, TestEnum> Map8 { get; set; }
		[SProperty(12, SO.Optional)]
		public Test1 T2 { get; set; }
	}

	[SClass(SID._111104)]
	public class Test4 : Serializable
	{
		[SProperty(0, SO.Optional)]
		public Test1 T1 { get; set; }
		[SProperty(1, SO.Optional)]
		public Test2 T2 { get; set; }
		[SProperty(3)]
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]  //only for bosn test
		public Dictionary<int, string> Map { get; set; }
	}

    [SClass(SID._111105, SO.Msg)]
    public class ReqTest : BaseMessage
    {
        [SProperty(0)]
        public string UserId { get; set; }
        public string Platform { get; set; }
    }



}
