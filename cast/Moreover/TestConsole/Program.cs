using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace TestConsole
{
  class Program
  {
    static void Main(string[] args)
    {

      Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));

      var fileName = "xxx.jpg";

      var obj = new {Name = "test"};

      Console.WriteLine(string.Join(",", obj.GetType().GetProperties().Select(u => u.Name)));

      StringBuilder builder = new StringBuilder("tewasoptuaowiptu");

      Console.WriteLine($"empty {builder}");

      Console.WriteLine(null + "test");

      Console.ReadKey(true);

      Console.WriteLine("Hello World!");
    }
  }

  class Temp
  {
    public string Name { get; set; }

    public string Description;
  }
}