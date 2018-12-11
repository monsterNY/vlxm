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
  /// this is sports group
  /// </summary>
  public class SportsGround
  {

    private IList<Personal> PersonalList { get; set; }
    private IPlay play;

    public void Init()
    {
      PersonalList = new List<Personal>();
      play = new Running();
    }

    public void AddPersonal(Personal personal)
    {
      if (personal.Toy == null || personal.Toy is Running)
      {
        personal.Toy = play;//通常情况下,运动场不可能看tv....
      }
      PersonalList.Add(personal);
    }

  }
}
