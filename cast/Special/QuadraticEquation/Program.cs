using System;

namespace QuadraticEquation
{
  class Program
  {
    static void Main(string[] args)
    {

      //******解方程

      //---------一元一次

      /**
       * 123456
       *
       * => A 进入
       * => B 移出
       *
       * 1 => A 1
       * 2 => A 21
       * 3 => A 321
       * 3 => B 3 21 
       * 2 => B 32 1
       * 4 => A 32 41
       * 5 => A 32 541
       * 5 => B 325 41
       * 6 => A 325 641
       *
       * 325641
       */

      var num = -2;

      Console.WriteLine((uint)num);

      Console.WriteLine("Hello World!");

      Console.ReadKey(true);

    }
  }
}
