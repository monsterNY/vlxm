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

namespace Api.Manage.CusInherit.Article
{
  public class EditArticleAuthService : IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var req = acceptParam.AnalyzeParam<EditArticleReq>();

      if (req == null || req.Id <= 0)
      {
        return ResultModel.GetNullErrorModel(string.Empty);
      }

      string msg;

      if ((msg = req.ValidInfo()) != string.Empty)
      {
        return ResultModel.GetParamErrorModel(string.Empty, msg);
      }

      var conn = appSetting.GetMysqlConn(context);

      var result = await DapperTools.SelectiveEdit(conn, new[]
      {
        $"{nameof(BaseModel.Id)} = {req.Id}",
        $"{nameof(BaseModel.ValidFlag)} = {(int) ValidFlagMenu.UseFul}",
        $"{EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.UserId))}={userId}"
      }, new ArticleInfo()
      {
        Id = req.Id,
        ArticleType = req.ArticleType,
        Author = req.Author,
        Category = string.Join(",", req.Category),
        Content = req.Content,
        PublishTime = req.PublishTime,
        UpdateTime = new DateTime(),
        Title = req.Title,
        Description = req.Description,
        FaceImg = req.FaceImg,
        Status = req.Status
      });

      return ResultModel.GetSuccessModel(result);

    }
  }
}