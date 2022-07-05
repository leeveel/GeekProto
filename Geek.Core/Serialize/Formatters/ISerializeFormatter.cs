using System;

namespace Geek.Server
{
    public interface ISerializeFormatter
    {
    }

    public interface ISerializeFormatter<T> : ISerializeFormatter
    {
        void Serialize(Span<byte> buffer, T value, ref int offset);

        T Deserialize(Span<byte> buffer, ref int offset);

        int GetSerializeLength(T value);
    }

}
