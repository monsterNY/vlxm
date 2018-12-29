using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using DapperContext;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Select
{
  public class GetArticleDetailService : IDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      var keyDto = acceptParam.AnalyzeParam<KeyDto<long>>();

      if (keyDto == null || keyDto.Key <= 0)
      {
        return ResultModel.GetParamErrorModel(string.Empty, "参数异常！");
      }

      var idField = EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.Id));

      var whereArr = new List<string>
      {
        SqlCharConst.DefaultWhere,
        $"{idField} = {keyDto.Key}"
//        $"Id = @Id"
      };

      var conn = context.GetConnection(appSetting.GetMysqlConn().FlagKey,appSetting.GetMysqlConn().ConnStr);

//      var info = await DapperTools.GetItem<ArticleInfo>(conn, EntityTools.GetTableName<ArticleInfo>(), whereArr, new { Id = keyDto.Key});
      var info = await DapperTools.GetItem<ArticleInfo>(conn, EntityTools.GetTableName<ArticleInfo>(), whereArr);

      if (info == null)
      {
        return ResultModel.GetParamErrorModel(string.Empty, "此文章不存在或已删除！");
      }

      return ResultModel.GetSuccessModel(String.Empty,info);
    }
  }
}