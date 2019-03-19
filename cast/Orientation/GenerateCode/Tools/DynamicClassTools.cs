using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.CSharp;

namespace GenerateCode.Tools
{
  /// <summary>
  /// @desc : 动态代码  
  /// @author :mons
  /// @create : 2019/3/19 14:33:07 
  /// @source : http://www.uml.org.cn/net/201808033.asp
  /// </summary>
  public class DynamicClassTools
  {
    /// <summary>
    /// 编译动态代码
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static Assembly DynamicAssembly(string code)
    {
      //get the code to compile

      // 1.Create a new CSharpCodePrivoder instance
      CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

      // 2.Sets the runtime compiling parameters by crating a new CompilerParameters instance
      CompilerParameters objCompilerParameters = new CompilerParameters();
      objCompilerParameters.ReferencedAssemblies.Add("System.dll");
      objCompilerParameters.GenerateInMemory = true;

      // 3.CompilerResults: Complile the code snippet by calling a method from the provider
      CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, code);

      if (cr.Errors.HasErrors)
      {
        string strErrorMsg = cr.Errors.Count + " Errors:";

        for (int x = 0; x < cr.Errors.Count; x++)
        {
          strErrorMsg = strErrorMsg + "/r/nLine: " +
                        cr.Errors[x].Line.ToString() + " - " +
                        cr.Errors[x].ErrorText;
        }

        throw new Exception(strErrorMsg);
      }

      // 4. Invoke the method by using Reflection
      Assembly objAssembly = cr.CompiledAssembly;

      return objAssembly;
    }

    /// <summary>
    /// 获取类对象
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="classFullName">类的全路径</param>
    /// <returns></returns>
    public static Object GetClass(Assembly assembly, string classFullName)
    {
      return assembly.CreateInstance(classFullName);
    }

    /// <summary>
    /// 执行类方法
    /// </summary>
    /// <param name="instance">对象</param>
    /// <param name="mothodName">方法名</param>
    /// <param name="paramArr">参数数组</param>
    /// <returns></returns>
    public static object InvokeMethod(object instance, string mothodName, params object[] paramArr)
    {
      return instance.GetType().InvokeMember(
        mothodName, BindingFlags.InvokeMethod, null, instance, paramArr);
    }



  }
}