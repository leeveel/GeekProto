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
	
    public class ReqTest : BaseMessage
	{
		static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

		public string UserId { get; set; }


        public override int MsgId { get; } = 111105;
		
		public override int Sid { get; set;} = 111105;

		public override T Create<T>(int sid)
        {
            return Proto.SClassFactory.Create<T>(sid);
        }

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

					//UserId = SerializeTool.Read_string(false,  _buffer_, ref _offset_);


					UserId = XBuffer.ReadString(_buffer_, ref _offset_);




				}else break;
			}while(false);
			
			return _offset_;
		}

		
		///<summary>序列化，写入数据</summary>
        public override int Write(byte[] _buffer_, int _offset_)
        {	
			XBuffer.WriteInt(UniId, _buffer_, ref _offset_);
			_offset_ = base.Write(_buffer_, _offset_);
			
			//写入字段数量,最多支持255个
			XBuffer.WriteByte(1, _buffer_, ref _offset_);
			
			//写入数据

			


			XBuffer.WriteString(UserId, _buffer_, ref _offset_);
            


			
			return _offset_;
		}
	}
}