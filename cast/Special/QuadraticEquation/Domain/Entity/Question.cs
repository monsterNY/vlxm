using System;
using System.Collections.Generic;
using System.Text;

namespace QuadraticEquation.Domain.Entity
{

  /// <summary>
  /// ax + by = c : simple version
  /// ax ? by = c
  /// </summary>
  public class Question
  {

    public int ConstA { get; set; }

    public int ConstB { get; set; }

    public int ConstResult { get; set; }
    
  }
}
