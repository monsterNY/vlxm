using System;
using Newtonsoft.Json;

namespace Api.Manage.Assist.Middle
{
  /// <summary>
  /// 进行分隔输出
  /// </summary>
  public class SplitConvert : JsonConverter
  {
    /// <summary>
    /// 分隔符
    /// </summary>
    public char SplitChar { get; set; }

    public SplitConvert()
    {
      SplitChar = ',';
    }

    public SplitConvert(char splitChar = ',')
    {
      SplitChar = splitChar;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="url">路径</param>
    public SplitConvert(string url)
    {
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      var url = value?.ToString();

      if (string.IsNullOrWhiteSpace(url))
      {
        writer.WriteNull();
      }
      else
      {
        writer.WriteStartArray();
        foreach (var str in url.Split(SplitChar))
          if (!string.IsNullOrWhiteSpace(str))
            writer.WriteValue(str);

        writer.WriteEndArray();
      }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      return reader.Value;
    }

    public override bool CanConvert(Type objectType)
    {
      return true;
    }
  }
}