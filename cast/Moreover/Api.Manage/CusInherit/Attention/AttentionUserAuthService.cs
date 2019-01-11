using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.Assist.Param;
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
  public class AttentionUserAuthService : IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var req = acceptParam.AnalyzeParam<AttentionUserReq>();

      if (req == null)
      {
        return ResultModel.GetNullErrorModel();
      }

      var msg = req.ValidInfo();

      if (msg != string.Empty)
      {
        return ResultModel.GetParamErrorModel(msg);
      }

      if (req.AttentionUser == userId)
      {
        return ResultModel.GetParamErrorModel();
      }

      var conn = appSetting.GetMysqlConn(context);

      AttentionInfo oldInfo = await DapperTools.GetItem<AttentionInfo>(conn, EntityTools.GetTableName<AttentionInfo>(),
        new[]
        {
          $"{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.AttentionUser))} ={req.AttentionUser}",
          $"{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.UserId))} ={userId}",
        },
        new[]
        {
          nameof(BaseModel.Id),
        });

//      var isExists = await DapperTools.IsExists(conn, EntityTools.GetTableName<AttentionInfo>(),
//        new[]
//        {
//          $"{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.AttentionUser))} ={req.AttentionUser}",
//          $"{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.UserId))} ={userId}",
//        });

      if (oldInfo == null)
      {
        var param = (CreateAttentionParam) req;

        param.UserId = userId;

        var result = await DapperTools.CreateItem(conn, EntityTools.GetTableName<AttentionInfo>(), param);

        return ResultModel.GetSuccessModel(result);
      }
      else
      {
        var editParam = new
        {
          UpdateTime = DateTime.Now,
          ValidFlag = (int) ValidFlagMenu.UseFul,
          req.Description,
          req.GroupKey,
        };

        var setFieldList = new List<string>();

        setFieldList.Add(nameof(BaseModel.UpdateTime));
        setFieldList.Add(nameof(BaseModel.ValidFlag));

        var result = await DapperTools.Edit(conn, EntityTools.GetTableName<AttentionInfo>(), new[]
        {
          $"{nameof(BaseModel.Id)}={oldInfo.Id}"
        }, editParam);

        return ResultModel.GetSuccessModel(result);
      }
    }
  }
}