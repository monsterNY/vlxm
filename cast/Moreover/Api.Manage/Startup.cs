using Api.Manage.Assist.Extension;
using Api.Manage.Middleware;
using Command.RedisHelper.CusInhert;
using Command.RedisHelper.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Common.Cache;
using Model.Common.ConfigModels;
using Newtonsoft.Json;
using NLog.Extensions.Logging;

namespace Api.Manage
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(option =>
        {
//          option.Filters.Add(new TestAuthorizationFilter());
        })
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddJsonOptions(options =>
        {
          options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; //处理时间格式
          options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; //过滤空值 异常时无效...
        });

      //将配置信息进行DI注入
      services.Configure<AppSetting>(Configuration.GetSection(nameof(AppSetting)));

      var requiredService = services.BuildServiceProvider().GetRequiredService<IOptionsMonitor<AppSetting>>();

      MemoryCache.GetInstance().TryWrite(nameof(AppSetting), requiredService.CurrentValue);

      //Cache.Add(requiredService.CurrentValue);//添加缓存

      //实时更新本地缓存
      requiredService.OnChange((setting, s) =>
      {
        //Cache.Refresh();
        MemoryCache.GetInstance().TryWrite(nameof(AppSetting), setting);
      });

      //注入redis引用
//      services.AddSingleton<CusRedisHelper>(
//        new CusRedisHelper(requiredService.CurrentValue.GetRedisConn().ConnStr, "api", new NewtonsoftDeal(),
//          71));

//      MemoryCache.GetInstance().TryWrite(requiredService.CurrentValue.GetRedisConn().FlagKey, new CusRedisHelper(requiredService.CurrentValue.GetRedisConn().ConnStr, "api", new NewtonsoftDeal(),
//        71));


      //配置跨域处理，允许所有来源：
      services.AddCors(options =>
        {
          options.AddPolicy("AllowCors",
            builder =>
            {
              builder.AllowAnyOrigin() //允许任何来源的主机访问
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials(); //指定处理cookie
            });
        }
      );

      services
        .AddAuthentication("Bearer")
        .AddIdentityServerAuthentication(options =>
        {
          options.Authority = requiredService.CurrentValue.Authorize.Url; //令牌签发人的基本地址
//          options.Authority = "http://localhost:5000/";//令牌签发人的基本地址
          options.RequireHttpsMetadata = false; //是否使用https
          options.ApiName = "user_api"; //用于针对内省端点进行身份验证的API资源的名称
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddNLog(); //添加nLog

      app.UseMiddleware<HttpModuleMiddleware>(); //添加管道中间件

      app.UseMiddleware<ErrorHandlingMiddleware>(); //添加异常中间件

      if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

      app.UseStaticFiles();

      app.UseAuthentication(); //...真气人

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          "default",
          "api/{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}