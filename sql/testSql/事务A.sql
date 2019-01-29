BEGIN TRANSACTION

	--执行修改 获取排他锁
	UPDATE Product
	SET Price = 10
	WHERE Id = 1

	--阶段2
	UPDATE Product
	SET Price = Price + 1
	WHERE Id = 1 

	SELECT * FROM Product
	WHERE Id = 1 

	--阶段3
	UPDATE Product
	SET Price = Price + 5
	WHERE Id = 1 

	SELECT * FROM Product
	WHERE Id = 1 

--阶段4
COMMIT TRANSACTION