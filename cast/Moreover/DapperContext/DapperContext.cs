using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace DapperContext
{
  public class DapperContext
  {

    private string connStr { get; set; }

    public DapperContext(string connStr)
    {
      this.connStr = connStr;

    }
  }
}
