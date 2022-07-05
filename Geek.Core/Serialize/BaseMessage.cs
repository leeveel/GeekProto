using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geek.Server
{
    public abstract class BaseMessage : Serializable
    {
        /// <summary>
        /// 每次通信的唯一ID
        /// </summary>
        [IgnoreMember]
        public virtual int UniId { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        [IgnoreMember]
        public virtual int MsgId { get; }
    }
}
