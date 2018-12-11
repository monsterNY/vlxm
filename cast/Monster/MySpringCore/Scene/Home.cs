using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySpringCore.CusInherit;
using MySpringCore.CusInterface;
using MySpringCore.Entity;

namespace MySpringCore.Scene
{

  /// <summary>
  /// this is Home
  /// </summary>
  public class Home
  {

    private IList<Personal> PersonalList { get; set; }
    private IPlay play;

    public void Init()
    {
      PersonalList = new List<Personal>();
      play = new WatchTv();
    }

    public void AddPersonal(Personal personal)
    {
      if (personal.Toy == null || personal.Toy is Running)
      {
        personal.Toy = play;//通常情况下,家里不可能跑步....
      }
      PersonalList.Add(personal);
    }

  }
}
