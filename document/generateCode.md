
> mysql

#### 查询所有表 ####

	SELECT 
	  table_name,#表名
	  table_comment,#表注释
	  create_time #创建时间
	FROM 
	  information_schema.tables 
	WHERE 
	  table_type = 'BASE TABLE' AND table_schema = '{db_name}'; 

#### 查询表的所有字段 ####

	SELECT
	  column_name,
	  data_type,
	  is_nullable,
	  column_comment 
	FROM
	  information_schema.columns 
	WHERE
	  table_schema='{dbName}' 
	  and table_name='{tableName}';

----------
author:monster

since:3/19/2019 10:49:03 AM 

direction:generate code