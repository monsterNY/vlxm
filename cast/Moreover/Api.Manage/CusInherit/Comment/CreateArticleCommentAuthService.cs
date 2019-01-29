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
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Comment
{
  public class CreateArticleCommentAuthService:IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var req = acceptParam.AnalyzeParam<CreateArticleCommentReq>();

      if (req == null)
      {
        return ResultModel.GetNullErrorModel();
      }

      var msg = req.ValidInfo();
      if (msg != string.Empty)
      {
        return ResultModel.GetParamErrorModel(msg);
      }

      var param = (CreateCommentParam) req;
      param.ActionUser = userId;

      var conn = appSetting.GetMysqlConn(context);

      var result = await DapperTools.CreateItem(conn, EntityTools.GetTableName<CommentInfo>(), param);

      if (result > 0)
      {
//        var task = new BaseSendMsgDeal().Run(new SendMsgReq()
//        {
//          Content = "收到一条评论信息，请赶快查看吧~",
//          MsgType = MsgTypeMenu.Comment,
//          Receiver = req.JoinKey,
//          Sender = userId
//        }, appSetting, context);
      }

      return ResultModel.GetSuccessModel(result);
    }
  }
}
