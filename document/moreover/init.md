
1. install mysql
	
		官网安装

		mysql server
		mysql for visual studio
		mysql for .net

2. .net core web 启动出错：缺少iis配置....

		vs安装 .net core 1.0

3. 解决在iis上调试

		...只解决了部署在iis上 【也足够了:)】

![搬运工,确定需要这个 need this !!! ,ctrl + F5 over~](https://images2018.cnblogs.com/blog/1113623/201803/1113623-20180311185158792-1385873090.png)

[https://www.jianshu.com/p/c4ca2989d26a](https://www.jianshu.com/p/c4ca2989d26a "dapper的使用")
		
4. 反射获取枚举特性

	
		public static T GetAttribute<T>(this Enum info) where T : Attribute
	    {
	      var fieldInfo = info.GetType().GetField(info.ToString(),BindingFlags.Static | BindingFlags.Public);
	
	      Trace.WriteLine(fieldInfo);
	
	      var customAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DealAttribute), false);
	
	      Trace.WriteLine(customAttribute);
	
	      return customAttribute as T;
	
	    }

5.解决.net core 跨域：

	<step1:startUp>

		services.AddCors(options =>
	          options.AddPolicy("AllowCors",
	            builder =>
	            {
	              builder.AllowAnyOrigin() //允许任何来源的主机访问
	                .AllowAnyMethod()
	                .AllowAnyHeader()
	                .AllowCredentials(); //指定处理cookie
	            })
	        );
	    }

	<step2:在需要使用跨域的控制器上>
		[EnableCors("AllowCors")]

6.react发送请求：
	
	import axios from 'axios';

	axios
      .post(baseUrl, paramData)
      .then((response) => {
        if (response.data.errorCode === code.successCode) {
          // response.joson().then((data) => {
          console.log(response.data.result);
          this.setState({
            dataSource: response.data.result,
          });
          // });
        } else {
          console.log(response);
        }

      })
      .catch((error) => {
        console.log(error);
      });

7.查看被占用文件夹的进程

	开始》资源监视器》在关联的句柄中搜索文件夹

8.vlxm 漫长 晓梦