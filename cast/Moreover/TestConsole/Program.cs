﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using AutoMapper;
using Dapper;
using DapperContext.Middleware;
using Newtonsoft.Json;
using TestConsole.Dto;
using TestConsole.Entities;
using TestConsole.Extension;
using TestConsole.Model;
using TestConsole.Tools;

namespace TestConsole
{
  class Program
  {
    #region empty

    public void GetValue(ref int i)
    {
      i = 999;
    }

    public static Dictionary<string, object> MergeDictionary(string prevKey, object value,
      Dictionary<string, object> target)
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

    public static void Sort(IList<int> arr)
    {
      int temp;
      bool swapped;


      for (int i = 0; i < arr.Count; i++)
      {
        swapped = false;
        for (int j = 1; j < arr.Count - i; j++)
        {
          if (arr[j - 1] > arr[j])
          {
            temp = arr[j];
            arr[j - 1] = arr[j];
            arr[j] = temp;
            if (!swapped)
              swapped = true;
          }
        }

        if (!swapped) return;
      }
    }

    #endregion


    static Program()
    {
      Mapper.Initialize((config => { config.CreateMap<TenderModel, TenderDto>(); }));
    }

    private static DateTime Time { get; set; }

    static void Main(string[] args)
    {
      try
      {
        var maps = Mapper.Configuration.GetAllTypeMaps();
        Mapper.Initialize(config =>
        {
//          var typeMapper = typeof(Mapper).GetTypeInfo();
//          var configuration = typeMapper.GetDeclaredField("_configuration");
//          configuration.SetValue(null, null, BindingFlags.Static, null, CultureInfo.CurrentCulture);
        });
      }
      catch (InvalidOperationException e)
      {
        Console.WriteLine(e);
      }

      var user = new User()
      {
        LoginPwd = Guid.NewGuid().ToString(),
        UserName = "xxx"
      };

      Console.WriteLine(Time);

      Console.WriteLine(Time == default(DateTime));

      SimpleFactory<string> instance = SimpleFactory<string>.GetInstance();
      var objectInstance = SimpleFactory<object>.GetInstance();

      dynamic info = SimpleFactory<string>.GetInstance();

      Dapper.SqlMapper.SetTypeMap(typeof(TenderModel), new TableAttributeTypeMapper<TenderModel>());

      using (var conn =
        new SqlConnection("Data Source=192.168.1.13;Initial Catalog=amao100_DB;User ID=yj;Password=123?abc"))
      {
        var list = conn.Query<TenderModel>("SELECT * FROM TenderTable");

        Console.WriteLine(JsonConvert.SerializeObject(list));

        var dtoList = list.MapToList<TenderDto>();

        var mapToList = list.MapToList<object>();

        Console.WriteLine("dto:");
        Console.WriteLine(JsonConvert.SerializeObject(dtoList));

        //        var result = conn.Insert<TenderModel>(new TenderModel()
        //        {
        //          Amount = 88,
        //          CaseIntro = "test",
        //          CompanyId = 1,
        //          TenderIntro = "test",
        //          FinishTime = 10,
        //          ValuationId = 1
        //        });
        //
        //        Console.WriteLine($"添加结果：{result}");
      }


      //var arr = new List<int>() {1, 2, 3, 4, 5, 6};

      //var list = arr.SelectMany((i => { return new[] {i - 1, i, i + 1, i * i}; }));

      //Console.WriteLine(JsonConvert.SerializeObject(list));

      //      var instance = new Program();
      //
      //      var rand = new Random();
      //
      //      var flag = true;
      //
      //      ThreadPool.QueueUserWorkItem(u =>
      //      {
      //        while (flag)
      //        {
      //          Console.WriteLine(rand.Next(9999));
      //        }
      //      });
      //
      ////      Console.BackgroundColor;
      //      Console.WriteLine("按任意键关闭....");
      //      Console.ReadKey(true);
      //      flag = false;
      //
      //      Console.WriteLine("close");

      //
      //      for (int i = 0; i < 10; i++)
      //      {
      //        new OperationA().Run();
      //
      //        new OperationB().Run();
      //
      //        Console.WriteLine("---------------------------------------");
      //
      //      }

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

      Console.WriteLine("Hello World!");

      Console.ReadKey(true);
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