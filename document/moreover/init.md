
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