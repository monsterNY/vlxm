using System;
using System.Collections.Generic;
using System.Text;
using Dal;

namespace Factory
{
  public class DalFactory
  {
    private DalFactory()
    {
    }

    //工厂一般单例
    private static DalFactory _instance;

    public static DalFactory GetInstance()
    {
      return _instance = _instance ?? new DalFactory();
    }

    public T GetDal<T>() where T : BaseDal, new() //正式中此处不因有where约束
    {
      //此次实现构造、初始化、配置的功能
      return new T();
    }
  }
}