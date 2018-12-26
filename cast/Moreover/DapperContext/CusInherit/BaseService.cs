using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace DapperContext.CusInherit
{
  public abstract class BaseService<T>
  {

    protected abstract IDbConnection GetConnection();

    public IEnumerable<T> GetList()
    {
      using (var conn = GetConnection())
      {
        return conn.Query<T>("");
      }
    }

  }
}
