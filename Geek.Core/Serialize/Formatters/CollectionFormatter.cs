using System;
using System.Collections.Generic;

namespace Geek.Server
{
    public abstract class CollectionFormatter<T> : ISerializeFormatter<ICollection<T>>
    {
        public abstract ICollection<T> Create(int capacity);

        public ICollection<T> Deserialize(Span<byte> buffer, ref int offset)
        {
            var hasVal = XBuffer.ReadBool(buffer, ref offset);
            if (hasVal)
            {
                var count = XBuffer.ReadInt(buffer, ref offset);
                var collection = Create(count);
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
                        if(formatter == null)
                            formatter = SerializerOptions.Resolver.GetFormatter<T>();
                        val = formatter.Deserialize(buffer, ref offset);
                    }
                    collection.Add(val);
                }
                return collection;
            }
            return default;
        }

        public int GetSerializeLength(ICollection<T> value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Span<byte> buffer, ICollection<T> value, ref int offset)
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
    }

    public class HasSetFormatter<T> : CollectionFormatter<T>
    {
        public override ICollection<T> Create(int capacity)
        {
            return new HashSet<T>(capacity);    
        }
    }

    public class StateSetFormatter<T> : CollectionFormatter<T>
    {
        public override ICollection<T> Create(int capacity)
        {
            return new StateSet<T>(capacity);
        }
    }

    public class LinkedListFormatter<T> : CollectionFormatter<T>
    {
        public override ICollection<T> Create(int capacity)
        {
            return new LinkedList<T>();
        }
    }

    public class StateLinkedListFormatter<T> : CollectionFormatter<T>
    {
        public override ICollection<T> Create(int capacity)
        {
            return new StateLinkedList<T>();
        }
    }

}
