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
    public class Test4 : BaseMessage
	{

		static Test4()
		{
			Geek.Server.SerializerOptions.Resolver = Geek.Server.Proto.SerializeResolver.Instance;
		}


		/*********************************************************/
		public Geek.Server.Proto.Test1 T1 {get;set;}
		public Geek.Server.Proto.Test2 T2 {get;set;}
		public Dictionary<Int32, String> Map {get;set;}
		/*********************************************************/


		public const int MsgID = SID;
		public override int Sid { get;} = 111104;
		public const int SID = 111104;
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
					T1 = ReadCustom<Geek.Server.Proto.Test1>(T1, _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{
					T2 = ReadCustom<Geek.Server.Proto.Test2>(T2, _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 2)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Map = new Dictionary<Int32, String>();
						int count2 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count2; ++i)
						{
							var key = XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = XBuffer.ReadString(_buffer_, ref _offset_);
							Map.Add(key, val);
						}
					}



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
			XBuffer.WriteByte(3, _buffer_, ref _offset_);
			
			//写入数据

			WriteCustom<Geek.Server.Proto.Test1>(T1, _buffer_, ref _offset_);
			

			WriteCustom<Geek.Server.Proto.Test2>(T2, _buffer_, ref _offset_);
			


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
				    XBuffer.WriteString(kv.Value, _buffer_, ref _offset_);
			    }
			}


			
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
					T1 = ReadCustom<Geek.Server.Proto.Test1>(T1, _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{
					T2 = ReadCustom<Geek.Server.Proto.Test2>(T2, _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 2)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Map = new Dictionary<Int32, String>();
						int count2 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count2; ++i)
						{
							var key = XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = XBuffer.ReadString(_buffer_, ref _offset_);
							Map.Add(key, val);
						}
					}



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
			XBuffer.WriteByte(3, _buffer_, ref _offset_);
			
			//写入数据

			WriteCustom<Geek.Server.Proto.Test1>(T1, _buffer_, ref _offset_);
			

			WriteCustom<Geek.Server.Proto.Test2>(T2, _buffer_, ref _offset_);
			


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
				    XBuffer.WriteString(kv.Value, _buffer_, ref _offset_);
			    }
			}


			
			return _offset_;
		}




		/*********************************************************/
		public override int GetSerializeLength()
		{
			int len = 0;

			len += XBuffer.IntSize; //UniId

			len += XBuffer.ByteSize; //_fieldNum_

			//T1
			len += GetCustomLength<Geek.Server.Proto.Test1>(T1);
			//T2
			len += GetCustomLength<Geek.Server.Proto.Test2>(T2);
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
			
					foreach (var keypair in Map)
					{
			
			
						len += XBuffer.GetStringSerializeLength(keypair.Value);
			
					}
				}
			}
			
			
			
			


			return len;
		}
	}
}
