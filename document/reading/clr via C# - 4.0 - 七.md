
#### 已异步方式实现服务器 ####

- 要构建异步ASP.NET Web 窗体，在.aspx文件中添加 Async="true"网页指令，并参考System.Web.UI.Page的RegisterAsyncTask方法
- 要构建异步ASP.NET MVC 控制器，使你的控制器类从System.Web.Mvc.AsyncController派生，让操作方法返回一个Task<ActionResult>即可
- 要构建异步ASP.NET 处理程序，使你的类从System.Web.HttpTaskAsyncHandler派生, 重写其抽象ProcessRequestAsync 方法。
- 要构建异步WCF服务，将服务作为异步函数实现，让它返回Task或Task<TResult>。

----------

since:6/21/2019 3:46:20 PM 
