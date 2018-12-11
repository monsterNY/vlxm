using System;
using System.Collections.Generic;
using System.Text;
using Context;

namespace Dal
{
  public class BaseDal
  {

    private DbContext DbContext;

    public BaseDal()
    {
      DbContext = new DbContext();
    }

  }
}
