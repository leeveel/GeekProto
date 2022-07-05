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
	[IsState]
    public class Test2 : BaseMessage
	{

		static Test2()
		{
			Geek.Server.SerializerOptions.Resolver = Geek.Server.Proto.SerializeResolver.Instance;
		}


		/*********************************************************/
		private long _L1_;
		public long L1 
		{ 
			get{ return _L1_; }
			set{ _L1_= value; _stateChanged=true;}
		}
		private StateList<String> _L2_;
		public StateList<String> L2 
		{ 
			get{ return _L2_; }
			set{ _L2_= value; _stateChanged=true;}
		}
		private StateList<Single> _L3_;
		public StateList<Single> L3 
		{ 
			get{ return _L3_; }
			set{ _L3_= value; _stateChanged=true;}
		}
		private StateList<Geek.Server.Proto.Test1> _L4_;
		public StateList<Geek.Server.Proto.Test1> L4 
		{ 
			get{ return _L4_; }
			set{ _L4_= value; _stateChanged=true;}
		}
		private StateMap<Int64, String> _M1_;
		public StateMap<Int64, String> M1 
		{ 
			get{ return _M1_; }
			set{ _M1_= value; _stateChanged=true;}
		}
		private StateMap<Int32, Geek.Server.Proto.Test1> _M2_;
		public StateMap<Int32, Geek.Server.Proto.Test1> M2 
		{ 
			get{ return _M2_; }
			set{ _M2_= value; _stateChanged=true;}
		}
		private long _L5_;
		public long L5 
		{ 
			get{ return _L5_; }
			set{ _L5_= value; _stateChanged=true;}
		}


		///<summary>状态是否改变</summary>
		public override bool IsChanged
		{
			get
			{
				if(_stateChanged)
					return true;
				if(L2.IsChanged)
					return true;
				if(L3.IsChanged)
					return true;
				if(L4.IsChanged)
					return true;
				if(M1.IsChanged)
					return true;
				if(M2.IsChanged)
					return true;
				return false;
			}
		}
		
		///<summary>清除所有改变[含子项]</summary>
		public override void ClearChanges()
		{
			_stateChanged = false;
						L2.ClearChanges();
			L3.ClearChanges();
			L4.ClearChanges();
			M1.ClearChanges();
			M2.ClearChanges();
		}
		/*********************************************************/


		public const int MsgID = SID;
		public override int Sid { get;} = 111102;
		public const int SID = 111102;
		public const bool IsState = true;

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
					L1 = XBuffer.ReadLong(_buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						L2 = new StateList<String>();
						int count1 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count1; ++i)
						{
							L2.Add(XBuffer.ReadString(_buffer_, ref _offset_));
						}
					}



				}else break;
				if(_fieldNum_ > 2)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						L3 = new StateList<Single>();
						int count2 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count2; ++i)
						{
							L3.Add(XBuffer.ReadFloat(_buffer_, ref _offset_));
						}
					}



				}else break;
				if(_fieldNum_ > 3)
				{

					 var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					 if(hasVal)
					 {
						L4 = new StateList<Geek.Server.Proto.Test1>();
						int count3 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count3; ++i)
						{
							var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
							if (sid <= 0)
							{
								L4.Add(default);
								continue;
							}
							var val = Create<Geek.Server.Proto.Test1>(sid);
							_offset_ = val.Read(_buffer_, _offset_);
							L4.Add(val);
						}
					}



				}else break;
				if(_fieldNum_ > 4)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						M1 = new StateMap<Int64, String>();
						int count4 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count4; ++i)
						{
							var key = XBuffer.ReadLong(_buffer_, ref _offset_);
							var val = XBuffer.ReadString(_buffer_, ref _offset_);
							M1.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 5)
				{

					var hasVal = XBuffer.ReadBool(_buffer_, ref _offset_);
					if(hasVal)
					{
						M2 = new StateMap<Int32, Geek.Server.Proto.Test1>();
						int count5 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int i = 0; i < count5; ++i)
						{
							var key = XBuffer.ReadInt(_buffer_, ref _offset_);
							var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
							if (sid <= 0)
							{
								M2[key] = default;
								continue;
							}
							var val = Create<Geek.Server.Proto.Test1>(sid);
							_offset_ = val.Read(_buffer_, _offset_);
							M2.Add(key, val);
						}
					}



				}else break;
				if(_fieldNum_ > 6)
				{
					L5 = XBuffer.ReadLong(_buffer_, ref _offset_);


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

			XBuffer.WriteLong(L1, _buffer_, ref _offset_);



			if(L2 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(L2.Count, _buffer_, ref _offset_);
			    foreach (var item in L2)
			    {
				    XBuffer.WriteString(item, _buffer_, ref _offset_);
			    }
			}
			




			if(L3 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(L3.Count, _buffer_, ref _offset_);
			    foreach (var item in L3)
			    {
				    XBuffer.WriteFloat(item, _buffer_, ref _offset_);
			    }
			}
			




			if(L4 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(L4.Count, _buffer_, ref _offset_);
			    for (int i=0; i<L4.Count; i++)
			    {
			        if (L4[i] == null)
			        {
			            Geek.Server.SerializeLogger.LogError($"{this.GetType().FullName}.L4 has null idx == : {i}");
			            XBuffer.WriteInt(0, _buffer_, ref _offset_);
			        }
			        else
			        {
			            XBuffer.WriteInt(L4[i].Sid, _buffer_, ref _offset_);
			            _offset_ = L4[i].Write(_buffer_, _offset_);
			        }
			    }
			}
			




			if(M1 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(M1.Count, _buffer_, ref _offset_);
			    foreach (var kv in M1)
			    {
				    XBuffer.WriteLong(kv.Key, _buffer_, ref _offset_);
				    XBuffer.WriteString(kv.Value, _buffer_, ref _offset_);
			    }
			}




			if(M2 == null)
			{
			    XBuffer.WriteBool(false, _buffer_, ref _offset_);
			}
			else
			{
			    XBuffer.WriteBool(true, _buffer_, ref _offset_);
			    XBuffer.WriteInt(M2.Count, _buffer_, ref _offset_);
			    foreach (var kv in M2)
			    {
				    XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);
			        if (kv.Value == null)
			        {
			            Geek.Server.SerializeLogger.LogError($"{this.GetType().FullName}.M2 has null item: {kv.Key}");
			            XBuffer.WriteInt(0, _buffer_, ref _offset_);
			        }
			        else
			        {
			            XBuffer.WriteInt(kv.Value.Sid, _buffer_, ref _offset_);
			            _offset_ = kv.Value.Write(_buffer_, _offset_);
			        }
			    }
			}



			XBuffer.WriteLong(L5, _buffer_, ref _offset_);

			
			return _offset_;
		}


		/*********************************************************/
		public override int GetSerializeLength()
		{
			int len = 0;

			len += XBuffer.IntSize; //UniId

			len += XBuffer.ByteSize; //_fieldNum_

			//L1
			len += XBuffer.LongSize;
			//L2
			if (L2 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(L2.Count > 0)
				{
					for (int i = 0; i < L2.Count; i++)
					{
						len += XBuffer.GetStringSerializeLength(L2[i]);
					}
				}
			}

			//L3
			if (L3 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(L3.Count > 0)
				{
					len += L3.Count * XBuffer.FloatSize;
				}
			}

			//L4
			if (L4 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(L4.Count > 0)
				{
					for (int i = 0; i < L4.Count; i++)
					{
						len += GetCustomLength<Geek.Server.Proto.Test1>(L4[i]);
					}
				}
			}

			//M1
			
			if (M1 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(M1.Count > 0)
				{
					len += M1.Count * XBuffer.LongSize;
			
					foreach (var keypair in M1)
					{
			
			
						len += XBuffer.GetStringSerializeLength(keypair.Value);
			
					}
				}
			}
			
			
			
			

			//M2
			
			if (M2 == null)
			{
				len += XBuffer.BoolSize; //hasVal 
			}
			else
			{
				len += XBuffer.BoolSize + XBuffer.IntSize; //count
				if(M2.Count > 0)
				{
					len += M2.Count * XBuffer.IntSize;
			
					foreach (var keypair in M2)
					{
			
			
						len += GetCustomLength<Geek.Server.Proto.Test1>(keypair.Value);
			
					}
				}
			}
			
			
			
			

			//L5
			len += XBuffer.LongSize;

			return len;
		}
	}
}
