using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusMenu;

namespace Tools.CusAttr
{
  /// <summary>
  /// @desc : LoveAttribute  
  /// @author :mons
  /// @create : 2019/3/29 11:02:17 
  /// @source : I love this
  /// </summary>
  public class LoveAttribute : Attribute
  {
    public LoveTypes[] Type { get; set; }


    public QuestionTypes[] QuestionTypes { get; set; }

    public LoveAttribute(params LoveTypes[] type)
    {
      Type = type;
    }

    public LoveAttribute(params QuestionTypes[] questionTypes)
    {
      QuestionTypes = questionTypes;
    }

    public LoveAttribute(LoveTypes[] type, QuestionTypes[] questionTypes)
    {
      Type = type;
      QuestionTypes = questionTypes;
    }
  }
}