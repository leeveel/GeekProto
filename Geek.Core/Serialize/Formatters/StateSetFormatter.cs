using System;
using System.Collections.Generic;

namespace Geek.Server
{
    public class StateSetFormatter<T> : ISerializeFormatter<StateSet<T>>
    {
        public StateSet<T> Deserialize(Span<byte> buffer, ref int offset)
        {
            var hasVal = XBuffer.ReadBool(buffer, ref offset);
            if (hasVal)
            {
                var count = XBuffer.ReadInt(buffer, ref offset);
                var collection = new StateSet<T>(count);
                bool isPrimitive = XBuffer.IsPrimitiveType<T>();
                ISerializeFormatter<T> formatter = null;
                for (int i = 0; i < count; i++)
                {
                    T val;
                    if (isPrimitive)
                    {
                        val = XBuffer.Read<T>(buffer, ref offset);
                    }
                    else
                    {
                        if (formatter == null)
                            formatter = SerializerOptions.Resolver.GetFormatter<T>();
                        val = formatter.Deserialize(buffer, ref offset);
                    }
                    collection.Add(val);
                }
                return collection;
            }
            return default;
        }

        public void Serialize(Span<byte> buffer, StateSet<T> value, ref int offset)
        {
            if (value == null)
            {
                XBuffer.WriteBool(false, buffer, ref offset);
                return;
            }
            XBuffer.WriteBool(true, buffer, ref offset);
            XBuffer.WriteInt(value.Count, buffer, ref offset);
            bool isPrimitive = XBuffer.IsPrimitiveType<T>();
            ISerializeFormatter<T> formatter = null;
            foreach (var item in value)
            {
                if (isPrimitive)
                {
                    XBuffer.Write<T>(item, buffer, ref offset);
                }
                else
                {
                    if (formatter == null)
                        formatter = SerializerOptions.Resolver.GetFormatter<T>();
                    formatter.Serialize(buffer, item, ref offset);
                }
            }
        }

        public int GetSerializeLength(StateSet<T> value)
        {
            if (value == null)
                return XBuffer.BoolSize; //记录是否有值 

            int len = XBuffer.BoolSize + XBuffer.IntSize; //count

            if(value.Count <= 0)
                return len;

            bool isPrimitive = XBuffer.IsStrictPrimitiveType<T>();
            if (isPrimitive)
            {
                len += value.Count * XBuffer.GetPrimitiveSerializeLength<T>();
            }
            else
            {
                ISerializeFormatter<T> formatter = null;
                Type type = typeof(T);
                foreach (var item in value)
                {
                    if (type == XBuffer.StringType)
                    {
                        len += XBuffer.GetStringSerializeLength(item as string);
                    }
                    else if (type == XBuffer.ByteArrType)
                    {
                        len += XBuffer.GetByteArraySerializeLength(item as byte[]);
                    }
                    else
                    {
                        if (formatter == null)
                            formatter = SerializerOptions.Resolver.GetFormatter<T>();
                        len += formatter.GetSerializeLength(item);
                    }
                }
            }
            return len;
        }

    }
}
