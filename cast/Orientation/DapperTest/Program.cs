using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using Tools.RefTools;

namespace DapperTest
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var timer = new CodeTimer();

      var connStr = "Data Source=192.168.1.13;Initial Catalog=amao100_DB;User ID=yj;Password=123?abc";

      using (var conn =
        new SqlConnection(connStr))
      {
        var result = conn.Query("SELECT * FROM TenderTable");

        Console.WriteLine(JsonConvert.SerializeObject(result)); //预热

        for (int i = 1; i < 100; i++)
        {
          Console.WriteLine($"\n-------------------------{i}--------------------------");

          var codeTimerResult = timer.Time(i, () =>
          {
            var enumerable = conn.Query(
              @"SELECT uda_Id, uda_Province_code, uda_Province, uda_City_code, uda_City, uda_District_code, uda_District, uda_Address,
          uda_Phone, uda_Receiver, uda_Is_default, uda_Post_code

            FROM dbo.User_Delivery_AddressTable

          WHERE uda_Status = 1 AND uda_User_id = @userId

          ORDER BY uda_Is_default DESC, uda_Id DESC", new {userId = 1});

            var list = enumerable.ToList();

            //          Console.WriteLine(JsonConvert.SerializeObject(enumerable));
          });

          Console.WriteLine($"Dapper time:{codeTimerResult}");

          var timerResult = timer.Time(i, (() =>
          {
            using (var reader = ExecuteReader(connStr, CommandType.StoredProcedure,
              "delivery_address_get_list", new SqlParameter("@userId", 1)))
            {
              var list = new List<Dictionary<string, object>>();
              if (reader.HasRows)
              {
                while (reader.Read()) list.Add(Reader2Dictionary(reader));

                reader.Close();

                //              Console.WriteLine(JsonConvert.SerializeObject(list));
              }
              else
              {
                Console.WriteLine("暂无数据");
              }
            }
          }));

          Console.WriteLine($"ado.net produce time:{timerResult}");
        }
      }


      Console.WriteLine("-------------");

      Console.ReadKey(true);
    }

    /// <summary>
    /// 枚举,标识数据库连接是由SqlHelper提供还是由调用者提供
    /// </summary>
    private enum SqlConnectionOwnership
    {
      /// <summary>由SqlHelper提供连接</summary>
      Internal,

      /// <summary>由调用者提供连接</summary>
      External
    }

    public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText,
      params SqlParameter[] commandParameters)
    {
      if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
      SqlConnection connection = null;
      try
      {
        connection = new SqlConnection(connectionString);
        connection.Open();

        return ExecuteReader(connection, null, commandType, commandText, commandParameters,
          SqlConnectionOwnership.Internal);
      }
      catch
      {
        // If we fail to return the SqlDatReader, we need to close the connection ourselves
        if (connection != null) connection.Close();
        throw;
      }
    }

    // <summary>
    /// 执行指定数据库连接对象的数据阅读器.
    /// </summary>
    /// <remarks>
    /// 如果是SqlHelper打开连接,当连接关闭DataReader也将关闭.
    /// 如果是调用都打开连接,DataReader由调用都管理.
    /// </remarks>
    /// <param name="connection">一个有效的数据库连接对象</param>
    /// <param name="transaction">一个有效的事务,或者为 'null'</param>
    /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
    /// <param name="commandText">存储过程名或T-SQL语句</param>
    /// <param name="commandParameters">SqlParameters参数数组,如果没有参数则为'null'</param>
    /// <param name="connectionOwnership">标识数据库连接对象是由调用者提供还是由SqlHelper提供</param>
    /// <returns>返回包含结果集的SqlDataReader</returns>
    private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction,
      CommandType commandType, string commandText, SqlParameter[] commandParameters,
      SqlConnectionOwnership connectionOwnership)
    {
      if (connection == null) throw new ArgumentNullException("connection");

      var mustCloseConnection = false;
      // 创建命令
      var cmd = new SqlCommand();
      try
      {
        PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters,
          out mustCloseConnection);

        // 创建数据阅读器
        SqlDataReader dataReader;

        if (connectionOwnership == SqlConnectionOwnership.External)
          dataReader = cmd.ExecuteReader();
        else
          dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        // 清除参数,以便再次使用..
        // HACK: There is a problem here, the output parameter values are fletched
        // when the reader is closed, so if the parameters are detached from the command
        // then the SqlReader can磘 set its values.
        // When this happen, the parameters can磘 be used again in other command.
        var canClear = true;
        foreach (SqlParameter commandParameter in cmd.Parameters)
          if (commandParameter.Direction != ParameterDirection.Input)
            canClear = false;

        if (canClear) cmd.Parameters.Clear();

        return dataReader;
      }
      catch
      {
        if (mustCloseConnection)
          connection.Close();
        throw;
      }
    }

    /// <summary>
    /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数
    /// </summary>
    /// <param name="command">要处理的SqlCommand</param>
    /// <param name="connection">数据库连接</param>
    /// <param name="transaction">一个有效的事务或者是null值</param>
    /// <param name="commandType">命令类型 (存储过程,命令文本, 其它.)</param>
    /// <param name="commandText">存储过程名或都T-SQL命令文本</param>
    /// <param name="commandParameters">和命令相关联的SqlParameter参数数组,如果没有参数为'null'</param>
    /// <param name="mustCloseConnection"><c>true</c> 如果连接是打开的,则为true,其它情况下为false.</param>
    private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction,
      CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
    {
      if (command == null) throw new ArgumentNullException("command");
      if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

      // If the provided connection is not open, we will open it
      if (connection.State != ConnectionState.Open)
      {
        mustCloseConnection = true;
        connection.Open();
      }
      else
      {
        mustCloseConnection = false;
      }

      // 给命令分配一个数据库连接.
      command.Connection = connection;

      // 设置命令文本(存储过程名或SQL语句)
      command.CommandText = commandText;

      // 分配事务
      if (transaction != null)
      {
        if (transaction.Connection == null)
          throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.",
            "transaction");
        command.Transaction = transaction;
      }

      // 设置命令类型.
      command.CommandType = commandType;

      // 分配命令参数
      if (commandParameters != null) AttachParameters(command, commandParameters);

      return;
    }

    /// <summary>
    /// 将SqlParameter参数数组(参数值)分配给SqlCommand命令.
    /// 这个方法将给任何一个参数分配DBNull.Value;
    /// 该操作将阻止默认值的使用.
    /// </summary>
    /// <param name="command">命令名</param>
    /// <param name="commandParameters">SqlParameters数组</param>
    private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
      if (command == null) throw new ArgumentNullException("command");
      if (commandParameters != null)
        foreach (var p in commandParameters)
          if (p != null)
          {
            // 检查未分配值的输出参数,将其分配以DBNull.Value.
            if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&
                p.Value == null)
              p.Value = DBNull.Value;

            command.Parameters.Add(p);
          }
    }

    /// <summary>
    /// SqlDataReader内的Dictionary处理
    /// </summary>
    /// <param name="reader">源数据</param>
    /// <returns></returns>
    private static Dictionary<string, object> Reader2Dictionary(SqlDataReader reader)
    {
      var item = new Dictionary<string, object>();

      for (var i = 0; i < reader.FieldCount; i++)
      {
        var rKey = reader.GetName(i).Substring(reader.GetName(i).IndexOf("_") + 1).ToLower();
        var rValue = reader.GetValue(i);

        var splitMatch = new Regex(@"\((.)\)").Match(rKey);
        var parseJson = rKey.IndexOf("$");

        //分组与反序列化不能共存
        if (splitMatch.Success)
        {
          rKey = rKey.Replace(splitMatch.Value, "");

          //无值时不进行分割
          if (!(rValue == null || rValue is DBNull))
          {
            var str = rValue.ToString();
            if (!string.IsNullOrEmpty(str))
            {
              rValue = str.Split(splitMatch.Result("$1").First());
            }
          }
        }
        else if (parseJson > 0)
        {
          rKey = rKey.Replace("$", "");
          try
          {
            rValue = JsonConvert.DeserializeObject(Convert.ToString(rValue));
          }
          catch
          {
            rValue = null;
          }
        }

        //归集处理
        var collection = rKey.IndexOf(".");
        if (collection > 0)
        {
          item = MergeDictionary(rKey, rValue, item);
        }
        else
        {
          item.Add(rKey, rValue);
        }
      }

      return item;
    }

    /// <summary>
    /// 合并结果集
    /// </summary>
    /// <param name="prevKey">上层key</param>
    /// <param name="value">值</param>
    /// <param name="target">目标集</param>
    /// <returns></returns>
    public static Dictionary<string, object> MergeDictionary(string prevKey, object value,
      Dictionary<string, object> target)
    {
      var dotIndex = prevKey.IndexOf('.');
      if (dotIndex > 0)
      {
        // 当前key
        var key = prevKey.Substring(0, dotIndex);

        // 下层key
        var nextKey = prevKey.Substring(dotIndex + 1);
        // 下层目标
        var nextTarget = new Dictionary<string, object>();

        if (target.ContainsKey(key))
        {
          if (target[key] is Dictionary<string, object>)
          {
            nextTarget = target[key] as Dictionary<string, object>;
          }
        }

        var result = MergeDictionary(nextKey, value, nextTarget);
        target[key] = result;
      }
      else
      {
        target[prevKey] = value;
      }

      return target;
    }
  }
}