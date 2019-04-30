using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace SourceAnalysis
{
  class Program
  {
    static void Main(string[] args)
    {

      var str1 = "123";
      var str2 = "123";

      Console.WriteLine(EqualsHelper(str1,str2));

      Console.WriteLine("Hello World!");

      Console.ReadKey(true);

    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static unsafe bool EqualsHelper(string strA, string strB)
    {
      var char1 = ' ';
      int length = strA.Length;

      char c = strA[0];
      char c2 = strB[0];

      char* chPtr1 = &c;
      char* chPtr2 = &c2;

//      fixed (char* chPtr1 = &c)
      //      fixed (char* chPtr2 = &0)
      {
        char* chPtr3 = chPtr1;
        char* chPtr4 = chPtr2;
        while (length >= 10)
        {
          if (*(int*)chPtr3 != *(int*)chPtr4
              || *(int*)(chPtr3 + 2) != *(int*)(chPtr4 + 2)
              || (*(int*)(chPtr3 + 4) != *(int*)(chPtr4 + 4)
                  || *(int*)(chPtr3 + 6) != *(int*)(chPtr4 + 6))
              || *(int*)(chPtr3 + 8) != *(int*)(chPtr4 + 8))
            return false;
          chPtr3 += 10;
          chPtr4 += 10;
          length -= 10;
        }
        while (length > 0 && *(int*)chPtr3 == *(int*)chPtr4)
        {
          chPtr3 += 2;
          chPtr4 += 2;
          length -= 2;
        }
        return length <= 0;
      }
    }

  }
}
