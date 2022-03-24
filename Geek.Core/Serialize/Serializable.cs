using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Core.Serialize
{

    /// <summary>
    /// 除了消息头和State头以外的全部继承此类
    /// </summary>
    public abstract class Serializable : ISerializable
    {

        static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        public virtual int Read(byte[] buffer, int offset)
        {
            return offset;
        }

        public virtual int Write(byte[] buffer, int offset)
        {
            return offset;
        }

        public byte[] Serialize()
        {
            return WriteAsBuffer(512);
        }

        protected virtual byte[] WriteAsBuffer(int size)
        {
            var data = new byte[size];
            var offset = 0;
            offset = this.Write(data, offset);
            if (offset <= data.Length)
            {
                if (offset < data.Length)
                {
                    var ret = new byte[offset];
                    Array.Copy(data, 0, ret, 0, offset);
                    data = ret;
                }
                return data;
            }
            else
            {
                return WriteAsBuffer(offset);
            }
        }

        public void Deserialize(byte[] data)
        {
            this.Read(data, 0);
        }

        public virtual int Sid { get; set; }


        #region Read
        public virtual T Create<T>(int sid) where T : Serializable
        {
            return null;
        }

        protected virtual List<T> GetList<T>()
        {
            return new List<T>();
        }

        protected virtual HashSet<T> GetSet<T>()
        {
            return new HashSet<T>();
        }

        protected virtual Dictionary<K, V> GetMap<K, V>()
        {
            return new Dictionary<K, V>();
        }


        protected virtual T ReadCustom<T>(T target, bool optional, byte[] buffer, ref int offset) where T : Serializable
        {
            if (optional)
            {
                var hasVal = XBuffer.ReadBool(buffer, ref offset);
                if (hasVal)
                {
                    var sid = XBuffer.ReadInt(buffer, ref offset);
                    target = Create<T>(sid);
                    offset = target.Read(buffer, offset);
                }
            }
            else
            {
                var sid = XBuffer.ReadInt(buffer, ref offset);
                target = Create<T>(sid);
                offset = target.Read(buffer, offset);
            }
            return target;
        }

        protected virtual T ReadPrimitive<T>(bool optional, byte[] buffer, ref int offset)
        {
            if (optional)
            {
                var hasVal = XBuffer.ReadBool(buffer, ref offset);
                if (hasVal)
                    return XBuffer.Read<T>(buffer, ref offset);
                else
                    return default;
            }
            else
            {
               return XBuffer.Read<T>(buffer, ref offset);
            }
        }

        /// <summary>
        /// 自定义类型 list-set
        /// </summary>
        protected virtual int ReadCustomCollection<T>(ICollection<T> target, byte[] buffer, ref int offset) where T : Serializable
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                var sid = XBuffer.ReadInt(buffer, ref offset);
                if (sid <= 0)
                {
                    target.Add(default);
                    continue;
                }
                var val = Create<T>(sid);
                offset = val.Read(buffer, offset);
                target.Add(val);
            }
            return offset;
        }

        /// <summary>
        /// 基础类型 list-set
        /// </summary>
        protected virtual int ReadPrimitiveCollection<T>(ICollection<T> target, byte[] buffer, ref int offset)
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                target.Add(XBuffer.Read<T>(buffer, ref offset));
            }
            return offset;
        }

        /// <summary>
        /// 自定义类型 dictionary
        /// </summary>
        protected virtual int ReadCustomMap<K, V>(Dictionary<K, V> target, byte[] buffer, ref int offset) where V : Serializable
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                var key = XBuffer.Read<K>(buffer, ref offset);
                var sid = XBuffer.ReadInt(buffer, ref offset);
                if (sid <= 0)
                {
                    target[key] = default;
                    continue;
                }
                var val = Create<V>(sid);
                offset = val.Read(buffer, offset);
                target.Add(key, val);
            }
            return offset;
        }

        /// <summary>
        /// 基础类型 dictionary
        /// </summary>
        protected virtual int ReadPrimitiveMap<K, V>(Dictionary<K, V> target, byte[] buffer, ref int offset)
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                var key = XBuffer.Read<K>(buffer, ref offset);
                var val = XBuffer.Read<V>(buffer, ref offset);
                target.Add(key, val);
            }
            return offset;
        }

        /// <summary>
        /// 嵌套dictionary (基础类型)
        /// </summary>
        protected virtual int ReadNestPrimitiveMap<K1, K2, V>(Dictionary<K1, Dictionary<K2, V>> target, byte[] buffer, ref int offset)
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                var key = XBuffer.Read<K1>(buffer, ref offset);
                var val = new Dictionary<K2, V>();         //TODO:类型处理
                ReadPrimitiveMap(val, buffer, ref offset);
                target.Add(key, val);
            }
            return offset;
        }

        /// <summary>
        /// 嵌套dictionary(自定义类型)
        /// </summary>
        protected virtual int ReadNestCustomMap<K1, K2, V>(Dictionary<K1, Dictionary<K2, V>> target, byte[] buffer, ref int offset) where V : Serializable
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                var key = XBuffer.Read<K1>(buffer, ref offset);
                var val = new Dictionary<K2, V>(); //TODO:类型处理
                ReadCustomMap(val, buffer, ref offset);
                target.Add(key, val);
            }
            return offset;
        }

        /// <summary>
        /// 嵌套list-set (基础类型)
        /// </summary>
        protected virtual int ReadNestPrimitiveList<K, V>(Dictionary<K, List<V>> target, byte[] buffer, ref int offset)
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                var key = XBuffer.Read<K>(buffer, ref offset);
                var val = new List<V>(); //TODO:类型处理
                ReadPrimitiveCollection(val, buffer, ref offset);
                target.Add(key, val);
            }
            return offset;
        }
        protected virtual int ReadNestPrimitiveSet<K, V>(Dictionary<K, HashSet<V>> target, byte[] buffer, ref int offset)
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                var key = XBuffer.Read<K>(buffer, ref offset);
                var val = new HashSet<V>(); //TODO:类型处理
                ReadPrimitiveCollection(val, buffer, ref offset);
                target.Add(key, val);
            }
            return offset;
        }
        /// <summary>
        /// 嵌套list-set (自定义类型)
        /// </summary>
        protected virtual int ReadNestCustomList<K, V>(Dictionary<K, List<V>> target, byte[] buffer, ref int offset) where V : Serializable
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                var key = XBuffer.Read<K>(buffer, ref offset);
                var val = new List<V>(); //TODO:类型处理
                ReadCustomCollection(val, buffer, ref offset);
                target.Add(key, val);
            }
            return offset;
        }

        protected virtual int ReadNestCustomSet<K, V>(Dictionary<K, HashSet<V>> target, byte[] buffer, ref int offset) where V : Serializable
        {
            int count = XBuffer.ReadInt(buffer, ref offset);
            for (int i = 0; i < count; ++i)
            {
                var key = XBuffer.Read<K>(buffer, ref offset);
                var val = new HashSet<V>(); //TODO:类型处理
                ReadCustomCollection(val, buffer, ref offset);
                target.Add(key, val);
            }
            return offset;
        }
        #endregion


        #region Write

        protected virtual int WriteCustom<T>(T target, bool optional, bool hasVal, byte[] buffer, ref int offset) where T : Serializable
        {
            if (optional)
            {
                XBuffer.WriteBool(hasVal, buffer, ref offset);
                if (hasVal)
                {
                    XBuffer.WriteInt(target.Sid, buffer, ref offset);
                    offset = target.Write(buffer, offset);
                }
            }
            else
            {
                XBuffer.WriteInt(target.Sid, buffer, ref offset);
                offset = target.Write(buffer, offset);
            }
            return offset;
        }

        protected virtual int WritePrimitive<T>(T val, bool optional, bool hasVal, byte[] buffer, ref int offset)
        {
            if (optional)
            {
                XBuffer.WriteBool(hasVal, buffer, ref offset);
                if (hasVal)
                {
                    XBuffer.Write<T>(val, buffer, ref offset);
                }
            }
            else
            {
                XBuffer.Write<T>(val, buffer, ref offset);
            }
            return offset;
        }


        protected virtual int WriteCustomCollection<T>(ICollection<T> target, byte[] buffer, ref int offset) where T : Serializable
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            int i = 0;
            foreach (var item in target)
            {
                if (item == null)
                {
                    LOGGER.Error("App.Proto.Test3.List has null item, idx == " + i);
                    XBuffer.WriteInt(0, buffer, ref offset);
                }
                else
                {
                    XBuffer.WriteInt(item.Sid, buffer, ref offset);
                    offset = item.Write(buffer, offset);
                }
                i++;
            }
            return offset;
        }

        /// <summary>
        /// 基础类型 list-set
        /// </summary>
        protected virtual int WritePrimitiveCollection<T>(ICollection<T> target, byte[] buffer, ref int offset)
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            foreach (var item in target)
            {
                XBuffer.Write<T>(item, buffer, ref offset);
            }
            return offset;
        }

        /// <summary>
        /// 自定义类型 dictionary
        /// </summary>
        protected virtual int WriteCustomMap<K, V>(Dictionary<K, V> target, byte[] buffer, ref int offset) where V : Serializable
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            foreach (var kv in target)
            {
                XBuffer.Write<K>(kv.Key, buffer, ref offset);
                if (kv.Value == null)
                {
                    LOGGER.Error($"{this.GetType().FullName} has null item: {kv.Key.ToString()}");
                    XBuffer.WriteInt(0, buffer, ref offset);
                }
                else
                {
                    XBuffer.WriteInt(kv.Value.Sid, buffer, ref offset);
                    offset = kv.Value.Write(buffer, offset);
                }
            }
            return offset;
        }

        /// <summary>
        /// 基础类型 dictionary
        /// </summary>
        protected virtual int WritePrimitiveMap<K, V>(Dictionary<K, V> target, byte[] buffer, ref int offset)
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            foreach (var kv in target)
            {
                XBuffer.Write<K>(kv.Key, buffer, ref offset);
                XBuffer.Write<V>(kv.Value, buffer, ref offset);
            }
            return offset;
        }

        //protected virtual int WritePrimitiveMap(Dictionary<int, int> target, byte[] buffer, ref int offset)
        //{
        //    XBuffer.WriteInt(target.Count, buffer, ref offset);
        //    foreach (var kv in target)
        //    {
        //        XBuffer.WriteInt(kv.Key, buffer, ref offset);
        //        XBuffer.WriteInt(kv.Value, buffer, ref offset);
        //    }
        //    return offset;
        //}

        //protected virtual int WritePrimitiveMap(Dictionary<int, string> target, byte[] buffer, ref int offset)
        //{
        //    XBuffer.WriteInt(target.Count, buffer, ref offset);
        //    foreach (var kv in target)
        //    {
        //        XBuffer.WriteInt(kv.Key, buffer, ref offset);
        //        XBuffer.WriteString(kv.Value, buffer, ref offset);
        //    }
        //    return offset;
        //}

        /// <summary>
        /// 嵌套dictionary (基础类型)
        /// </summary>
        protected virtual int WriteNestPrimitiveMap<K1, K2, V>(Dictionary<K1, Dictionary<K2, V>> target, byte[] buffer, ref int offset)
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            foreach (var kv in target)
            {
                XBuffer.Write<K1>(kv.Key, buffer, ref offset);
                WritePrimitiveMap<K2, V>(kv.Value, buffer, ref offset);
            }
            return offset;
        }

        /// <summary>
        /// 嵌套dictionary(自定义类型)
        /// </summary>
        protected virtual int WriteNestCustomMap<K1, K2, V>(Dictionary<K1, Dictionary<K2, V>> target, byte[] buffer, ref int offset) where V : Serializable
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            foreach (var kv in target)
            {
                XBuffer.Write<K1>(kv.Key, buffer, ref offset);
                WriteCustomMap<K2, V>(kv.Value, buffer, ref offset);
            }
            return offset;
        }

        /// <summary>
        /// 嵌套list-set (基础类型)
        /// </summary>
        protected virtual int WriteNestPrimitiveList<K, V>(Dictionary<K, List<V>> target, byte[] buffer, ref int offset)
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            foreach (var kv in target)
            {
                XBuffer.Write<K>(kv.Key, buffer, ref offset);
                WritePrimitiveCollection<V>(kv.Value, buffer, ref offset);
            }
            return offset;
        }

        protected virtual int WriteNestPrimitiveSet<K, V>(Dictionary<K, HashSet<V>> target, byte[] buffer, ref int offset)
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            foreach (var kv in target)
            {
                XBuffer.Write<K>(kv.Key, buffer, ref offset);
                WritePrimitiveCollection<V>(kv.Value, buffer, ref offset);
            }
            return offset;
        }

        /// <summary>
        /// 嵌套list-set (自定义类型)
        /// </summary>
        protected virtual int WriteNestCustomList<K, V>(Dictionary<K, List<V>> target, byte[] buffer, ref int offset) where V : Serializable
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            foreach (var kv in target)
            {
                XBuffer.Write<K>(kv.Key, buffer, ref offset);
                WriteCustomCollection<V>(kv.Value, buffer, ref offset);
            }
            return offset;
        }

        protected virtual int WriteNestCustomSet<K, V>(Dictionary<K, HashSet<V>> target, byte[] buffer, ref int offset) where V : Serializable
        {
            XBuffer.WriteInt(target.Count, buffer, ref offset);
            foreach (var kv in target)
            {
                XBuffer.Write<K>(kv.Key, buffer, ref offset);
                WriteCustomCollection<V>(kv.Value, buffer, ref offset);
            }
            return offset;
        }

        #endregion


    }
}
