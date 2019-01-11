using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{

  [TableMapper("article_tag","t_at")]
  public class ArticleTag : BaseModel
  {

    public string TagName { get; set; }

  }
}
