using System;
using System.Collections.Generic;
using System.Text;

namespace Command.RedisHelper.Models
{

    [Serializable]
    public class ChatModel
    {

        /// <summary>
        /// 用户标识
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 聊天内容
        /// </summary>
        public string chat { get; set; }


    }
}
