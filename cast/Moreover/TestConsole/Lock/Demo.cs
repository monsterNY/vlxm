using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleApp.Domain.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using TestConsole.Tools;

namespace TestConsole.Lock
{
  /// <summary>
  /// 数据库锁机制
  /// </summary>
  public class Demo
  {
    public void Test()
    {



      #region 初级测试 仅使用事务操作 不设计锁操作  结果：以数据库中当前数据为准 修改优先级以时间顺序为依据

      var validFlag = new[] {false, false, false , false };

      var optKey = 4;

      #region 事务A update failure

      ThreadPool.QueueUserWorkItem((state =>
      {
        var currentThreadId = Thread.CurrentThread.ManagedThreadId;

        Console.WriteLine($"{currentThreadId} is work [edit failure]");

        var conn = ConnTools.GetConnection();

        try
        {
          if (conn.State != ConnectionState.Open)
            conn.Open();

          using (var transaction = conn.BeginTransaction())
          {
//            transaction.IsolationLevel//隔离级别
            var billInfos = conn.Query<BillInfo>("SELECT * FROM bill_info");

            //拿到最后一个账单
            var lastBill = billInfos.OrderByDescending(u => u.Id).FirstOrDefault();

            if (lastBill == null)
            {
              throw new Exception("no bill");
            }

            lastBill.Money = lastBill.Money + 2019; //金额修改

            Console.WriteLine($"{currentThreadId} edit opt");
            conn.Execute($@"
  UPDATE bill_info 
  SET {string.Join(",", typeof(BillInfo).GetProperties().Where(u => !u.Name.Equals(nameof(BaseModel.Id))).Select(u => $"{u.Name}=@{u.Name}"))}
  WHERE {nameof(BaseModel.Id)} = {lastBill.Id}

", lastBill);

            var editInfo = conn.QueryFirstOrDefault<BillInfo>($"SELECT * FROM bill_info WHERE {nameof(BaseModel.Id)} = {lastBill.Id}");

            Console.WriteLine($"{currentThreadId} editInfo:{editInfo.Money}");

            Console.WriteLine($"{currentThreadId} other opt");
            //其他操作
            Thread.Sleep(5000);

            var zero = 0;
            Console.WriteLine(100 / zero); //error

            transaction.Commit();
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
          validFlag[0] = true; //work A finish
        }
        finally
        {
          Console.WriteLine($"{currentThreadId} is over");
          conn.Close();
        }
      }));

      #endregion

      #region 事务B read

      ThreadPool.QueueUserWorkItem((state =>
      {
        var currentThreadId = Thread.CurrentThread.ManagedThreadId;

        Console.WriteLine($"{currentThreadId} is work [read]");

        var conn = ConnTools.GetConnection();

        try
        {
          if (conn.State != ConnectionState.Open)
            conn.Open();

          using (var transaction = conn.BeginTransaction())
          {
            Thread.Sleep(1000);//等待a完成修改

            var editInfo = conn.QueryFirstOrDefault<BillInfo>($"SELECT * FROM bill_info WHERE {nameof(BaseModel.Id)} = 4");

            Console.WriteLine($"{currentThreadId} read editInfo:{editInfo.Money}");//默认情况下 此处读取的为数据库中的数据 其他待提交事务并不影响此处读取
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
          validFlag[1] = true; //work A finish
        }
        finally
        {
          Console.WriteLine($"{currentThreadId} is over");
          conn.Close();
        }
      }));

      #endregion

      #region 事务C edit success

      ThreadPool.QueueUserWorkItem((state =>
      {

        Thread.Sleep(200);

        var currentThreadId = Thread.CurrentThread.ManagedThreadId;

        Console.WriteLine($"{currentThreadId} is work [edit success add]");

        var conn = ConnTools.GetConnection();

        try
        {
          if (conn.State != ConnectionState.Open)
            conn.Open();

          using (var transaction = conn.BeginTransaction())
          {
            var bill = conn.QueryFirst<BillInfo>($"SELECT * FROM bill_info WHERE {nameof(BaseModel.Id)} = {optKey}");
            
            bill.Money = bill.Money + 201900; //金额修改

            Console.WriteLine($"{currentThreadId} edit opt");
            conn.Execute($@"
  UPDATE bill_info 
  SET {string.Join(",", typeof(BillInfo).GetProperties().Where(u => !u.Name.Equals(nameof(BaseModel.Id))).Select(u => $"{u.Name}=@{u.Name}"))}
  WHERE {nameof(BaseModel.Id)} = {bill.Id}

", bill);

            var editInfo = conn.QueryFirstOrDefault<BillInfo>($"SELECT * FROM bill_info WHERE {nameof(BaseModel.Id)} = {bill.Id}");

            Console.WriteLine($"{currentThreadId} editInfo:{editInfo.Money}");

            Console.WriteLine($"{currentThreadId} other opt");
            //其他操作
//            Thread.Sleep(3000);

            //no error
            transaction.Commit();
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
          validFlag[2] = true; //work A finish
        }
        finally
        {
          Console.WriteLine($"{currentThreadId} is over");
          conn.Close();
        }
      }));

      #endregion

      #region 事务D edit success

      ThreadPool.QueueUserWorkItem((state =>
      {

        Thread.Sleep(201);

        var currentThreadId = Thread.CurrentThread.ManagedThreadId;

        Console.WriteLine($"{currentThreadId} is work [edit success reduce]");

        var conn = ConnTools.GetConnection();

        try
        {
          if (conn.State != ConnectionState.Open)
            conn.Open();

          using (var transaction = conn.BeginTransaction())
          {
            var bill = conn.QueryFirst<BillInfo>($"SELECT * FROM bill_info WHERE {nameof(BaseModel.Id)} = {optKey}");

            bill.Money = bill.Money - 201900; //金额修改

            Console.WriteLine($"{currentThreadId} edit opt");
            conn.Execute($@"
  UPDATE bill_info 
  SET {string.Join(",", typeof(BillInfo).GetProperties().Where(u => !u.Name.Equals(nameof(BaseModel.Id))).Select(u => $"{u.Name}=@{u.Name}"))}
  WHERE {nameof(BaseModel.Id)} = {bill.Id}

", bill);

            var editInfo = conn.QueryFirstOrDefault<BillInfo>($"SELECT * FROM bill_info WHERE {nameof(BaseModel.Id)} = {bill.Id}");

            Console.WriteLine($"{currentThreadId} editInfo:{editInfo.Money}");

            Console.WriteLine($"{currentThreadId} other opt");
            //其他操作
            //            Thread.Sleep(3000);

            //no error
            transaction.Commit();
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
          validFlag[3] = true; //work A finish
        }
        finally
        {
          Console.WriteLine($"{currentThreadId} is over");
          conn.Close();
        }
      }));

      #endregion

      Console.WriteLine("running");

      while (validFlag.Select(u => !u).Any())
      {
      }

      Console.WriteLine("work finish");

      #endregion
    }
  }
}