using System;
using System.Collections.Generic;
using System.Text;
using MySpringCore.CusInherit;
using MySpringCore.Entity;
using MySpringCore.Scene;

namespace MySpringCore.DI
{
  public class Simple
  {
    public void Demo()
    {
      //<<简单业务场景>>

      //---------Description----------
      //---------Description----------

      Personal personal = new Personal(); //I'm a personal

      personal.Toy = new Running(); //I will running 

      personal.Toy.Run();

      personal.Toy = new WatchTv(); //I will watch tv

      personal.Toy.Run();

      //<<结合场景>>
      //体育场只能跑步[简单考虑]
      SportsGround ground = new SportsGround();

      //em...
      Home home = new Home();

      //由此可见,不同的场景会需要不同的实现.

      //<<结合常见业务>>
      //service 中需要实例化 dal对象
      //常见使用:service.dal = new XxxDal();
      //而DI的情况:
      //在dal属性上添加特性，标明需要的具体对象的key
      //当获取对象时，便通过标记和属性的set方法来指定引用指针

      //DI : 定义约定【即标记规范,常用type、抽象】 
      //IOC 获取对象 实际就是一种DI
      //IOC 用于去除类与类之间的关联,DI用于通过标记关联类与类之间的关系
      //即原有类与类的关联,由DI来处理. 关联-->由标记代替
    }
  }
}