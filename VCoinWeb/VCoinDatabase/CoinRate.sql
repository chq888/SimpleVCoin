CREATE TABLE [dbo].[CoinRate]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Rate] NVARCHAR(MAX) NOT NULL, 
    [LinkId] INT NOT NULL,
	[CreatedDate] DATETIME NOT NULL, 
    [CreatedBy] NVARCHAR(1000) NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [UpdatedBy] NVARCHAR(1000) NULL, 
    [IsActived] BIT NULL, 
    [IsDeleted] BIT NULL, 
    CONSTRAINT [FK_CoinRate_Link] FOREIGN KEY ([LinkId]) REFERENCES [Link]([Id])
)
