
what is ioc:
	
	控制注入，是一种设计模式

the benefits of using this:
	
	降低耦合度

for example:
	
![](https://i.imgur.com/O5kyWaD.png)

> 图解

#### 常见场景 ####
	
	request --> UI层 --> response
	
	小型UI层:
		获取dal/dbcontext
		执行操作

	三层UI业务:
		获取 service
			获取service关联的dal
		执行操作
	
	多层业务:
		获取 service
			获取上述关联层实例
			获取上述关联层实例
			获取上述关联层实例...
		执行操作

### flow ###


> 未使用IOC vs 使用了IOC

	<IOC>
	1. UI只与容器关联
	2. 容器与对象之间通过标识关联(通常是泛型type+抽象)
	3. 业务调整时，无需考虑相关层的业务关系，只需维护本层业务
	4. 当业务内容过多时，可以自行建立层级标准来维护标识的准确性
	5. 简单易用,无需考虑过多元素

	<无IOC时>
	1. UI与最低层关联
	2. 最低层与上层层层关联
	3. 业务调整时，应考虑相关层的使用
	4. 当业务内容过多时，阅读业务含义需要访问所有关联层方可理解
	5. 复杂关联，容易出现代码重复，即便抽象也难以维护。。。
	
em... 总结太多了，熟悉得不能再熟悉了。

----------
author:monster

since:12/11/2018 3:26:09 PM 

direction:ioc