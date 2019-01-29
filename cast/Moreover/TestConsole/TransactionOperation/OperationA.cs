using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp.Domain.Model;
using Dapper;
using Newtonsoft.Json;
using TestConsole.Tools;

namespace TestConsole.TransactionOperation
{
  public class OperationA
  {
    public void Run()
    {
      
      var flag = $"事务A - {Thread.CurrentThread.ManagedThreadId} : ";

      var optKey = 5;

      var conn = ConnTools.GetConnection();

      dynamic searchInfo;

      try
      {
        if (conn.State != ConnectionState.Open)
          conn.Open();

        var transaction = conn.BeginTransaction();

        #region 阶段1

        //修改信息 请求锁
        conn.Execute($"UPDATE bill_info SET money = 10 WHERE id = {optKey}");

        #endregion

        #region step1:未提交读

        #region 阶段2

        conn.Execute($"UPDATE bill_info SET money = money + 1 WHERE id = {optKey}");

        searchInfo = conn.QueryFirst($"SELECT * FROM bill_info WHERE id = {optKey}");

        Console.WriteLine(flag + JsonConvert.SerializeObject(searchInfo));

        #endregion

        #region 阶段3

//          conn.Execute($"UPDATE bill_info SET money = money + 5 WHERE id = {optKey}");
//
//          searchInfo = conn.QueryFirst($"SELECT * FROM bill_info WHERE id = {optKey}");
//
//          Console.WriteLine(flag + JsonConvert.SerializeObject(searchInfo));

        #endregion

        #region 阶段4

//          transaction.Commit();

        #endregion

        #endregion

      }
      catch (Exception e)
      {
        Console.WriteLine($"{flag} error:{e}");
      }
      finally
      {
        Console.WriteLine($"{flag} is over");
//        conn.Close();  
      }

      ThreadPool.QueueUserWorkItem(obj =>
      {
        //System.InvalidOperationException:“Nested transactions are not supported. -- 不支持嵌套事务 ,,,,
        using (var transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted)) //不同conn 事务互不影响。
        {
          searchInfo = conn.QueryFirst($"SELECT * FROM bill_info WHERE id = {optKey}");

          Console.WriteLine(flag + JsonConvert.SerializeObject(searchInfo));

          transaction.Commit();
        }
      });
    }
  }
}