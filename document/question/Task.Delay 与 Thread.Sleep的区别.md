
1. Thread.Sleep()是同步延迟，Task.Delay()是异步延迟。
1. Thread.Sleep()会阻塞线程，Task.Delay()不会。
1. Thread.Sleep()不能取消，Task.Delay()可以。
1. Task.Delay()实质创建一个运行给定时间的任务，Thread.Sleep()使当前线程休眠给定时间。
1. 反编译Task.Delay()，基本上讲它就是个包裹在任务中的定时器。
1. Task.Delay()和Thread.Sleep()最大的区别是Task.Delay()旨在异步运行，在同步代码中使用Task.Delay()是没有意义的；在异步代码中使用Thread.Sleep()是一个非常糟糕的主意。通常使用await关键字调用Task.Delay()。
1. 我的理解：Task.Delay()，async/await和CancellationTokenSource组合起来使用可以实现可控制的异步延迟。

[https://blog.csdn.net/chenweicode/article/details/91372281](https://blog.csdn.net/chenweicode/article/details/91372281 "C#中的Task.Delay()和Thread.Sleep()")