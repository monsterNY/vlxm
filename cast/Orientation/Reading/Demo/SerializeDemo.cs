using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : SerializeDemo   序列化/反序列化
  /// @author : mons
  /// @create : 2019/6/19 9:42:14 
  /// @source : p542 clr via C# 
  /// </summary>
  public class SerializeDemo
  {
    public static void Run()
    {
      //创建对象图
      var objectGraph = new List<string>() {"Jeff", "Kristin", "Aidan", "Grant"};

      var stream = SerializeToMemory(objectGraph);

      //重置位置
      stream.Position = 0;
      //objectGraph = null;

      objectGraph = (List<string>) DeserializeFromStream(stream);

      foreach (var item in objectGraph)
      {
        Console.WriteLine(item);
      }
    }

    private static object DeserializeFromStream(MemoryStream stream)
    {
      //构造序列化格式化器
      BinaryFormatter formatter = new BinaryFormatter();

      return formatter.Deserialize(stream);
    }

    private static MemoryStream SerializeToMemory(List<string> objectGraph)
    {
      //构建流
      MemoryStream stream = new MemoryStream();

      //构造序列化格式化器
      BinaryFormatter formatter = new BinaryFormatter();

      //告诉格式化器将对象序列化到流中
      formatter.Serialize(stream, objectGraph);

      return stream;
    }

    //利用序列化深拷贝
    private static object DeepClone(object original)
    {
      using (MemoryStream stream = new MemoryStream())
      {
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Context = new StreamingContext(StreamingContextStates.Clone);

        formatter.Serialize(stream, original);

        stream.Position = 0;

        return formatter.Deserialize(stream);
      }
    }
  }
}