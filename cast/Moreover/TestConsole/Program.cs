using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command.RedisHelper.CusInhert;
using Command.RedisHelper.Helper;
using Dapper;
using DapperContext;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using TestConsole.Entities;
using TestConsole.Lock;
using TestConsole.TransactionOperation;

namespace TestConsole
{
  class Program
  {

    public static Dictionary<string, object> MergeDictionary(string prevKey, object value, Dictionary<string, object> target)
    {
      var dotIndex = prevKey.IndexOf('.');

      if (dotIndex >= 0)
      {
        // 当前key
        var key = prevKey.Substring(0, dotIndex);

        // 下层key
        var nextKey = prevKey.Substring(dotIndex + 1);
        // 下层目标
        var nextTarget = new Dictionary<string, object>();

        if (target.ContainsKey(key))
        {
          if (target[key] is Dictionary<string, object>)
          {
            nextTarget = target[key] as Dictionary<string, object>;
          }
        }

        var result = MergeDictionary(nextKey, value, nextTarget);
        target[key] = result;
      }
      else
      {
        target[prevKey] = value;
      }

      return target;
    }

    static void Main(string[] args)
    {

      for (int i = 0; i < 10; i++)
      {
        new OperationA().Run();

        new OperationB().Run();

        Console.WriteLine("---------------------------------------");

      }

//      new Demo().Test();

//      var str = Convert.ToString(null);
//
//      Console.WriteLine(Convert.ToString(null));

//      var user = new User()
//      {
//        LoginPwd = "54656465",
//        UserName = "                  "
//      };
//
//      var context = new ValidationContext(user, null, null);
////      var results = new List<ValidationResult>();
////      var attributes = typeof(User)
////        .GetProperties()
////        .GetCustomAttributes(false)
////        .OfType<ValidationAttribute>()
////        .ToArray();
//
//      ICollection<ValidationResult> failure = new List<ValidationResult>();
//
//      var validSuccess = Validator.TryValidateObject(user, context, failure,true);
//
//      if (!validSuccess)
//      {
//        foreach (var item in failure)
//        {
//          Console.WriteLine(item.ErrorMessage);
//        }
//      }
//      else
//      {
//        Console.WriteLine("valid success");
//      }

//      if (!Validator.TryValidateValue(user, context, results, attributes))
//      {
//        foreach (var result in results)
//        {
//          Console.WriteLine(result.ErrorMessage);
//        }
//      }
//      else
//      {
//        Console.WriteLine("{0} is valid", user);
//      }

      //      var map = new Dictionary<string,object>();
      //
      //      var data = JsonConvert.DeserializeObject("[{\"id\":123}]");
      //
      //      Console.WriteLine(JsonConvert.SerializeObject(map));

      //      IDbConnection conn = new MySqlConnection("Server=localhost;Database=vlxm; User=root;Password=root;charset=utf8mb4;");
      //
      //      var list = conn.Query<UserInfo>("SELECT * FROM user_info");
      //
      //      Console.WriteLine(JsonConvert.SerializeObject(list));

      //      CusRedisHelper helper = new CusRedisHelper("localhost:6379","vlxm",new NewtonsoftDeal(),71);
      //
      //      var stringSet = helper.StringSet("monster", "I come back!");
      //
      //      if (stringSet)
      //      {
      //        Console.WriteLine("添加值成功！");
      //      }
      //
      //      var stringGet = helper.StringGet("monster");
      //
      //      Console.WriteLine(stringGet);
      //
      //      Console.ReadKey(true);
      //
      //      var arr = new []{"123", "a==3"};
      //
      //      Console.WriteLine(string.Join("\nAND",arr));
      //
      //      var temp = new Temp()
      //      {
      //        Description = Guid.NewGuid().ToString(),
      //        Id = 3,
      //        Name = "---",
      //      };
      //
      //      Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
      //
      //      var fileName = "xxx.jpg";
      //
      //      var obj = new {Name = "test"};
      //
      //      Console.WriteLine(string.Join(",", obj.GetType().GetProperties().Select(u => u.Name)));
      //
      //      StringBuilder builder = new StringBuilder("tewasoptuaowiptu");
      //
      //      Console.WriteLine($"empty {builder}");
      //
      //      Console.WriteLine(null + "test");

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