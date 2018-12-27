using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{

  [TableName("article_type")]
  public class ArticleType:BaseModel
  {

    public string TypeName { get; set; }

  }
}
