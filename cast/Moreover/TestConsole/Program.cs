using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Command.RedisHelper.CusInhert;
using Command.RedisHelper.Helper;
using Dapper;
using Newtonsoft.Json;

namespace TestConsole
{
  class Program
  {
    static void Main(string[] args)
    {


      CusRedisHelper helper = new CusRedisHelper("localhost:6379","vlxm",new NewtonsoftDeal(),71);

      var stringSet = helper.StringSet("monster", "I come back!");

      if (stringSet)
      {
        Console.WriteLine("添加值成功！");
      }

      var stringGet = helper.StringGet("monster");

      Console.WriteLine(stringGet);

      Console.ReadKey(true);

      var arr = new []{"123", "a==3"};

      Console.WriteLine(string.Join("\nAND",arr));

      var temp = new Temp()
      {
        Description = Guid.NewGuid().ToString(),
        Id = 3,
        Name = "---",
      };

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

    public int Id { get; set; }

    public bool Is
    {
      get => Id > 0;
      set { }
    }

  }
}