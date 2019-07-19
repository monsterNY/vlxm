为什么web api 可以自托管 而 mvc不行

> Webapi与MVC不同,是以独立于主机的方式编写的.这是在欧文之前,但显然他们预计欧文会迟早会发生.主机独立性主要表示在Webapi代码中的任何地方都不使用System.Web.这是System.Web,它完全依赖IIS,如果没有它就不行.这样一来,Webapi可以理论上托管在任何地方 – 一旦其他主机可用.
> Webhost(Microsoft.Owin.Host.SystemWeb,Microsoft.AspNet.WebApi.WebHost)是一个层次之间的较高级别的API(如Webapi)和IIS.由于Webapi最初是独立于主机,因此需要一个中间层来使其在特定主机(如IIS)上运行. Webapi for Webapi(Microsoft.AspNet.WebApi.WebHost)提供了这个层.后来还将有针对Owin(Microsoft.Owin.Host.SystemWeb)的Webhost层,这将允许托管IIS上的任何Owin兼容.
欧文来了最后.它基本上提供了一个抽象,理论上可以允许在任何主机上托管任何Owin兼容的应用程序,只要在owin和该主机之间有一层. Owin带有Webhost(Microsoft.Owin.Host.SystemWeb)(类似于Webapi与Webhost一起使用),允许O IIS应用程序在IIS上托管.它还带有自主(Microsoft.Owin.SelfHost),允许Owin应用程序托管在任何可执行文件中.就Webapi而言,Owin还与Oapi主机一起使用Webapi(Microsoft.AspNet.WebApi.Owin),它允许在Owin堆栈上运行WebApi.

以上所有这一切意味着在IIS上托管Webapi有两种不同的方式.使用Webapi WebHost可以在没有Owin的情况下完成,也可以使用Oapi Host for Webapi和使用Webhost for Owin来完成.

[https://codeday.me/bug/20181029/341001.html](https://codeday.me/bug/20181029/341001.html)

个人总结： System.Web 依赖于 IIS, System.Web.Mvc 依赖于System.Web,而 web api (System.Web.Http) 并不依赖于 System.Web 故而api不用依赖于 IIS ，便可以实现自托管

注： SignalR也支持自托管


----------

	HttpListenerException: 拒绝访问。

	AddressAccessDeniedException: HTTP 无法注册 URL http://+:5050/。进程不具有此命名空间的访问权限(有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkId=70353)。

异常处理： 使用管理员权限运行或打开项目。
