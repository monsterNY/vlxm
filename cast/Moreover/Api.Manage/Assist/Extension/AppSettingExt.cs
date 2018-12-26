using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.CusConst;
using Model.Common.ConfigModels;

namespace Api.Manage.Assist.Extension
{
  public static class AppSettingExt
  {
    public static ConnectionParam GetMysqlConn(this AppSetting appSetting)
    {
      return appSetting.DbConnMap[ConfigConst.MysqlConn];
    }
  }
}