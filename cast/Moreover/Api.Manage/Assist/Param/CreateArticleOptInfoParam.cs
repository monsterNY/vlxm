using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Param
{
  public class CreateArticleOptInfoParam
  {

    /// <summary>
    /// 操作key
    /// </summary>
    public string ActionKey { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public int OptionType { get; set; }

    /// <summary>
    /// 关联key
    /// </summary>
    public long RelationKey { get; set; }

    /// <summary>
    /// 操作熟练
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 触发者
    /// </summary>
    public long ActionUser { get; set; }

  }
}
