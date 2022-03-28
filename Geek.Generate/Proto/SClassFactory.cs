//auto generated, do not modify it

using Geek.Server;
namespace Proto
{
	public class SClassFactory
	{
		///<summary>通过msgId构造msg</summary>
		public static Serializable Create(int sid)
		{
			switch(sid)
			{
				case 111101: return new Proto.Test1();
				case 111102: return new Proto.Test2();
				case 111103: return new Proto.Test3();
				case 111104: return new Proto.Test4();
				case 111105: return new Proto.ReqTest();
				default: return default;
			}
		}
		
		public static T Create<T>(int sid) where T : Serializable
		{
			return (T)Create(sid);
		}

	}
}