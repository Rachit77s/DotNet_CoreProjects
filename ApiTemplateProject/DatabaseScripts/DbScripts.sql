CREATE TABLE [dbo].[Users]
(
 [Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
 [Username] VARCHAR(30) NOT NULL,
 [Password] VARCHAR(30) NOT NULL,
)



Insert Into [dbo].[Users](Username, Password ) Values ('Rachit', 'rachit')
Insert Into [dbo].[Users](Username, Password ) Values ('Sachin', 'sachin')