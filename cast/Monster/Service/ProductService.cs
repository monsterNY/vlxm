using System;
using System.Collections.Generic;
using System.Text;
using Dal;
using Model;

namespace Service
{
  public class ProductService
  {

    private ProductDal productDal;

    public ProductService(ProductDal productDal)
    {
      this.productDal = productDal;
    }

    public ProductService()
    {
      throw new NotImplementedException();
    }

    public bool AddInfo(Product product)
    {

      return productDal.Add(product);

    }

  }
}
