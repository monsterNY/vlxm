using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;
using Model.Vlxm.Menu;

namespace Model.Vlxm.Entity
{

  /// <summary>
  /// 文章操作表
  /// type <see cref="ArticleOptMenu"/>
  /// </summary>
  [TableMapper("article_opt_info","t_aoi")]
  public class ArticleOptInfo:BaseModel
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
    /// 操作数量
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 触发人
    /// </summary>
    public int ActionUser { get; set; }

  }
}
