using System;
using System.Collections.Generic;
using System.Text;

namespace Advance.Codewars
{
  /// <summary>
  /// @desc : FindNb  
  /// @author : mons
  /// @create : 2019/7/18 17:13:17 
  /// @source : 
  /// </summary>
  public class FindNb
  {
    /**
     * Your task is to construct a building which will be a pile of n cubes. The cube at the bottom will have a volume of n^3, the cube above will have volume of (n-1)^3 and so on until the top which will have a volume of 1^3.
       
       You are given the total volume m of the building. Being given m can you find the number n of cubes you will have to build?
       
       The parameter of the function findNb (find_nb, find-nb, findNb) will be an integer m and you have to return the integer n such as n^3 + (n-1)^3 + ... + 1^3 = m if such a n exists or -1 if there is no such n.
     */

    // emm.... 这也能过？
    public static long Empty(long m)
    {
      long res = 1, sum = 1;

      while (m > sum)
      {
        sum += ++res * res * res;
      }

      if (sum == m) return res;

      return -1;

    }
  }
}