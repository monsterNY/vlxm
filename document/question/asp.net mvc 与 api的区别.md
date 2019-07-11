## mvc 与 api的区别 ##

Asp.Net MVC用于创建返回视图和数据的Web应用程序，但Asp.Net Web API用于创建完整的HTTP服务，只需返回数据而不是视图。

Web API有助于在.NET Framework上构建REST-ful服务，它还支持内容协商（它是关于决定客户可以接受的最佳响应格式数据。它可能是JSON，XML，自托管，而不是在MVC中。

Web API还负责以特定格式返回数据，如JSON，XML或基于请求中的Accept标头的任何其他格式，您不必担心这一点。MVC仅使用JsonResult以JSON格式返回数据。

#### 内容协商 ####



> 是超文本传输协议（HTTP）中定义的一个机制，它使同一个统一资源标志符（URI）上的文档可以根据用户代理中指定的适用信息提供不同的版本。这种机制的传统用法是提供GIF或PNG格式的图像，为不支持显示PNG图像的浏览器（例如微软Internet Explorer 4）提供GIF版本的图像。

> 因此，一个资源可以有多种不同版本可用。例如，它可能有不同语言或不同多媒体格式的版本，或者其他用法。选择最合适版本的一种方法是为用户提供一个索引页面，由他们选择所需的版本。但在某些情况下，也可根据一些标准来自动选择。

HTTP提供了多种内容协商机制：服务器驱动（或称主动），用户代理驱动（或称被动），透明，及其他组合、配合。

[https://www.dotnettricks.com/learn/webapi/difference-between-aspnet-mvc-and-aspnet-web-api](https://www.dotnettricks.com/learn/webapi/difference-between-aspnet-mvc-and-aspnet-web-api "dotnetTricks")

1. Asp.Net MVC用于创建返回视图和数据的Web应用程序，但Asp.Net Web API用于创建完整的HTTP服务，只需返回数据而不是视图的简单方法。

1. Web API有助于通过.NET Framework构建REST-ful服务，它还支持内容协商（它是关于决定客户端可接受的最佳响应格式数据。它可以是JSON，XML，ATOM或其他格式化数据），自托管不属于MVC。


1. Web API还负责以特定格式返回数据，如JSON，XML或基于请求中的Accept标头的任何其他格式，您不必担心这一点。MVC仅使用JsonResult以JSON格式返回数据。


1. 在Web API中，请求被映射到基于HTTP谓词的操作，但在MVC中，它被映射到操作名称。


1. Asp.Net Web API是一个新的框架，是ASP.NET核心框架的一部分。Web API中存在的模型绑定，过滤器，路由和其他MVC功能与MVC不同，并且存在于新System.Web.Http程序集中。在MVC中，这些功能存在于其中。System.Web.Mvc因此，Web API也可以与Asp.Net一起使用，也可以作为独立的服务层使用。


1. 您可以在单个项目中混合使用Web API和MVC控制器来处理高级AJAX请求，这些请求可能以JSON，XML或任何其他格式返回数据并构建完整的HTTP服务。通常，这将被称为Web API自托管。


1. 如果您有混合的MVC和Web API控制器并且您想要实现授权，那么您必须为MVC创建两个过滤器，为Web API创建另一个过滤器，因为两者都不同。


1. 此外，Web API是轻量级架构，除Web应用程序外，它还可以与智能手机应用程序一起使用。

[https://www.cnblogs.com/cjm123/p/8067109.html](https://www.cnblogs.com/cjm123/p/8067109.html "博文")

Web Api除了扩展了前者以外，另外写出了一套独立的，独立于Asp .Net的消息处理管道，就像是借鉴原来房子的模型，重新设计出了另外一套别墅。这也很好地解释了为什么Web Api可以寄宿在不同的宿主上（寄宿的本质就是利用一个具体的应用程序为Web Api提供一个运行的环境，并解决请求的接收和响应的回复）