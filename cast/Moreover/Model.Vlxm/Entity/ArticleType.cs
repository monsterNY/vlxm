using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{

  [TableMapper("article_type","t_at",nameof(ArticleType))]
  public class ArticleType:BaseModel
  {

    public string TypeName { get; set; }

    public string Icon { get; set; }

  }
}
