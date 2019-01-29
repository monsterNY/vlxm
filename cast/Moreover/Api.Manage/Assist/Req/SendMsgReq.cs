using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Vlxm.Menu;

namespace Api.Manage.Assist.Req
{
  public class SendMsgReq
  {

    public string Content { get; set; }
    public MsgTypeMenu MsgType { get; set; }
    public long Sender { get; set; }
    public long Receiver { get; set; }

    public string ValidInfo()
    {

      if (string.IsNullOrWhiteSpace(Content))
      {
        return "消息内容不能为空";
      }

      return string.Empty;

    }

  }
}
