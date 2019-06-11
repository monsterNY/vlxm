
### 寄宿 ###

> 寄宿(hosting)

作用：

> 寄宿使任何应用程序都能利用clr的功能

> 为应用程序提供了通过编程来进行自定义和扩展的能力

### AppDomain ###

> AppDomain - 应用程序域

> AppDomain是一组程序集的逻辑容器

✨程序集是存放执行代码的地方

AppDomain允许第三方、不受信任的代码在现有的进程中运行，而clr保证数据结构、代码和安全上下文不被滥用或破坏

> create?

1. clr com 服务器初始化时会创建一个AppDomain.称为"默认AppDomain"，仅当Windows进程终止时销毁

2. 使用非托管COM接口方法或托管类型方法的宿主还可要求CLR创建额外的AppDomain

> why need appdomain?

AppDomain是为了提供隔离而设计的

> function:

- 一个AppDomain的代码不能直接访问另外一个AppDomain的代码创建的对象

		在AppDomain中创建了一个对象后，此对象便被该AppDomain“拥有”。它的生存期不能超过创建它的AppDomain
	
		若需要跨AppDomain使用则需要使用“按引用封送”(marshal-by-reference)或“按值封送”(marshal-by-value)的语义

		这种隔离使得AppDomain很容易地从进程中卸载，而不会影响其他AppDomain正在运行的代码
 
		  var appDomain = AppDomain.CreateDomain("product");

	      appDomain.SetData("key",new InfoModel(1));
	
	      var data = AppDomain.CurrentDomain.GetData("key");
	
	      Console.WriteLine(data);

- AppDomain可以卸载

 
- AppDomain可以单独保护


- AppDomain可以单独配置

----------

### confirm ###

windows进程完全可以不加载CLR,只有在进程中执行托管代码时才进行加载。

----------

appdomain的功能在.net core 平台被移除

其他dll:Microsoft.Extensions.DependencyModel

常用方式：寄宿+AppDomain+程序集的加载和反射

- 部分内容在clr内存管理中有提到

----------
since:6/5/2019 10:37:18 AM 

direction:CLR寄宿