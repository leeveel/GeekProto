using System;

namespace RocketMQ.Client
{
    public class XBuffer
    {
        public static readonly int intSize = sizeof(int);
        public static readonly int shortSize = sizeof(short);
        public static readonly int longSize = sizeof(long);
        public static readonly int floatSize = sizeof(float);
        public static readonly int doubleSize = sizeof(double);
        public static readonly int byteSize = sizeof(byte);
        public static readonly int sbyteSize = sizeof(sbyte);
        public static readonly int boolSize = sizeof(bool);

        #region Write
        public static unsafe void WriteInt(int value, byte[] buffer, ref int offset, bool littleEndian=false)
        {
            if (offset + intSize > buffer.Length)
            {
                offset += intSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                if(littleEndian)
                    *(int*)(ptr + offset) = value;
                else
                    *(int*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += intSize;
            }
        }

        public static unsafe void WriteShort(short value, byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (offset + shortSize > buffer.Length)
            {
                offset += shortSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                if (littleEndian)
                    *(short*)(ptr + offset) = value;
                else
                    *(short*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += shortSize;
            }
        }

        public static unsafe void WriteLong(long value, byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (offset + longSize > buffer.Length)
            {
                offset += longSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                if (littleEndian)
                    *(long*)(ptr + offset) = value;
                else
                    *(long*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += longSize;
            }
        }

        public static unsafe void WriteFloat(float value, byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (offset + floatSize > buffer.Length)
            {
                offset += floatSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(float*)(ptr + offset) = value;
                if(!littleEndian)
                    *(int*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(*(int*)(ptr + offset));
                offset += floatSize;
            }
        }

        public static unsafe void WriteDouble(double value, byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (offset + doubleSize > buffer.Length)
            {
                offset += doubleSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(double*)(ptr + offset) = value;
                if(!littleEndian)
                    *(long*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(*(long*)(ptr + offset));
                offset += doubleSize;
            }
        }

        public static unsafe void WriteByte(byte value, byte[] buffer, ref int offset)
        {
            if (offset + byteSize > buffer.Length)
            {
                offset += byteSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(ptr + offset) = value;
                offset += byteSize;
            }
        }

        public static unsafe void WriteBytes(byte[] value, byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (value == null)
            {
                WriteInt(0, buffer, ref offset, littleEndian);
                return;
            }

            if (offset + value.Length + intSize > buffer.Length)
            {
                offset += value.Length + intSize;
                return;
            }

            WriteInt(value.Length, buffer, ref offset, littleEndian);
            System.Array.Copy(value, 0, buffer, offset, value.Length);
            offset += value.Length;
        }

        public static unsafe void WriteRawBytes(byte[] value, byte[] buffer, ref int offset)
        {
            if (value == null)
                return;

            if (offset + value.Length > buffer.Length)
            {
                offset += value.Length;
                return;
            }
            Array.Copy(value, 0, buffer, offset, value.Length);
            offset += value.Length;
        }

        public static unsafe void WriteRawBytes(byte[] value, int len, byte[] buffer, ref int offset)
        {
            if (value == null)
                return;

            if (offset + len > buffer.Length)
            {
                offset += len;
                return;
            }
            Array.Copy(value, 0, buffer, offset, len);
            offset += len;
        }

        public static unsafe void WriteSByte(sbyte value, byte[] buffer, ref int offset)
        {
            if (offset + sbyteSize > buffer.Length)
            {
                offset += sbyteSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(sbyte*)(ptr + offset) = value;
                offset += sbyteSize;
            }
        }

        public static unsafe void WriteString(string value, byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (value == null)
                value = "";

            int strLen = System.Text.Encoding.UTF8.GetByteCount(value);
            if (offset + strLen + intSize > buffer.Length)
            {
                offset += strLen + intSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                System.Text.Encoding.UTF8.GetBytes(value, 0, value.Length, buffer, offset + intSize);
                WriteInt(strLen, buffer, ref offset, littleEndian);
                offset += strLen;
            }
        }

        public static unsafe void WriteBool(bool value, byte[] buffer, ref int offset)
        {
            if (offset + boolSize > buffer.Length)
            {
                offset += boolSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(bool*)(ptr + offset) = value;
                offset += boolSize;
            }
        }
        #endregion

        #region Read

        public static unsafe int ReadInt(byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (offset > buffer.Length + intSize || buffer.Length < intSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{offset},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(int*)(ptr + offset);
                offset += intSize;
                if (littleEndian)
                    return value;
                else
                    return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }

        public static unsafe int GetInt(byte[] buffer, int postion, bool littleEndian = false)
        {
            if (postion > buffer.Length + intSize || buffer.Length < intSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{postion},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(int*)(ptr + postion);
                //postion += intSize;
                if (littleEndian)
                    return value;
                else
                    return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }

        public static unsafe short ReadShort(byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (offset > buffer.Length + shortSize || buffer.Length < shortSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{offset},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(short*)(ptr + offset);
                offset += shortSize;
                if (littleEndian)
                    return value;
                else
                    return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }

        public static unsafe short GetShort(byte[] buffer, int position, bool littleEndian = false)
        {
            if (position > buffer.Length + shortSize || buffer.Length < shortSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{position},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(short*)(ptr + position);
                //position += shortSize;
                if (littleEndian)
                    return value;
                else
                    return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }

        public static unsafe long ReadLong(byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (offset > buffer.Length + longSize || buffer.Length < longSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{offset},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(long*)(ptr + offset);
                offset += longSize;
                if (littleEndian)
                    return value;
                else
                    return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }

        public static unsafe long GetLong(byte[] buffer, int position, bool littleEndian = false)
        {
            if (position > buffer.Length + longSize || buffer.Length < longSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{position},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(long*)(ptr + position);
                //position += longSize;
                if (littleEndian)
                    return value;
                else
                    return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }

        public static unsafe float ReadFloat(byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (offset > buffer.Length + floatSize || buffer.Length < floatSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{offset},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                if(!littleEndian)
                    *(int*)(ptr + offset) = System.Net.IPAddress.NetworkToHostOrder(*(int*)(ptr + offset));
                var value = *(float*)(ptr + offset);
                offset += floatSize;
                return value;
            }
        }

        public static unsafe double ReadDouble(byte[] buffer, ref int offset, bool littleEndian = false)
        {
            if (offset > buffer.Length + doubleSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{offset},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                if(!littleEndian)
                    *(long*)(ptr + offset) = System.Net.IPAddress.NetworkToHostOrder(*(long*)(ptr + offset));
                var value = *(double*)(ptr + offset);
                offset += doubleSize;
                return value;
            }
        }

        public static unsafe byte ReadByte(byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + byteSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{offset},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(ptr + offset);
                offset += byteSize;
                return value;
            }
        }

        public static unsafe byte GetByte(byte[] buffer, int position)
        {
            if (position > buffer.Length + byteSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{position},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(ptr + position);
                //position += byteSize;
                return value;
            }
        }


        public static unsafe byte[] ReadBytes(byte[] buffer, ref int offset, bool littleEndian = false)
        {
            var len = ReadInt(buffer, ref offset, littleEndian);
            //数据不可信
            if (len <= 0 || offset > buffer.Length + len * byteSize)
                return Array.Empty<byte>();

            var data = new byte[len];
            System.Array.Copy(buffer, offset, data, 0, len);
            offset += len;
            return data;
        }

        public static unsafe byte[] ReadRawBytes(byte[] buffer, int len, ref int offset)
        {
            //var len = ReadInt(buffer, ref offset, littleEndian);
            if (len <= 0 || offset > buffer.Length + len * byteSize)
                return Array.Empty<byte>();

            var data = new byte[len];
            System.Array.Copy(buffer, offset, data, 0, len);
            offset += len;
            return data;
        }

        public static unsafe void ReadRawBytes(byte[] buffer, byte[] dst, int dstIndex, int len, ref int offset)
        {
            if (dst == null)
                return; 
            if (len <= 0 || offset > buffer.Length + len * byteSize)
                return;
            Array.Copy(buffer, offset, dst, dstIndex, len);
            offset += len;
        }

        public static unsafe sbyte ReadSByte(byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + byteSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{offset},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(sbyte*)(ptr + offset);
                offset += byteSize;
                return value;
            }
        }

        public static unsafe string ReadString(byte[] buffer, ref int offset, bool littleEndian = false)
        {
            fixed (byte* ptr = buffer)
            {
                var len = ReadInt(buffer, ref offset, littleEndian);
                //数据不可信
                if (len <= 0 || offset > buffer.Length + len * byteSize)
                    return "";

                var value = System.Text.Encoding.UTF8.GetString(buffer, offset, len);
                offset += len;
                return value;
            }
        }

        public static unsafe bool ReadBool(byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + boolSize)
                throw new ArgumentException($"xbuffer read out of index.offset:{offset},buffer.Length:{buffer.Length}");

            fixed (byte* ptr = buffer)
            {
                var value = *(bool*)(ptr + offset);
                offset += boolSize;
                return value;
            }
        }
        #endregion

    }
}