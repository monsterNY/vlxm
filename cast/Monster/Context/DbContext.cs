using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Context
{
  public class DbContext
  {

    public virtual ISet<Product> Product { get; set; }

    public virtual int SaveChanges()
    {
      return 0;
    }

  }
}
