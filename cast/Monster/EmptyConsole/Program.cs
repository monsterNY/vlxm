using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyConsole.LinqToEntityWhere;

namespace EmptyConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine((int) 'a');
      Console.WriteLine((int) 'z');
      Console.WriteLine((int) 'A');
      Console.WriteLine((int) 'Z');

      Console.WriteLine((char) ('a' - 32));

      var where = GenerateWhere.GetWhere<Personal>((u => u.Age > 3 && u.Sex == 2));

      object arr = new[] {1, 2, 34, 5};

      Console.WriteLine(arr.GetType().IsSubclassOf(typeof(Array)));

      if (arr is Array)
      {
        Console.WriteLine(string.Join(",",(arr as IEnumerable<object>)));
      }

      Console.WriteLine(where);

      Console.ReadKey(true);
    }
  }

  public class Personal
  {
    [Column("w_age")] public int Age { get; set; }

    [Column("w_sex")] public int Sex { get; set; }
  }
}