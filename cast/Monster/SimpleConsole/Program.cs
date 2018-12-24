using System;
using System.IO;
using System.Text;
using API.DoMain.Command;
using Newtonsoft.Json;

namespace SimpleConsole
{
  class Program
  {
    static void Main(string[] args)
    {

      var strE =
        "{\"MaxSize\":5,\"SaveUrl\":\"/course/img\",\"Key\":\"url\",\"ReduceParams\":[{\"Key\":\"upperReduce\",\"SaveUrl\":\"/course/upperReduce\",\"Width\":300,\"Height\":100,\"ReduceMode\":2},{\"Key\":\"lowerReduce\",\"SaveUrl\":\"/course/lowerReduce\",\"Width\":160,\"Height\":100,\"ReduceMode\":2}]}";

      Console.WriteLine(strE.Replace("\"","'"));

      var empty =
        "{'MaxSize':5,'SaveUrl':'/course/img','Key':'url','ReduceParams':[{'Key':'upperReduce','SaveUrl':'/course/upperReduce','Width':300,'Height':100,'ReduceMode':2},{'Key':'lowerReduce','SaveUrl':'/course/lowerReduce','Width':160,'Height':100,'ReduceMode':2}]}";

      var deserializeObject = JsonConvert.DeserializeObject<UploadParam>(empty);


      var reduceParams =  new ReduceParam[]
      {
        new ReduceParam()
        {
          Height = 100,
          ReduceMode = 2,
          SaveUrl = "/course/upperReduce",
          Width = 300,
          Key = "upperReduce"
        },
        new ReduceParam()
        {
          Height = 100,
          ReduceMode = 2,
          SaveUrl = "/course/lowerReduce",
          Width = 160,
          Key = "lowerReduce"
        }
      };

      var info = new UploadParam()
      {
        MaxSize = 5,
        ReduceParams = reduceParams,
        SaveUrl = "/course/img",
        Key = "url"
      };

      Console.WriteLine(JsonConvert.SerializeObject(info));

      Console.ReadKey(true);

      Console.WriteLine("Hello World!");

      var str = " Normal Day";

      byte[] bytes = Encoding.UTF8.GetBytes(str);

      Console.WriteLine(JsonConvert.SerializeObject(bytes));

      MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);

      var array = ms.ToArray();
      Console.WriteLine(JsonConvert.SerializeObject(array));

      ms.Write(bytes, 0, bytes.Length);

      var array1 = ms.ToArray();
      Console.WriteLine(JsonConvert.SerializeObject(array1));

      using (ms)
      {
      }

      var bytes1 = Encoding.UTF8.GetBytes("Empty");

      ms.Write(bytes1, 0, bytes1.Length);

      Console.ReadKey(true);
    }

  }
}