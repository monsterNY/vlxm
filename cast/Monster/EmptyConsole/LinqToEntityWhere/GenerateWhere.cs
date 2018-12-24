using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JesseLinqProvider;

namespace EmptyConsole.LinqToEntityWhere
{
  public static class GenerateWhere
  {
    public static string GetWhere<T>(Expression<Func<T, bool>> whereExpression)
      where T : class, new()
    {
      var translator = new WhereTranslator();
      return translator.Translate(whereExpression,typeof(T));
    }
  }
}