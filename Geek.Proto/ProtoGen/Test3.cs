//auto generated, do not modify it
//限制：命名不能以下划线结尾(可能冲突)
//限制：map的key只支持基础类型和string；list/map不能optional,list/map不能嵌套
//兼容限制：字段只能添加，添加后不能删除，添加字段只能添加到最后,添加消息类型只能添加到最后
//兼容限制：不能修改字段类型（如从bool改为long）
//兼容限制：消息类型(含msdId)不能作为其他消息的成员类型


using Core.Serialize;
using System.Collections.Generic;

///<summary></summary>
namespace Proto
{
	
    public partial class Test3 : Serializable
	{
		static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();



		public Test3()
		{
			List = new List<Proto.Test1>();

			Map = new Dictionary<int, int>();


			Map2 = new Dictionary<int, Proto.Test1>();


			Map3 = new Dictionary<int, List<Test1>>();


			Map4 = new Dictionary<int, HashSet<Test1>>();


			Map5 = new Dictionary<int, Dictionary<long, Proto.Test1>>();

		}



		
		public override int Sid { get; set;} = 111103;

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

					UserId = SerializeTool.Read_string(false,  _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 1)
				{

					Platform = SerializeTool.Read_string(false,  _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 2)
				{
					
					/*********************************************************/
					SerializeTool.ReadCustomCollection<Proto.Test1>(List, _buffer_, ref _offset_);
					/*********************************************************/


				}else break;
				if(_fieldNum_ > 3)
				{

					
					/*********************************************************/
					SerializeTool.Read_int_int_Map(Map, _buffer_, ref _offset_);
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 4)
				{

					
					/*********************************************************/
					SerializeTool.Read_int_CustomMap<Proto.Test1>(Map2, _buffer_, ref _offset_);
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 5)
				{



					/*********************************************************/
					SerializeTool.Read_int_NestCustomList<Test1>(Map3, _buffer_, ref _offset_);
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 6)
				{



					/*********************************************************/
					SerializeTool.Read_int_NestCustomSet<Test1>(Map4, _buffer_, ref _offset_);
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 7)
				{


					/*********************************************************/
					SerializeTool.Read_int_long_NestCustomMap<Proto.Test1>(Map5, _buffer_, ref _offset_);
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 8)
				{

					T1 = SerializeTool.ReadCustom<Proto.Test1>(T1,true,  _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 9)
				{

					T2 = SerializeTool.ReadCustom<Proto.Test1>(T2,true,  _buffer_, ref _offset_);


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
			XBuffer.WriteByte(10, _buffer_, ref _offset_);
			
			//写入数据

			
			_offset_ = SerializeTool.WritePrimitive(UserId,false, UserId!=default, _buffer_, ref _offset_);


			
			_offset_ = SerializeTool.WritePrimitive(Platform,false, Platform!=default, _buffer_, ref _offset_);



			/*********************************************************/
			_offset_ = SerializeTool.WriteCustomCollection(List, _buffer_, ref _offset_);
			/*********************************************************/
			


			
			/*********************************************************/
			_offset_ = SerializeTool.WritePrimitiveMap(Map, _buffer_, ref _offset_);
			/*********************************************************/




			
			/*********************************************************/
			_offset_ = SerializeTool.WriteCustomMap<Proto.Test1>(Map2, _buffer_, ref _offset_);
			/*********************************************************/






			/*********************************************************/
			_offset_ = SerializeTool.WriteNestCustomList(Map3, _buffer_, ref _offset_);
			/*********************************************************/







			/*********************************************************/
			_offset_ = SerializeTool.WriteNestCustomSet(Map4, _buffer_, ref _offset_);
			/*********************************************************/





			_offset_ = SerializeTool.WriteNestCustomMap(Map5, _buffer_, ref _offset_);



			
			_offset_ = SerializeTool.WriteCustom(T1,true,   T1!=default, _buffer_, ref _offset_);


			
			_offset_ = SerializeTool.WriteCustom(T2,true,   T2!=default, _buffer_, ref _offset_);

			
			//覆盖当前对象长度
			XBuffer.WriteInt(_offset_ - _startOffset_, _buffer_, ref _startOffset_);
			return _offset_;
		}
	}
}