using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.CusConst;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;

namespace Api.Manage.Assist.Extension
{
  public static class AppSettingExt
  {
    public static ConnectionParam GetMysqlConn(this AppSetting appSetting)
    {
      return appSetting.DbConnMap[ConfigConst.MysqlConn];
    }

    public static ConnectionParam GetRedisConn(this AppSetting appSetting)
    {
      return appSetting.DbConnMap[ConfigConst.RedisConn];
    }

    public static IDbConnection GetMysqlConn(this AppSetting appSetting,HttpContext context)
    {
      var connParam = appSetting.DbConnMap[ConfigConst.MysqlConn];
      return context.GetConnection(connParam.FlagKey, connParam.ConnStr);
    }
    

  }
}