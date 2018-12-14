using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MySpringCore.AOP
{
  /// <summary>
  /// 对于aop的功能描述，个人觉得最贴切的就是：世界上没有什么难题，是一层中间件解决不了的
  ///
  /// aop $lt;==$gt; 动态代理
  /// 
  /// </summary>
  public class AopCore
  {
    public void Init()
    {
    }

    /// <summary>
    /// 简单例子
    /// aop的作用 无非就是在方法的边界处添加其他操作 例如：日志记录
    /// </summary>
    /// <param name="beforeAction"></param>
    /// <param name="afterAction"></param>
    public void ForExample(Action beforeAction, Action afterAction)
    {
      beforeAction.Invoke();

      {
        //...代码块
        Console.WriteLine("I'm running");
      }

      afterAction.Invoke();
    }

    /// <summary>
    /// 采用Expression表达式树构建方法
    ///
    /// Expression == 动态反射
    /// 
    /// </summary>
    /// <param name="beforeAction"></param>
    /// <param name="afterAction"></param>
    /// <param name="runner"></param>
    public void ForExampleByExpression(Action beforeAction, Action afterAction, Action runner)
    {
      var parameterExpression = Expression.Parameter(typeof(Exception));

      var blockExpression = Expression.Block(
        Expression.TryCatchFinally(
          Expression.Block(
            Expression.Call(Expression.Constant(beforeAction.Target), beforeAction.Method),
            Expression.Call(Expression.Constant(runner.Target), runner.Method),
            Expression.Constant(null)
          ),
          Expression.Call(Expression.Constant(afterAction.Target), afterAction.Method)
          ,
          Expression.Catch(parameterExpression, Expression.Block(
            Expression.Call(
              typeof(Console).GetMethod("WriteLine", new[] {typeof(string)}),
//              Expression.Property(parameterExpression, "Message")
              Expression.Call(parameterExpression, typeof(Exception).GetMethod(nameof(ToString)))
            ) //ah find this
            , Expression.Constant(null)
          ))
//          new CatchBlock[]
//          {
////            Expression.Catch(typeof(Expression), Expression.Block(
////              //...没找到获取异常信息的方法,玩蛇=-=
////              ))
//            Expression.Catch(parameterExpression, Expression.Block(
//              Expression.Call(parameterExpression, typeof(Exception).GetMethod(nameof(ToString))) //ah find this
//            ))
//          }
        )
      );

      Expression.Lambda<Action>(blockExpression).Compile()();
    }

    /// <summary>
    /// 反射分两种：
    ///   1.通过已有程序集反射获取信息
    ///   2.通过动态反射构建内存程序集进行使用
    /// </summary>
    public void ForExampleByReflector()
    {
      //....应用少就不弄了
    }

    /*
     * aop的原理:动态反射
     *
     * 然后就没啥要扯的了...
     *
     */

    /// <summary>
    /// 既然扯到了aop,就来了解一下代理模式吧...
    /// [闲的.
    /// </summary>
    public void Proxy()
    {

      /**
       *
       *  《维基百科》
       *
       * 代理模式（英语：Proxy Pattern）是程序设计中的一种设计模式。
       * 所谓的代理者是指一个类别可以作为其它东西的接口。代理者可以作任何东西的接口：网络连接、存储器中的大对象、文件或其它昂贵或无法复制的资源。
       * 著名的代理模式例子为引用计数（英语：reference counting）指针对象。
       * 当一个复杂对象的多份副本须存在时，代理模式可以结合享元模式以减少存储器用量。
       * 典型作法是创建一个复杂对象及多个代理者，每个代理者会引用到原本的复杂对象。而作用在代理者的运算会转送到原本对象。一旦所有的代理者都不存在时，复杂对象会被移除。
       */

    }

  }
}