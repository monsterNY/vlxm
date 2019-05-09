
<style type="text/css">
pre {
  max-height: 180px;
}
</style>

# study #

## 线程 ##
> 线程的开销及调度

	主要包括线程内核对象、线程环境块、1M大小的用户模式栈、内核模式栈。
	其中用户模式栈对于普通的系统线程那1M是预留的，在需要的时候才会分配，但是对于CLR线程，那1M是一开始就分类了内存空间的。

> 补充一句，CLR线程是直接对应于一个Windows线程的。

![](https://images2015.cnblogs.com/blog/151257/201603/151257-20160321141550589-1339297361.png)

	还记得以前学校里学习计算机课程里讲到，计算机的核心计算资源就是CPU核心和CPU寄存器，这也就是线程运行的主要战场。操作系统中那么多线程（一般都有上千个线程，大部分都处于休眠状态），
	对于单核CPU，一次只能有一个线程被调度执行，那么多线程怎么分配的呢？Windows系统采用时间轮询机制，CPU计算资源以时间片(大约30ms)的形式分配给执行线程。

计算鸡资源（CPU核心和CPU寄存器）一次只能调度一个线程，具体的调度流程：

- 	把CPU寄存器内的数据保存到当前线程内部（线程上下文等地方），给下一个线程腾地方；
- 	线程调度：在线程集合里取出一个需要执行的线程；
- 	加载新线程的上下文数据到CPU寄存器；
- 	新线程执行，享受她自己的CPU时间片（大约30ms），完了之后继续回到第一步，继续轮回；

对于Thread的使用太简单了，这里就不重复了，总结一下线程的主要几点性能影响：

- 	线程的创建、销毁都是很昂贵的；
- 	线程上下文切换有极大的性能开销，当然假如需要调度的新线程与当前是同一线程的话，就不需要线程上下文切换了，效率要快很多；
- 	这一点需要注意，GC执行回收时，首先要（安全的）挂起所有线程，遍历所有线程栈（根），GC回收后更新所有线程的根地址，再恢复线程调用，线程越多，GC要干的活就越多；


当然现在硬件的发展，CPU的核心越来越多，多线程技术可以极大提高应用程序的效率。但这也必须在合理利用多线程技术的前提下，了线程的基本原理，然后根据实际需求，还要注意相关资源环境，如磁盘IO、网络等情况综合考虑。

## lock ##

> 常见混合锁

### SemaphoreSlim ###

> 表示对可同时访问资源或资源池的线程数加以限制的 Semaphore 的轻量替代。

Semaphore - 信号

### 基础使用: ###

	创建一个示例,initialCount表示初始可执行线程，maxCount表示最大可执行线程，maxCount >= initialCount
    //public SemaphoreSlim(int initialCount, int maxCount);
    SemaphoreSlim semaphore = new SemaphoreSlim(0, 3);

	//初始执行时，先wait，若semaphore存在可执行数量则直接执行，否则一直等待
	semaphore.Wait();

	//do something

	//释放资源并返回上一个可执行数量
	semaphore.Release();

	//一次释放多个信号
	//releaseCount 释放数量，	
    public int Release(int releaseCount);

1. 执行前 - 先查看是否有空位

2. 若有 - 则占取位置，并开始执行
	- 执行完毕，则离开

3. 若无 - 则等待空位，直到有人离开(释放)

	ManualResetEventSlim、Monitor、ReadWriteLockSlim

----------

source

[https://www.cnblogs.com/anding/p/5301754.html#undefined](https://www.cnblogs.com/anding/p/5301754.html#undefined)
