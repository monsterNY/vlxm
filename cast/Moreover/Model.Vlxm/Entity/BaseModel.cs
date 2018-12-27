using System;

namespace Model.Vlxm.Entity
{
    public abstract class BaseModel
    {

      public long Id { get; set; }

      public DateTime CreateTime { get; set; }

      public DateTime? UpdateTime { get; set; }

      public int ValidFlag { get; set; }

  }
}
