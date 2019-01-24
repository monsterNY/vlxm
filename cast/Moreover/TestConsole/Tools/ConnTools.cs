using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace TestConsole.Tools
{
  public class ConnTools
  {

    public static IDbConnection GetConnection()
    {
      return new MySqlConnection("Server=localhost;Database=dapper_test; User=root;Password=root;charset=utf8mb4;");
    }

  }
}
