using System;
using System.Collections.Generic;
using System.Text;

namespace Oop.Single
{
  public class BoyPlayBiz:PlayBiz
  {
    public BoyPlayBiz() : base("男孩子")
    {
    }

    public override void AfterRun()
    {
      Console.WriteLine("我是个男孩子，喝瓶水就开始了~");
    }

    public override object Choice()
    {
      //男孩子比较简单，手机游戏就足够了~
      return "玩手机游戏";
    }
  }
}
