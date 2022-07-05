using System;
using System.Collections.Generic;

namespace Geek.Server.Proto
{
	public class SerializeResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new SerializeResolver();

        private SerializeResolver()
        {
        }

        public ISerializeFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly ISerializeFormatter<T> Formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    Formatter = (ISerializeFormatter<T>)f;
                }
                else
                {
                    Geek.Server.SerializeLogger.LogError($"找不到类型{typeof(T).FullName}");
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        private static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(25)
            {
                { typeof(StateList<String>), 0 },
                { typeof(StateList<Single>), 1 },
                { typeof(StateList<Geek.Server.Proto.Test1>), 2 },
                { typeof(Geek.Server.Proto.Test1), 3 },
                { typeof(StateMap<Int64, String>), 4 },
                { typeof(StateMap<Int32, Geek.Server.Proto.Test1>), 5 },
                { typeof(List<Geek.Server.Proto.Test1>), 6 },
                { typeof(Dictionary<Int32, Int32>), 7 },
                { typeof(Dictionary<Int32, Geek.Server.Proto.Test1>), 8 },
                { typeof(Dictionary<Int32, List<Geek.Server.Proto.Test1>>), 9 },
                { typeof(Dictionary<Int32, HashSet<Geek.Server.Proto.Test1>>), 10 },
                { typeof(HashSet<Geek.Server.Proto.Test1>), 11 },
                { typeof(Dictionary<Int32, Dictionary<Int64, Geek.Server.Proto.Test1>>), 12 },
                { typeof(Dictionary<Int64, Geek.Server.Proto.Test1>), 13 },
                { typeof(Dictionary<String, String>), 14 },
                { typeof(Geek.Server.Proto.Test2), 15 },
                { typeof(Dictionary<Int32, String>), 16 },
                { typeof(List<Int32>), 17 },
                { typeof(List<TestEnum>), 18 },
                { typeof(Dictionary<TestEnum, Int32>), 19 },
                { typeof(Dictionary<TestEnum, TestEnum>), 20 },
                { typeof(Dictionary<TestEnum, String>), 21 },
                { typeof(List<DateTime>), 22 },
                { typeof(Dictionary<DateTime, DateTime>), 23 },
                { typeof(List<List<DateTime>>), 24 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            
            switch (key)
            {
                case 0: return new StateListFormatter<String>();
                case 1: return new StateListFormatter<Single>();
                case 2: return new StateListFormatter<Geek.Server.Proto.Test1>();
                case 3: return new CustomFormatter<Geek.Server.Proto.Test1>();
                case 4: return new StateMapFormatter<Int64,String>();
                case 5: return new StateMapFormatter<Int32,Geek.Server.Proto.Test1>();
                case 6: return new ListFormatter<Geek.Server.Proto.Test1>();
                case 7: return new DictionaryFormatter<Int32,Int32>();
                case 8: return new DictionaryFormatter<Int32,Geek.Server.Proto.Test1>();
                case 9: return new DictionaryFormatter<Int32,List<Geek.Server.Proto.Test1>>();
                case 10: return new DictionaryFormatter<Int32,HashSet<Geek.Server.Proto.Test1>>();
                case 11: return new HasSetFormatter<Geek.Server.Proto.Test1>();
                case 12: return new DictionaryFormatter<Int32,Dictionary<Int64, Geek.Server.Proto.Test1>>();
                case 13: return new DictionaryFormatter<Int64,Geek.Server.Proto.Test1>();
                case 14: return new DictionaryFormatter<String,String>();
                case 15: return new CustomFormatter<Geek.Server.Proto.Test2>();
                case 16: return new DictionaryFormatter<Int32,String>();
                case 17: return new ListFormatter<Int32>();
                case 18: return new ListFormatter<TestEnum>();
                case 19: return new DictionaryFormatter<TestEnum,Int32>();
                case 20: return new DictionaryFormatter<TestEnum,TestEnum>();
                case 21: return new DictionaryFormatter<TestEnum,String>();
                case 22: return new ListFormatter<DateTime>();
                case 23: return new DictionaryFormatter<DateTime,DateTime>();
                case 24: return new ListFormatter<List<DateTime>>();
                default: return null;
            }
        }
    }

    public class CustomFormatter<T> : ISerializeFormatter<T> where T : Serializable, new()
    {
        public T Deserialize(Span<byte> buffer, ref int offset)
        {
            T val = null;
            var hasVal = XBuffer.ReadBool(buffer, ref offset);
            if (hasVal)
            {
                var sid = XBuffer.ReadInt(buffer, ref offset);
                val = SClassFactory.Create<T>(sid);
                offset = val.Read(buffer, offset);
            }
            return val;
        }

        public int GetSerializeLength(T value)
        {
            if (value == null)
                return XBuffer.BoolSize;
             return XBuffer.BoolSize + XBuffer.IntSize + value.GetSerializeLength(); //hasval + sid + length
        }

        public void Serialize(Span<byte> buffer, T value, ref int offset)
        {
            bool hasVal = value != default;
            XBuffer.WriteBool(hasVal, buffer, ref offset);
            if (hasVal)
            {
                XBuffer.WriteInt(value.Sid, buffer, ref offset);
                offset = value.Write(buffer, offset);
            }
        }
    }

}
