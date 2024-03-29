## redis的应用 ##

### 支持的数据类型 ###

- string

	> string 是 redis 最基本的类型，你可以理解成与 Memcached 一模一样的类型，一个 key 对应一个 value。

	> string 类型是二进制安全的。意思是 redis 的 string 可以包含任何数据。比如jpg图片或者序列化的对象。

	> string 类型是 Redis 最基本的数据类型，**string 类型的值最大能存储 512MB**。

- hash

	> 每个 hash 可以存储 232 -1 键值对（40多亿）。

- list

	> 列表最多可存储 232 - 1 元素 (4294967295, 每个列表可存储40多亿)。

- set

	> Redis的Set是string类型的无序集合。

	> 集合是通过哈希表实现的，所以添加，删除，查找的复杂度都是O(1)。

	> 集合中最大的成员数为 232 - 1(4294967295, 每个集合可存储40多亿个成员)。
	
- zset(sort set)

	> Redis zset 和 set 一样也是string类型元素的集合,且不允许重复的成员。
	
	> 不同的是每个元素都会关联一个double类型的分数。redis正是通过分数来为集合中的成员进行从小到大的排序。

	> zset的成员是唯一的,但分数(score)却可以重复。


| 类型 | 简介 | 特性 | 场景 |
| :------: | :------: | :------ | :------ |
| String(字符串) | 二进制安全 | 可以包含任何数据，比如jpg图片或者序列化的对象，一个键最大能存储512M | --- |
| Hash(字典) | 键值对集合，即编程语言中的Map类型 | 适合存储对象，并且可以像数据库中update一个属性一样只修改某一项属性值(Memcached中需要取出整个字符串反序列成对象修改完再序列化存回去) | 存储、读取、修改用户属性 |
| List(列表) | 链表(双向链表) | 增删块，提供了操作某一段元素的API | 1.最新消息排行等功能(比如朋友圈的时间线) 2.消息队列 |
| Set(集合) | 哈希表实现，元素不重复 | 1.添加、删除、查找的复杂度都是O(1) 2.为集合提供了求交集、并集、差集等操作 | 1.共同好友 2.利用唯一性，统计访问网站的所有独立ip 3.好友推荐时，根据tag求交集，大于某个阈值就可以推荐 |
| Sorted Set(有序集合) | 将Set中的元素增加一个权重参数score,元素按score有序排列 | 数据插入集合时，已经进行天然排序 | 1.排行榜 2.带权重的消息队列 |

> **注意：Redis支持多个数据库，并且每个数据库的数据是隔离的不能共享，并且基于单机才有，如果是集群就没有数据库的概念。**

> Redis是一个字典结构的存储服务器，而实际上一个Redis实例提供了多个用来存储数据的字典，客户端可以指定将数据存储在哪个字典中。这与我们熟知的在一个关系数据库实例中可以创建多个数据库类似，所以可以将其中的每个字典都理解成一个独立的数据库。

> 每个数据库对外都是一个从0开始的递增数字命名，Redis默认支持16个数据库（可以通过配置文件支持更多，无上限），可以通过配置databases来修改这一数字。客户端与Redis建立连接后会自动选择0号数据库，不过可以随时使用SELECT命令更换数据库

> 然而这些以数字命名的数据库又与我们理解的数据库有所区别。首先Redis不支持自定义数据库的名字，每个数据库都以编号命名，开发者必须自己记录哪些数据库存储了哪些数据。另外Redis也不支持为每个数据库设置不同的访问密码，所以一个客户端要么可以访问全部数据库，要么连一个数据库也没有权限访问。最重要的一点是多个数据库之间并不是完全隔离的，比如FLUSHALL命令可以清空一个Redis实例中所有数据库中的数据。综上所述，**这些数据库更像是一种命名空间，而不适宜存储不同应用程序的数据。**比如可以使用0号数据库存储某个应用生产环境中的数据，使用1号数据库存储测试环境中的数据，但不适宜使用0号数据库存储A应用的数据而使用1号数据库B应用的数据，不同的应用应该使用不同的Redis实例存储数据。由于Redis非常轻量级，**一个空Redis实例占用的内在只有1M左右**，所以不用担心多个Redis实例会额外占用很多内存。

#### 事务相关 ####

> 单个 Redis 命令的执行是原子性的，但 Redis 没有在事务上增加任何维持原子性的机制，所以 Redis 事务的执行并不是原子性的。



> **事务可以理解为一个打包的批量执行脚本**，但批量指令并非原子化的操作，中间某条指令的失败不会导致前面已做指令的回滚，也不会造成后续的指令不做。

相对于 sqlserver 的 GO (批处理) 

#### Redis 脚本 [了解] ####

Redis 脚本使用 Lua 解释器来执行脚本。 Redis 2.6 版本通过内嵌支持 Lua 环境。执行脚本的常用命令为 EVAL。

### 服务器命令 ###


#### BGSAVE ####

[https://www.runoob.com/redis/server-bgsave.html](https://www.runoob.com/redis/server-bgsave.html "BGSAVE")

在后台异步保存当前数据库的数据到磁盘

#### SAVE ####

[https://www.runoob.com/redis/server-save.html](https://www.runoob.com/redis/server-save.html "SAVE")

同步保存数据到硬盘

#### SHUTDOWN [NOSAVE] [SAVE]  ####

[https://www.runoob.com/redis/server-shutdown.html](https://www.runoob.com/redis/server-shutdown.html "Redis Shutdown")

异步保存数据到硬盘，并关闭服务器

### Redis 客户端连接 ###

Redis 通过监听一个 TCP 端口或者 Unix socket 的方式来接收来自客户端的连接，当一个连接建立后，Redis 内部会进行以下一些操作：

- 首先，客户端 socket 会被设置为非阻塞模式，因为 Redis 在网络事件处理上采用的是非阻塞多路复用模型。

- 然后为这个 socket 设置 TCP_NODELAY 属性，禁用 Nagle 算法

- 然后创建一个可读的文件事件用于监听这个客户端 socket 的数据发送

### Redis管道技术 ###

Redis是一种基于客户端-服务端模型以及请求/响应协议的TCP服务。这意味着通常情况下一个请求会遵循以下步骤：

- 客户端向服务端发送一个查询请求，并监听Socket返回，通常是以阻塞模式，等待服务端响应。

- 服务端处理命令，并将结果返回给客户端。

Redis 管道技术可以在服务端未响应时，客户端可以继续向服务端发送请求，并最终一次性读取所有服务端的响应。

#### 管道技术的优势 ####

管道技术最显著的优势是提高了 redis 服务的性能。

### Redis分区 ###

分区是分割数据到多个Redis实例的处理过程，因此每个实例只保存key的一个子集。

#### 分区的优势 ####

- 通过利用多台计算机内存的和值，允许我们构造更大的数据库。
- 通过多核和多台计算机，允许我们扩展计算能力；通过多台计算机和网络适配器，允许我们扩展网络带宽。

#### 分区的劣势 ####

redis的一些特性在分区方面表现的不是很好：

- 涉及多个key的操作通常是不被支持的。举例来说，当两个set映射到不同的redis实例上时，你就不能对这两个set执行交集操作。
- 涉及多个key的redis事务不能使用。
- 当使用分区时，数据处理较为复杂，比如你需要处理多个rdb/aof文件，并且从多个实例和主机备份持久化文件。
- 增加或删除容量也比较复杂。redis集群大多数支持在运行时增加、删除节点的透明数据平衡的能力，但是类似于客户端分区、代理等其他系统则不支持这项特性。然而，一种叫做presharding的技术对此是有帮助的。

#### 分区类型 ####

Redis 有两种类型分区。 假设有4个Redis实例 R0，R1，R2，R3，和类似user:1，user:2这样的表示用户的多个key，对既定的key有多种不同方式来选择这个key存放在哪个实例中。也就是说，有不同的系统来映射某个key到某个Redis服务。

1. 范围分区

	最简单的分区方式是按范围分区，就是映射一定范围的对象到特定的Redis实例。
	
	比如，ID从0到10000的用户会保存到实例R0，ID从10001到 20000的用户会保存到R1，以此类推。
	
	这种方式是可行的，并且在实际中使用，不足就是要有一个区间范围到实例的映射表。这个表要被管理，同时还需要各 种对象的映射表，通常对Redis来说并非是好的方法

2. 哈希分区

	另外一种分区方法是hash分区。这对任何key都适用，也无需是object_name:这种形式，像下面描述的一样简单：

	- 用一个hash函数将key转换为一个数字，比如使用crc32 hash函数。对key foobar执行crc32(foobar)会输出类似93024922的整数。
	
	- 	对这个整数取模，将其转化为0-3之间的数字，就可以将这个整数映射到4个Redis实例中的一个了。93024922 % 4 = 2，就是说key foobar应该被存到R2实例中。注意：取模操作是取除的余数，通常在多种编程语言中用%操作符实现。

----------

[https://www.runoob.com/redis/redis-data-types.html](https://www.runoob.com/redis/redis-data-types.html "bird course")