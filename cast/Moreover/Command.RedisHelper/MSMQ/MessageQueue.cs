using System;
using System.Collections.Generic;
using System.Text;
using Command.RedisHelper.Helper;
using Command.RedisHelper.Models;

namespace Command.RedisHelper.MSMQ
{
    /// <summary>
    /// 博文》测试案例_study
    /// </summary>
    public class MessageQueue
    {
        //用于更新消息的定时器
        static System.Timers.Timer timer = new System.Timers.Timer(1000);
        public static ChatModel CurrentChatModels;//当前消息，由↑可知 消息信息缓存1s
        static MessageQueue()
        {
            timer.AutoReset = true;//自动开启
            timer.Enabled = true;//有效
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);//更新事件
            timer.Start();//开启定时器
        }

        //消息出列
        private static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
//            var redisClient = new SiteRedisHelper(2);//获取连接
            // 消息出列
//            CurrentChatModels = redisClient.ListLeftPop<ChatModel>("MessageQuene");
        }
    }
}
