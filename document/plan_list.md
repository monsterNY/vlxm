1. 自旋 spin
1. Interlocked - 原子操作帮助类
1. 下载Rotor并配置
1. CancellationToken - 传播有关应取消操作的通知
1. SpinWait - 提供对基于自旋的等待的支持
1. PlatformHelper
1. TimeoutHelper
1. Thread
2. signal-信号
3. map _ 负载因子
4. 关于开放寻址、线性探测等内容，可以参考网上资料或者TAOCP（《计算机程序设计艺术》）第三卷的6.4章节。
5. Compare and Swap ( CAS )。 _https://www.cnblogs.com/qjjazry/p/6581568.html
6. TypeHandle
7. 引用类型，初始为NULL，4个字节，指向空地址；
8. （附加成员：4字节TypeHandle地址，4字节同步索引块）
9. LocalDataStoreSlot、ThreadStatic、ThreadLocal<T>、TLS、Lazy<T>
10. Scale Out（也就是Scale horizontally）横向扩展，向外扩展Scale Up（也就是Scale vertically）纵向扩展
11. 内容分发网络（英语：Content delivery network或Content distribution network，缩写：CDN）是指一种透过互联网互相连接的计算机网络系统，利用最靠近每位用户的服务器，更快、更可靠地将音乐、图片、影片、应用程序及其他文件发送给用户，来提供高性能、可扩展性及低成本的网络内容传递给用户。