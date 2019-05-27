using System;
using System.Collections.Generic;
using System.Text;
using Advance.Models;

namespace Advance.RefDemo
{
  /// <summary>
  /// @desc : TypeHandleDemo  
  /// @author : mons
  /// @create : 2019/5/27 16:57:01 
  /// @source : 
  /// </summary>
  public class TypeHandleDemo
  {
    public void Run()
    {
      try
      {
        InfoModel myClass = new InfoModel(1);

        // Get the type of MyClass.
        Type myClassType = myClass.GetType();

        // Get the runtime handle of MyClass.
        RuntimeTypeHandle myClassHandle = myClassType.TypeHandle;

        DisplayTypeHandle(myClassHandle);
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception: {0}", e.Message);
      }
    }

    public void DisplayTypeHandle(RuntimeTypeHandle myTypeHandle)
    {
      // Get the type from the handle.
      Type myType = Type.GetTypeFromHandle(myTypeHandle);
      // Display the type.
      Console.WriteLine("\nDisplaying the type from the handle:\n");
      Console.WriteLine("The type is {0}.", myType.ToString());
    }
  }
}