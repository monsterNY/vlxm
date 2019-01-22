using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ConsoleApp.Domain.Const;
using ConsoleApp.Domain.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace ConsoleApp
{
  internal class Program
  {
    private static void Main(string[] args)
    {

      TestFreeSql();

      //支持事务
      //结果缓存
      //异步方法
      //存储过程
      using (IDbConnection conn = new MySqlConnection(ParamConst.MysqlConnStr))
      {
        //FirstTestMethod();
        //TestExecute();
        //TestQuery(conn);
        //TestQueryMultiple(conn);
        //支持扩展
      }

      Console.ReadKey(true);
    }

    /// <summary>
    /// version : 0.0.7
    ///
    /// 对扩展不友好，不易用，操作复杂
    /// 可维护信高，仅支持固定语法，可用度不高
    /// 缓存+表达式树
    /// 
    /// </summary>
    private static void TestFreeSql()
    {

      IFreeSql fsql = new FreeSql.FreeSqlBuilder()
        .UseConnectionString(FreeSql.DataType.MySql, ParamConst.MysqlConnStr)
        .UseSlave("connectionString1", "connectionString2") //使用从数据库，支持多个

        .UseMonitorCommand(
          cmd => Console.WriteLine(cmd.CommandText), //监听SQL命令对象，在执行前
          (cmd, traceLog) => Console.WriteLine(traceLog)) //监听SQL命令对象，在执行后

        .UseLogger(null) //使用日志，不指定默认输出控制台 ILogger
        .UseCache(null) //使用缓存，不指定默认使用内存 IDistributedCache

        .UseAutoSyncStructure(true) //自动同步实体结构到数据库
        .UseSyncStructureToLower(true) //转小写同步结构
        .Build();

      var list = fsql.Select<UserInfo>();

      Console.WriteLine(JsonConvert.SerializeObject(list));

    }

    private static void TestQueryMultiple(IDbConnection conn)
    {

      //感觉用不上系列 官方示例代码附上
//      string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID; SELECT * FROM InvoiceItem WHERE InvoiceID = @InvoiceID;";
//
//      using (var connection = My.ConnectionFactory())
//      {
//        connection.Open();
//
//        using (var multi = connection.QueryMultiple(sql, new { InvoiceID = 1 }))
//        {
//          var invoice = multi.Read<Invoice>().First();
//          var invoiceItems = multi.Read<InvoiceItem>().ToList();
//        }
//      }

    }

    private static void TestQuery(IDbConnection conn)
    {
      #region single easy operation

      //var list = conn.Query<UserInfo>("SELECT * FROM user_info");

      #endregion

      #region Multi operation 

      #region one to one


      //可以说是特别贴心了，支持多类转换，自定义映射关系 - nice
//            var enumerable = conn.Query<UserInfo, UserMoney, UserInfo>(
//              @"
//      SELECT * FROM user_info userT
//      INNER JOIN user_money moneyT
//      ON userT.id = moneyT.userId
//      ",
//              ((info, money) =>
//              {
//                info.Wallet = money;
//                return info;
//              }),
//              //当存在重复列需要在此处标明
//              splitOn: string.Join(",",typeof(BaseModel).GetProperties().Select(u=>u.Name))
//            );
//
//            Console.WriteLine(JsonConvert.SerializeObject(enumerable));

      #endregion

      #region one to many

      var userInfoDictionary = new Dictionary<long, UserInfo>();

      var list = conn.Query<UserInfo, BillInfo,UserInfo>(
          @"
        SELECT 
        userT.*,
        billT.Id,billT.money
      FROM user_info userT
      INNER JOIN bill_info billT
      ON userT.id = billT.userId
",
          (user, bill) =>
          {

            if (!userInfoDictionary.TryGetValue(user.Id, out var userEntry))
            {
              userEntry = user;
              userEntry.BillList = new List<BillInfo>();
              userInfoDictionary.Add(user.Id, userEntry);
            }

            userEntry.BillList.Add(bill);
            return userEntry;
          },
          splitOn: string.Join(",", typeof(BaseModel).GetProperties().Select(u => u.Name))
          )
        .Distinct();

      Console.WriteLine(JsonConvert.SerializeObject(list));

      #endregion

      #region diff type convert

      //强大但感觉用不着  一般设计没这么高级。。。
      //官方示例代码附上~~~~~
      //      string sql = "SELECT * FROM Invoice;";
      //
      //      if (conn.State != ConnectionState.Open)
      //      {
      //        conn.Open();
      //      }
      //
      //      var invoices = new List<Invoice>();
      //
      //      using (var reader = connection.ExecuteReader(sql))
      //      {
      //        var storeInvoiceParser = reader.GetRowParser<StoreInvoice>();
      //        var webInvoiceParser = reader.GetRowParser<WebInvoice>();
      //
      //        while (reader.Read())
      //        {
      //          Invoice invoice;
      //
      //          switch ((InvoiceKind) reader.GetInt32(reader.GetOrdinal("Kind")))
      //          {
      //            case InvoiceKind.StoreInvoice:
      //              invoice = storeInvoiceParser(reader);
      //              break;
      //            case InvoiceKind.WebInvoice:
      //              invoice = webInvoiceParser(reader);
      //              break;
      //            default:
      //              throw new Exception(ExceptionMessage.GeneralException);
      //          }
      //
      //          invoices.Add(invoice);
      //        }
      //      }
      //
      //      My.Result.Show(invoices);

      #endregion

      #endregion

//      First, Single & Default  same~

    }

    private static void TestExecute()
    {
      //执行操作并返回影响行数

      //适用范围：
      //Stored Procedure
      //INSERT statement
      //UPDATE statement
      //DELETE statement

      //Name Description
      //sql The command text to execute.
      //  param The command parameters(default = null).
      //  transaction The transaction to use(default = null).
      //  commandTimeout The command timeout(default = null)
      //commandType The command type(default = null)

      using (IDbConnection conn = new MySqlConnection(ParamConst.MysqlConnStr))
      {

        #region single operation

        //        var result = conn.Execute(@"
        //INSERT INTO user_info
        //	(userName, displayName, loginPwd, channel, email)
        //	VALUES (@userName, @displayName, @loginPwd, @channel, @email)
        //", new UserInfo()
        //          {
        //            UserName = "test_account",
        //            DisplayName = "测试用户2",
        //            LoginPwd = "test123",
        //            Channel = "vlxm",
        //            Email = "monster2071@163.com"
        //          }
        //        );

        //        Console.WriteLine($"添加结果:{result}");

        #endregion

        #region many operation

        var result = conn.Execute(@"
INSERT INTO user_info
	(userName, displayName, loginPwd, channel, email)
	VALUES (@userName, @displayName, @loginPwd, @channel, @email)
", new UserInfo[]
          {
            new UserInfo()
            {
              UserName = "test_account",
              DisplayName = "测试用户3",
              LoginPwd = "test123",
              Channel = "vlxm",
              Email = "monster2071@163.com"
            },
            new UserInfo()
            {
              UserName = "test_account",
              DisplayName = "测试用户4",
              LoginPwd = "test123",
              Channel = "vlxm",
              Email = "monster2071@163.com"
            },
            new UserInfo()
            {
              UserName = "test_account",
              DisplayName = "测试用户5",
              LoginPwd = "test123",
              Channel = "vlxm",
              Email = "monster2071@163.com"
            }
          }
        );

        //当参数为集合时，会重复执行sql  适用于操作sql和存储过程
        //只是不知道这样的优势是什么.,
        Console.WriteLine($"添加结果:{result}");//3

        #endregion

        //其余操作类型与以上类似，便不重复

      }
    }

    /// <summary>
    /// 测试方法
    /// </summary>
    private static void FirstTestMethod()
    {
      //创建连接对象
      using (IDbConnection conn = new MySqlConnection(ParamConst.MysqlConnStr))
      {
        //测试查询
        //dapper 通过拓展方法实现
        //由此可见 dapper 内部管理连接的打开与关闭 - 便捷 故我们只用担心连接对象的初始化与对象的释放
        //dapper 支持泛型查询 默认返回可变类型 dynamic - 确认过功能，是我喜欢的类型~
        //buffered 是否在内存中缓存结果 默认：是

        //var enumerable = conn.Query("SELECT * FROM user_info",buffered:true);

        //Console.WriteLine(JsonConvert.SerializeObject(enumerable));

        //使用泛型的处理：
        //属性名不区分大小写 - nice
        //        var list = conn.Query<UserInfo>("SELECT * FROM user_info");

        //执行存储过程
        //也是非常简单快捷呢~
        //        var list = conn.Query<UserInfo>("sp_get_all_user",
        //          //参数 可以是 实体对象 也可以是Parameter
        //          null,
        //          commandType: CommandType.StoredProcedure);

        // Dynamic 类似于常用的sqlParameter
        //        DynamicParameters parameter = new DynamicParameters();
        //
        //        parameter.Add("@Kind", "---", DbType.Int32, ParameterDirection.Input);
        //        parameter.Add("@Code", "Many_Insert_0", DbType.String, ParameterDirection.Input);
        //        parameter.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

        //        Console.WriteLine(JsonConvert.SerializeObject(list));

        //IDbDataParameter[] parameters = new IDbDataParameter[]
        //{
        //  new MySqlParameter("@username", "dapper"),
        //};

        //DynamicParameters parameter = new DynamicParameters();
        //parameter.Add("@userName", "dapper", DbType.String,ParameterDirection.Input);

        //var list = conn.Query<UserInfo>("SELECT * FROM user_info WHERE userName = @userName",
        //  //参数名不区分大小写 - 简便的方式~
        //  //new { userName = "dapper"}

        //  //不支持的类型
        //  //parameters

        //  //success
        //  parameter
        //);

        //Console.WriteLine(JsonConvert.SerializeObject(list));


        // Transaction
        //error The connection is not open
        //使用事务时需要关心。。。
//        if (conn.State != ConnectionState.Open)
//        {
//          conn.Open();
//        }
//        using (var transaction = conn.BeginTransaction())
//        {
//          var affectedRows = conn.Execute(@"
//INSERT INTO user_info
//	(userName, displayName, loginPwd, channel, email)
//	VALUES (@userName, @displayName, @loginPwd, @channel, @email)
//", new UserInfo()
//          {
//            UserName = "test_account",
//            DisplayName = "测试用户2",
//            LoginPwd = "test123",
//            Channel = "vlxm",
//            Email = "monster2071@163.com"
//          }
//           //可以不加
//          //, transaction
//          );

//          var zero = 0;

//          Console.WriteLine(8/zero);

//          transaction.Commit();
//          conn.Close();
//        }

        //over ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
      }
    }
  }
}