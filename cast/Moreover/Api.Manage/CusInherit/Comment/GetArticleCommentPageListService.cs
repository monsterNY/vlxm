using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Model.Common.Models;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Comment
{
  public class GetArticleCommentPageListService : BaseGetCommentPageListService
  {
    public override IEnumerable<string> GetWhereEnumerable(AcceptParam acceptParam, out PageModel<object> page,
      out string msg)
    {
      var param = acceptParam.AnalyzeParam<PageModel<int>>();

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
        $"{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.JoinKey))} = {param.Result}",
        $"{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.CommentType))} = {(int)CommentTypeMenu.Article}",
      };
    }
  }
}