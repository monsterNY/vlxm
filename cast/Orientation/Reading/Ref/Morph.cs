using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Ref
{
  /// <summary>
  /// @desc : Morph  
  /// @author : mons
  /// @create : 2019/6/25 16:47:46 
  /// @source : clr via C# p687
  /// </summary>
  public class InterlockedTools
  {
    public static TResult Morph<TResult, TArgument>(ref int target, TArgument argument,
      Morpher<TResult, TArgument> morpher)
    {
      TResult morphResult;

      int currentVal = target, startVal, desiredVal;

      do
      {
        startVal = currentVal;
        desiredVal = morpher(startVal, argument, out morphResult);
        currentVal = Interlocked.CompareExchange(ref target, desiredVal, startVal);
      } while (startVal != currentVal);

      return morphResult;
    }
  }

  public delegate int Morpher<TResult, TArgument>(int startValue, TArgument argument, out TResult morphResult);
}