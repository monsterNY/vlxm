using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Manage.Assist.Req;
using Api.Manage.Assist.Entity;
using Api.Manage.CusInterface;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Newtonsoft.Json;
using NLog;

namespace Api.Manage.CusInherit.User
{

  /// <summary>
  /// 登录校验
  /// </summary>
  public class AuthLoginService : IDeal
  {

    ILogger Logger = LogManager.GetCurrentClassLogger();


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

      // discover endpoints from metadata
      //error: HTTPS required
      //      var disco = await DiscoveryClient.GetAsync(appSetting.Authorize.Url);

      //      Logger.Debug(JsonConvert.SerializeObject(disco));
//      var tokenClient = new TokenClient(disco.TokenEndpoint, "client2", "secret");

      // request token
      var tokenClient = new TokenClient(appSetting.Authorize.Url + "/connect/token", appSetting.Authorize.Client, appSetting.Authorize.Secret);
      var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(dto.UserName, dto.LoginPwd);

      if (tokenResponse.IsError)
      {
        Logger.Debug(JsonConvert.SerializeObject(tokenResponse));
        return ResultModel.GetDealErrorModel(tokenResponse.Error);
      }

      return ResultModel.GetSuccessModel(tokenResponse.Json);
    }
  }
}