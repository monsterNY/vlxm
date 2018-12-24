using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Article.Entity
{
    public abstract class BaseModel
    {

      public long Id { get; set; }

      public DateTime? CreateTime { get; set; }

      public DateTime? UpdateTime { get; set; }

      public int ValidFlag { get; set; }

  }
}
