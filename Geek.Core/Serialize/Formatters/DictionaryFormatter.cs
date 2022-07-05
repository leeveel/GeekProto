using System;
using System.Collections.Generic;

namespace Geek.Server
{
    public abstract class DictionaryFormatterBase<TKey, TValue, TIntermediate, TDictionary> : ISerializeFormatter<TDictionary>
        where TDictionary : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        protected abstract TIntermediate Create(int count);
        protected abstract void Add(TIntermediate collection, int index, TKey key, TValue value);
        protected abstract TDictionary Complete(TIntermediate intermediateCollection);
        public TDictionary Deserialize(Span<byte> buffer, ref int offset)
        {
            var hasVal = XBuffer.ReadBool(buffer, ref offset);
            if (hasVal)
            {
                var count = XBuffer.ReadInt(buffer, ref offset);
                ISerializeFormatter<TValue> valueFormatter = null;
                bool isPrimitive = XBuffer.IsPrimitiveType<TValue>();
                TIntermediate dict = Create(count);
                for (int i = 0; i < count; i++)
                {
                    TKey key = XBuffer.Read<TKey>(buffer, ref offset);
                    TValue value;
                    if (isPrimitive)
                    {
                        value = XBuffer.Read<TValue>(buffer, ref offset);
                    }
                    else
                    {
                        if(valueFormatter == null)
                            valueFormatter = SerializerOptions.Resolver.GetFormatter<TValue>();
                        value = valueFormatter.Deserialize(buffer, ref offset);
                    }
                    Add(dict, i, key, value);
                }
                return Complete(dict);
            }
            return default;
        }

        public void Serialize(Span<byte> buffer, TDictionary value, ref int offset)
        {
            if (value == null)
            {
                XBuffer.WriteBool(false, buffer, ref offset);
                return;
            }

            XBuffer.WriteBool(true, buffer, ref offset);
            int count;
            {
                if (value is ICollection<KeyValuePair<TKey, TValue>> col)
                {
                    count = col.Count;
                }
                else
                {
                    if (value is IReadOnlyCollection<KeyValuePair<TKey, TValue>> col2)
                        count = col2.Count;
                    else
                        throw new Exception("DictionaryFormatterBase's TDictionary supports only ICollection<KVP> or IReadOnlyCollection<KVP>");
                }
            }
            XBuffer.WriteInt(count, buffer, ref offset);

            ISerializeFormatter<TValue> valueFormatter = null;
            bool isPrimitive = XBuffer.IsPrimitiveType<TValue>();
            var e = value.GetEnumerator();
            try
            {
                while (e.MoveNext())
                {
                    KeyValuePair<TKey, TValue> item = e.Current;
                    XBuffer.Write<TKey>(item.Key, buffer, ref offset);
                    if (isPrimitive)
                    {
                        XBuffer.Write<TValue>(item.Value, buffer, ref offset);
                    }
                    else
                    {
                        if (valueFormatter == null)
                            valueFormatter = SerializerOptions.Resolver.GetFormatter<TValue>();
                        valueFormatter.Serialize(buffer, item.Value, ref offset);
                    }
                }
            }
            finally
            {
                e.Dispose();
            }
        }

        public int GetSerializeLength(TDictionary value)
        {
            if (value == null)
                return XBuffer.BoolSize;

            int len = XBuffer.BoolSize + XBuffer.IntSize; //hasval + count
            int count;
            {
                if (value is ICollection<KeyValuePair<TKey, TValue>> col)
                {
                    count = col.Count;
                }
                else
                {
                    if (value is IReadOnlyCollection<KeyValuePair<TKey, TValue>> col2)
                        count = col2.Count;
                    else
                        throw new Exception("DictionaryFormatterBase's TDictionary supports only ICollection<KVP> or IReadOnlyCollection<KVP>");
                }
            }

            if (count <= 0)
                return len;

            bool keyIsPrimitive = false;
            //计算key长度
            Type keyType = typeof(TKey);
            if (keyType != XBuffer.StringType)
            {
                keyIsPrimitive = true;
                len += count * XBuffer.GetPrimitiveSerializeLength<TKey>();
            }

            //计算value长度
            bool valueIsPrimitive = XBuffer.IsStrictPrimitiveType<TValue>();
            if (valueIsPrimitive)
                len += count * XBuffer.GetPrimitiveSerializeLength<TValue>();

            if(!keyIsPrimitive || !valueIsPrimitive)
            {
                ISerializeFormatter<TValue> formatter = null;
                Type valueType = typeof(TValue);
                foreach (var keypair in value)
                {
                    if(!keyIsPrimitive) //这里只有可能是string
                        len += XBuffer.GetStringSerializeLength(keypair.Key as string);

                    if (!valueIsPrimitive)
                    {
                        if (valueType == XBuffer.StringType)
                        {
                            len += XBuffer.GetStringSerializeLength(keypair.Value as string);
                        }
                        else if (valueType == XBuffer.ByteArrType)
                        {
                            len += XBuffer.GetByteArraySerializeLength(keypair.Value as byte[]);
                        }
                        else
                        {
                            if (formatter == null)
                                formatter = SerializerOptions.Resolver.GetFormatter<TValue>();
                            len += formatter.GetSerializeLength(keypair.Value);
                        }
                    }
                }
            }
            return len;
        }
    }

    public sealed class DictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, Dictionary<TKey, TValue>>
    {
        protected override void Add(Dictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override Dictionary<TKey, TValue> Complete(Dictionary<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override Dictionary<TKey, TValue> Create(int count)
        {
            return new Dictionary<TKey, TValue>(count);
        }
    }

    public sealed class StateMapFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, StateMap<TKey, TValue>, StateMap<TKey, TValue>>
    {
        protected override void Add(StateMap<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override StateMap<TKey, TValue> Complete(StateMap<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override StateMap<TKey, TValue> Create(int count)
        {
            return new StateMap<TKey, TValue>(count);
        }
    }

}
