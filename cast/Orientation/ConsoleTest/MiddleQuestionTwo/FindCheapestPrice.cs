using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : FindCheapestPrice  
  /// @author :mons
  /// @create : 2019/4/19 15:20:32 
  /// @source : https://leetcode.com/problems/cheapest-flights-within-k-stops/
  /// </summary>
  [Obsolete]
  public class FindCheapestPrice
  {

    //bug what is stops???
    public int Solution(int n, int[][] flights, int src, int dst, int K)
    {
      return Helper(n, flights, src, dst, K + 1, new bool[n], new int[n]);
    }

    public int Helper(int n, int[][] flights, int src, int target, int K, bool[] visited, int[] dp)
    {
      if (dp[src] > 0) return dp[src];
      if (K == 0) return -1;
      visited[src] = true;

      var minAmount = int.MaxValue;

      foreach (var flight in flights)
      {
        var start = flight[0];
        var end = flight[1];
        var amount = flight[2];

        if (visited[end]) continue;

        if (start == src)
        {
          var money = 0;
          if (end != target)
          {
            money = Helper(n, flights, end, target, K - 1, visited, dp);
          }

          if (money >= 0 && amount + money < minAmount)
          {
            minAmount = amount + money;
          }
        }
      }

      visited[src] = false;

      minAmount = minAmount == int.MaxValue ? -1 : minAmount;

      dp[src] = minAmount;

      return minAmount;
    }

    //Time Limit
    public int Helper(int n, int[][] flights, int src, int target, int K, bool[] visited)
    {
      if (src == target) return 0;
      if (K + 1 == 0) return -1;
      visited[src] = true;

      var minAmount = int.MaxValue;

      foreach (var flight in flights)
      {
        var start = flight[0];
        var end = flight[1];
        var amount = flight[2];

        if (visited[end]) continue;

        if (start == src)
        {
          var money = Helper(n, flights, end, target, K - 1, visited);
          if (money >= 0 && amount + money < minAmount)
          {
            minAmount = amount + money;
          }
        }
      }

      visited[src] = false;

      return minAmount == int.MaxValue ? -1 : minAmount;
    }

    public int Try(int n, int[][] flights, int src, int dst, int K)
    {
      var res = int.MaxValue;

      var visited = new bool[n];
      var dp = new int[n];

      for (int i = 0; i < n; i++)
      {
        res = Math.Min(Dfs(visited, flights, src, dst, K, 0), res);
        if (i != src && i != dst)
        {
          visited[i] = true;
        }
      }

      return res == int.MaxValue ? -1 : res;
    }

    public int Dfs(bool[] visited, int[][] flights, int start, int target, int k, int amount)
    {
      if (k == -1)
        return int.MaxValue;

      visited[start] = true;

      foreach (var flight in flights)
      {
        if (visited[flight[1]]) continue;

        if (flight[0] == start)
        {
          if (flight[1] == target)
          {
            return amount + flight[2];
          }
          else
          {
            return Dfs(visited, flights, flight[1], target, k - 1, amount + flight[2]);
          }
        }
      }

      visited[start] = false;
      return int.MaxValue;
    }
  }
}