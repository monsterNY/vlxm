using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCode.Tools
{
  public class GenerateHelper
  {
    public string GetTableName(string name) //获取表名
    {
      return Command(name);
    }

    public string GetPropName(string name) //获取属性名
    {
      return Command(name);
    }

    public string Command(string str) //首字母大写 去除下划线
    {
      List<char> list = new List<char>();

      bool isUpper = true;

      for (int i = 0; i < str.Length; i++)
      {
        var item = str[i];

        if ((item >= 65 && item < 91) || (item >= 97 && item < 123))
        {
          if (isUpper && item >= 97)
            list.Add((char) (item - 32));
          else
            list.Add(item);

          if (isUpper) isUpper = false;
        }else if (!isUpper)
          isUpper = true;
      }

      return new string(list.ToArray());
    }
  }
}