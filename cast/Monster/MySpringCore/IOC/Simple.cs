using System;
using System.Collections.Generic;
using System.Text;
using Container;
using Context;
using Dal;
using Factory;
using Model;
using Service;

namespace MySpringCore.IOC
{
  public class Simple
  {

    public void Demo()
    {

      //----------Before-----------
      //  暂不考虑上下文
      //----------Before-----------

      Random rand = new Random();

      bool result;

      var product = new Product()
      {
        Id = rand.Next(100),
        Name = Guid.NewGuid().ToString().Replace("-", ""),
        Price = rand.Next(1000) * 100 + 100
      }; //需要处理的产品

      //<<简单业务场景>>

      //step1.创建上下文
      var dbContext = new DbContext();

      //step2.创建dal处理对象
      var productDal = new ProductDal(dbContext);

      //step3.执行操作
      result = productDal.Add(product);

      //----------Summary-----------
      //  当用户访问进来时 --> UI层
      //  创建了一个上下文对象 --> UI层
      //  创建dal对象 --> UI层 --> 关联dal层
      //  执行操作,返回结果 --> UI层
      //----------Summary-----------

      //<<常见三层业务场景>>

      //step1.同上
      //step2.同上
      //step3.创建service处理对象
      var productService = new ProductService(productDal);

      //step4.执行操作
      result = productService.AddInfo(product);


      //----------Summary-----------
      //  当用户访问进来时 --> UI层
      //  创建了一个上下文对象 --> UI层
      //  创建dal对象 --> UI层 --> 关联dal层
      //  创建service对象 --> UI层 --> 关联service层
      //  执行操作,返回结果 --> UI层
      //----------Summary-----------

      //----------Question-----------
      //  当业务流程越来越复杂时,层级关系随着越来越多,UI层关联的业务层也会越来越多
      //  用户访问进来，UI层会关联到所有相关联的层
      //  当后续维护时，维护一个层的同时必须调整相关层的所有关系耦合 --> 大工程
      //  层级关系混乱：UI层在一定程度上影响了所有相关层的构造  但dal明显不与ui直接关联
      //----------Question-----------

      //>>IOC<< 控制反转
      //在普遍的场景中  对象中属性的引用指针由对象指定
      //在IOC场景中 对象中属性的引用指针由容器决定 [spring即由spring容器控制]

      //<<工厂模式处理>>

      //step1.创建dal处理对象
      //var dal = DalFactory.GetInstance().GetDal<ProductDal>();

      //or 创建service处理对象  dal的创建应该由service层决定

      //普通工厂：
      //每个service对应一个方法
      //var productService1 = ServiceFactory.GetInstance().GetProductService();

      //泛型获取 工厂:
      //让所有service符合一个规范
      //然后通过泛型方法来获取
      var service = ServiceFactory.GetInstance().GetService<ProductService>();

      //step2.执行操作

      //result = dal.Add(product);
      result = service.AddInfo(product);

      //<<缓存容器模式>>

      //step1.往容器中添加对象
      //此步骤一般在程序初始化时进行
      //配置注册，全局使用
      CacheContainer.AddInstance<DbContext, DbContext>();
      CacheContainer.AddInstance<ProductDal, ProductDal>();
      CacheContainer.AddInstance<ProductService, ProductService>();

      //step2.从容器中获取对象
      var instance = CacheContainer.GetInstance<ProductService>();

      //step3.执行操作
      result = instance.AddInfo(product);

      //----------Summary-----------
      //  使用IOC容器的优势：
      //  1.简洁  一处添加对象后，全局使用[注：此处并非直接添加对象,而是相对于添加一个对象标识,添加一个由type到instance的关系]
      //  2.降低耦合度 通过操作可见,UI层最终只需要操作service层，便可完成操作，即UI层只与最近的一个层低关联着
      //    虽然最终还是有UI->Service层的依赖,但是通过容器实际是UI -> 容器 容器 ->Service 
      //    而且容器->everything 即实际 只存在UI -> 容器的存在
      //    且UI/使用着 无须考虑容器中对象的生命周期  对象存储在容器中 --> 对象的生命周期也由容器进行管理
      //----------Summary-----------

      Console.WriteLine($"【添加产品】操作结果:{result}");

    }

  }
}
