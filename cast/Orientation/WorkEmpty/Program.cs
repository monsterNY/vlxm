using System;
using System.Linq;
using Tools.CusTools;

namespace WorkEmpty
{
  class Program
  {

    static void Main(string[] args)
    {
      EditParam param = new EditName();

      foreach (var item in param.GetType().GetProperties())
      {
        Console.WriteLine(item.Name);
      }

      Console.ReadKey(true);
    }

    public class EditParam
    {
      public int Id { get; set; }

      public string User { get; set; }
    }

    public class EditName : EditParam
    {
      public string Name { get; set; }
    }

    private static void FirstWordUpper()
    {
      #region first code upper

      //      Console.WriteLine((int) 'a');65
      //      Console.WriteLine((int) 'A');97

      var txt = @"
type = (int) reader[""ar_Type""],
              code = reader[""ar_Code""].ToString(),
              value = reader[""ar_Value""].ToString()
";

      var lines = txt.Split("\n");

      for (int i = 0; i < lines.Length; i++)
      {
        var line = lines[i].Trim();

        var words = line.Split(" ");

        words[0] = words[0].HeadUpper();

        lines[i] = string.Join(" ", words);
      }

      Console.WriteLine(string.Join("\n", lines));

      #endregion
    }
  }
}