using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Tools.CusTools;

namespace Spider
{
  class Program
  {
    static void Main(string[] args)
    {

      var url =
        "http://www.upup.sports.api/activity/5";

      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
      request.Method = "GET";
//      request.ContentType = "text/html;charset=UTF-8";

      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      Stream myResponseStream = response.GetResponseStream();
      StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
      string retString = myStreamReader.ReadToEnd();
      myStreamReader.Close();
      myResponseStream.Close();

      Console.WriteLine(retString);

//      ApiHelper.HttpGetAsync(url
//        ,
//        null, Encoding.UTF8).ContinueWith((
//        task =>
//        {
//          if (task.IsCompleted)
//            Console.WriteLine(task.Result);
//        }));

      Console.WriteLine("Hello World!");

      Console.ReadKey(true);
    }
  }
}