using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.Assist.Req;
using Api.Manage.CusInterface;
using DapperContext;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Attention
{
  public class CancelAttentionUserAuthService : IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var req = acceptParam.AnalyzeParam<KeyReq<int>>();

      if (req == null || req.Key <= 0)
      {
        return ResultModel.GetParamErrorModel();
      }

      var conn = appSetting.GetMysqlConn(context);

      var result = await DapperTools.Edit(conn, EntityTools.GetTableName<AttentionInfo>(), new[]
        {
          $"{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.AttentionUser))}={req.Key}",
          $"{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.UserId))}={userId}",
          $"{nameof(BaseModel.ValidFlag)}={(int)ValidFlagMenu.UnUseFul}"
        },
        new[]
        {
          $"{nameof(BaseModel.ValidFlag)}=@{nameof(BaseModel.ValidFlag)}",
          $"{nameof(BaseModel.UpdateTime)}=@{nameof(BaseModel.UpdateTime)}",
        },
        new
        {
          ValidFlag = ValidFlagMenu.UnUseFul,
          UpdateTime = DateTime.Now,
        });

      return ResultModel.GetSuccessModel(result);
    }
  }
}