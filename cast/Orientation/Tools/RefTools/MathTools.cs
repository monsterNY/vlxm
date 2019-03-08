using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.RefTools
{
  /// <summary>
  /// @desc : MathTools  高效20%
  /// @author :mons
  /// @create : 2019/3/8 17:30:22 
  /// @source : https://blog.csdn.net/qq_15071263/article/details/77142001
  /// </summary>
  public class MathTools
  {

    /**
    * Created by 谭健 2017/8/13. 12:47.
    * All Rights Reserved
    *
    * int 是 32 位数据
    * int 类型的任何正数右移31位 = 0，任何负数右移31位 = 1
    * 溢出 31 位截断，空出 31 位补1，得到-1
    * a>>31 可以得到该数的符号位 + 还是 -
    * 如果 a>>31 + ,那么 a ^ 0 = a ,如果 a>>31 - ,那么 a ^ -1 翻转 a 的二进制
    *
    * @param a int a
    * @return a 的绝对值
    */
    public static int Abs(int a)
    {
      return (a ^ (a >> 31)) - (a >> 31);
    }

    /**
     * 一般普遍采用 n % 2 == 0 的方式
     * 但是如果换成位运算方式，效率会比前者好很多
     *
     * 在二进制中，末位为 0 必然是偶数，否则是奇数，并且不论正负
     * 所以，是什么数，看看末位就行了
     *
     * @param a long a
     * @return 如果是奇数，返回true，否则返回false
     */
    public static bool IsOdd(long a)
    {
      return (a & 1) == 1;
    }

  }
}
