using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using DapperContext;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.User
{
  /// <summary>
  /// 获取用户详情信息
  /// </summary>
  public class GetUserDetailAuthService: IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var item = await DapperTools.GetItem<UserInfo>(appSetting.GetMysqlConn(context), EntityTools.GetTableName<UserInfo>(),
        new List<string>()
        {
          $"id = {userId}"
        });

      return ResultModel.GetSuccessModel(item);
    }
  }
}
