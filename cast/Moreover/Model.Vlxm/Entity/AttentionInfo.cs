using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{

  /// <summary>
  /// 
  /// </summary>
  [TableMapper("attention_info","ai",nameof(AttentionInfo))]
  public class AttentionInfo:BaseModel
  {

    public long UserId { get; set; }
    public long AttentionUser { get; set; }
    public string GroupKey { get; set; }
    public string Description { get; set; }

  }
}
