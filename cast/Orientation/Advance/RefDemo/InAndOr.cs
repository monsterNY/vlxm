using System;
using System.Collections.Generic;
using System.Linq;

namespace Advance.RefDemo
{
  /// <summary>
  /// @desc : InAndOr  
  /// @author : mons
  /// @create : 2019/5/10 16:02:55 
  /// @source : 
  /// </summary>
  public class InAndOr
  {
    #region 协变&逆变

    //逆变 
    public static void InTest()
    {
      IOutput<Human> human = new Output<Human>();

      // 因为 IOutput<in T> ，并且 Human 的子类是 Hero ，所以 IOutput<Human> 可以逆变到 IOutput<Hero>
      IOutput<Hero> hero = human; //amazing!!!!!!
      hero.Write(new Hero {Name = "webabcd"});
    }

    interface IOutput<in T>
    {
      void Write(T o);
    }

    class Output<T> : IOutput<T>
      where T : Human
    {
      public void Write(T o)
      {
        Console.WriteLine(typeof(T));
        Console.WriteLine(o.GetType());
      }
    }

    //协变
    public void OutTest()
    {
      List<Human> human = new List<Human>();
      human.Add(new Human {Name = "aaa"});
      human.Add(new Human {Name = "bbb"});
      human.Add(new Human {Name = "ccc"});

      List<Hero> hero = new List<Hero>();
      hero.Add(new Hero {Name = "ddd", Story = "吃饭"});
      hero.Add(new Hero {Name = "eee", Story = "睡觉"});
      hero.Add(new Hero {Name = "fff", Story = "打豆豆"});

      /* 
       * List<T> 实现了如下接口 IEnumerable<out T> ，所以可以实现协变
       * public interface IEnumerable<out T> : IEnumerable
       * {
       *     // Summary:
       *     //     Returns an enumerator that iterates through the collection.
       *     //
       *     // Returns:
       *     //     A System.Collections.Generic.IEnumerator<T> that can be used to iterate through
       *     //     the collection.
       *     IEnumerator<T> GetEnumerator();
       * }
       */

      // Hero 的基类是 Human，所以 Hero 可以协变到 Human，所以下面的表达式成立
      human.AddRange(hero);
      List<Human> list = human.Union(hero).ToList();
    }

    class Human
    {
      public string Name { get; set; }
    }

    class Hero : Human
    {
      public string Story { get; set; }
    }

    #endregion
  }
}