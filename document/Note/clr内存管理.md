
何谓程序集（Assembly）？

> 它是一个托管应用的基本的部署单元。一个程序集是自描述的（通过元数据）、能够实施版本策略和部署策略。我倾向于这样的方式来定义程序集：“Assembly is a reusable, versionable, and self-describing building block of a CLR application.”

> 从结构组成来看，一个程序集主要由三个部署组成：IL指令、元数据和资源。

什么是应用程序域？

> 从功能上讲，通过应用程序域实现的隔离机制为托管代码的执行提供了一个安全的边界。

> 从与程序集的关系来讲，我们可以将应用程序域看成是加载程序集的容器。

> 只有相关的程序集被CLR加载到相应的应用程序域中，才谈得上代码的执行。

当托管应用被启动后，在执行第一句代码之前，CLR会先后为我们创建三个应用程序域：系统程序域（System Domain）、共享程序域（Shared Domain）和默认程序域（Default Domain）

- 系统程序域：系统程序域是第一个被创建的应用程序域，同时也是其他两个应用程序域的创建者。在该程序域初始化过程中，由它将msCorLib.dll这个程序集（这是一个很重要的程序集，.NET类型系统最基本的类型定义其中）加载到共享程序域中。此外，驻留的字符串也被保存在此系统程序域中。系统程序域的一个主要的任务是追踪其他所有应用程序域的状态，并负责加载和卸载它们；
- 共享程序域：共享程序域主要用于保存以“中立域（Domain-neutral Domain ）”加载的程序集容器。所谓“中立域 ”方式加载的程序集，就是说程序集并不被加载到当前的程序域中并被该程序域专用，而是加载到一个公共的程序域中被所有程序域共享。
- 默认程序域：我们的托管程序最终就运行在该程序域中，默认程序域可以通过System.AppDomain表示。

基于应用程序域的隔离，归根结底是内存的隔离。

一个基本的反映就是：在一个应用程序域中创建的对象，不能直接在另一个应用程序域中使用。这中间需要有一个基本的跨应用程序域传递的机制，我们将这种机制称之为“封送（Marshaling）”。

具体来讲，又具有两种不同的封送方式：按值封送（MBV：Marshaling By Value ）和按引用封送（MBR：Marshaling By Reference）。MBV主要采用序列化的方式，而MBR最典型的就是.ENT Remoting。

> 以中立域的方式加载msCorLib.dll这个程序集，但是这不是程序集默认采用的加载方式。在默认的情况下，程序集被加载到当前的程序域中，供该程序集独占使用。我个人将这两种不同的程序集加载方式称为：独占加载（Exclusive Loading ）和共享加载（Shared Loading）

我们自定义的程序集可以像msCorLib.dll一样以中立域的方式共享加载吗？

> 对于控制台应用，你只需要在Main方法上应用LoaderOptimizationAttribute特性，并指定LoaderOptimization为MultiDomain即可。


----------

since:5/31/2019 9:58:52 AM 

source:[https://www.cnblogs.com/artech/archive/2010/10/18/CLR_Memory_Mgt_01.html](https://www.cnblogs.com/artech/archive/2010/10/18/CLR_Memory_Mgt_01.html "关于CLR内存管理")