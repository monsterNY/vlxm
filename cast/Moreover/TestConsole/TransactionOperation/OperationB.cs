using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using Dapper;
using Newtonsoft.Json;
using TestConsole.Tools;

namespace TestConsole.TransactionOperation
{
  public class OperationB
  {
    public void Run()
    {
      var flag = $"事务B - {Thread.CurrentThread.ManagedThreadId} : ";

      var optKey = 5;

      var conn = ConnTools.GetConnection();

      dynamic searchInfo;
      try
      {
        if (conn.State != ConnectionState.Open)
          conn.Open();

        /**
           * 2.隔离级别的分类
           * （1）未提交读 （READ UNCOMMITTED）
           * （2）已提交读（READ COMMITTED）（默认值）
           *（3）可重复读（REPEATABLE READ）
           *
           *（4）可序列化（SERIALIZABLE）
           *
           *（5）快照（SNAPSHOT）
           *
           *（6）已经提交读快照（READ_COMMITTED_SNAPSHOT）
           */
        using (var transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted))//不同conn 事务互不影响。
        {
//          transaction.IsolationLevel = IsolationLevel.ReadUncommitted;//隔离级别后续不可更改

          searchInfo = conn.QueryFirst($"SELECT * FROM bill_info WHERE id = {optKey}");

          Console.WriteLine(flag + JsonConvert.SerializeObject(searchInfo));

          transaction.Commit();
        }
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
    }
  }
}