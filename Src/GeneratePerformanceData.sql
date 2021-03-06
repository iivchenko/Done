DELETE FROM [dbo].TaskItems

DECLARE @amount int = 100000
DECLARE @maxId bigint

WHILE @amount > 0  
BEGIN  

	INSERT INTO [dbo].[TaskItems] (Name, Status) Values(CONCAT('Name_', @amount), 0)
	SET @amount = @amount - 1
END  

SELECT COUNT(id) FROM [MyTasks_Performance].[dbo].[TaskItems]