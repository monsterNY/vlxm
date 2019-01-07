using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Req;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using Command.RedisHelper.Helper;
using DapperContext;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Common.Cache;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.User
{

  /// <summary>
  /// 用户登录
  ///
  /// new --> identityServer4
  /// 
  /// </summary>
  [Obsolete]
  public class LoginService:IDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {

      var dto = acceptParam.AnalyzeParam<UserLoginReq>();

      if (dto == null)
      {
        return ResultModel.GetNullErrorModel();
      }

      var msg = dto.ValidInfo();

      if (msg != string.Empty)
      {
        return ResultModel.GetParamErrorModel(msg);
      }

      var whereList = new List<string>()
      {
        $"{nameof(UserLoginReq.UserName)} = @{nameof(UserLoginReq.UserName)}",
        $"{nameof(UserLoginReq.LoginPwd)} = @{nameof(UserLoginReq.LoginPwd)}",
      };

      var userInfo = await DapperTools.GetItem<UserInfo>(appSetting.GetMysqlConn(context),EntityTools.GetTableName<UserInfo>(), whereList, dto);

      var cusRedisHelper = MemoryCache.GetInstance().TryGet<CusRedisHelper>(appSetting.GetRedisConn().FlagKey);

      var token = Guid.NewGuid().ToString().Replace("-", "");

//      cusRedisHelper.StringSetAsync("user_api_token")

        //使用 is4 =-= gg

      throw new NotImplementedException();
    }
  }

}
