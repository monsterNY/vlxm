


> IL:一种面对对象的机器语言

执行方法时，需要将方法的IL转换为本机CPU指令

这正是CLR JIT 的职责

> 概念词

- 枚举
- 数组
- 属性
- 索引器
- 委托
- 事件
- 构造器
- 终结器、析构函数
- 操作符重载
- 转换操作符

编译器直接支持的数据类型称为基元类型


----------
6/11/2019 11:11:04 AM 

#### 引用类型和值类型 ####

1. 内存必须从托管堆分配。
2. 堆上分配的每个对象都有一些额外成员，这些成员必须初始化
3. 对象中的其他字节(为字段而设)总是设为零。
4. 从托管堆分配对象时，可能强制执行一次回收

#### 装箱 ####

> 过程：

1. 在托管堆中分配内存。分配的内存量是值类型各字段所需的内存量，还要加上托管堆所有对象都有的两个额外成员(类型对象指针和同步索引块)所需的内存量。
2. 值类型的字段复制到新分配的堆内存。
3. 返回对象地址。

#### 拆箱 ####

> 拆箱其实就是获取指针的过程，该指针指向包含在一个对象中的原始值类型(数据字段)。其实，指针指向的是已装箱实例中的未装箱部分。
> 
> 所以和装箱不同，拆箱不要求在内存中复制任何字节

> 一次拆箱操作经常紧接着一次字段复制。

过程

1. is null 检查
2. cast 检查 ， 类型检查

$语法糖需要进行装箱，实际使用format

用基类（例如object）的方法时 需要进行装箱(注：虚方法若已重写则不需要)

typeof(结构) 不进行装箱，IL:

	IL_005b: ldtoken[System.Runtime]System.Int32
	IL_0060:  call       class [System.Runtime]
	System.Type[System.Runtime] System.Type::GetTypeFromHandle(valuetype[System.Runtime] System.RuntimeTypeHandle)

未装箱实例转换为接口时需要装箱

#### 相等性实现 ####

	bool Equals(object obj)

1. 如果obj为null返回false
2. 如果this 与 obj为引用同一对象 返回true
3. this 与 obj 引用对象的类型不同 返回false
4. 将this与obj对象中的每个值进行比较,不同则返回false
5. 调用基类的Equals方法来比较它定义的任何字段。


----------

note:

类型构造器：

	static class_name(){
		do something...
	}

> string的拼接实际上使用了concat方法。
> 
> 默认调用Concat(object,object,...) 存在装箱处理

> 由于未装箱值类型没有同步块索引，所以不能使用lock.

> 检查同一性应使用Object的ReferenceEquals。避免==重载

> == 用于相等性。

----------
since:6/10/2019 11:19:43 AM 

direction:reading