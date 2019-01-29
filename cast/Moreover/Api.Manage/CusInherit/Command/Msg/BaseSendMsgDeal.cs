using System;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.Assist.Req;
using DapperContext;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using NLog;

namespace Api.Manage.CusInherit.Command.Msg
{
  /// <summary>
  /// 基础发送消息
  /// </summary>
  public class BaseSendMsgDeal
  {

    protected ILogger Logger = LogManager.GetCurrentClassLogger();

    public async Task<ResultModel> Run(SendMsgReq req, AppSetting appSetting, HttpContext context)
    {

      if (req == null)
      {
        return ResultModel.GetNullErrorModel();
      }

      var msg = req.ValidInfo();
      if (msg != string.Empty)
      {
        return ResultModel.GetParamErrorModel(msg);
      }

      var conn = appSetting.GetMysqlConn(context);

      var param = new MsgInfo()
      {
        Content = req.Content,
        MsgNo = Guid.NewGuid().ToString(),
        Sender = req.Sender,
        Receiver = req.Receiver,
        TypeId = (int)req.MsgType
      };

      var result = await DapperTools.CreateSelectiveItem(conn, param);

      if (result > 0)
      {
        Logger.Debug($"发送消息成功:{param.MsgNo}");
      }

      return ResultModel.GetSuccessModel(result);

    }
  }
}
