using System;
using System.Collections.Generic;
using System.Linq;

namespace DbConsole
{
  class Program
  {
    static void Main(string[] args)
    {

      dynamic num = "test";

      if (!(num is int iNum))
      {
        Console.WriteLine("no int");
      }
      else
      {
        Console.WriteLine(iNum);
      }

      int[] arr = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9,};

      var list = new List<int>();

      for (int i = 0; i < 1000; i++)
      {
        list.Add(i);
      }

      var readData = ReadData();

      Console.WriteLine("-----------");

      var ints = readData;
      
      Console.ReadKey(true);

    }

    public static IEnumerable<int> ReadData()
    {
      for (int i = 0; i < 10; i++)
      {
        Console.WriteLine($"return {i}");
        yield return i;//参破延时加载...
      }
      yield break;
    }
  }
}