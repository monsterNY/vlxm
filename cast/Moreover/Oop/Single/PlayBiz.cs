using System;
using System.Collections.Generic;
using System.Text;
using Oop.CusInterface;

namespace Oop.Single
{
  /// <summary>
  /// 开闭原则
  /// </summary>
  public abstract class PlayBiz : IPlayBiz
  {
    public object Player { get; set; }

    /// <summary>
    /// 有玩家才能玩
    /// </summary>
    /// <param name="player"></param>
    protected PlayBiz(object player)
    {
      Player = player;
    }


    public void Run()
    {
      //1.I'm simple human
      //Console.WriteLine("that play is play");

      //2.玩了一段时间，发现其他人有了其他的玩法,感觉自己这么简单玩太单调了

      var game = Choice();

      //3.没力气怎么玩
      //Console.WriteLine("吃零食两小时");

      //4.感觉吃零食不好，想要吃点其他有营养的东西
      //Console.WriteLine("吃水果和保健品");

      //5.吃还不够，玩之前还有进行准备活动或者其他
      AfterRun();

      //不管之前进行了什么，最终肯定还是要玩的，这点不可变
      Console.WriteLine($"开始玩 '{game}'");
    }

    /// <summary>
    /// 果然每个人玩前的活动都不一样:补充体力、热身运动等等
    /// </summary>
    public abstract void AfterRun();

    /// <summary>
    /// 想要先选择后再开始玩
    /// </summary>
    /// <returns></returns>
    public abstract object Choice();
  }
}