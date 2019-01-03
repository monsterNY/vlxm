using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Common.Cache;
using Model.Common.ConfigModels;
using Monster.AuthServer.CusConfig;
using Monster.AuthServer.CusInherit;
using NLog.Extensions.Logging;

namespace Monster.AuthServer
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      //注入配置信息
      services.Configure<AppSetting>(Configuration.GetSection(nameof(AppSetting)));

      var buildServiceProvider = services.BuildServiceProvider();

      //获取配置信息
      var optionsMonitor = buildServiceProvider.GetRequiredService<IOptionsMonitor<AppSetting>>();

      //将信息写入内存缓存
      MemoryCache.GetInstance().TryWrite(nameof(AppSetting), optionsMonitor.CurrentValue);

      optionsMonitor.OnChange(info =>
      {
        MemoryCache.GetInstance().TryWrite(nameof(AppSetting), optionsMonitor.CurrentValue);
      });

      // services.AddDbContext<MonsterEntities>(optionsBuilder => ConfigDbContext.GetInstance().Run(optionsBuilder, optionsMonitor.CurrentValue.ConnectionStrings.MainConnection));

      //            buildServiceProvider.GetService<MonsterEntities>();

      //添加中间件
      services
        .AddMvcCore() //添加mvc
        .AddAuthorization() //添加授权认证
        .AddJsonFormatters(); //添加json格式处理

      services
        .AddIdentityServer() //添加is
        .AddDeveloperSigningCredential() //设置为临时凭证

        //添加本地资源  (向ioc容器中添加对象)
        .AddInMemoryIdentityResources(AuthConfig.GetIdentityResources()) //添加内存_用户建模资源
        .AddInMemoryApiResources(AuthConfig.GetApiResources()) //添加api资源

        //将client 添加到唯一注入
        //并添加其他Transient依赖
        .AddInMemoryClients(AuthConfig.GetClients()) //添加内存_客户端

        //.AddTestUsers(Config.GetUsers())//添加测试用户
        .AddProfileService<CusProfileService>() //添加概要服务
        .AddResourceOwnerValidator<CusResourceOwnerPasswordValidator>(); //添加资源验证对象
      
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddNLog(); //添加nLog

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseIdentityServer();
    }
  }
}