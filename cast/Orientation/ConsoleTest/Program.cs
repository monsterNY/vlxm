using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ConsoleTest.Entity;
using ConsoleTest.LeetCode;
using Tools.RefTools;

namespace ConsoleTest
{
  class Program
  {

    public static dynamic Build<T>(T instance, string[] properties)
    {

      dynamic obj = new System.Dynamic.ExpandoObject();

      var type = typeof(T);

      foreach (var prop in properties)
      {
        var propertyInfo = type.GetProperty(prop);

        if (propertyInfo != null)
        {
          ((IDictionary<string, object>)obj).Add(propertyInfo.Name, propertyInfo.GetValue(instance));
        }

      }

      return obj;

    }

    static void Main(string[] args)
    {

      CodeTimer timer = new CodeTimer();

      ConvertANumberToHexadecimal instance = new ConvertANumberToHexadecimal();

      Console.WriteLine(instance.ToHex(26));

      Console.WriteLine(instance.ToHex(-1));

      var rand = new Random();

      var successCount = 0;

      for (int i = 0; i < 1000; i++)
      {
        var num = rand.Next(int.MaxValue);

        num *= rand.Next(2) == 0 ? 1 : -1;

        Console.WriteLine($"\n\n传入num:{num}");

        var result = timer.Time(1, (() =>
        {

          Console.WriteLine(instance.ToHex(num));

        }));

        Console.WriteLine($"耗费时间：{result}");

        var result2 = timer.Time(1, (() =>
        {

          Console.WriteLine($"正确答案：{Convert.ToString(num, 16)}");

        }));

        Console.WriteLine($"帮助类-耗费时间：{result}");

        if (result.TimeElapsed <= result2.TimeElapsed)
        {
          successCount++;
        }

      }

      Console.WriteLine($"成功次数：{successCount}");

//      Console.WriteLine(0xffff);
//      Console.WriteLine(int.MaxValue);
//
//      Console.WriteLine(Convert.ToString(0, 16));
//      Console.WriteLine(Convert.ToString(1, 16));
//      Console.WriteLine(Convert.ToString(-1, 16));
//      Console.WriteLine(Convert.ToString(-2, 16));
//      Console.WriteLine(Convert.ToString(-3, 16));
//      Console.WriteLine(Convert.ToString(0xffffffff, 16));

      //      var stu = new Student()
      //      {
      //        Age = 18,
      //        ClassId = 1,
      //        Name = "xxx"
      //      };
      //
      //      var result = Build(stu, new[] {nameof(Student.Age), nameof(Student.Name)});
      //
      //      Console.WriteLine(result.Age);
      //      Console.WriteLine(result.Name);

      Console.WriteLine("Hello World!");

      Console.ReadKey(true);
    }

    private static void RomanToIntegerTest(CodeTimer timer)
    {
      var instance = new RomanToInteger();

      //success
      //      Console.WriteLine(instance.RomanToInt("III"));
      //      Console.WriteLine(instance.RomanToInt("IV"));
      //      Console.WriteLine(instance.RomanToInt("IX"));
      //      Console.WriteLine(instance.RomanToInt("LVIII"));
      //      Console.WriteLine(instance.RomanToInt("MCMXCIV"));
      //
      //      Console.WriteLine(instance.RomanToInt("CXCXL"));
      //
      //      Console.ReadKey(true);

      var rand = new Random();
      var charArr = new[]
      {
        'I',
        'V',
        'X',
        'L',
        'C',
        'D',
        'M'
      };

      var randCount = 10;
      var fixCount = 4;
      for (int i = 0; i < 10; i++)
      {

        var len = rand.Next(randCount) + fixCount;

        var arr = new char[len];

        for (int j = 0; j < len; j++)
        {
          arr[j] = charArr[rand.Next(charArr.Length)];
        }

        var str = new string(arr);

        Console.WriteLine($"\n随机生成字符串：{str}\n");

        var result = timer.Time(1, (() => { Console.WriteLine($"[RomanToInt]计算结果：{instance.RomanToInt(str)}"); }));

        Console.WriteLine(result);

        Console.WriteLine("--------------------------");

        result = timer.Time(1, (() => { Console.WriteLine($"[Optimize]计算结果：{instance.Optimize(str)}"); }));

        Console.WriteLine(result);

        Console.WriteLine("--------------------------");

      }
    }

    private static void GroupCount()
    {
      var testCount = 10;

      var arr = new List<dynamic>();

      var rand = new Random();

      for (int i = 0; i < 1000000; i++)
      {
        arr.Add(new { sex = rand.Next(2), age = rand.Next(100) });
      }

      var timer = new CodeTimer();

      var result = timer.Time(testCount, (() =>
      {
        //        Console.WriteLine("-------------------------linq-----------------------");
        var enumerable = arr.GroupBy(u => u.sex).Select(u => new { u.Key, count = u.Count() });
        foreach (var item in enumerable)
        {
          //          Console.WriteLine($"性别：{(item.Key == 0 ? "女" : "男")}，数量：{item.count}");
        }

        //
        //        Console.WriteLine();
      }));

      Console.WriteLine("-------------------------linq-----------------------"); //效率最低。。。
      Console.WriteLine(result);

      result = timer.Time(testCount, (() =>
      {
        var countArr = new int[2];

        foreach (var item in arr)
        {
          countArr[item.sex == 0 ? 0 : 1]++;
        }

        foreach (var item in countArr)
        {
        }

        //        Console.WriteLine("-------------------------arr-----------------------");
        //        Console.WriteLine($"性别男，数量：{countArr[1]}");
        //        Console.WriteLine($"性别女，数量：{countArr[0]}");
        //        Console.WriteLine();
      }));

      Console.WriteLine("-------------------------arr-----------------------"); //arr效率最高
      Console.WriteLine(result);

      result = timer.Time(testCount, (() =>
      {
        //        Console.WriteLine("-------------------------dictionary-----------------------");
        var countDic = new Dictionary<int, int>();

        foreach (var item in arr)
        {
          if (countDic.ContainsKey(item.sex))
          {
            countDic[item.sex]++;
          }
          else
          {
            countDic.Add(item.sex, 1);
          }
        }

        foreach (var item in countDic)
        {
          //          Console.WriteLine($"性别：{(item.Key == 0 ? "女" : "男")}，数量：{item.Value}");
        }

        //
        //        Console.WriteLine();
      }));

      Console.WriteLine("-------------------------dictionary-----------------------"); //字典效率第二
      Console.WriteLine(result);
    }
  }
}