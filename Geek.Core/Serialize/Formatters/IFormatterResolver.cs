namespace Geek.Server
{
    public interface IFormatterResolver
    {
        ISerializeFormatter<T> GetFormatter<T>();
    }
}
