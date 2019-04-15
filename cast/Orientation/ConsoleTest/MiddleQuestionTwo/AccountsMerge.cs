using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : AccountsMerge  
  /// @author :mons
  /// @create : 2019/4/12 17:37:31 
  /// @source : https://leetcode.com/problems/accounts-merge/
  /// </summary>
  [Obsolete]
  public class AccountsMerge
  {

    // bug submit sort error????
    public IList<IList<string>> Solution(IList<IList<string>> accounts)
    {
      IList<IList<string>> res = new List<IList<string>>();

      Dictionary<string, int> dictionary = new Dictionary<string, int>();

      for (int i = 0; i < accounts.Count; i++)
      {
        IList<string> account = accounts[i];
        int inAccount = -1, nowAccount;

        for (int j = 1; j < account.Count; j++)
        {
          nowAccount = -1;
          if (dictionary.ContainsKey(account[j]))
            nowAccount = dictionary[account[j]];

          if (nowAccount == -1)
          {
            if (inAccount != -1)
            {
              accounts[inAccount].Add(account[j]);
              dictionary.Add(account[j], inAccount);
            }
            else
              dictionary.Add(account[j], i);
          }
          else if (i == nowAccount && inAccount != -1)
          {
            accounts[inAccount].Add(account[j]);
            dictionary[account[j]] = inAccount;
          }
          else if (inAccount == -1 && i != nowAccount)
          {
            inAccount = nowAccount;
            j = 0;
          }
        }

        if (inAccount == -1) res.Add(accounts[i]);
      }

      return res;
    }
  }
}