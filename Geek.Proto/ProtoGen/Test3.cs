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


			Map6 = new Dictionary<int, Dictionary<long, Proto.TestEnum>>();


			Map7 = new Dictionary<int, Proto.TestEnum>();


			Map8 = new Dictionary<string, Proto.TestEnum>();

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

					//UserId = SerializeTool.Read_string(false,  _buffer_, ref _offset_);


					UserId = XBuffer.ReadString(_buffer_, ref _offset_);




				}else break;
				if(_fieldNum_ > 1)
				{

					//Platform = SerializeTool.Read_string(false,  _buffer_, ref _offset_);


					Platform = XBuffer.ReadString(_buffer_, ref _offset_);




				}else break;
				if(_fieldNum_ > 2)
				{
					
					/*********************************************************/
					int count2 = XBuffer.ReadInt(_buffer_, ref _offset_);
					for (int i = 0; i < count2; ++i)
					{
						var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
						if (sid <= 0)
						{
							List.Add(default);
							continue;
						}
						var val = Create<Proto.Test1>(sid);
						_offset_ = val.Read(_buffer_, _offset_);
						List.Add(val);
					}
					/*********************************************************/


				}else break;
				if(_fieldNum_ > 3)
				{

					
					/*********************************************************/
					int count3 = XBuffer.ReadInt(_buffer_, ref _offset_);
					for (int i = 0; i < count3; ++i)
					{

						var key = XBuffer.ReadInt(_buffer_, ref _offset_);

						
						var val = XBuffer.ReadInt(_buffer_, ref _offset_);

						Map.Add(key, val);
					}
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 4)
				{

					
					/*********************************************************/
					//SerializeTool.Read_int_CustomMap<Proto.Test1>(Map2, _buffer_, ref _offset_);
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
						var val = Create<Proto.Test1>(sid);
						_offset_ = val.Read(_buffer_, _offset_);
						Map2.Add(key, val);
					}
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 5)
				{



					/*********************************************************/
					int count5 = XBuffer.ReadInt(_buffer_, ref _offset_);
					for (int i = 0; i < count5; ++i)
					{
						var key = XBuffer.ReadInt(_buffer_, ref _offset_);

						var val = new List<Test1>(); //TODO:类型处理

						int count52 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int j = 0; j < count52; ++j)
						{
							var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
							if (sid <= 0)
							{
								val.Add(default);
								continue;
							}
							var val2 = Create<Test1>(sid);
							_offset_ = val2.Read(_buffer_, _offset_);
							val.Add(val2);
						}
						Map3.Add(key, val);
					}
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 6)
				{



					/*********************************************************/
					int count6 = XBuffer.ReadInt(_buffer_, ref _offset_);
					for (int i = 0; i < count6; ++i)
					{
						var key = XBuffer.ReadInt(_buffer_, ref _offset_);

						var val = new HashSet<Test1>(); //TODO:类型处理

						int count62 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int j = 0; j < count62; ++j)
						{
							var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
							if (sid <= 0)
							{
								val.Add(default);
								continue;
							}
							var val2 = Create<Test1>(sid);
							_offset_ = val2.Read(_buffer_, _offset_);
							val.Add(val2);
						}
						Map4.Add(key, val);
					}
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 7)
				{


					/*********************************************************/
					//SerializeTool.Read_int_long_NestCustomMap<Proto.Test1>(Map5, _buffer_, ref _offset_);
					int count7 = XBuffer.ReadInt(_buffer_, ref _offset_);
					for (int i = 0; i < count7; ++i)
					{
						var key = XBuffer.ReadInt(_buffer_, ref _offset_);
						var val = new Dictionary<long, Proto.Test1>(); //TODO:类型处理
						//ReadCustomMap(val, buffer, ref offset);
						int count72 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int j = 0; j < count72; ++j)
						{
							var key2 = XBuffer.ReadLong(_buffer_, ref _offset_);

							var sid = XBuffer.ReadInt(_buffer_, ref _offset_);
							if (sid <= 0)
							{
								val[key2] = default;
								continue;
							}
							var val2 = Create<Proto.Test1>(sid);
							_offset_ = val2.Read(_buffer_, _offset_);
							val.Add(key2, val2);
						}
						Map5.Add(key, val);
					}
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 8)
				{

					T1 = ReadCustom<Proto.Test1>(T1,true,  _buffer_, ref _offset_);


				}else break;
				if(_fieldNum_ > 9)
				{

					//E1 = SerializeTool.Read_Proto.TestEnum(false,  _buffer_, ref _offset_);


					E1 = (Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);;




				}else break;
				if(_fieldNum_ > 10)
				{


					/*********************************************************/
					//SerializeTool.Read_int_long_Proto.TestEnum_NestMap(Map6, _buffer_, ref _offset_);
					int count10 = XBuffer.ReadInt(_buffer_, ref _offset_);
					for (int i = 0; i < count10; ++i)
					{
						var key = XBuffer.ReadInt(_buffer_, ref _offset_);

						var val = new Dictionary<long, Proto.TestEnum>();         //TODO:类型处理
						int count102 = XBuffer.ReadInt(_buffer_, ref _offset_);
						for (int j = 0; j < count102; ++j)
						{
							var key2 = XBuffer.ReadLong(_buffer_, ref _offset_);

							var val2 = (Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);

							val.Add(key2, val2);
						}
						Map6.Add(key, val);
					}
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 11)
				{

					
					/*********************************************************/
					int count11 = XBuffer.ReadInt(_buffer_, ref _offset_);
					for (int i = 0; i < count11; ++i)
					{

						var key = XBuffer.ReadInt(_buffer_, ref _offset_);

						
						var val = (Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);

						Map7.Add(key, val);
					}
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 12)
				{

					
					/*********************************************************/
					int count11 = XBuffer.ReadInt(_buffer_, ref _offset_);
					for (int i = 0; i < count11; ++i)
					{

						var key = XBuffer.ReadString(_buffer_, ref _offset_);

						
						var val = (Proto.TestEnum)XBuffer.ReadInt(_buffer_, ref _offset_);

						Map8.Add(key, val);
					}
					/*********************************************************/



				}else break;
				if(_fieldNum_ > 13)
				{

					T2 = ReadCustom<Proto.Test1>(T2,true,  _buffer_, ref _offset_);


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
			XBuffer.WriteByte(14, _buffer_, ref _offset_);
			
			//写入数据

			


			XBuffer.WriteString(UserId, _buffer_, ref _offset_);
            



			


			XBuffer.WriteString(Platform, _buffer_, ref _offset_);
            




			/*********************************************************/
			XBuffer.WriteInt(List.Count, _buffer_, ref _offset_);
            for (int i=0; i<List.Count; i++)
            {
                if (List[i] == null)
                {
                    LOGGER.Error("App.Proto.Test3.List has null item, idx == " + i);
                    XBuffer.WriteInt(0, _buffer_, ref _offset_);
                }
                else
                {
                    XBuffer.WriteInt(List[i].Sid, _buffer_, ref _offset_);
                    _offset_ = List[i].Write(_buffer_, _offset_);
                }
            }
			/*********************************************************/
			


			
			/*********************************************************/
			//_offset_ = SerializeTool.WritePrimitiveMap(Map, _buffer_, ref _offset_);
			XBuffer.WriteInt(Map.Count, _buffer_, ref _offset_);
            foreach (var kv in Map)
            {
				XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);

				XBuffer.WriteInt(kv.Value, _buffer_, ref _offset_);
            }
			/*********************************************************/




			
			/*********************************************************/
			XBuffer.WriteInt(Map2.Count, _buffer_, ref _offset_);
            foreach (var kv in Map2)
            {
				XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);

                if (kv.Value == null)
                {
                    LOGGER.Error($"{this.GetType().FullName}.Map2 has null item: {kv.Key}");
                    XBuffer.WriteInt(0, _buffer_, ref _offset_);
                }
                else
                {
                    XBuffer.WriteInt(kv.Value.Sid, _buffer_, ref _offset_);
                    _offset_ = kv.Value.Write(_buffer_, _offset_);
                }
            }
			/*********************************************************/






			/*********************************************************/
			XBuffer.WriteInt(Map3.Count, _buffer_, ref _offset_);
            foreach (var kv in Map3)
            {
				XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);

				XBuffer.WriteInt(kv.Value.Count, _buffer_, ref _offset_);
				foreach (var item in kv.Value)
				{
					if (item == null)
					{
						LOGGER.Error($"{this.GetType().FullName}.Map3.{kv.Key} has null item");
						XBuffer.WriteInt(0, _buffer_, ref _offset_);
					}
					else
					{
						XBuffer.WriteInt(item.Sid, _buffer_, ref _offset_);
						_offset_ = item.Write(_buffer_, _offset_);
					}
				}
            }
			/*********************************************************/






			/*********************************************************/
			XBuffer.WriteInt(Map4.Count, _buffer_, ref _offset_);
            foreach (var kv in Map4)
            {
				XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);

				XBuffer.WriteInt(kv.Value.Count, _buffer_, ref _offset_);
				foreach (var item in kv.Value)
				{
					if (item == null)
					{
						LOGGER.Error($"{this.GetType().FullName}.Map4.{kv.Key} has null item");
						XBuffer.WriteInt(0, _buffer_, ref _offset_);
					}
					else
					{
						XBuffer.WriteInt(item.Sid, _buffer_, ref _offset_);
						_offset_ = item.Write(_buffer_, _offset_);
					}
				}
            }
			/*********************************************************/





			/*********************************************************/
			//_offset_ = SerializeTool.WriteNestCustomMap(Map5, _buffer_, ref _offset_);
			XBuffer.WriteInt(Map5.Count, _buffer_, ref _offset_);
            foreach (var kv in Map5)
            {
				XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);

				XBuffer.WriteInt(kv.Value.Count, _buffer_, ref _offset_);
				foreach (var kv2 in kv.Value)
				{
					XBuffer.WriteLong(kv2.Key, _buffer_, ref _offset_);

					if (kv.Value == null)
					{
						LOGGER.Error($"{this.GetType().FullName}.Map5 has null item: {kv2.Key.ToString()}");
						XBuffer.WriteInt(0, _buffer_, ref _offset_);
					}
					else
					{
						XBuffer.WriteInt(kv2.Value.Sid, _buffer_, ref _offset_);
						_offset_ = kv2.Value.Write(_buffer_, _offset_);
					}
				}
            }
			/*********************************************************/



			
			_offset_ = WriteCustom<Proto.Test1>(T1,true, _buffer_, ref _offset_);


			


			XBuffer.WriteInt((int)E1, _buffer_, ref _offset_);
            





			/*********************************************************/
			//_offset_ = SerializeTool.WriteNestPrimitiveMap(Map6, _buffer_, ref _offset_);
			XBuffer.WriteInt(Map6.Count, _buffer_, ref _offset_);
            foreach (var kv in Map6)
            {
				XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);

				XBuffer.WriteInt(kv.Value.Count, _buffer_, ref _offset_);
				foreach (var kv2 in kv.Value)
				{
					XBuffer.WriteLong(kv2.Key, _buffer_, ref _offset_);
					
					XBuffer.WriteInt((int)kv2.Value, _buffer_, ref _offset_);
				}
            }
			/*********************************************************/




			
			/*********************************************************/
			//_offset_ = SerializeTool.WritePrimitiveMap(Map7, _buffer_, ref _offset_);
			XBuffer.WriteInt(Map7.Count, _buffer_, ref _offset_);
            foreach (var kv in Map7)
            {
				XBuffer.WriteInt(kv.Key, _buffer_, ref _offset_);

				XBuffer.WriteInt((int)kv.Value, _buffer_, ref _offset_);
            }
			/*********************************************************/




			
			/*********************************************************/
			//_offset_ = SerializeTool.WritePrimitiveMap(Map8, _buffer_, ref _offset_);
			XBuffer.WriteInt(Map8.Count, _buffer_, ref _offset_);
            foreach (var kv in Map8)
            {
				XBuffer.WriteString(kv.Key, _buffer_, ref _offset_);

				XBuffer.WriteInt((int)kv.Value, _buffer_, ref _offset_);
            }
			/*********************************************************/



			
			_offset_ = WriteCustom<Proto.Test1>(T2,true, _buffer_, ref _offset_);

			
			//覆盖当前对象长度
			XBuffer.WriteInt(_offset_ - _startOffset_, _buffer_, ref _startOffset_);
			return _offset_;
		}
	}
}