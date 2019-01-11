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
using Model.Common.Models;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Attention
{
  public class GetAttentionPageListAuthService:IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var req = acceptParam.AnalyzeParam<PageModel<AttentionPageFilterReq>>();

      if (req == null || req.PageNo == 0 || req.Result == null)
      {
        return ResultModel.GetParamErrorModel();
      }

      var conn = appSetting.GetMysqlConn(context);

      var whereList = new List<string>()
      {
        $"{nameof(BaseModel.ValidFlag)}={(int)ValidFlagMenu.UseFul}"
      };

      if (req.Result.FilterType == 1)
      {
        whereList.Add($"{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.AttentionUser))}={userId}");
      }
      else
      {
        whereList.Add($"{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.UserId))}={userId}");
      }

      var result = await DapperTools.GetPageList<AttentionInfo>(req.PageNo, req.PageSize, conn, whereList);

      return ResultModel.GetSuccessModel(result);

    }
  }
}
