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
  public class QuestionAttribute : Attribute
  {

    public QuestionTypes[] QuestionTypes { get; set; }
    
    public QuestionAttribute(params QuestionTypes[] questionTypes)
    {
      QuestionTypes = questionTypes;
    }
    
  }
}