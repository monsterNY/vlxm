
> SyncBlockIndex 同步索引块

### 简单概况： ###

	The DWORD(Syncblk) is called Object Header and holds an index (a 1-based syncblk number) into a SyncTableEntry table.
	DWORD(Syncblk)被称为对象标头，并将索引(基于1的Syncblk编号)保存到SyncTableEntry表中。

	As the chaining is through an index, the CLR can move the table around in memory while increasing the size as needed.
	由于链接是通过索引进行的，CLR可以在内存中移动表，同时根据需要增加表的大小。

	The SyncTableEntry maintains a weak reference back to the object so that the SyncBlock ownership can be tracked by the CLR.
	SyncTableEntry维护对对象的弱引用，以便CLR可以跟踪SyncBlock的所有权。

	Weak references enable the GC to collect the object when no other strong references exist.
	当不存在其他强引用时，弱引用使GC能够收集对象。

	SyncTableEntry also stores a pointer to SyncBlock that contains useful information, but is rarely needed by all instances of an object.
	SyncTableEntry还存储指向SyncBlock的指针，该指针包含有用的信息，但对象的所有实例很少需要它。

	This information includes the object's lock, its hash code, any thunking data, and its AppDomain index.
	这些信息包括对象的锁、哈希码、任何打雷数据和AppDomain索引。

	For most object instances, there will be no storage allocated for the actual SyncBlock and the syncblk number will be zero.
	对于大多数对象实例，没有为实际的SyncBlock分配存储，syncblk号为零。

	This will change when the execution thread hits statements like lock(obj) or obj.GetHashCode().
	当执行线程遇到lock(obj)或obj. gethashcode()等语句时，这种情况将发生变化。

### note ###

	数据类型“委托（Delegate）”，它可以被视为是一种类型安全的函数指针。

----------
author: monster

since:5/8/2019 3:03:59 PM 

direction: lock 学习(中)