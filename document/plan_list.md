1. 自旋 spin

	熟悉，已阅读了相关文章

1. Interlocked - 原子操作帮助类

	了解，尚未追踪到源码

1. 下载Rotor并配置
1. CancellationToken - 传播有关应取消操作的通知

	熟悉。实践通过CancellationTokenSource进行控制

1. SpinWait - 提供对基于自旋的等待的支持

	了解，无法追踪下去...		

1. PlatformHelper

	internal 外部无法调用...

1. TimeoutHelper

	同上	

1. Thread

	过。

2. signal-信号
	

3. map _ 负载因子

	(java)在HashMap中存在一个负载因子

	用于决定扩充，负载因子 * 容量 >= map大小

	> 负载因子本身就是在控件和时间之间的折衷。当我使用较小的负载因子时，虽然降低了冲突的可能性，使得单个链表的长度减小了，加快了访问和更新的速度，但是它占用了更多的控件，使得数组中的大部分控件没有得到利用，元素分布比较稀疏，同时由于Map频繁的调整大小，可能会降低性能。但是如果负载因子过大，会使得元素分布比较紧凑，导致产生冲突的可能性加大，从而访问、更新速度较慢。所以我们一般推荐不更改负载因子的值，采用默认值0.75.

4. 关于开放寻址、线性探测等内容，可以参考网上资料或者TAOCP（《计算机程序设计艺术》）第三卷的6.4章节。

	[http://www.voidcn.com/article/p-yxkoiyge-gr.html](http://www.voidcn.com/article/p-yxkoiyge-gr.html "开放寻址法(恶心)")

5. Compare and Swap ( CAS )。 

	[https://www.cnblogs.com/qjjazry/p/6581568.html](https://www.cnblogs.com/qjjazry/p/6581568.html)	
6. TypeHandle
7. 引用类型，初始为NULL，4个字节，指向空地址；
（附加成员：4字节TypeHandle地址，4字节同步索引块）
9. LocalDataStoreSlot、ThreadStatic、ThreadLocal<T>、TLS、Lazy<T>

	已分析，大概了解

	tls 线程本地存储（Thread Local Storage）

	

10. Scale Out（也就是Scale horizontally）横向扩展，向外扩展Scale Up（也就是Scale vertically）纵向扩展

	Scale Out 通过增加设备可以解决,更加广泛

	Scale Up 单项功能进行扩展,更加深入

11. 内容分发网络（英语：Content delivery network或Content distribution network，缩写：CDN）是指一种透过互联网互相连接的计算机网络系统，利用最靠近每位用户的服务器，更快、更可靠地将音乐、图片、影片、应用程序及其他文件发送给用户，来提供高性能、可扩展性及低成本的网络内容传递给用户。
12. Volatile

	已查阅，仅了解其效果

13. SparselyPopulatedArray<CancellationCallbackInfo> - source:CancellationToken