
## ASP.Net Web api ##

### web api 与 asp.net mvc的区别 ###

- mvc路由通过名称查找，api通过http谓词查找，故api更利于构建restful
- mvc是创建一个完整的web应用，返回视图和数据，api构建一个完整的http服务，仅返回数据。
- api是一个全新的框架，存在于System.Web.Http中
- api支持自托管，而mvc不支持
- api支持内容协商
- api支持返回指定数据格式(json/xml等)
- 此外，Web API是轻量级架构，除Web应用程序外，它还可以与智能手机应用程序一起使用。

### 使用helpPage构建帮助文档 ###

1. nuGet添加依赖包：Microsoft.AspNet.WebApi.HelpPage
2. 在Global注册所有area 

		AreaRegistration.RegisterAllAreas();
 
3. 在HelpPageConfig中开启xml文档注释

		config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));

4. 打开项目属性，找到Build/生成，开启XML 文档文件，值为App_Data/XmlDocument.xml
5. 访问 {domain_url:项目地址}/help/index即可查看到所有文档。

	helpPage使用System.Web.Mvc进行构建
	
	具体原理可查看HelpController中的方法获取
	
	AreaRegistration.RegisterAllAreas(); 通过Type和Route查找。

6. 添加测试工具

	NuGet添加Test Client

	在Areas\HelpPage\Views\Help\Api.cshtml添加以下代码：

		@Html.DisplayForModel("TestClientDialogs")
	
		@section Scripts {
	
	    　　<link type="text/css" href="~/Areas/HelpPage/HelpPage.css" rel="stylesheet" />
	
	    　　@Html.DisplayForModel("TestClientReferences")
	
		}

### Swashbuckle Help Page ###

1. 在NuGet添加Swashbuckle组件。

[https://www.cnblogs.com/Erik_Xu/p/5638381.html](https://www.cnblogs.com/Erik_Xu/p/5638381.html "帮助文档生成")


### Http生命周期 ###

[https://www.asp.net/media/4071077/aspnet-web-api-poster.pdf](https://www.asp.net/media/4071077/aspnet-web-api-poster.pdf "asp.net web api Http message 生命周期")

- 路由匹配
- 控制器处理
- 授权拦截
- 模型绑定
- Action 拦截与执行(OnActionExecuted,OnActionExecuting)
- 异常拦截
- Action 执行结束
- Result Conversion 结果会话



> DelegatingHandler 
> 
> 一种典型的 HTTP 处理程序委托给另一个处理程序，HTTP 响应消息的处理称为内部处理程序。

### 高并发处理 ###

- HTML静态化
- 图片服务器分离
- 数据库集群和库表散列
- 缓存
- 镜像
- 负载均衡

[https://blog.csdn.net/lgb861127/article/details/20628053](https://blog.csdn.net/lgb861127/article/details/20628053 "参考文章")

#### web api 配置支持Route特性路由 ####

	config.MapHttpAttributeRoutes();

注：未配置时Route特性无效....