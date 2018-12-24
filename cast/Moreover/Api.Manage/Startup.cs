using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Model.Common.Cache;
using Model.Common.ConfigModels;

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
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      //将配置信息进行DI注入
      services.Configure<AppSetting>(Configuration.GetSection(nameof(AppSetting)));

      var requiredService = services.BuildServiceProvider().GetRequiredService<IOptionsMonitor<AppSetting>>();

      MemoryCache.GetInstance().TryWrite(nameof(AppSetting), requiredService.CurrentValue);

      //Cache.Add(requiredService.CurrentValue);//添加缓存

      //实时更新本地缓存
      requiredService.OnChange(((setting, s) =>
      {
        //Cache.Refresh();
        MemoryCache.GetInstance().TryWrite(nameof(AppSetting), setting);
      }));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "api/{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}