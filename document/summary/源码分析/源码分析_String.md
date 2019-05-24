

> 认识string

首先需要明确的，string是一个引用类型，其对象值存储在托管堆中。string的内部是一个char集合，他的长度Length就是字符char数组的字符个数。string不允许使用new string()的方式创建实例，而是另一种更简单的语法，直接赋值（string aa= “000”这一点也类似值类型）。



> String的恒定性（不变性）

字符串是不可变的，字符串一经创建，就不会改变，任何改变都会产生新的字符串。

	string s1 = "a";
	string s2 = s1 + "b";

![](https://images2015.cnblogs.com/blog/151257/201603/151257-20160303222417112-1147871973.png)

上文中的”任何改变都会产生新的字符串“，包括字符串的一些操作函数，如str1.ToLower，Trim()，Remove(int startIndex, int count)，ToUpper()等，都会产生新的字符串，因此在很多编程实践中，对于字符串忽略大小的比较

> String的驻留性


由于字符串的不变性，在大量使用字符串操作时，会导致创建大量的字符串对象，带来极大的性能损失。因此CLR又给string提供另外一个法宝，就是字符串驻留

	var s1 = "123";
	var s2 = "123";
	Console.WriteLine(System.Object.Equals(s1, s2));  //输出 True
	Console.WriteLine(System.Object.ReferenceEquals(s1, s2));  //输出 True

相同的字符串在内存（堆）中只分配一次，第二次申请字符串时，发现已经有该字符串是，直接返回已有字符串的地址，这就是驻留的基本过程。



> 字符串驻留的基本原理：

	-   CLR初始化时会在内存中创建一个驻留池，内部其实是一个哈希表，存储被驻留的字符串和其内存地址。
	- 	驻留池是进程级别的，多个AppDomain共享。同时她不受GC控制，生命周期随进程，意思就是不会被GC回收（不回收！难道不会造成内存爆炸吗？不要急，且看下文）
	- 	当分配字符串时，首先会到驻留池中查找，如找到，则返回已有相同字符串的地址，不会创建新字符串对象。如果没有找到，则创建新的字符串，并把字符串添加到驻留池中。

如果大量的字符串都驻留到内存里，而得不到释放，不是很容易造成内存爆炸吗，当然不会了？因为不是任何字符串都会驻留，只有通过IL指令ldstr创建的字符串才会留用。


----------

em...在了解之后又感觉没啥想写的

----------

[https://www.cnblogs.com/anding/p/5240313.html](https://www.cnblogs.com/anding/p/5240313.html "字符串操作")

----------
since:5/24/2019 3:08:16 PM 

direction:source code string