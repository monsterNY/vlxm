1. asp.net mvc 与 api的区别

	mvc 创建一个完整的web项目，并提供视图和绑定数据

	api 用于构建一个完整的http服务，仅提供数据

	mvc 提供 URI进行路由匹配，而api提供http谓词进行匹配，故api更有利于构建restful

	api 支持内容协商

	api 支持返回指定的数据格式(json/xml等)

	api 支持自托管(由于对于消息队列的优化)

	api 的相关包在 *System.Http*中 相较于 *System.Web* 更加轻量级。 故api中的拦截器、路由与mvc不同

	**System.Web.Http System.Web.Http**

	**此外，Web API是轻量级架构，除Web应用程序外，它还可以与智能手机应用程序一起使用。**

2. redis如何实现持久化

	持久化有*dba*和aof两种方式。

	可以通过配置触发和手动触发的方式进行持久化

	*dba*的配置是多少s中存在多少次操作则进行持久化

	aof的配置是每过多少s进行持久化一次

	*dba*是备份所有的数据

	**RDB 生成快照存储**

	aof是备份所有的操作指令，通过操作还原数据

	**记录每次对服务器写的操作,当服务器重启的时候会重新执行这些命令来恢复原始的数据。**

	两者都可配置异常时的处理(中断、发送日志且继续执行)

3. 索引的高级应用

4. 如何提高存储过程的执行效率

	避免重复查询(变量、临时表)

	对于筛选的列添加索引

	采用表函数简化操作

	逻辑优化

	**不要在where中包含子查询**

	**在过滤条件中，可以过滤掉最大数量记录的条件必须放在where子句的末尾;**

	**在WHERE中尽量不要使用OR**

	**’!=’ 将不使用索引;**

	**避免在索引列上使用IS NULL和IS NOT NULL;**

	**用IN来替代OR： WHERE LOC_ID=10 OR LOC_ID=15 OR LOC_ID=20**

	**尽量明确的完成SQL语句，尽量少让数据库工作。比如写SELECT语句时，需要把查询的字段明确指出表名。尽量不要使用SELECT *语句。组织SQL语句的时候，尽量按照数据库的习惯进行组织。**

5. 异步的原理

	与同步的区别：

		不是一步完成，而是分多步完成

		执行的线程可能有多个，具体线程由线程池决定

	故异步就是一个支持挂机/恢复的状态机

	**它的执行方式是一种工作流的方式。**

7. C#中常用的接口

	ICompare

	IEquatable

	ICollection

	ISet/IList/IDirectionary

	ICloneable

	**IEnumerable<T>**

	**IEnumerator<T>**

	**IQueryable<T>**

8. signalR的应用场景

	聊天室

	支付回调

----------
since:7/15/2019 3:06:51 PM 

over:7/15/2019 3:19:06 PM 