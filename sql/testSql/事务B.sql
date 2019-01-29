--2.隔离级别的分类
--（1）未提交读 （READ UNCOMMITTED）

--（2）已提交读（READ COMMITTED）（默认值）

--（3）可重复读（REPEATABLE READ）

--（4）可序列化（SERIALIZABLE）

--（5）快照（SNAPSHOT）

--（6）已经提交读快照（READ_COMMITTED_SNAPSHOT）

--BEGIN TRANSACTION

--	--未提交读
--	SET TRAN ISOLATION LEVEL READ UNCOMMITTED

--	SELECT * FROM Product--与事务A各阶段显示相同
--	WHERE Id = 1 

--（1）读操作可以读取未提交的修改（也称为脏读）。

--（2）读操作不会妨碍写操作请求排他锁，其他事务正在进行读操作时，写操作可以同时对这些数据进行修改。

--（3）事务A进行了多次修改，事务B在不同阶段进行查询时可能会有不同的结果。
--COMMIT TRANSACTION



--BEGIN TRANSACTION

--	--已提交读【默认级别】
--	SET TRAN ISOLATION LEVEL READ COMMITTED

--	--读取获取共享锁  
--	--当查询信息具有排他锁 需等待至排他锁结束
--	SELECT * FROM Product--与事务A各阶段显示相同
--	WHERE Id = 1 

----（1）必须获得共享锁才能进行读操作，其他事务如果对该资源持有排他锁，则共享锁必须等待排他锁释放。

----（2）读操作不能读取未提交的修改，读操作读取到的数据是提交过的修改。

----（3）读操作不会在事务持续期间内保留共享锁，其他事务可以在两个读操作之间更改数据资源，读操作因而可能每次得到不同的取值。这种现象称为“不可重复读”
	
--COMMIT TRANSACTION

--★ 3.可重复读（REPEATABLE READ）
--BEGIN TRANSACTION

--	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
--	SELECT * FROM Product WHERE Id = 1

--（1）必须获得共享锁才能进行读操作，获得的共享锁将一直保持直到事务完成之止。

--（2）在获得共享锁的事务完成之前，没有其他事务能够获得排他锁修改这一数据资源，这样可以保证实现可重复的读取。

--（3）两个事务在第一次读操作之后都将保留它们获得的共享锁，所以任何一个事务都不能获得为了更新数据而需要的排他锁，这种情况将会导致死锁（deadlock），不过却避免了更新冲突。

--COMMIT TRANSACTION

--★ 4.可序列化（SERIALIZABLE）
BEGIN TRANSACTION

	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	SELECT * FROM Product WHERE Id = 1

--“可序列化（SERIALIZABLE）”隔离级别的含义：

--（1）必须获得共享锁才能进行读操作，获得的共享锁将一直保持直到事务完成之止。

--（2）在获得共享锁的事务完成之前，没有其他事务能够获得排他锁修改这一数据资源，且当其他事务增加能够满足当前事务的读操作的查询搜索条件的新行时，其他事务将会被阻塞，直到当前事务完成然后释放共享锁，其他事务才能获得排他锁进行插入操作。

--（3）事务中的读操作在任何情况下读取到的数据是一致的，不会出现幻影行（幻读）。

--（4）范围锁：读操作锁定满足查询搜索条件范围的锁

COMMIT TRANSACTION
