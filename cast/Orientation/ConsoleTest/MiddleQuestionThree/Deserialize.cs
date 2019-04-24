using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : Deserialize  
  /// @author : mons
  /// @create : 2019/4/24 17:11:38 
  /// @source : https://leetcode.com/problems/mini-parser/
  /// </summary>
  [Obsolete]
  public class Deserialize
  {
    public NestedInteger Solution(string s)
    {
      NestedInteger res = new NestedInteger();
      res.Add(new NestedInteger(123));

      NestedInteger res2 = new NestedInteger();
      res2.SetInteger(456);
      res2.Add(new NestedInteger(789));

      res.Add(res2);


      var i = 0;
      var res3 = Helper(s, ref i);
      return res3;
    }

    public NestedInteger Helper(string s, ref int index)
    {
      NestedInteger res = new NestedInteger();

      var num = 0;
      bool flag = false;
      for (; index < s.Length; index++)
      {
        if (s[index] == '[')
        {
          if (flag)
            res.Add(Helper(s, ref index));
          else
            flag = true;
        }
        else if (s[index] >= '0' && s[index] <= '9')
        {
          num = num * 10 + (s[index] - '0');
          res.SetInteger(num);
        }
        else if (s[index] == ']')
        {
          return res;
        }
        else
        {
          res.Add(new NestedInteger(num));
          num = 0;
        }
      }

      return res;
    }
  }

  public class NestedInteger
  {
    private int Value;

    private IList<NestedInteger> List;

    // Constructor initializes an empty nested list.
    public NestedInteger()
    {
    }

    // Constructor initializes a single integer.
    public NestedInteger(int value)
    {
      Value = value;
    }

    // @return true if this NestedInteger holds a single integer, rather than a nested list.
    bool IsInteger()
    {
      return List == null || List.Count == 0;
    }

    // @return the single integer that this NestedInteger holds, if it holds a single integer
    // Return null if this NestedInteger holds a nested list
    int GetInteger()
    {
      return Value;
    }

    // Set this NestedInteger to hold a single integer.
    public void SetInteger(int value)
    {
      Value = value;
    }

    // Set this NestedInteger to hold a nested list and adds a nested integer to it.
    public void Add(NestedInteger ni)
    {
      if (List == null)
        List = new List<NestedInteger>();

      List.Add(ni);
    }

    // @return the nested list that this NestedInteger holds, if it holds a nested list
    // Return null if this NestedInteger holds a single integer
    IList<NestedInteger> GetList()
    {
      return List;
    }
  }
}