CREATE TABLE [dbo].[Link]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Note] NVARCHAR(MAX) NULL, 
	[CreatedDate] DATETIME NOT NULL, 
    [CreatedBy] NVARCHAR(1000) NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [UpdatedBy] NVARCHAR(1000) NULL, 
    [IsActived] BIT NULL, 
    [IsDeleted] BIT NULL
)
