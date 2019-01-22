
	use dapper for some time,I wanna deep in dapper

> environment

- .net core 2.1
- vs2017

> step1:import 

- Dapper 1.50.5
- MySql.Data 8.0.13
 

> 附一下官网介绍：

## What's Dapper? ##
Dapper is a simple object mapper for .NET and own the title of King of Micro ORM in terms of speed and is virtually as fast as using a raw ADO.NET data reader. An ORM is an Object Relational Mapper, which is responsible for mapping between database and programming language.

Dapper是.NET的简单对象映射器，在速度方面拥有微型ORM之王的标题，几乎与使用原始ADO.NET数据读取器一样快。ORM是一个对象关系映射器，负责数据库和编程语言之间的映射。

Dapper extend the IDbConnection by providing useful extension methods to query your database.

Dapper通过提供有用的扩展方法来查询数据库来扩展IDbConnection。

	与原始读取器一样快，可以说很强大了 
	毕竟框架再好，也难以超过原生的速度,
	可见是使用扩展方法将优化操作进行构建【猜测】
	具有orm映射,这对于项目构建的帮助也非常大,对于维护扩展的帮助也特别大
	而且具有跨数据库的功能 - nice 

----------
> execute

### 参数 ###

| Name        | 	Description           
| ------------- |:-------------:| 
| sql     | The command text to execute. |
| param      |   The command parameters (default = null).    |
| transaction     | The transaction to use (default = null).       |
| commandTimeout     | The command timeout (default = null)      |
| commandType     | The command type (default = null)       |

### 特点 ###

根据参数类型[单个或集合]，可执行一次或多次操作，支持AUD和存储过程

> query

| Name        | 	Description           
| ------------- |:-------------:| 
| sql     | The command text to execute. |
| param      |   The command parameters (default = null).    |
| transaction     | The transaction to use (default = null).       |
| buffered     | True to buffer readeing the results of the query (default = true).       |
| commandTimeout     | The command timeout (default = null)      |
| commandType     | The command type (default = null)       |



----------

[https://www.jianshu.com/p/7b2d74701ccd](https://www.jianshu.com/p/7b2d74701ccd "mysql storeProduce help document")

[https://dapper-tutorial.net/dapper](https://dapper-tutorial.net/dapper "dapper office site")
----------
author:monster

since:1/16/2019 9:59:40 AM 

direction:dapper intro

