using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.CusInterface;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Common.Models;
using Model.Vlxm.CusAttr;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Comment
{
  public class GetReplyCommentPageListService: BaseGetCommentPageListService
  {
    public override IEnumerable<string> GetWhereEnumerable(AcceptParam acceptParam, out PageModel<object> page, out string msg)
    {

      var param = acceptParam.AnalyzeParam<PageModel<long>>();

      if (param == null || param.Result <= 0)
      {
        msg = "参数异常！";
      }

      msg = string.Empty;

      page = new PageModel<object>()
      {
        PageNo = param.PageNo,
        PageSize = param.PageSize,
      };

      return new[]
      {
        $"{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.ReplyId))} = {param.Result}",
        $"{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.ValidFlag))} = {(int)ValidFlagMenu.UseFul}",
      };

    }

    public override IEnumerable<string> GetFieldEnumerable(TableMapperAttribute tableMapper)
    {
      return EntityTools.GetFields<CommentInfo>();
    }

    public override IEnumerable<string> GetOrderEnumerable(TableMapperAttribute tableMapper)
    {
      return new[]
      {
        SqlCharConst.DefaultOrder
      };
    }
  }
}
