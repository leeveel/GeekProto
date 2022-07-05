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
    public class ReqLogin : BaseMessage
	{

		static ReqLogin()
		{
			Geek.Server.SerializerOptions.Resolver = Geek.Server.Proto.SerializeResolver.Instance;
		}


		/*********************************************************/
		public string UserName {get;set;}
		public string Platform {get;set;}
		public int SdkType {get;set;}
		public string SdkToken {get;set;}
		public string Device {get;set;}
		public List<Int32> List {get;set;}
		public Dictionary<Int32, Int32> Map1 {get;set;}
		/*********************************************************/


		public const int MsgID = SID;
		public override int Sid { get;} = 9999;
		public const int SID = 9999;
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
					UserName = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{
					Platform = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 2)
				{
					SdkType = XBuffer.ReadInt(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 3)
				{
					SdkToken = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 4)
				{
					Device = XBuffer.ReadString(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 5)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						List = new List<Int32>();
						int count5 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count5; ++i)
						{
							List.Add(XBuffer.ReadInt(_buffer_, ref _offset_));
						}
					}



				}else break;
				if(_fieldNum_ > 6)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						Map1 = new Dictionary<Int32, Int32>();
						int count6 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count6; ++i)
						{
							var key = XBuffer.ReadInt(_buffer_, ref _offset_);
							var val = XBuffer.ReadInt(_buffer_, ref _offset_);
							Map1.Add(key, val);
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
			XBuffer.WriteByte(7, _buffer_, ref _offset_);
			
			//写入数据

			XBuffer.WriteString(UserName, _buffer_, ref _offset_);


			XBuffer.WriteString(Platform, _buffer_, ref _offset_);


			XBuffer.WriteInt(SdkType, _buffer_, ref _offset_);


			XBuffer.WriteString(SdkToken, _buffer_, ref _offset_);


			XBuffer.WriteString(Device, _buffer_, ref _offset_);



			if(List == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(List.Count, _buffer_, ref _offset_);
			    foreach (var item in List)
			    {
				    XBuffer.WriteInt(item, _buffer_, ref _offset_);
			    }
			}
			




			if(Map1 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(Map1.Count, _buffer_, ref _offset_);
			    foreach (var kv in Map1)
			    {
				    XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteInt(kv.Value, _buffer_, ref _offset_);
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

			//UserName
			len += XBuffer.GetStringSerializeLength(UserName);
			//Platform
			len += XBuffer.GetStringSerializeLength(Platform);
			//SdkType
			len += XBuffer.IntSize;
			//SdkToken
			len += XBuffer.GetStringSerializeLength(SdkToken);
			//Device
			len += XBuffer.GetStringSerializeLength(Device);
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
					len += List.Count * XBuffer.IntSize;
				}
			}

			//Map1
			
			if (Map1 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(Map1.Count > 0)
				{
					len += Map1.Count * XBuffer.IntSize;
					len += Map1.Count * XBuffer.IntSize;
			
				}
			}
			
			
			
			


			return len;
		}
	}
}
