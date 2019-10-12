INSERT INTO [Products] (Name, Price)VALUES(N'Картошка', 60);

UPDATE [Products] SET [Price]=90 WHERE [Name]=N'Картошка';

SELECT COUNT(*) AS Count FROM [Products]

DELETE FROM [Products] WHERE [Price]>1000;