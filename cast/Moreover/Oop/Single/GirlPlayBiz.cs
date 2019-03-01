using System;
using System.Collections.Generic;
using System.Text;

namespace Oop.Single
{
  public class GirlPlayBiz:PlayBiz
  {
    public GirlPlayBiz() : base("女孩子")
    {

    }

    public override void AfterRun()
    {
      Console.WriteLine("做好安全防护准备！");
    }

    public override object Choice()
    {
      //女孩子可要好好挑选
      var gameArr = new string[]
      {
        "扮家家",
        "捉迷藏",
        "跳绳",
        "追剧"
      };

      return gameArr[new Random().Next(gameArr.Length)];

    }
  }
}
