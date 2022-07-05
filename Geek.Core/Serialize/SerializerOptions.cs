namespace Geek.Server
{
    public class SerializerOptions
    {

        public static IFormatterResolver Resolver { get; set; }

        /// <summary>
        /// 压缩阈值
        /// </summary>
        public static int CompressThreshold { get; set; } = 1024;

    }
}
