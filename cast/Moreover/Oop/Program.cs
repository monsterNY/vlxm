using System;
using Oop.CusInterface;
using Oop.Single;

namespace Oop
{
  /// <summary>
  /// 面对对象
  /// </summary>
  class Program
  {
    private const string ReadMe = "单一功能、开闭原则、里氏替换、接口隔离以及依赖反转） SOLID (面向对象设计) ";

    static void Main(string[] args)
    {

      #region Single responsibility principle

      /**
       * 
       * 单一功能 -- 表示一个类只有一个职责
       *
       * 类似于这个UserBiz类  用于处理用户信息，那么便只包含用户信息处理的方法
       *
       * 优点：
       * 类的复杂性降低，实现什么职责都有清晰明确的定义；
       * 
       * 可读性提高，复杂性降低，那当然可读性提高了；
       * 
       * 可维护性提高，可读性提高，那当然更容易维护了；
       * 
       * 变更引起的风险降低，变更是必不可少的，如果接口的单一职责做得好，一个接口修
       * 改只对相应的实现类有影响，对其他的接口无影响，这对系统的扩展性、维护性都有非常大
       * 的帮助
       */

      Console.WriteLine("----------开始用户业务操作-------------");

      IUserBiz biz = new UserBiz();

      var userId = biz.Register("monster", "小白", "good good study,day day up!");

      Console.WriteLine($"注册用户编号:{userId}");

      var loginResult = biz.Login("monster", "good good study,day day up!");

      Console.WriteLine($"登录结果：{(loginResult > 0 ? "成功" : "用户名或密码错误")}");

      var editResult = biz.EditInfo(userId, "www.xxx.com/source/img/xxx.jpg", "fly", "good man");

      Console.WriteLine($"修改用户信息结果：{(editResult ? "成功" : "操作有误！")}");

      var editPwdResult = biz.EditPwd(userId, "good good study,day day up!", "nice day!");

      Console.WriteLine($"修改密码结果：{(editPwdResult ? "成功" : "操作有误！")}");

      //skip~
      IUserBiz errorBiz = new ErrorUserBiz();

      Console.WriteLine("over~");

      /**
       *
       * 到此一个简单的单一功能设计就结束了
       * 这个原则，主要的是把握住一个类的职责的考量
       * 比如在用户业务中，缓存信息，密码加密，是属于用户的职责还是属于其他?
       * 实际上，很多项目都没有符合这个原则，但那些项目中也有许多好项目，所以主要还是要根据实际情况来决定原则的实施程度
       *
       */

      #endregion

      #region open closed principle

      /**
       * 
       * 对扩展开发，对修改关闭
       *
       */

      IPlayBiz boyPlayBiz = new BoyPlayBiz();

      IPlayBiz girlPlayBiz = new GirlPlayBiz();

      //skip test~

      /**
       *
       * 主要通过抽象将可变化的部分进行抽离
       * 当遭遇到不可变因素时，采用这个原则将最大限度的降低维护成本
       * 不过也要根据实际的情况的考虑，不然容易造成设计过度，层级过高
       * 一个方法最高通过两层就差不多了，
       *
       * 维护时也应尽量采取这个原则，保持历史代码的纯洁性，提高系统的稳定性
       *
       */

      #endregion

      #region Liskov Substitution Principle

      /**
      * ● 代码共享，减少创建类的工作量，每个子类都拥有父类的方法和属性；
      * ● 提高代码的重用性；
      * ● 子类可以形似父类，但又异于父类，“龙生龙，凤生凤，老鼠生来会打洞”是说子拥有
      * 父的“种”，“世界上没有两片完全相同的叶子”是指明子与父的不同；
      * ● 提高代码的可扩展性，实现父类的方法就可以“为所欲为”了，君不见很多开源框架的
      * 扩展接口都是通过继承父类来完成的；
      * ● 提高产品或项目的开放性。
      * 自然界的所有事物都是优点和缺点并存的，即使是鸡蛋，有时候也能挑出骨头来，继承
      * 的缺点如下：
      * ● 继承是侵入性的。只要继承，就必须拥有父类的所有属性和方法；
      * ● 降低代码的灵活性。子类必须拥有父类的属性和方法，让子类自由的世界中多了些约
      * 束；
      * ● 增强了耦合性。当父类的常量、变量和方法被修改时，需要考虑子类的修改，而且在
      * 缺乏规范的环境下，这种修改可能带来非常糟糕的结果——大段的代码需要重构。
      * Java使用extends关键字来实现继承，它采用了单一继承的规则，C++则采用了多重继承
      * 的规则，一个子类可以继承多个父类。从整体上来看，利大于弊，怎么才能让“利”的因素发
      * 挥最大的作用，同时减少“弊”带来的麻烦呢？解决方案是引入里氏替换原则（Liskov
      * Substitution Principle，LSP），什么是里氏替换原则呢？它有两种定义：
      * ● 第一种定义，也是最正宗的定义：If for each object o1 of type S there is an object o2 of
      * type T such that for all programs P defined in terms of T,the behavior of P is unchanged when o1 is
      * substituted for o2 then S is a subtype of T.（如果对每一个类型为S的对象o1，都有类型为T的对
      * 象o2，使得以T定义的所有程序P在所有的对象o1都代换成o2时，程序P的行为没有发生变
      * 化，那么类型S是类型T的子类型。）
      * ● 第二种定义：Functions that use pointers or references to base classes must be able to use
      * objects of derived classes without knowing it.（所有引用基类的地方必须能透明地使用其子类的
      * 对象。）
      * 第二个定义是最清晰明确的，通俗点讲，只要父类能出现的地方子类就可以出现，而且
      * 替换为子类也不会产生任何错误或异常，使用者可能根本就不需要知道是父类还是子类。但
      * 是，反过来就不行了，有子类出现的地方，父类未必就能适应。
       */

      #endregion

      #region Interface Segregation Principle

      /**
       * ● The dependency of one class to another one should depend on the smallest possible interface.
*（类间的依赖关系应该建立在最小的接口上。）
       *
       * 区别于单一功能，接口隔离主要是建议接口方法应该尽量少，当使用接口抽象作为依赖时，接口应只定义出需要的方法，减少不必要的方法
       *
       */

      #endregion

      #region 依赖反转

      //skip .,

      #endregion

      Console.ReadKey(true);

    }
  }
}