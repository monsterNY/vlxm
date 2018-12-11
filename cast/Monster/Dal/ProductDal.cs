using System;
using System.Collections.Generic;
using System.Text;
using Context;
using Model;

namespace Dal
{
  public class ProductDal:BaseDal
  {

    private readonly DbContext _dbContext;

    public ProductDal(DbContext dbContext)
    {
      this._dbContext = dbContext;
    }

    public ProductDal()
    {
      throw new NotImplementedException();
    }

    public bool Add(Product product)
    {
      _dbContext.Product.Add(product);

      var saveChanges = _dbContext.SaveChanges();

      return saveChanges > 0;

    }

  }
}
