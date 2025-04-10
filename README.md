# SampleProject
### NOTE: 
Added a quick table creation script below. If you want to run the project create the table and update the connection string in appsettings.json

```
IF OBJECT_ID (N'Users', N'U') IS NULL 
  BEGIN
	CREATE TABLE Users
	(
	Id UniqueIdentifier NOT NULL PRIMARY KEY  DEFAULT (NEWID()),
	Email nvarchar(100) NOT NULL,
	Enabled BIT NOT NULL Default(0)
	)

END

IF NOT EXISTS (SELECT TOP 1 * FROM Users WHERE Email = 'test1@email.com')
BEGIN
	INSERT INTO USERS (Email, Enabled) VALUES ('test1@email.com', 0)
END

IF NOT EXISTS (SELECT TOP 1 * FROM Users WHERE Email = 'another@email.com')
BEGIN
	INSERT INTO USERS (Email, Enabled) VALUES ('another@email.com', 0)
END
```

![image](https://github.com/user-attachments/assets/ed7b73cd-972d-4ec9-a698-59b2bd8b272d)
