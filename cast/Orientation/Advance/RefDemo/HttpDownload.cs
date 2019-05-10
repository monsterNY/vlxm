using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advance.RefDemo
{
  /// <summary>
  /// @desc : HttpDownload  
  /// @author : mons
  /// @create : 2019/5/10 16:29:48 
  /// @source : https://www.cnblogs.com/huangxincheng/archive/2012/05/20/2509715.html
  /// </summary>
  public class HttpDownload
  {
    public CountdownEvent cde = new CountdownEvent(0);

    //每个线程下载的字节数，方便最后合并
    public ConcurrentDictionary<long, byte[]> dic = new ConcurrentDictionary<long, byte[]>();

    //请求文件
    public const string url = "https://images5.alphacoders.com/551/551139.jpg";

    public void Run()
    {
      for (int i = 1; i < 6; i++)
      {
        Console.WriteLine("\n****************************\n第{0}次比较\n****************************", i);

        //不用线程
        //RunSingle();

        //使用多线程
        RunMultiTask(i);
      }

      Console.Read();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="threadCount">开多少线程</param>
    void RunMultiTask(int threadCount)
    {
      Stopwatch watch = Stopwatch.StartNew();

      long start = 0;

      long end = 0;

      var total = GetSourceHead();

      if (total == 0)
        return;

      var pageSize = (int) Math.Ceiling((Double) total / threadCount);

      cde.Reset(threadCount);

      Task[] tasks = new Task[threadCount];

      for (int i = 0; i < threadCount; i++)
      {
        start = i * pageSize;

        end = (i + 1) * pageSize - 1;

        if (end > total)
          end = total;

        var obj = start + "|" + end;

        tasks[i] = Task.Factory.StartNew(j => new DownFile().DownTaskMulti(obj, url, dic, cde), obj);
      }

      Task.WaitAll(tasks);

      var targetFile = @"D:\work_y\test\" + url.Substring(url.LastIndexOf('/') + 1);

      FileStream fs = new FileStream(targetFile, FileMode.Create);

      var result = dic.Keys.OrderBy(i => i).ToList();

      foreach (var item in result)
      {
        fs.Write(dic[item], 0, dic[item].Length);
      }

      fs.Close();

      watch.Stop();

      Console.WriteLine("多线程：下载耗费时间:{0}", watch.Elapsed.TotalMilliseconds);
    }

    static void RunSingle()
    {
      Stopwatch watch = Stopwatch.StartNew();

      if (GetSourceHead() == 0)
        return;

      var request = (HttpWebRequest) HttpWebRequest.Create(url);

      var response = (HttpWebResponse) request.GetResponse();

      var stream = response.GetResponseStream();

      var outStream = new MemoryStream();

      var bytes = new byte[10240];

      int count = 0;

      while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
      {
        outStream.Write(bytes, 0, count);
      }

      var targetFile = "C://" + url.Substring(url.LastIndexOf('/') + 1);

      FileStream fs = new FileStream(targetFile, FileMode.Create);

      fs.Write(outStream.ToArray(), 0, (int) outStream.Length);

      outStream.Close();

      response.Close();

      fs.Close();

      watch.Stop();

      Console.WriteLine("不用线程：下载耗费时间:{0}", watch.Elapsed);
    }

    //获取头信息
    public static long GetSourceHead()
    {
      var request = (HttpWebRequest) HttpWebRequest.Create(url);

      request.Method = "Head";
      request.Timeout = 3000;

      var response = (HttpWebResponse) request.GetResponse();

      var code = response.StatusCode;

      if (code != HttpStatusCode.OK)
      {
        Console.WriteLine("下载的资源无效！");
        return 0;
      }

      var total = response.ContentLength;

      Console.WriteLine("当前资源大小为:" + total);

      response.Close();

      return total;
    }
  }

  public class DownFile
  {
    // 多线程下载
    public void DownTaskMulti(object obj, string url, ConcurrentDictionary<long, byte[]> dic, CountdownEvent cde)
    {
      var single = obj.ToString().Split('|');

      long start = Convert.ToInt64(single.FirstOrDefault());

      long end = Convert.ToInt64(single.LastOrDefault());

      var request = (HttpWebRequest) HttpWebRequest.Create(url);

      request.AddRange(start, end);

      var response = (HttpWebResponse) request.GetResponse();

      var stream = response.GetResponseStream();

      var outStream = new MemoryStream();

      var bytes = new byte[10240];

      int count = 0;

      while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
      {
        outStream.Write(bytes, 0, count);
      }

      outStream.Close();

      response.Close();

      dic.TryAdd(start, outStream.ToArray());

      cde.Signal();
    }
  }
}