using System;
using System.Collections.Generic;
using System.Text;
using MySpringCore.CusInterface;

namespace MySpringCore.CusInherit
{
  public class Running : IPlay
  {
    public void Run()
    {
      Console.WriteLine($"I'm {nameof(Running)},that's funny");
    }
  }
}