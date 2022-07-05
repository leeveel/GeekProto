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
    public class My1 : BaseMessage
	{

		static My1()
		{
			Geek.Server.SerializerOptions.Resolver = Geek.Server.Proto.SerializeResolver.Instance;
		}


		/*********************************************************/
		public long Id {get;set;}
		public string S1 {get;set;}
		public int I1 {get;set;}
		public bool B1 {get;set;}
		public float F1 {get;set;}
		public short S2 {get;set;}
		public double D1 {get;set;}
		public byte[] B2 {get;set;}
		public string O1 {get;set;}
		/*********************************************************/


		public const int MsgID = SID;
		public override int Sid { get;} = 9998;
		public const int SID = 9998;
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
					B1 = XBuffer.ReadBool(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 4)
				{
					F1 = XBuffer.ReadFloat(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 5)
				{
					S2 = XBuffer.ReadShort(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 6)
				{
					D1 = XBuffer.ReadDouble(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 7)
				{
					B2 = XBuffer.ReadBytes(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 8)
				{
					O1 = XBuffer.ReadString(_buffer_, ref _offset_);


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
			XBuffer.WriteByte(9, _buffer_, ref _offset_);
			
			//写入数据

			XBuffer.WriteLong(Id, _buffer_, ref _offset_);


			XBuffer.WriteString(S1, _buffer_, ref _offset_);


			XBuffer.WriteInt(I1, _buffer_, ref _offset_);


			XBuffer.WriteBool(B1, _buffer_, ref _offset_);


			XBuffer.WriteFloat(F1, _buffer_, ref _offset_);


			XBuffer.WriteShort(S2, _buffer_, ref _offset_);


			XBuffer.WriteDouble(D1, _buffer_, ref _offset_);


			XBuffer.WriteBytes(B2, _buffer_, ref _offset_);


			XBuffer.WriteString(O1, _buffer_, ref _offset_);

			
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
					B1 = XBuffer.ReadBool(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 4)
				{
					F1 = XBuffer.ReadFloat(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 5)
				{
					S2 = XBuffer.ReadShort(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 6)
				{
					D1 = XBuffer.ReadDouble(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 7)
				{
					B2 = XBuffer.ReadBytes(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 8)
				{
					O1 = XBuffer.ReadString(_buffer_, ref _offset_);


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
			XBuffer.WriteByte(9, _buffer_, ref _offset_);
			
			//写入数据

			XBuffer.WriteLong(Id, _buffer_, ref _offset_);


			XBuffer.WriteString(S1, _buffer_, ref _offset_);


			XBuffer.WriteInt(I1, _buffer_, ref _offset_);


			XBuffer.WriteBool(B1, _buffer_, ref _offset_);


			XBuffer.WriteFloat(F1, _buffer_, ref _offset_);


			XBuffer.WriteShort(S2, _buffer_, ref _offset_);


			XBuffer.WriteDouble(D1, _buffer_, ref _offset_);


			XBuffer.WriteBytes(B2, _buffer_, ref _offset_);


			XBuffer.WriteString(O1, _buffer_, ref _offset_);

			
			return _offset_;
		}




		/*********************************************************/
		public override int GetSerializeLength()
		{
			int len = 0;

			len += XBuffer.IntSize; //UniId

			len += XBuffer.ByteSize; //_fieldNum_

			//Id
			len += XBuffer.LongSize;
			//S1
			len += XBuffer.GetStringSerializeLength(S1);
			//I1
			len += XBuffer.IntSize;
			//B1
			len += XBuffer.BoolSize;
			//F1
			len += XBuffer.FloatSize;
			//S2
			len += XBuffer.ShortSize;
			//D1
			len += XBuffer.DoubleSize;
			//B2
			len += XBuffer.GetByteArraySerializeLength(B2);
			//O1
			len += XBuffer.GetStringSerializeLength(O1);

			return len;
		}
	}
}
