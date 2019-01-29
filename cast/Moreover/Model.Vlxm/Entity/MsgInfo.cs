using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{

  [TableMapper("msg_info","mi",nameof(MsgInfo))]
  public class MsgInfo:BaseModel
  {

    public string MsgNo { get; set; }
    public string Content { get; set; }
    public int TypeId { get; set; }
    public long Sender { get; set; }
    public long Receiver { get; set; }

  }
}
