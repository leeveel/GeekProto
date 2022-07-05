//auto generated, do not modify it
//限制：命名不能以下划线结尾(可能冲突)
//限制：map的key只支持基础类型和string；
//兼容限制：字段只能添加，添加后不能删除，添加字段只能添加到最后,添加消息类型只能添加到最后
//兼容限制：不能修改字段类型（如从bool改为long）
//兼容限制：消息类型(含msdId)不能作为其他消息的成员类型


using System;
using System.Text;
using Geek.Server;
using System.Collections.Generic;

///<summary></summary>
namespace Geek.Server.Proto
{
    public class Test3 : BaseMessage
	{

		static Test3()
		{
			Geek.Server.SerializerOptions.Resolver = Geek.Server.Proto.SerializeResolver.Instance;
		}


		/*********************************************************/
		public string UserId {get;set;}
		public string Platform {get;set;}
		public List<Geek.Server.Proto.Test1> List {get;set;}
		public Dictionary<Int32, Int32> Map {get;set;}
		public Dictionary<Int32, Geek.Server.Proto.Test1> Map2 {get;set;}
		public Dictionary<Int32, List<Geek.Server.Proto.Test1>> Map3 {get;set;}
		public Dictionary<Int32, HashSet<Geek.Server.Proto.Test1>> Map4 {get;set;}
		public Dictionary<Int32, Dictionary<Int64, Geek.Server.Proto.Test1>> Map5 {get;set;}
		public Geek.Server.Proto.Test1 T1 {get;set;}
		public Dictionary<String, String> Map6 {get;set;}
		public Geek.Server.Proto.Test1 T2 {get;set;}
		/*********************************************************/


		public const int MsgID = SID;
		public override int Sid { get;} = 111103;
		public const int SID = 111103;
		public const bool IsState = false;

		public override T Create<T>(int sid)
        {
            return Geek.Server.Proto.SClassFactory.Create<T>(sid);
        }

		///<summary>反序列化，读取数据</summary>
        public override int Read(Span<byte> _buffer_, int _offset_)
		{
			UniId = XBuffer.ReadInt(_buffer_, ref _offset_);
			_offset_ = base.Read(_buffer_, _offset_);
			
			//字段个数,最多支持255个
			var _fieldNum_ = XBuffer.ReadByte(_buffer_, ref _offset_);
			
			do {
				if(_fieldNum_ > 0)
				{
					UserId = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{
					Platform = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 2)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						List = new List<Geek.Server.Proto.Test1>();
						int count2 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count2; ++i)
						{
							var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
							if (sid <= 0)
							{
								List.Add(default);
								continue;
							}
							var val = Create<Geek.Server.Proto.Test1>(sid);
							_offset_ = val.Read(_buffer_, _offset_);
							List.Add(val);
						}
					}



				}else break;
				if(_fieldNum_ > 3)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Map = new Dictionary<Int32, Int32>();
						int count3 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count3; ++i)
						{
							var key = XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = XBuffer.ReadInt(_buffer_, ref _offset_);
							Map.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 4)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Map2 = new Dictionary<Int32, Geek.Server.Proto.Test1>();
						int count4 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count4; ++i)
						{
							var key = XBuffer.ReadInt(_buffer_, ref _offset_);
							var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
							if (sid <= 0)
							{
								Map2[key] = default;
								continue;
							}
							var val = Create<Geek.Server.Proto.Test1>(sid);
							_offset_ = val.Read(_buffer_, _offset_);
							Map2.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 5)
				{

					Map3 = ReadCollection<Dictionary<Int32, List<Geek.Server.Proto.Test1>>>(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 6)
				{

					Map4 = ReadCollection<Dictionary<Int32, HashSet<Geek.Server.Proto.Test1>>>(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 7)
				{

					Map5 = ReadCollection<Dictionary<Int32, Dictionary<Int64, Geek.Server.Proto.Test1>>>(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 8)
				{
					T1 = ReadCustom<Geek.Server.Proto.Test1>(T1, _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 9)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Map6 = new Dictionary<String, String>();
						int count9 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count9; ++i)
						{
							var key = XBuffer.ReadString(_buffer_, ref _offset_);
							var val = XBuffer.ReadString(_buffer_, ref _offset_);
							Map6.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 10)
				{
					T2 = ReadCustom<Geek.Server.Proto.Test1>(T2, _buffer_, ref _offset_);


				}else break;
			}while(false);
			
			return _offset_;
		}

		
		///<summary>序列化，写入数据</summary>
        public override int Write(Span<byte> _buffer_, int _offset_)
        {	
			XBuffer.WriteInt(UniId, _buffer_, ref _offset_);
			_offset_ = base.Write(_buffer_, _offset_);
			
			//写入字段数量,最多支持255个
			XBuffer.WriteByte(11, _buffer_, ref _offset_);
			
			//写入数据

			XBuffer.WriteString(UserId, _buffer_, ref _offset_);


			XBuffer.WriteString(Platform, _buffer_, ref _offset_);



			if(List == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(List.Count, _buffer_, ref _offset_);
			    for (int i=0; i<List.Count; i++)
			    {
			        if (List[i] == null)
			        {
			            Geek.Server.SerializeLogger.LogError($"{this.GetType().FullName}.List has null idx == : {i}");
			            XBuffer.WriteInt(0, _buffer_, ref _offset_);
			        }
			        else
			        {
			            XBuffer.WriteInt(List[i].Sid, _buffer_, ref _offset_);
			            _offset_ = List[i].Write(_buffer_, _offset_);
			        }
			    }
			}
			




			if(Map == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Map.Count, _buffer_, ref _offset_);
			    foreach (var kv in Map)
			    {
				    XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteInt(kv.Value, _buffer_, ref _offset_);
			    }
			}




			if(Map2 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Map2.Count, _buffer_, ref _offset_);
			    foreach (var kv in Map2)
			    {
				    XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);
			        if (kv.Value == null)
			        {
			            Geek.Server.SerializeLogger.LogError($"{this.GetType().FullName}.Map2 has null item: {kv.Key}");
			            XBuffer.WriteInt(0, _buffer_, ref _offset_);
			        }
			        else
			        {
			            XBuffer.WriteInt(kv.Value.Sid, _buffer_, ref _offset_);
			            _offset_ = kv.Value.Write(_buffer_, _offset_);
			        }
			    }
			}




			WriteCollection<Dictionary<Int32, List<Geek.Server.Proto.Test1>>>(Map3, _buffer_, ref _offset_);



			WriteCollection<Dictionary<Int32, HashSet<Geek.Server.Proto.Test1>>>(Map4, _buffer_, ref _offset_);



			WriteCollection<Dictionary<Int32, Dictionary<Int64, Geek.Server.Proto.Test1>>>(Map5, _buffer_, ref _offset_);


			WriteCustom<Geek.Server.Proto.Test1>(T1, _buffer_, ref _offset_);
			


			if(Map6 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Map6.Count, _buffer_, ref _offset_);
			    foreach (var kv in Map6)
			    {
				    XBuffer.WriteString(kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteString(kv.Value, _buffer_, ref _offset_);
			    }
			}



			WriteCustom<Geek.Server.Proto.Test1>(T2, _buffer_, ref _offset_);
			
			
			return _offset_;
		}

		/**********************Byte[]*******************************/

		///<summary>反序列化，读取数据</summary>
        public override int Read(byte[] _buffer_, int _offset_)
		{
			UniId = XBuffer.ReadInt(_buffer_, ref _offset_);
			_offset_ = base.Read(_buffer_, _offset_);
			
			//字段个数,最多支持255个
			var _fieldNum_ = XBuffer.ReadByte(_buffer_, ref _offset_);
			
			do {
				if(_fieldNum_ > 0)
				{
					UserId = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{
					Platform = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 2)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						List = new List<Geek.Server.Proto.Test1>();
						int count2 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count2; ++i)
						{
							var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
							if (sid <= 0)
							{
								List.Add(default);
								continue;
							}
							var val = Create<Geek.Server.Proto.Test1>(sid);
							_offset_ = val.Read(_buffer_, _offset_);
							List.Add(val);
						}
					}



				}else break;
				if(_fieldNum_ > 3)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Map = new Dictionary<Int32, Int32>();
						int count3 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count3; ++i)
						{
							var key = XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = XBuffer.ReadInt(_buffer_, ref _offset_);
							Map.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 4)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Map2 = new Dictionary<Int32, Geek.Server.Proto.Test1>();
						int count4 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count4; ++i)
						{
							var key = XBuffer.ReadInt(_buffer_, ref _offset_);
							var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
							if (sid <= 0)
							{
								Map2[key] = default;
								continue;
							}
							var val = Create<Geek.Server.Proto.Test1>(sid);
							_offset_ = val.Read(_buffer_, _offset_);
							Map2.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 5)
				{

					Map3 = ReadCollection<Dictionary<Int32, List<Geek.Server.Proto.Test1>>>(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 6)
				{

					Map4 = ReadCollection<Dictionary<Int32, HashSet<Geek.Server.Proto.Test1>>>(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 7)
				{

					Map5 = ReadCollection<Dictionary<Int32, Dictionary<Int64, Geek.Server.Proto.Test1>>>(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 8)
				{
					T1 = ReadCustom<Geek.Server.Proto.Test1>(T1, _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 9)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Map6 = new Dictionary<String, String>();
						int count9 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count9; ++i)
						{
							var key = XBuffer.ReadString(_buffer_, ref _offset_);
							var val = XBuffer.ReadString(_buffer_, ref _offset_);
							Map6.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 10)
				{
					T2 = ReadCustom<Geek.Server.Proto.Test1>(T2, _buffer_, ref _offset_);


				}else break;
			}while(false);
			
			return _offset_;
		}

		///<summary>序列化，写入数据</summary>
        public override int Write(byte[]  _buffer_, int _offset_)
        {	
			XBuffer.WriteInt(UniId, _buffer_, ref _offset_);
			_offset_ = base.Write(_buffer_, _offset_);
			
			//写入字段数量,最多支持255个
			XBuffer.WriteByte(11, _buffer_, ref _offset_);
			
			//写入数据

			XBuffer.WriteString(UserId, _buffer_, ref _offset_);


			XBuffer.WriteString(Platform, _buffer_, ref _offset_);



			if(List == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(List.Count, _buffer_, ref _offset_);
			    for (int i=0; i<List.Count; i++)
			    {
			        if (List[i] == null)
			        {
			            Geek.Server.SerializeLogger.LogError($"{this.GetType().FullName}.List has null idx == : {i}");
			            XBuffer.WriteInt(0, _buffer_, ref _offset_);
			        }
			        else
			        {
			            XBuffer.WriteInt(List[i].Sid, _buffer_, ref _offset_);
			            _offset_ = List[i].Write(_buffer_, _offset_);
			        }
			    }
			}
			




			if(Map == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Map.Count, _buffer_, ref _offset_);
			    foreach (var kv in Map)
			    {
				    XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteInt(kv.Value, _buffer_, ref _offset_);
			    }
			}




			if(Map2 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Map2.Count, _buffer_, ref _offset_);
			    foreach (var kv in Map2)
			    {
				    XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);
			        if (kv.Value == null)
			        {
			            Geek.Server.SerializeLogger.LogError($"{this.GetType().FullName}.Map2 has null item: {kv.Key}");
			            XBuffer.WriteInt(0, _buffer_, ref _offset_);
			        }
			        else
			        {
			            XBuffer.WriteInt(kv.Value.Sid, _buffer_, ref _offset_);
			            _offset_ = kv.Value.Write(_buffer_, _offset_);
			        }
			    }
			}




			WriteCollection<Dictionary<Int32, List<Geek.Server.Proto.Test1>>>(Map3, _buffer_, ref _offset_);



			WriteCollection<Dictionary<Int32, HashSet<Geek.Server.Proto.Test1>>>(Map4, _buffer_, ref _offset_);



			WriteCollection<Dictionary<Int32, Dictionary<Int64, Geek.Server.Proto.Test1>>>(Map5, _buffer_, ref _offset_);


			WriteCustom<Geek.Server.Proto.Test1>(T1, _buffer_, ref _offset_);
			


			if(Map6 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Map6.Count, _buffer_, ref _offset_);
			    foreach (var kv in Map6)
			    {
				    XBuffer.WriteString(kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteString(kv.Value, _buffer_, ref _offset_);
			    }
			}



			WriteCustom<Geek.Server.Proto.Test1>(T2, _buffer_, ref _offset_);
			
			
			return _offset_;
		}




		/*********************************************************/
		public override int GetSerializeLength()
		{
			int len = 0;

			len += XBuffer.IntSize; //UniId

			len += XBuffer.ByteSize; //_fieldNum_

			//UserId
			len += XBuffer.GetStringSerializeLength(UserId);
			//Platform
			len += XBuffer.GetStringSerializeLength(Platform);
			//List
			if (List == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(List.Count > 0)
				{
					for (int i = 0; i < List.Count; i++)
					{
						len += GetCustomLength<Geek.Server.Proto.Test1>(List[i]);
					}
				}
			}

			//Map
			
			if (Map == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Map.Count > 0)
				{
					len += Map.Count * XBuffer.IntSize;
					len += Map.Count * XBuffer.IntSize;
			
				}
			}
			
			
			
			

			//Map2
			
			if (Map2 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Map2.Count > 0)
				{
					len += Map2.Count * XBuffer.IntSize;
			
					foreach (var keypair in Map2)
					{
			
			
						len += GetCustomLength<Geek.Server.Proto.Test1>(keypair.Value);
			
					}
				}
			}
			
			
			
			

			//Map3
			len += GetCollectionLength(Map3);
			//Map4
			len += GetCollectionLength(Map4);
			//Map5
			len += GetCollectionLength(Map5);
			//T1
			len += GetCustomLength<Geek.Server.Proto.Test1>(T1);
			//Map6
			
			if (Map6 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Map6.Count > 0)
				{
			
					foreach (var keypair in Map6)
					{
						len += XBuffer.GetStringSerializeLength(keypair.Key);
			
			
						len += XBuffer.GetStringSerializeLength(keypair.Value);
			
					}
				}
			}
			
			
			
			

			//T2
			len += GetCustomLength<Geek.Server.Proto.Test1>(T2);

			return len;
		}
	}
}
