using System;
using System.Collections.Generic;
using System.Text;
using Dal;
using Service;

namespace Factory
{
  public class ServiceFactory
  {

    private ServiceFactory()
    {

    }

    private static ServiceFactory _instance;

    public static ServiceFactory GetInstance()
    {
      return _instance = _instance ?? new ServiceFactory();
    }

    public ProductService GetProductService()
    {
      return new ProductService();
//      return new ProductService(DalFactory.GetInstance().GetDal<ProductDal>());
    }

    public T GetService<T>() where T : new()//正式中此处不因有where约束
    {
      //此次实现构造、初始化、配置的功能
      return new T();
    }

  }
}
