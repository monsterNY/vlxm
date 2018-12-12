using System;
using System.Linq;
using Container;
using Context;
using Dal;
using Factory;
using Model;
using MySpringCore.AOP;
using Service;

namespace MySpringCore
{
  class Program
  {
    /// <summary>
    /// UI 入口  /  用户交互处
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
      var aopCore = new AopCore();

      Action beforeAction = () => { Console.WriteLine("日志记录 START"); };

      Action afterAction = () => { Console.WriteLine("日志记录 STOP"); };

      aopCore.ForExample(beforeAction, afterAction);

      Console.WriteLine("---------------------------");

      aopCore.ForExampleByExpression(beforeAction, afterAction, (() =>
      {

        var num = 0;

        Console.WriteLine(100 / num);

        Console.WriteLine("执行成功");

      }));

      Console.ReadKey(true);
    }
  }
}