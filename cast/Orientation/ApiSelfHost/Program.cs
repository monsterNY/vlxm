using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.Filters;
using System.Web.Http.Routing;
using System.Web.Http.SelfHost;

namespace ApiSelfHost
{
  class Program:ActionFilterAttribute
  {
    static void Main(string[] args)
    {

      var port = "5050";
      var config = new HttpSelfHostConfiguration(string.Format("http://localhost:{0}", port ?? "8080"));

      //      config.Routes.MapHttpRoute(
      //        "API Default", "api/{controller}/{id}",
      //        new {id = RouteParameter.Optional});

      config.MapHttpAttributeRoutes(); // 支持路由特性设置，与支持http谓词无关

      config.Routes.MapHttpRoute(
        name: "API Default",
        routeTemplate: "{controller}/{id}",
        defaults: new {id = RouteParameter.Optional}
      );

      using (HttpSelfHostServer server = new HttpSelfHostServer(config))
      {
        server.OpenAsync().Wait();
        Console.WriteLine("Press ESC to quit.");
        while (
          !Console.ReadKey().Key.Equals(ConsoleKey.Escape))
        {
        }
      }
    }
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public static class HttpRouteCollectionExtensions
  {
    /// <summary>映射指定的路由模板。</summary>
    /// <returns>对映射路由的引用。</returns>
    /// <param name="routes">应用程序的路由的集合。</param>
    /// <param name="name">要映射的路由的名称。</param>
    /// <param name="routeTemplate">路由的路由模板。</param>
    public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate)
    {
      return routes.MapHttpRoute(name, routeTemplate, (object)null, (object)null, (HttpMessageHandler)null);
    }

    /// <summary>映射指定的路由模板并设置默认路由值。</summary>
    /// <returns>对映射路由的引用。</returns>
    /// <param name="routes">应用程序的路由的集合。</param>
    /// <param name="name">要映射的路由的名称。</param>
    /// <param name="routeTemplate">路由的路由模板。</param>
    /// <param name="defaults">一个包含默认路由值的对象。</param>
    public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults)
    {
      return routes.MapHttpRoute(name, routeTemplate, defaults, (object)null, (HttpMessageHandler)null);
    }

    /// <summary>映射指定的路由模板并设置默认路由值和约束。</summary>
    /// <returns>对映射路由的引用。</returns>
    /// <param name="routes">应用程序的路由的集合。</param>
    /// <param name="name">要映射的路由的名称。</param>
    /// <param name="routeTemplate">路由的路由模板。</param>
    /// <param name="defaults">一个包含默认路由值的对象。</param>
    /// <param name="constraints">一组表达式，用于指定 <paramref name="routeTemplate" /> 的值。</param>
    public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints)
    {
      return routes.MapHttpRoute(name, routeTemplate, defaults, constraints, (HttpMessageHandler)null);
    }

    /// <summary>映射指定的路由模板并设置默认的路由值、约束和终结点消息处理程序。</summary>
    /// <returns>对映射路由的引用。</returns>
    /// <param name="routes">应用程序的路由的集合。</param>
    /// <param name="name">要映射的路由的名称。</param>
    /// <param name="routeTemplate">路由的路由模板。</param>
    /// <param name="defaults">一个包含默认路由值的对象。</param>
    /// <param name="constraints">一组表达式，用于指定 <paramref name="routeTemplate" /> 的值。</param>
    /// <param name="handler">请求将被调度到的处理程序。</param>
    public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, HttpMessageHandler handler)
    {
      if (routes == null)
        throw new ArgumentNullException(nameof(routes));
      HttpRouteValueDictionary routeValueDictionary1 = new HttpRouteValueDictionary(defaults);
      HttpRouteValueDictionary routeValueDictionary2 = new HttpRouteValueDictionary(constraints);
      IHttpRoute route = routes.CreateRoute(routeTemplate, (IDictionary<string, object>)routeValueDictionary1, (IDictionary<string, object>)routeValueDictionary2, (IDictionary<string, object>)null, handler);
      routes.Add(name, route);
      return route;
    }

    /// <summary> 映射指定的路由以处理 HTTP 批请求。</summary>
    /// <param name="routes">应用程序的路由的集合。</param>
    /// <param name="routeName">要映射的路由的名称。</param>
    /// <param name="routeTemplate">路由的路由模板。</param>
    /// <param name="batchHandler">用于处理批请求的 <see cref="T:System.Web.Http.Batch.HttpBatchHandler" />。</param>
    public static IHttpRoute MapHttpBatchRoute(this HttpRouteCollection routes, string routeName, string routeTemplate, HttpBatchHandler batchHandler)
    {
      return routes.MapHttpRoute(routeName, routeTemplate, (object)null, (object)null, (HttpMessageHandler)batchHandler);
    }

    /// <summary>忽略指定的路由。</summary>
    /// <returns>返回 <see cref="T:System.Web.Http.Routing.IHttpRoute" />。</returns>
    /// <param name="routes">应用程序的路由的集合。</param>
    /// <param name="routeName">要忽略的路由的名称。</param>
    /// <param name="routeTemplate">路由的路由模板。</param>
    public static IHttpRoute IgnoreRoute(this HttpRouteCollection routes, string routeName, string routeTemplate)
    {
      return routes.IgnoreRoute(routeName, routeTemplate, (object)null);
    }

    /// <summary>忽略指定的路由。</summary>
    /// <returns>返回 <see cref="T:System.Web.Http.Routing.IHttpRoute" />。</returns>
    /// <param name="routes">应用程序的路由的集合。</param>
    /// <param name="routeName">要忽略的路由的名称。</param>
    /// <param name="routeTemplate">路由的路由模板。</param>
    /// <param name="constraints">一组表达式，用于指定路由模板的值。</param>
    public static IHttpRoute IgnoreRoute(this HttpRouteCollection routes, string routeName, string routeTemplate, object constraints)
    {
      if (routes == null)
        throw new ArgumentNullException(nameof(routes));
      if (routeName == null)
        throw new ArgumentNullException(nameof(routeName));
      if (routeTemplate == null)
        throw new ArgumentNullException(nameof(routeTemplate));
      HttpRouteCollectionExtensions.IgnoreHttpRouteInternal httpRouteInternal = new HttpRouteCollectionExtensions.IgnoreHttpRouteInternal(routeTemplate, new HttpRouteValueDictionary(constraints), (HttpMessageHandler)new StopRoutingHandler());
      routes.Add(routeName, (IHttpRoute)httpRouteInternal);
      return (IHttpRoute)httpRouteInternal;
    }

    private sealed class IgnoreHttpRouteInternal : HttpRoute
    {
      public IgnoreHttpRouteInternal(string routeTemplate, HttpRouteValueDictionary constraints, HttpMessageHandler handler)
        : base(routeTemplate, (HttpRouteValueDictionary)null, constraints, (HttpRouteValueDictionary)null, handler)
      {
      }

      public override IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
      {
        return (IHttpVirtualPathData)null;
      }
    }
  }

}