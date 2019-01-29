--2.隔离级别的分类
--（1）未提交读 （READ UNCOMMITTED）

--（2）已提交读（READ COMMITTED）（默认值）

--（3）可重复读（REPEATABLE READ）

--（4）可序列化（SERIALIZABLE）

--（5）快照（SNAPSHOT）

--（6）已经提交读快照（READ_COMMITTED_SNAPSHOT）


--5.sys.dm_exec_requests 视图
--识别出阻塞链涉及到的会话、争用的资源、被阻塞会话等待了多长时间
--SELECT session_id FROM sys.dm_exec_sessions

--★ 1.sys.dm_tran_locks 视图

--（1）该动态视图可以查询出哪些资源被哪个进程ID锁了

--（2）查询出对资源授予或正在等待的锁模式

--（3）查询出被锁定资源的类型

--上面的查询语句3已经用到了这个视图，可以参考上图中的分析说明。

SELECT request_session_id AS 会话id ,
resource_type AS 请求锁定的资源类型 ,
resource_description AS 描述 ,
request_mode AS 模式 ,
request_status AS 状态
FROM sys.dm_tran_locks

--2.sys.dm_exec_connections 视图

--（1）查询出该动态视图可以查询出进程相关的信息

--（2）查询出最后一次发生读操作和写操作的时间last_read,last_write

--（3）查询出进程执行的最后一个SQL批处理的二进制标记most_recent_sql_handle

--SELECT  session_id ,
--        connect_time ,
--        last_read ,
--        last_write ,
--        most_recent_sql_handle
--FROM    sys.dm_exec_connections
 

-- dm_exec_sql_text

--（1）该函数可以将二进制标记most_recent_sql_handle作为参数，然后返回SQL代码。

--（2）阻塞进程在不断地运行，所以在代码中看到的最后一个操作不一定是导致问题的语句。在本例中最后一条执行语句是导致阻塞的语句。 

--SELECT  session_id ,
--        text
--FROM    sys.dm_exec_connections
--        CROSS APPLY sys.dm_exec_sql_text
--        (most_recent_sql_handle) AS ST

--结束会话
--KILL 52

--5.隔离级别总结

--脏读：读取未提交的更改。

--不可重复读：读操作不会在事务持续期间内保留共享锁，其他事务可以在两个读操作之间更改数据资源，读操作因而可能每次得到不同的取值。

--丢失更新：两个事务进行读操作，获得资源上的共享锁，读取完数据后，不再持有资源上的任何锁，两个事务都能更新这个值，

--　　　　最后进行更新的事务将会覆盖其他事务做的更改，导致其他事务更改的数据丢失。

--幻读：第一次和第二次读取到的数据行数不一致。

--范围锁：读操作锁定满足查询搜索条件范围的锁

--“死锁 （Dead Lock）”的一些注意事项：

--（1）如果两个事务没有设置死锁优先级，且两个事务进行的工作量也差不多一样时，任何一个事务都有可能被终止。

--（2）解除死锁要付出一定的系统开销，因为这个过程会涉及撤销已经执行过的处理。

--（3）事务处理的时间时间越长，持有锁的时间就越长，死锁的可能性也就越大，应该尽可能保持事务简短，把逻辑上可以不属于同一个工作单元的操作移到事务以外。

--（4）上面的例子中，事务A和事务B以相反顺序访问资源，所以发生了死锁。如果两个事务按同样的顺序来访问资源，则不会发生这种类型的死锁。在不改变程序的逻辑情况下，可以通过交换顺序来解决死锁的问题。