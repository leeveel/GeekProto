//auto generated, do not modify it
//限制：命名不能以下划线结尾(可能冲突)
//限制：map的key只支持基础类型和string；list/map不能optional,list/map不能嵌套
//兼容限制：字段只能添加，添加后不能删除，添加字段只能添加到最后,添加消息类型只能添加到最后
//兼容限制：不能修改字段类型（如从bool改为long）
//兼容限制：消息类型(含msdId)不能作为其他消息的成员类型


using Geek.Server;
using System.Collections.Generic;

///<summary></summary>
namespace Proto
{
	
    public class Test4 : Serializable
	{
		static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

		public Proto.Test1 T1 { get; set; }
		public Proto.Test2 T2 { get; set; }

		public Dictionary<int, string> Map = new Dictionary<int, string>();



		
		public override int Sid { get; set;} = 111104;

		public override T Create<T>(int sid)
        {
            return Proto.SClassFactory.Create<T>(sid);
        }

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

					T1 = ReadCustom<Proto.Test1>(T1,true,  _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{

					T2 = ReadCustom<Proto.Test2>(T2,true,  _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 2)
				{

					
					/*********************************************************/
					int count3 = XBuffer.ReadInt(_buffer_, ref _offset_);
					for (int i = 0; i < count3; ++i)
					{

						var key = XBuffer.ReadInt(_buffer_, ref _offset_);

						
						var val = XBuffer.ReadString(_buffer_, ref _offset_);

						Map.Add(key, val);
					}
					/*********************************************************/



				}else break;
			}while(false);
			
			//剔除多余数据
			if(_offset_ < _toReadLength_ - _startOffset_)
				_offset_ += _toReadLength_ - _startOffset_;
			return _offset_;
		}

		
		///<summary>序列化，写入数据</summary>
        public override int Write(byte[] _buffer_, int _offset_)
        {	
			_offset_ = base.Write(_buffer_, _offset_);
			//先写入当前对象长度占位符
			int _startOffset_ = _offset_;
			XBuffer.WriteInt(0, _buffer_, ref _offset_);
			
			//写入字段数量,最多支持255个
			XBuffer.WriteByte(3, _buffer_, ref _offset_);
			
			//写入数据

			
			_offset_ = WriteCustom<Proto.Test1>(T1,true, _buffer_, ref _offset_);


			
			_offset_ = WriteCustom<Proto.Test2>(T2,true, _buffer_, ref _offset_);



			
			/*********************************************************/
			//_offset_ = SerializeTool.WritePrimitiveMap(Map, _buffer_, ref _offset_);
			XBuffer.WriteInt(Map.Count, _buffer_, ref _offset_);
            foreach (var kv in Map)
            {
				XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);

				XBuffer.WriteString(kv.Value, _buffer_, ref _offset_);
            }
			/*********************************************************/


			
			//覆盖当前对象长度
			XBuffer.WriteInt(_offset_ - _startOffset_, _buffer_, ref _startOffset_);
			return _offset_;
		}
	}
}