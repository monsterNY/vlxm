using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : DistinctSubseqII  
  /// @author : mons
  /// @create : 2019/7/9 10:48:12 
  /// @source : 
  /// </summary>
  [Obsolete]
  public class DistinctSubseqII
  {
    public int Simple(string S)
    {
      ISet<string> set = new HashSet<string>();

      for (int i = 0; i < S.Length; i++)
      {
        StringBuilder builder = new StringBuilder();

        builder.Append(S[i]);

        Helper(builder, S, i, set);
      }

      Console.WriteLine(JsonConvert.SerializeObject(set));

      return set.Count;
    }

    public void Helper(StringBuilder builder, string str, int i, ISet<string> set)
    {
      set.Add(builder.ToString());

      if (i == 0) return;

      builder.Append(str[i]);
      Helper(builder, str, i - 1, set);

      builder.Remove(builder.Length - 1, 1);
      Helper(builder, str, i - 1, set);
    }
  }
}