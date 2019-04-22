using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace ConsoleTest.Funny
{
  /// <summary>
  /// @desc : ReflectoreTest  
  /// @author :mons
  /// @create : 2019/4/19 17:50:35 
  /// @source : 
  /// </summary>
  [ReadOnly(true)]
  public class ReflectoreTest
  {

    /// <summary>
    /// 设置对象的只读属性
    /// </summary>
    /// <param name="myobj">需设置的属性表</param>
    /// <param name="Readonly">只读为true，可写为false</param>f
    void SetClassReadonly(ReflectoreTest myobj, bool Readonly)
    {
      if (myobj == null) return;  //myobj就是类TaoNei的一个实例

      var readOnlyAttribute = myobj.GetType().GetCustomAttribute<ReadOnlyAttribute>();
      
      Type readonlyType = typeof(ReadOnlyAttribute);

      FieldInfo fld = readonlyType.GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

      System.ComponentModel.AttributeCollection attrs = TypeDescriptor.GetAttributes(myobj);

      fld.SetValue(attrs[typeof(ReadOnlyAttribute)], Readonly);
    }

  }
}
