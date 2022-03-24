using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Serialize
{
    public abstract class Message : Serializable
    {
        /// <summary>
        /// 每次通信的唯一ID
        /// </summary>
        public virtual int UniId { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public virtual int MsgId { get; }
    }
}
