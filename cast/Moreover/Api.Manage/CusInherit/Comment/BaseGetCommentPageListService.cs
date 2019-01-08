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
using Model.Common.Models;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;

namespace Api.Manage.CusInherit.Comment
{
  public abstract class BaseGetCommentPageListService:IDeal
  {

    public abstract IEnumerable<string> GetWhereEnumerable(AcceptParam acceptParam,out PageModel<object> page,out string msg);

    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {

      var whereEnumerable = GetWhereEnumerable(acceptParam, out PageModel<object> page,out string msg);

      if (msg != string.Empty)
      {
        return ResultModel.GetParamErrorModel(msg);
      }

      var conn = appSetting.GetMysqlConn(context);

      var pageList = await DapperTools.GetPageList<CommentInfo>(page.PageNo, page.PageSize, conn, whereEnumerable, page.Result);

      return ResultModel.GetSuccessModel(pageList);
    }
  }
}
