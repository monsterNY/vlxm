using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Req
{
  public class AttentionPageFilterReq
  {

    /// <summary>
    /// 默认 -- 查看我关注的人  1 -- 查看关注我的人
    /// </summary>
    public int FilterType { get; set; }

  }
}
