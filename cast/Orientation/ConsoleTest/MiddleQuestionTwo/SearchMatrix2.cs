using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : SearchMatrix2  
  /// @author :mons
  /// @create : 2019/4/19 10:48:43 
  /// @source : https://leetcode.com/problems/search-a-2d-matrix/
  /// </summary>
  public class SearchMatrix2
  {
    /**
     * Runtime: 100 ms, faster than 99.41% of C# online submissions for Search a 2D Matrix.
     * Memory Usage: 23.6 MB, less than 97.78% of C# online submissions for Search a 2D Matrix.
     *
     * Runtime: 100 ms, faster than 99.41% of C# online submissions for Search a 2D Matrix.
     * Memory Usage: 23.6 MB, less than 100.00% of C# online submissions for Search a 2D Matrix.
     *
     * ???
     *
     * cool find key , easy~
     *
     */
    public bool Solution(int[][] matrix, int target)
    {
      for (int i = 0, j = 0; i < matrix.Length && j < matrix[0].Length; )
      {
        if (matrix[i][j] == target) return true;

        if (matrix[i][j] > target) return false;

        if (matrix[i][matrix[i].Length - 1] < target) i++;
        else j++;
      }

//      int i = 0, j = 0;
//
//      while (i != matrix.Length && j != matrix[0].Length)
//      {
//        if (matrix[i][j] == target) return true;
//
//        if (matrix[i][j] > target) return false;
//
//        if (matrix[i][matrix[i].Length - 1] < target)
//        {
//          i++;
//        }
//        else
//        {
//          j++;
//        }
//      }

      return false;
    }
  }
}