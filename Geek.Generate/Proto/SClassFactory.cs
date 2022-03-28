//auto generated, do not modify it

using Geek.Server;

namespace Geek.Server.Proto
{
	public class SClassFactory
	{
		///<summary>通过msgId构造msg</summary>
		public static Serializable Create(int sid)
		{
			switch(sid)
			{
				case 111101: return new Geek.Server.Proto.Test1();
				case 111102: return new Geek.Server.Proto.Test2();
				case 111103: return new Geek.Server.Proto.Test3();
				case 111104: return new Geek.Server.Proto.Test4();
				case 111105: return new Geek.Server.Proto.ReqTest();
				default: return default;
			}
		}
		
		public static T Create<T>(int sid) where T : Serializable
		{
			return (T)Create(sid);
		}

	}
}