using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisDemo.Demo;
using StackExchange.Redis;

namespace RedisDemo
{
  class Program
  {
    static void Main(string[] args)
    {

      BaseDemo demo = new BaseDemo();

//      demo.TestConn();
//
//      demo.StringDemo();

      demo.FriendDemo();

      Console.WriteLine("Hello World!");

      Console.ReadKey(true);

    }
  }
}
