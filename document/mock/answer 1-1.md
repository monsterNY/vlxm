1. asp.net mvc 与 api的区别

mvc 用于构建一个完整的web应用，用于返回视图和数据

api 用于构建一个完整的http应用，仅返回数据

**api 构建 完整的HTTP服务**

api 支持内容协商

在路由方面，mvc通过名称匹配，api通过http谓词构建，故api有利于构建restful风格api

api支持自托管(由于对消息队列进行了优化)，而mvc仅能托管在iis中

api可以返回指定的数据格式(xml/json等)，而mvc仅能返回ActionResult

**已特定的格式返回数据**

api想较于mvc更加轻量级，其相关封装在System.http中 而 mvc在System.Web中，故api可以使用asp.net.

**asp.net web api是一个新的框架，是ASP.NET核心框架的一部分。Web API中存在的模型绑定，过滤器，路由和其他MVC功能与MVC不同，并且存在于新System.Web.Http程序集中。在MVC中，这些功能存在于System.Web.Mvc中，因此，Web API可以与ASP.Net 一起使用，也可以作为独立的服务层使用。**

若同时使用这两种，需要为mvc和api单独创建路由

**如果您有混合的MVC和Web Api控制器，并且您想要实现授权，那么您必须为MVC创建两个过滤器，为Web API创建另一个过滤器**

**Web API是轻量级架构，除Web应用程序外，它还可以与智能手机应用程序一起使用。**

2. redis如何实现持久化

	redis实现持久化的方式：

	RDB/*SOP*

	**RDB/AOF**

	两者都可以通过配置或是手动的方式进行持久化。

	其中 RDB 是将数据保存到磁盘中，*SOP*是将对数据的操作指令保存到磁盘中

	**AOF : 记录每次对服务器写的操作，当服务器重启的时候会重新执行这些命令来恢复原始的数据。**

**RDB的持久化配置： save {time} {count} 表示{time}s内如果有{count}条是写入命令，就触发产生一次快照，相对于进行一次备份**

**stop-writes-on-bgsave-error yes 非常重要的一项配置，当备份进程出错时，主进程就停止接收新的写入操作，是为了保护持久化的数据一致性问题。如果自己的业务有完善的监控系统，可以禁止此项配置，否则请开启。**

**rdbcompression yes ，建议不开启，毕竟Redis本身就属于CPU密集型服务器，再开启压缩会带来更多的CPU消耗，相比硬盘成本，CPU更值钱。**

**AOF的配置**

**appendfsync everysec 其值有三种模式：**

**always : 把每个命令都立即同步到aof，很慢，但是很安全**

**everysec : 每秒同步一次，是折中方案**

**no : redis不处理交给OS来处理，非常快，但是也最不安全**
	
**aof-load-truncated yes 启用时，在加载时发现aof尾部不正确时，会向客户端写入一个log,但是会继续执行，如果设置为no,发现错误就会停止，必须修复后才能重新加载。**

**自动触发RDB的场景：**

**根据我们的 save m n 配置规则自动触发**

**从节点全量复制时，主节点发送rdb文件给从节点完成复制操作，主节点会触发bgsave**

**执行 debug reload时**

**执行shutdown时，如果没有开启aof,也会触发**

**增量追加到文件这一步主要的流程是：命令写入=》追加到aof_buf =》同步到aof磁盘。**

3. 索引的高级应用

4. 如何提高存储过程的执行效率

	避免重复查询

	临时表、变量、表函数

	需要几列就取几列

	不要嵌套视图

	使用临时表来提高游标性能

	不要使用触发器

	[https://www.techug.com/post/17-ways-to-speed-your-sql-queries.html](https://www.techug.com/post/17-ways-to-speed-your-sql-queries.html "参考")

5. 异步的原理

	与同步的区别

	- 异步是分步执行的
	- 异步执行每一步可能不是同一线程，每次执行的线程是通过线程池分配的。

	由于异步可以*启动*和挂起，其实际上就是维护着一个状态机

	**挂起和恢复，状态机结构，执行方式是一种工作流的方式。**

7. C#中常用的接口

IList

ICompare

*IEqual*

**IEquatable**

**ICloneable**

**IEqualityComparer**

IQueryable

*IEnumable*

**IEnumerable**

*IEnumator*

**IEnumerator**

**ICollection**

ISet

IDirectionry



8. signalR的应用场景

	聊天室

	支付回调


----------

since:7/12/2019 2:21:47 PM 
