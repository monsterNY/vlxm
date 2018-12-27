using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;

namespace TestConsole
{
  class Program
  {
    static void Main(string[] args)
    {

      IDbConnection conn = new MySqlConnection("Server=localhost;Database=vlxm; User=root;Password=root;charset=utf8;");

      var content =
        "<p><span style=\"font-size:30px\"><span style=\"font-family:Impact, serif\">🤣</span></span></p><p><span style=\"font-size:30px\"><span style=\"font-family:Impact, serif\">真逗呢</span></span></p><p><span style=\"font-size:30px\"><span style=\"font-family:Impact, serif\">哈哈哈</span></span></p>";

      conn.Execute($@"INSERT INTO article_info
        ( title, author, category, content)

      VALUES( '', '', '', '{content}'");

      var fileName = "xxx.jpg";

      var obj = new {Name = "test"};

      Console.WriteLine(string.Join(",", obj.GetType().GetProperties().Select(u => u.Name)));

      StringBuilder builder = new StringBuilder("tewasoptuaowiptu");

      Console.WriteLine($"empty {builder}");

      Console.WriteLine(null + "test");

      Console.ReadKey(true);

      Console.WriteLine("Hello World!");
    }
  }

  class Temp
  {
    public string Name { get; set; }

    public string Description;
  }
}