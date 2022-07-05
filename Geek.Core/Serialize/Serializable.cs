using System;
using MessagePack;

namespace Geek.Server
{

    /// <summary>
    /// 除了消息头和State头以外的全部继承此类
    /// </summary>
    public abstract class Serializable : ISerializable
    {
        protected bool _stateChanged;
        [IgnoreMember]
        public virtual bool IsChanged { get { return _stateChanged; } }

        /// <summary>需要在actor线程内部调用才安全</summary>
        public virtual void ClearChanges() { _stateChanged = false; }


        public virtual int Read(byte[] buffer, int offset)
        {
            return Read(new Span<byte>(buffer), offset);
        }

        public virtual int Write(byte[] buffer, int offset)
        {
            return Write(new Span<byte>(buffer), offset);
        }

        public virtual int Read(Span<byte> buffer, int offset)
        {
            return offset;
        }

        public virtual int Write(Span<byte> buffer, int offset)
        {
            return offset;
        }

        public virtual int GetSerializeLength()
        {
            throw new NotImplementedException();
        }

        public void Serialize(Span<byte> span, int offset = 0)
        {
            Write(span, offset);
        }

        public byte[] Serialize()
        {
            int size = GetSerializeLength();
            var data = new byte[size];
            int offset = 0;
            offset = Write(data, offset);
            if (offset != size)
                throw new Exception($"{GetType().FullName}.GetSerializeLength 计算错误=>{size}:{offset}");
            return data;
        }

        public void Deserialize(byte[] data)
        {
            this.Read(data, 0);
        }

        public void Deserialize(Span<byte> data)
        {
            this.Read(data, 0);
        }


        [IgnoreMember]
        public virtual int Sid { get; }

        public virtual T Create<T>(int sid) where T : Serializable
        {
            return null;
        }

        protected virtual T ReadCustom<T>(T val, Span<byte> buffer, ref int offset) where T : Serializable
        {
            var hasVal = XBuffer.ReadBool(buffer, ref offset);
            if (hasVal)
            {
                var sid = XBuffer.ReadInt(buffer, ref offset);
                val = Create<T>(sid);
                offset = val.Read(buffer, offset);
            }
            return val;
        }

        protected virtual int WriteCustom<T>(T val, Span<byte> buffer, ref int offset) where T : Serializable
        {
            bool hasVal = val != null;
            XBuffer.WriteBool(hasVal, buffer, ref offset);
            if (hasVal)
            {
                XBuffer.WriteInt(val.Sid, buffer, ref offset);
                offset = val.Write(buffer, offset);
            }
            return offset;
        }

        protected virtual int GetCustomLength<T>(T val) where T : Serializable
        {
            if (val == null)
                return XBuffer.BoolSize;
            return XBuffer.BoolSize + XBuffer.IntSize + val.GetSerializeLength(); //hasval + sid + length
        }

        protected virtual int WriteCollection<T>(T val, Span<byte> buffer, ref int offset)
        {
            var formmater = SerializerOptions.Resolver.GetFormatter<T>();
            formmater.Serialize(buffer, val, ref offset);
            return offset;
        }

        protected virtual T ReadCollection<T>(Span<byte> buffer, ref int offset)
        {
            var formmater = SerializerOptions.Resolver.GetFormatter<T>();
            return formmater.Deserialize(buffer, ref offset);
        }

        protected virtual int GetCollectionLength<T>(T val)
        {
            var formmater = SerializerOptions.Resolver.GetFormatter<T>();
            return formmater.GetSerializeLength(val);
        }

    }
}
