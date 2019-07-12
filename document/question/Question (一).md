1. asp.net mvc 与 api的区别

	1. Asp.Net MVC用于创建返回视图和数据的Web应用程序，但Asp.Net Web API用于创建完整的HTTP服务，只需返回数据而不是视图的简单方法。

	1. Web API有助于通过.NET Framework构建REST-ful服务，它还支持内容协商（它是关于决定客户端可接受的最佳响应格式数据。它可以是JSON，XML，ATOM或其他格式化数据），自托管不属于MVC。


	1. Web API还负责以特定格式返回数据，如JSON，XML或基于请求中的Accept标头的任何其他格式，您不必担心这一点。MVC仅使用JsonResult以JSON格式返回数据。


	1. 在Web API中，请求被映射到基于HTTP谓词的操作，但在MVC中，它被映射到操作名称。


	1. Asp.Net Web API是一个新的框架，是ASP.NET核心框架的一部分。Web API中存在的模型绑定，过滤器，路由和其他MVC功能与MVC不同，并且存在于新System.Web.Http程序集中。在MVC中，这些功能存在于其中。System.Web.Mvc因此，Web API也可以与Asp.Net一起使用，也可以作为独立的服务层使用。


	1. 您可以在单个项目中混合使用Web API和MVC控制器来处理高级AJAX请求，这些请求可能以JSON，XML或任何其他格式返回数据并构建完整的HTTP服务。通常，这将被称为Web API自托管。


	1. 如果您有混合的MVC和Web API控制器并且您想要实现授权，那么您必须为MVC创建两个过滤器，为Web API创建另一个过滤器，因为两者都不同。


	1. 此外，Web API是轻量级架构，除Web应用程序外，它还可以与智能手机应用程序一起使用。

	来源：

	[https://www.dotnettricks.com/learn/webapi/difference-between-aspnet-mvc-and-aspnet-web-api](https://www.dotnettricks.com/learn/webapi/difference-between-aspnet-mvc-and-aspnet-web-api)

2. redis如何实现持久化

	参考：
	
	[https://juejin.im/post/5b70dfcf518825610f1f5c16](https://juejin.im/post/5b70dfcf518825610f1f5c16 "redis的持久化原理")

3. 索引的高级应用


4. 如何提高存储过程的执行效率

	利用变量存储重复查询的数据，或是使用临时表操作

5. 异步的原理

	与同步的区别：

	- 并非一次完成，而且分多次完成
	- 并非由同一个线程完成，而是线程池每次动态分配一个线程来处理；

	结合这些特点，C#编译器将异步函数转换为一个状态机结构。这种结构能挂起和恢复。它的执行方式是一种工作流的方式。

	异步这个字眼儿就是说回调次序是灵活的、不确定的。
	

6. 创建异步线程的父线程，与执行异步的子线程是否可能为同一线程

	如果在子任务执行时，父线程空闲，父线程会回到线程池中，则可能出现父线程“复用”的情况。从而出现父线程与执行子任务的子线程为同一线程。

7. C#中常用的接口

	ICompare
		主要用于排序

	IEquatable
		用于比对值是否相等

	ICloneable
		对象复制

	IConvertible
		转换

	IEqualityComparer
		相等性与hashCode

	IEnumerable<T>

	IEnumerator<T>

	IQueryable<T>

	ICollection<T>

	IDictionary<TKey,TValue>

	IList<T>

	来源：

	[https://www.cnblogs.com/myrocknroll/p/7359916.html](https://www.cnblogs.com/myrocknroll/p/7359916.html "参考博文")

8. signalR的应用场景

	1. 支付回调
	2. 聊天室


----------


