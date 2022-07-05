using System;
using System.Collections.Generic;

namespace Geek.Server
{
    /// <summary>
    /// 对于一些非常用的collection类型，可以直接继承此类
    /// 常用类最好不要继承此类，如List<T>, 泛型抽象会影响性能
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <typeparam name="TIntermediate"></typeparam>
    /// <typeparam name="TEnumerator"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class CollectionFormatterBase<TElement, TIntermediate, TCollection> : ISerializeFormatter<TCollection>
        where TCollection : IEnumerable<TElement>
    {

        protected abstract TIntermediate Create(int capacity);

        protected abstract void Add(TIntermediate collection, int index, TElement value);

        protected abstract TCollection Complete(TIntermediate intermediateCollection);

        public TCollection Deserialize(Span<byte> buffer, ref int offset)
        {
            var hasVal = XBuffer.ReadBool(buffer, ref offset);
            if (hasVal)
            {
                var count = XBuffer.ReadInt(buffer, ref offset);
                var collection = Create(count);
                bool isPrimitive = XBuffer.IsPrimitiveType<TElement>();
                ISerializeFormatter<TElement> formatter = null;
                for (int i = 0; i < count; i++)
                {
                    TElement val;
                    if (isPrimitive)
                    {
                        val = XBuffer.Read<TElement>(buffer, ref offset);
                    }
                    else
                    {
                        if(formatter == null)
                            formatter = SerializerOptions.Resolver.GetFormatter<TElement>();
                        val = formatter.Deserialize(buffer, ref offset);
                    }
                    Add(collection, i, val);
                }
                return Complete(collection);
            }
            return default;
        }

        public int GetSerializeLength(TCollection value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Span<byte> buffer, TCollection value, ref int offset)
        {
            if (value == null)
            {
                XBuffer.WriteBool(false, buffer, ref offset);
                return;
            }
            XBuffer.WriteBool(true, buffer, ref offset);
            XBuffer.WriteInt(GetCount(value), buffer, ref offset);
            bool isPrimitive = XBuffer.IsPrimitiveType<TElement>();
            ISerializeFormatter<TElement> formatter = null;
            foreach (var item in value)
            {

                if (isPrimitive)
                {
                    XBuffer.Write<TElement>(item, buffer, ref offset);
                }
                else
                {
                    if (formatter == null)
                        formatter = SerializerOptions.Resolver.GetFormatter<TElement>();
                    formatter.Serialize(buffer, item, ref offset);
                }
            }
        }

        protected virtual int GetCount(TCollection sequence)
        {
            var collection = sequence as ICollection<TElement>;
            if (collection != null)
            {
                return collection.Count;
            }
            else
            {
                var c2 = sequence as IReadOnlyCollection<TElement>;
                if (c2 != null)
                {
                    return c2.Count;
                }
            }
            throw new Exception($"未知的数据类型:{sequence.GetType().FullName}");
        }
    }


    /// <summary>
    /// 仅仅用于展示如何使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DemoLinkedListFormatter<T> : CollectionFormatterBase<T, LinkedList<T>, LinkedList<T>>
    {
        protected override void Add(LinkedList<T> collection, int index, T value)
        {
            collection.AddLast(value);
        }

        protected override LinkedList<T> Complete(LinkedList<T> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override LinkedList<T> Create(int count)
        {
            return new LinkedList<T>();
        }
    }


}
