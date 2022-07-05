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
    public class Test5 : Serializable
	{

		static Test5()
		{
			Geek.Server.SerializerOptions.Resolver = Geek.Server.Proto.SerializeResolver.Instance;
		}


		/*********************************************************/
		public long Id {get;set;}
		public string S1 {get;set;}
		public int I1 {get;set;}
		public Geek.Server.Proto.TestEnum Enum1 {get;set;}
		public List<TestEnum> Enum2 {get;set;}
		public Dictionary<TestEnum, Int32> Enum3 {get;set;}
		public Dictionary<TestEnum, TestEnum> Enum4 {get;set;}
		public Geek.Server.Proto.Test1 Test1 {get;set;}
		public Dictionary<TestEnum, String> Dic1 {get;set;}
		public System.DateTime Time {get;set;}
		public List<DateTime> Time1 {get;set;}
		public Dictionary<DateTime, DateTime> Time2 {get;set;}
		public List<List<DateTime>> Time3 {get;set;}
		/*********************************************************/


		public override int Sid { get;} = 200001;
		public const int SID = 200001;
		public const bool IsState = false;

		public override T Create<T>(int sid)
        {
            return Geek.Server.Proto.SClassFactory.Create<T>(sid);
        }

		///<summary>反序列化，读取数据</summary>
        public override int Read(Span<byte> _buffer_, int _offset_)
		{
			_offset_ = base.Read(_buffer_, _offset_);
			int _startOffset_ = _offset_;
			int _toReadLength_ = XBuffer.ReadInt(_buffer_, ref _offset_);
			
			//字段个数,最多支持255个
			var _fieldNum_ = XBuffer.ReadByte(_buffer_, ref _offset_);
			
			do {
				if(_fieldNum_ > 0)
				{
					Id = XBuffer.ReadLong(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{
					S1 = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 2)
				{
					I1 = XBuffer.ReadInt(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 3)
				{
					Enum1 = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 4)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						Enum2 = new List<TestEnum>();
						int count4 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count4; ++i)
						{
							Enum2.Add((Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_));
						}
					}



				}else break;
				if(_fieldNum_ > 5)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Enum3 = new Dictionary<TestEnum, Int32>();
						int count5 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count5; ++i)
						{
							var key = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = XBuffer.ReadInt(_buffer_, ref _offset_);
							Enum3.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 6)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Enum4 = new Dictionary<TestEnum, TestEnum>();
						int count6 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count6; ++i)
						{
							var key = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);
							Enum4.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 7)
				{
					Test1 = ReadCustom<Geek.Server.Proto.Test1>(Test1, _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 8)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Dic1 = new Dictionary<TestEnum, String>();
						int count8 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count8; ++i)
						{
							var key = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = XBuffer.ReadString(_buffer_, ref _offset_);
							Dic1.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 9)
				{
					Time = XBuffer.ReadDateTime(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 10)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						Time1 = new List<DateTime>();
						int count10 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count10; ++i)
						{
							Time1.Add(XBuffer.ReadDateTime(_buffer_, ref _offset_));
						}
					}



				}else break;
				if(_fieldNum_ > 11)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Time2 = new Dictionary<DateTime, DateTime>();
						int count11 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count11; ++i)
						{
							var key = XBuffer.ReadDateTime(_buffer_, ref _offset_);
							var val = XBuffer.ReadDateTime(_buffer_, ref _offset_);
							Time2.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 12)
				{

					Time3 = ReadCollection<List<List<DateTime>>>(_buffer_, ref _offset_);


				}else break;
			}while(false);
			
			//剔除多余数据
			if(_offset_ < _toReadLength_ - _startOffset_)
				_offset_ += _toReadLength_ - _startOffset_;
			return _offset_;
		}

		
		///<summary>序列化，写入数据</summary>
        public override int Write(Span<byte> _buffer_, int _offset_)
        {	
			_offset_ = base.Write(_buffer_, _offset_);
			//先写入当前对象长度占位符
			int _startOffset_ = _offset_;
			XBuffer.WriteInt(0, _buffer_, ref _offset_);
			
			//写入字段数量,最多支持255个
			XBuffer.WriteByte(13, _buffer_, ref _offset_);
			
			//写入数据

			XBuffer.WriteLong(Id, _buffer_, ref _offset_);


			XBuffer.WriteString(S1, _buffer_, ref _offset_);


			XBuffer.WriteInt(I1, _buffer_, ref _offset_);


			XBuffer.WriteInt((int)Enum1, _buffer_, ref _offset_);



			if(Enum2 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Enum2.Count, _buffer_, ref _offset_);
			    foreach (var item in Enum2)
			    {
				    XBuffer.WriteInt((int)item, _buffer_, ref _offset_);
			    }
			}
			




			if(Enum3 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Enum3.Count, _buffer_, ref _offset_);
			    foreach (var kv in Enum3)
			    {
				    XBuffer.WriteInt((int)kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteInt(kv.Value, _buffer_, ref _offset_);
			    }
			}




			if(Enum4 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Enum4.Count, _buffer_, ref _offset_);
			    foreach (var kv in Enum4)
			    {
				    XBuffer.WriteInt((int)kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteInt((int)kv.Value, _buffer_, ref _offset_);
			    }
			}



			WriteCustom<Geek.Server.Proto.Test1>(Test1, _buffer_, ref _offset_);
			


			if(Dic1 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Dic1.Count, _buffer_, ref _offset_);
			    foreach (var kv in Dic1)
			    {
				    XBuffer.WriteInt((int)kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteString(kv.Value, _buffer_, ref _offset_);
			    }
			}



			XBuffer.WriteDateTime(Time, _buffer_, ref _offset_);



			if(Time1 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Time1.Count, _buffer_, ref _offset_);
			    foreach (var item in Time1)
			    {
				    XBuffer.WriteDateTime(item, _buffer_, ref _offset_);
			    }
			}
			




			if(Time2 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Time2.Count, _buffer_, ref _offset_);
			    foreach (var kv in Time2)
			    {
				    XBuffer.WriteDateTime(kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteDateTime(kv.Value, _buffer_, ref _offset_);
			    }
			}




			WriteCollection<List<List<DateTime>>>(Time3, _buffer_, ref _offset_);

			
			//覆盖当前对象长度
			XBuffer.WriteInt(_offset_ - _startOffset_, _buffer_, ref _startOffset_);
			return _offset_;
		}

		/**********************Byte[]*******************************/

		///<summary>反序列化，读取数据</summary>
        public override int Read(byte[] _buffer_, int _offset_)
		{
			_offset_ = base.Read(_buffer_, _offset_);
			int _startOffset_ = _offset_;
			int _toReadLength_ = XBuffer.ReadInt(_buffer_, ref _offset_);
			
			//字段个数,最多支持255个
			var _fieldNum_ = XBuffer.ReadByte(_buffer_, ref _offset_);
			
			do {
				if(_fieldNum_ > 0)
				{
					Id = XBuffer.ReadLong(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{
					S1 = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 2)
				{
					I1 = XBuffer.ReadInt(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 3)
				{
					Enum1 = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 4)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						Enum2 = new List<TestEnum>();
						int count4 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count4; ++i)
						{
							Enum2.Add((Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_));
						}
					}



				}else break;
				if(_fieldNum_ > 5)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Enum3 = new Dictionary<TestEnum, Int32>();
						int count5 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count5; ++i)
						{
							var key = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = XBuffer.ReadInt(_buffer_, ref _offset_);
							Enum3.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 6)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Enum4 = new Dictionary<TestEnum, TestEnum>();
						int count6 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count6; ++i)
						{
							var key = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);
							Enum4.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 7)
				{
					Test1 = ReadCustom<Geek.Server.Proto.Test1>(Test1, _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 8)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Dic1 = new Dictionary<TestEnum, String>();
						int count8 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count8; ++i)
						{
							var key = (Geek.Server.Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = XBuffer.ReadString(_buffer_, ref _offset_);
							Dic1.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 9)
				{
					Time = XBuffer.ReadDateTime(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 10)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						Time1 = new List<DateTime>();
						int count10 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count10; ++i)
						{
							Time1.Add(XBuffer.ReadDateTime(_buffer_, ref _offset_));
						}
					}



				}else break;
				if(_fieldNum_ > 11)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Time2 = new Dictionary<DateTime, DateTime>();
						int count11 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count11; ++i)
						{
							var key = XBuffer.ReadDateTime(_buffer_, ref _offset_);
							var val = XBuffer.ReadDateTime(_buffer_, ref _offset_);
							Time2.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 12)
				{

					Time3 = ReadCollection<List<List<DateTime>>>(_buffer_, ref _offset_);


				}else break;
			}while(false);
			
			//剔除多余数据
			if(_offset_ < _toReadLength_ - _startOffset_)
				_offset_ += _toReadLength_ - _startOffset_;
			return _offset_;
		}

		///<summary>序列化，写入数据</summary>
        public override int Write(byte[]  _buffer_, int _offset_)
        {	
			_offset_ = base.Write(_buffer_, _offset_);
			//先写入当前对象长度占位符
			int _startOffset_ = _offset_;
			XBuffer.WriteInt(0, _buffer_, ref _offset_);
			
			//写入字段数量,最多支持255个
			XBuffer.WriteByte(13, _buffer_, ref _offset_);
			
			//写入数据

			XBuffer.WriteLong(Id, _buffer_, ref _offset_);


			XBuffer.WriteString(S1, _buffer_, ref _offset_);


			XBuffer.WriteInt(I1, _buffer_, ref _offset_);


			XBuffer.WriteInt((int)Enum1, _buffer_, ref _offset_);



			if(Enum2 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Enum2.Count, _buffer_, ref _offset_);
			    foreach (var item in Enum2)
			    {
				    XBuffer.WriteInt((int)item, _buffer_, ref _offset_);
			    }
			}
			




			if(Enum3 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Enum3.Count, _buffer_, ref _offset_);
			    foreach (var kv in Enum3)
			    {
				    XBuffer.WriteInt((int)kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteInt(kv.Value, _buffer_, ref _offset_);
			    }
			}




			if(Enum4 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Enum4.Count, _buffer_, ref _offset_);
			    foreach (var kv in Enum4)
			    {
				    XBuffer.WriteInt((int)kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteInt((int)kv.Value, _buffer_, ref _offset_);
			    }
			}



			WriteCustom<Geek.Server.Proto.Test1>(Test1, _buffer_, ref _offset_);
			


			if(Dic1 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Dic1.Count, _buffer_, ref _offset_);
			    foreach (var kv in Dic1)
			    {
				    XBuffer.WriteInt((int)kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteString(kv.Value, _buffer_, ref _offset_);
			    }
			}



			XBuffer.WriteDateTime(Time, _buffer_, ref _offset_);



			if(Time1 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Time1.Count, _buffer_, ref _offset_);
			    foreach (var item in Time1)
			    {
				    XBuffer.WriteDateTime(item, _buffer_, ref _offset_);
			    }
			}
			




			if(Time2 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Time2.Count, _buffer_, ref _offset_);
			    foreach (var kv in Time2)
			    {
				    XBuffer.WriteDateTime(kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteDateTime(kv.Value, _buffer_, ref _offset_);
			    }
			}




			WriteCollection<List<List<DateTime>>>(Time3, _buffer_, ref _offset_);

			
			//覆盖当前对象长度
			XBuffer.WriteInt(_offset_ - _startOffset_, _buffer_, ref _startOffset_);
			return _offset_;
		}




		/*********************************************************/
		public override int GetSerializeLength()
		{
			int len = 0;

			len += XBuffer.IntSize; //true length

			len += XBuffer.ByteSize; //_fieldNum_

			//Id
			len += XBuffer.LongSize;
			//S1
			len += XBuffer.GetStringSerializeLength(S1);
			//I1
			len += XBuffer.IntSize;
			//Enum1
			len += XBuffer.IntSize;
			//Enum2
			if (Enum2 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Enum2.Count > 0)
				{
					len += Enum2.Count * XBuffer.IntSize;
				}
			}

			//Enum3
			
			if (Enum3 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Enum3.Count > 0)
				{
					len += Enum3.Count * XBuffer.IntSize;
					len += Enum3.Count * XBuffer.IntSize;
			
				}
			}
			
			
			
			

			//Enum4
			
			if (Enum4 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Enum4.Count > 0)
				{
					len += Enum4.Count * XBuffer.IntSize;
					len += Enum4.Count * XBuffer.IntSize;
			
				}
			}
			
			
			
			

			//Test1
			len += GetCustomLength<Geek.Server.Proto.Test1>(Test1);
			//Dic1
			
			if (Dic1 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Dic1.Count > 0)
				{
					len += Dic1.Count * XBuffer.IntSize;
			
					foreach (var keypair in Dic1)
					{
			
			
						len += XBuffer.GetStringSerializeLength(keypair.Value);
			
					}
				}
			}
			
			
			
			

			//Time
			len += XBuffer.LongSize;
			//Time1
			if (Time1 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Time1.Count > 0)
				{
					len += Time1.Count * XBuffer.LongSize;
				}
			}

			//Time2
			
			if (Time2 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Time2.Count > 0)
				{
					len += Time2.Count * XBuffer.LongSize;
					len += Time2.Count * XBuffer.LongSize;
			
				}
			}
			
			
			
			

			//Time3
			len += GetCollectionLength(Time3);

			return len;
		}
	}
}
