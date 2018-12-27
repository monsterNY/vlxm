using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{

  [TableName("article_tag")]
  public class ArticleTag : BaseModel
  {

    public string TagName { get; set; }

  }
}
