using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCode.Domain.CusConst
{
  /// <summary>
  /// @desc : ParamConst  
  /// @author :mons
  /// @create : 2019/3/19 15:44:37 
  /// @source : 
  /// </summary>
  public class ParamConst
  {

    public const string TemplateTableStr = @"using System;
using System.Collections.Generic;
using System.Text;

namespace #work_namespace
{
    /// <summary>
    /// @desc : _Description_  
    /// @author :monster
    /// @create : _CreateTime_
    /// @update : _UpdateTime_
    /// </summary>
    public class _Name_Model
    {
{Props}
    }
}";

    public const string TemplateFieldStr = @"
      /// <summary>
      /// _Description_
      /// </summary>
      public _Type__IsNullAble_ _Name_{get;set;}
";

    public const string DynamicCodeStr = @"
using System;

namespace Dynamicly//此命名空间不可修改
{
    public class GenerateHelper//此类名不可修改
    {
        
		public string GetTableName(string name)//获取表名
		{
		  return name;
		}

		public string GetPropName(string name)//获取属性名
		{
		  return name;
		}
    }
}
";

    public const string DynamicCommandCodeStr = @"
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dynamicly
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
        } else if (!isUpper)
          isUpper = true;
      }

      return new string(list.ToArray());
    }
  }
}
";

  }
}
