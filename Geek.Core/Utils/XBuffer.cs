using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Geek.Server
{
    public class XBuffer
    {
        public const int BoolSize = sizeof(bool);
        public const int SbyteSize = sizeof(sbyte);
        public const int ByteSize = sizeof(byte);
        public const int CharSize = sizeof(char);
        public const int ShortSize = sizeof(short);
        public const int UShortSize = sizeof(ushort);
        public const int IntSize = sizeof(int);
        public const int UIntSize = sizeof(uint);
        public const int LongSize = sizeof(long);
        public const int ULongSize = sizeof(ulong);
        public const int FloatSize = sizeof(float);
        public const int DoubleSize = sizeof(double);

        public static readonly Type BoolType = typeof(bool);
        public static readonly Type SbyteType = typeof(sbyte);
        public static readonly Type ByteType = typeof(byte);
        public static readonly Type CharType = typeof(char);
        public static readonly Type ShortType = typeof(short);
        public static readonly Type UShortType = typeof(ushort);
        public static readonly Type IntType = typeof(int);
        public static readonly Type UIntType = typeof(uint);
        public static readonly Type LongType = typeof(long);
        public static readonly Type ULongType = typeof(ulong);
        public static readonly Type FloatType = typeof(float);
        public static readonly Type DoubleType = typeof(double);
        public static readonly Type StringType = typeof(string);
        public static readonly Type ByteArrType = typeof(byte[]);


        private static readonly Dictionary<Type, int> supportTypes = new Dictionary<Type, int>()
        {
            { typeof(bool), 0 },
            { typeof(sbyte), 1 },
            { typeof(byte), 2 },
            { typeof(char), 3 },
            { typeof(short), 4 },
            { typeof(ushort), 5 },
            { typeof(int), 6 },
            { typeof(uint), 7 },
            { typeof(long), 8 },
            { typeof(ulong), 9 },
            { typeof(float), 10 },
            { typeof(double), 11 },
            { typeof(DateTime), 12 },
            { typeof(string), 13 },
            { typeof(byte[]), 14 },
        };

        private static readonly Dictionary<Type, int> typeSize = new Dictionary<Type, int>()
        {
            { typeof(bool), BoolSize },
            { typeof(sbyte), SbyteSize },
            { typeof(byte), ByteSize },
            { typeof(char), CharSize },
            { typeof(short), ShortSize },
            { typeof(ushort), UShortSize },
            { typeof(int), IntSize },
            { typeof(uint), UIntSize },
            { typeof(long), LongSize },
            { typeof(ulong), ULongSize },
            { typeof(float), FloatSize },
            { typeof(double), DoubleSize },
            { typeof(DateTime), LongSize },
        };

        public static bool IsPrimitiveType<T>()
        {
            Type type = typeof(T);
            if (supportTypes.ContainsKey(type))
            {
                return true;
            }
            if (type.IsEnum)
            {
                return true;
            }
            return false;
        }

        public static bool IsStrictPrimitiveType<T>()
        {
            Type type = typeof(T);
            if (type == StringType)
                return false;
            else if (type == ByteArrType)
                return false;
            if (supportTypes.ContainsKey(type))
            {
                return true;
            }
            if (type.IsEnum)
            {
                return true;
            }
            return false;
        }


        #region SerializeLength
        public static int GetStringSerializeLength(string str)
        {
            if (str == null)
                return BoolSize;
            if (str.Length <= 0)
                return BoolSize + IntSize;
            else
                return BoolSize + IntSize + System.Text.Encoding.UTF8.GetByteCount(str);
        }

        public static int GetByteArraySerializeLength(byte[] bytes)
        {
            if (bytes == null)
                return BoolSize;
            return BoolSize + IntSize + bytes.Length;
        }

        public static int GetPrimitiveSerializeLength<T>()
        {
            Type t = typeof(T);
            if (t.IsEnum)
                return IntSize;
            if (typeSize.TryGetValue(t, out int len))
                return len;
            throw new ArgumentException($"未知的数据类型:{t.FullName}");
        }

        public static T Read<T>(Span<byte> buffer, ref int offset)
        {
            Type t = typeof(T);
            if (t.IsEnum)
                return (T)(object)ReadInt(buffer, ref offset);

            if (supportTypes.TryGetValue(t, out int code))
            {
                switch (code)
                {
                    case 0:
                        return (T)(object)ReadBool(buffer, ref offset);
                    case 1:
                        return (T)(object)ReadSByte(buffer, ref offset);
                    case 2:
                        return (T)(object)ReadByte(buffer, ref offset);
                    case 3:
                        return (T)(object)ReadChar(buffer, ref offset);
                    case 4:
                        return (T)(object)ReadShort(buffer, ref offset);
                    case 5:
                        return (T)(object)ReadUShort(buffer, ref offset);
                    case 6:
                        return (T)(object)ReadInt(buffer, ref offset);
                    case 7:
                        return (T)(object)ReadUInt(buffer, ref offset);
                    case 8:
                        return (T)(object)ReadLong(buffer, ref offset);
                    case 9:
                        return (T)(object)ReadULong(buffer, ref offset);
                    case 10:
                        return (T)(object)ReadFloat(buffer, ref offset);
                    case 11:
                        return (T)(object)ReadDouble(buffer, ref offset);
                    case 12:
                        return (T)(object)ReadDateTime(buffer, ref offset);
                    case 13:
                        return (T)(object)ReadString(buffer, ref offset);
                    case 14:
                        return (T)(object)ReadBytes(buffer, ref offset);
                    default:
                        break;
                }
            }
            return default;
        }
        #endregion

        public static int Write<T>(T value, Span<byte> buffer, ref int offset)
        {
            Type t = typeof(T);
            if (t.IsEnum)
            {
                WriteInt((int)(object)value, buffer, ref offset);
                return offset;
            }
            if (supportTypes.TryGetValue(t, out int code))
            {
                switch (code)
                {
                    case 0:
                        WriteBool((bool)(object)value, buffer, ref offset);
                        break;
                    case 1:
                        WriteSByte((sbyte)(object)value, buffer, ref offset);
                        break;
                    case 2:
                        WriteByte((byte)(object)value, buffer, ref offset);
                        break;
                    case 3:
                        WriteChar((char)(object)value, buffer, ref offset);
                        break;
                    case 4:
                        WriteShort((short)(object)value, buffer, ref offset);
                        break;
                    case 5:
                        WriteUShort((ushort)(object)value, buffer, ref offset);
                        break;
                    case 6:
                        WriteInt((int)(object)value, buffer, ref offset);
                        break;
                    case 7:
                        WriteUInt((uint)(object)value, buffer, ref offset);
                        break;
                    case 8:
                        WriteLong((long)(object)value, buffer, ref offset);
                        break;
                    case 9:
                        WriteULong((ulong)(object)value, buffer, ref offset);
                        break;
                    case 10:
                        WriteFloat((float)(object)value, buffer, ref offset);
                        break;
                    case 11:
                        WriteDouble((double)(object)value, buffer, ref offset);
                        break;
                    case 12:
                        WriteDateTime((DateTime)(object)value, buffer, ref offset);
                        break;
                    case 13:
                        WriteString((string)(object)value, buffer, ref offset);
                        break;
                    case 14:
                        WriteBytes((byte[])(object)value, buffer, ref offset);
                        break;
                    default:
                        break;
                }
            }
            return offset;
        }


        #region Endian

        public static short HostToNetworkOrder(short host)
        {
            return BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(host) : host;
        }

        public static ushort HostToNetworkOrder(ushort host)
        {
            return BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(host) : host;
        }

        public static int HostToNetworkOrder(int host)
        {
            return BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(host) : host;
        }

        public static uint HostToNetworkOrder(uint host)
        {
            return BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(host) : host;
        }

        public static long HostToNetworkOrder(long host)
        {
            return BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(host) : host;
        }

        public static ulong HostToNetworkOrder(ulong host)
        {
            return BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(host) : host;
        }

        public static long NetworkToHostOrder(long network)
        {
            return HostToNetworkOrder(network);
        }
        public static ulong NetworkToHostOrder(ulong network)
        {
            return HostToNetworkOrder(network);
        }

        public static int NetworkToHostOrder(int network)
        {
            return HostToNetworkOrder(network);
        }

        public static uint NetworkToHostOrder(uint network)
        {
            return HostToNetworkOrder(network);
        }

        public static short NetworkToHostOrder(short network)
        {
            return HostToNetworkOrder(network);
        }

        public static ushort NetworkToHostOrder(ushort network)
        {
            return HostToNetworkOrder(network);
        }
        #endregion



        #region WriteSpan


        public static unsafe void WriteDateTime(DateTime value, Span<byte> buffer, ref int offset)
        {
            if (offset + LongSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + IntSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(long*)(ptr + offset) = HostToNetworkOrder(value.Ticks);
                offset += LongSize;
            }
        }

        public static unsafe void WriteChar(char value, Span<byte> buffer, ref int offset)
        {
            if (offset + CharSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + CharSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(short*)(ptr + offset) = HostToNetworkOrder((short)value);
                offset += CharSize;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteInt(int value, Span<byte> buffer, ref int offset)
        {
            if (offset + IntSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + IntSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(int*)(ptr + offset) = HostToNetworkOrder(value);
                offset += IntSize;
            }
        }

        public static unsafe void WriteUInt(uint value, Span<byte> buffer, ref int offset)
        {
            if (offset + UIntSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + UIntSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(uint*)(ptr + offset) = HostToNetworkOrder(value);
                offset += UIntSize;
            }
        }

        public static unsafe void WriteShort(short value, Span<byte> buffer, ref int offset)
        {
            if (offset + ShortSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + ShortSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(short*)(ptr + offset) = HostToNetworkOrder(value);
                offset += ShortSize;
            }
        }

        public static unsafe void WriteUShort(ushort value, Span<byte> buffer, ref int offset)
        {
            if (offset + UShortSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + UShortSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(ushort*)(ptr + offset) = HostToNetworkOrder(value);
                offset += UShortSize;
            }
        }

        public static unsafe void WriteLong(long value, Span<byte> buffer, ref int offset)
        {
            if (offset + LongSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + LongSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(long*)(ptr + offset) = HostToNetworkOrder(value);
                offset += LongSize;
            }
        }

        public static unsafe void WriteULong(ulong value, Span<byte> buffer, ref int offset)
        {
            if (offset + ULongSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + ULongSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(ulong*)(ptr + offset) = HostToNetworkOrder(value);
                offset += ULongSize;
            }
        }

        public static unsafe void WriteFloat(float value, Span<byte> buffer, ref int offset)
        {
            if (offset + FloatSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + FloatSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(float*)(ptr + offset) = value;
                *(int*)(ptr + offset) = HostToNetworkOrder(*(int*)(ptr + offset));
                offset += FloatSize;
            }
        }

        public static unsafe void WriteDouble(double value, Span<byte> buffer, ref int offset)
        {
            if (offset + DoubleSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + DoubleSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(double*)(ptr + offset) = value;
                *(long*)(ptr + offset) = HostToNetworkOrder(*(long*)(ptr + offset));
                offset += DoubleSize;
            }
        }

        public static unsafe void WriteByte(byte value, Span<byte> buffer, ref int offset)
        {
            if (offset + ByteSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + ByteSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(ptr + offset) = value;
                offset += ByteSize;
            }
        }

        public static unsafe void WriteBytes(byte[] value, Span<byte> buffer, ref int offset)
        {
            if (value == null)
            {
                WriteBool(false, buffer, ref offset);
                return;
            }
            WriteBool(true, buffer, ref offset);

            if (offset + value.Length + IntSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + value.Length + IntSize}, {buffer.Length}");
            }

            WriteInt(value.Length, buffer, ref offset);
            fixed (byte* ptr = buffer, valPtr = value)
            {
                Buffer.MemoryCopy(valPtr, ptr+offset, value.Length, value.Length);
                offset += value.Length;
            }
        }

        public static unsafe void WriteBytesWithoutLength(byte[] value, Span<byte> buffer, ref int offset)
        {
            if (value == null)
                return;

            if (offset + value.Length > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + value.Length}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer, valPtr = value)
            {
                Buffer.MemoryCopy(valPtr, ptr + offset, value.Length, value.Length);
                offset += value.Length;
            }
        }

        public static unsafe void WriteSByte(sbyte value, Span<byte> buffer, ref int offset)
        {
            if (offset + SbyteSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + SbyteSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(sbyte*)(ptr + offset) = value;
                offset += SbyteSize;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteString(string value, Span<byte> buffer, ref int offset)
        {
            if (value == null)
            {
                WriteBool(false, buffer, ref offset);
                return;
            }
            WriteBool(true, buffer, ref offset);

            int len = System.Text.Encoding.UTF8.GetByteCount(value);
            //预判已经超出长度了，直接计算长度就行了
            if (offset + len + IntSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + len + IntSize}, {buffer.Length}");
            }

            WriteInt(len, buffer, ref offset);
            var val = System.Text.Encoding.UTF8.GetBytes(value);
            fixed (byte* ptr = buffer, valPtr = val)
            {
                Buffer.MemoryCopy(valPtr, ptr + offset, len, len);
                offset += len;
            }
        }

        public static unsafe void WriteBool(bool value, Span<byte> buffer, ref int offset)
        {
            if (offset + BoolSize > buffer.Length)
            {
                throw new ArgumentException($"xbuffer write out of index {offset + BoolSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer)
            {
                *(bool*)(ptr + offset) = value;
                offset += BoolSize;
            }
        }


        #endregion



        #region ReadSpan
        public static unsafe DateTime ReadDateTime(Span<byte> buffer, ref int offset)
        {
            if (offset + LongSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(long*)(ptr + offset);
                offset += LongSize;
                long tick = NetworkToHostOrder(value);
                return new DateTime(tick);
            }
        }

        public static unsafe char ReadChar(Span<byte> buffer, ref int offset)
        {
            if (offset + CharSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(short*)(ptr + offset);
                offset += CharSize;
                return (char)NetworkToHostOrder(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int ReadInt(Span<byte> buffer, ref int offset)
        {
            if (offset + IntSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(int*)(ptr + offset);
                offset += IntSize;
                return NetworkToHostOrder(value);
            }
        }

        public static unsafe uint ReadUInt(Span<byte> buffer, ref int offset)
        {
            if (offset + UIntSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(uint*)(ptr + offset);
                offset += UIntSize;
                return NetworkToHostOrder(value);
            }
        }

        public static unsafe short ReadShort(Span<byte> buffer, ref int offset)
        {
            if (offset + ShortSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(short*)(ptr + offset);
                offset += ShortSize;
                return NetworkToHostOrder(value);
            }
        }

        public static unsafe ushort ReadUShort(Span<byte> buffer, ref int offset)
        {
            if (offset + UShortSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(ushort*)(ptr + offset);
                offset += UShortSize;
                return NetworkToHostOrder(value);
            }
        }

        public static unsafe long ReadLong(Span<byte> buffer, ref int offset)
        {
            if (offset + LongSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(long*)(ptr + offset);
                offset += LongSize;
                return NetworkToHostOrder(value);
            }
        }

        public static unsafe ulong ReadULong(Span<byte> buffer, ref int offset)
        {
            if (offset + ULongSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(ulong*)(ptr + offset);
                offset += ULongSize;
                return NetworkToHostOrder(value);
            }
        }

        public static unsafe float ReadFloat(Span<byte> buffer, ref int offset)
        {
            if (offset + FloatSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                *(int*)(ptr + offset) = NetworkToHostOrder(*(int*)(ptr + offset));
                var value = *(float*)(ptr + offset);
                offset += FloatSize;
                return value;
            }
        }

        public static unsafe double ReadDouble(Span<byte> buffer, ref int offset)
        {
            if (offset + DoubleSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                *(long*)(ptr + offset) = NetworkToHostOrder(*(long*)(ptr + offset));
                var value = *(double*)(ptr + offset);
                offset += DoubleSize;
                return value;
            }
        }

        public static unsafe byte ReadByte(Span<byte> buffer, ref int offset)
        {
            if (offset + ByteSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(ptr + offset);
                offset += ByteSize;
                return value;
            }
        }

        public static unsafe byte[] ReadBytes(Span<byte> buffer, ref int offset)
        {
            bool hasVal = ReadBool(buffer, ref offset);
            if (hasVal)
            {
                var len = ReadInt(buffer, ref offset);
                //数据不可信
                if (len <= 0 || offset + len > buffer.Length)
                    return Array.Empty<byte>();

                var data = buffer.Slice(offset, len).ToArray();
                offset += len;
                return data;
            }
            return null;
        }

        public static unsafe sbyte ReadSByte(Span<byte> buffer, ref int offset)
        {
            if (offset + ByteSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(sbyte*)(ptr + offset);
                offset += ByteSize;
                return value;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe string ReadString(Span<byte> buffer, ref int offset)
        {
            bool hasVal = ReadBool(buffer, ref offset);
            if (hasVal)
            {
                var len = ReadInt(buffer, ref offset);
                //数据不可信
                if (len <= 0 || offset + len > buffer.Length)
                    return "";
                fixed (byte* ptr = buffer)
                {
                    var value = System.Text.Encoding.UTF8.GetString(ptr + offset, len);
                    offset += len;
                    return value;
                }
            }
            return null;
        }

        public static unsafe bool ReadBool(Span<byte> buffer, ref int offset)
        {
            if (offset + BoolSize > buffer.Length)
                throw new Exception("xbuffer read out of index");

            fixed (byte* ptr = buffer)
            {
                var value = *(bool*)(ptr + offset);
                offset += BoolSize;
                return value;
            }
        }
        #endregion


        //#region Write

        //public static unsafe void WriteInt(int value, byte[] buffer, ref int offset)
        //{
        //    if (offset + IntSize > buffer.Length)
        //    {
        //        offset += IntSize;
        //        return;
        //    }

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(int*)(ptr + offset) = HostToNetworkOrder(value);
        //        offset += IntSize;
        //    }
        //}

        //public static unsafe void WriteShort(short value, byte[] buffer, ref int offset)
        //{
        //    if (offset + ShortSize > buffer.Length)
        //    {
        //        offset += ShortSize;
        //        return;
        //    }

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(short*)(ptr + offset) = HostToNetworkOrder(value);
        //        offset += ShortSize;
        //    }
        //}

        //public static unsafe void WriteLong(long value, byte[] buffer, ref int offset)
        //{
        //    if (offset + LongSize > buffer.Length)
        //    {
        //        offset += LongSize;
        //        return;
        //    }

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(long*)(ptr + offset) = HostToNetworkOrder(value);
        //        offset += LongSize;
        //    }
        //}

        //public static unsafe void WriteFloat(float value, byte[] buffer, ref int offset)
        //{
        //    if (offset + FloatSize > buffer.Length)
        //    {
        //        offset += FloatSize;
        //        return;
        //    }

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(float*)(ptr + offset) = value;
        //        *(int*)(ptr + offset) = HostToNetworkOrder(*(int*)(ptr + offset));
        //        offset += FloatSize;
        //    }
        //}

        //public static unsafe void WriteDouble(double value, byte[] buffer, ref int offset)
        //{
        //    if (offset + DoubleSize > buffer.Length)
        //    {
        //        offset += DoubleSize;
        //        return;
        //    }

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(double*)(ptr + offset) = value;
        //        *(long*)(ptr + offset) = HostToNetworkOrder(*(long*)(ptr + offset));
        //        offset += DoubleSize;
        //    }
        //}

        //public static unsafe void WriteByte(byte value, byte[] buffer, ref int offset)
        //{
        //    if (offset + ByteSize > buffer.Length)
        //    {
        //        offset += ByteSize;
        //        return;
        //    }

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(ptr + offset) = value;
        //        offset += ByteSize;
        //    }
        //}

        //public static unsafe void WriteBytes(byte[] value, byte[] buffer, ref int offset)
        //{
        //    if (value == null)
        //    {
        //        WriteBool(false, buffer, ref offset);
        //        return;
        //    }
        //    WriteBool(true, buffer, ref offset);

        //    if (offset + value.Length + IntSize > buffer.Length)
        //    {
        //        offset += value.Length + IntSize;
        //        return;
        //    }
        //    WriteInt(value.Length, buffer, ref offset);
        //    System.Array.Copy(value, 0, buffer, offset, value.Length);
        //    offset += value.Length;
        //}

        //public static unsafe void WriteSByte(sbyte value, byte[] buffer, ref int offset)
        //{
        //    if (offset + SbyteSize > buffer.Length)
        //    {
        //        offset += SbyteSize;
        //        return;
        //    }

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(sbyte*)(ptr + offset) = value;
        //        offset += SbyteSize;
        //    }
        //}

        //public static unsafe void WriteString(string value, byte[] buffer, ref int offset)
        //{
        //    if (value == null)
        //    {
        //        WriteBool(false, buffer, ref offset);
        //        return;
        //    }

        //    WriteBool(true, buffer, ref offset);
        //    int len = System.Text.Encoding.UTF8.GetByteCount(value);
        //    //预判已经超出长度了，直接计算长度就行了
        //    if (offset + len + ShortSize > buffer.Length)
        //    {
        //        offset += len + ShortSize;
        //        return;
        //    }

        //    fixed (byte* ptr = buffer)
        //    {
        //        System.Text.Encoding.UTF8.GetBytes(value, 0, value.Length, buffer, offset + ShortSize);
        //        WriteShort((short)len, buffer, ref offset);
        //        offset += len;
        //    }
        //}

        //public static unsafe void WriteBool(bool value, byte[] buffer, ref int offset)
        //{
        //    if (offset + BoolSize > buffer.Length)
        //    {
        //        offset += BoolSize;
        //        return;
        //    }

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(bool*)(ptr + offset) = value;
        //        offset += BoolSize;
        //    }
        //}
        //#endregion

        //#region Read

        //public static unsafe int ReadInt(byte[] buffer, ref int offset)
        //{
        //    if (offset > buffer.Length + IntSize)
        //        throw new Exception("xbuffer read out of index");

        //    fixed (byte* ptr = buffer)
        //    {
        //        var value = *(int*)(ptr + offset);
        //        offset += IntSize;
        //        return NetworkToHostOrder(value);
        //    }
        //}

        //public static unsafe short ReadShort(byte[] buffer, ref int offset)
        //{
        //    if (offset > buffer.Length + ShortSize)
        //        throw new Exception("xbuffer read out of index");

        //    fixed (byte* ptr = buffer)
        //    {
        //        var value = *(short*)(ptr + offset);
        //        offset += ShortSize;
        //        return NetworkToHostOrder(value);
        //    }
        //}

        //public static unsafe long ReadLong(byte[] buffer, ref int offset)
        //{
        //    if (offset > buffer.Length + LongSize)
        //        throw new Exception("xbuffer read out of index");

        //    fixed (byte* ptr = buffer)
        //    {
        //        var value = *(long*)(ptr + offset);
        //        offset += LongSize;
        //        return NetworkToHostOrder(value);
        //    }
        //}

        //public static unsafe float ReadFloat(byte[] buffer, ref int offset)
        //{
        //    if (offset > buffer.Length + FloatSize)
        //        throw new Exception("xbuffer read out of index");

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(int*)(ptr + offset) = NetworkToHostOrder(*(int*)(ptr + offset));
        //        var value = *(float*)(ptr + offset);
        //        offset += FloatSize;
        //        return value;
        //    }
        //}

        //public static unsafe double ReadDouble(byte[] buffer, ref int offset)
        //{
        //    if (offset > buffer.Length + DoubleSize)
        //        throw new Exception("xbuffer read out of index");

        //    fixed (byte* ptr = buffer)
        //    {
        //        *(long*)(ptr + offset) = NetworkToHostOrder(*(long*)(ptr + offset));
        //        var value = *(double*)(ptr + offset);
        //        offset += DoubleSize;
        //        return value;
        //    }
        //}

        //public static unsafe byte ReadByte(byte[] buffer, ref int offset)
        //{
        //    if (offset > buffer.Length + ByteSize)
        //        throw new Exception("xbuffer read out of index");

        //    fixed (byte* ptr = buffer)
        //    {
        //        var value = *(ptr + offset);
        //        offset += ByteSize;
        //        return value;
        //    }
        //}

        //public static unsafe byte[] ReadBytes(byte[] buffer, ref int offset)
        //{
        //    var len = ReadInt(buffer, ref offset);
        //    //数据不可信
        //    if (len <= 0 || offset + len > buffer.Length)
        //        return new byte[0];

        //    var data = new byte[len];
        //    System.Array.Copy(buffer, offset, data, 0, len);
        //    offset += len;
        //    return data;
        //}

        //public static unsafe sbyte ReadSByte(byte[] buffer, ref int offset)
        //{
        //    if (offset > buffer.Length + ByteSize)
        //        throw new Exception("xbuffer read out of index");

        //    fixed (byte* ptr = buffer)
        //    {
        //        var value = *(sbyte*)(ptr + offset);
        //        offset += ByteSize;
        //        return value;
        //    }
        //}

        //public static unsafe string ReadString(byte[] buffer, ref int offset)
        //{
        //    fixed (byte* ptr = buffer)
        //    {
        //        var len = ReadShort(buffer, ref offset);
        //        //数据不可信
        //        if (len <= 0 || offset + len > buffer.Length)
        //            return "";

        //        var value = System.Text.Encoding.UTF8.GetString(buffer, offset, len);
        //        offset += len;
        //        return value;
        //    }
        //}

        //public static unsafe bool ReadBool(byte[] buffer, ref int offset)
        //{
        //    if (offset > buffer.Length + BoolSize)
        //        throw new Exception("xbuffer read out of index");

        //    fixed (byte* ptr = buffer)
        //    {
        //        var value = *(bool*)(ptr + offset);
        //        offset += BoolSize;
        //        return value;
        //    }
        //}
        //#endregion

    }
}