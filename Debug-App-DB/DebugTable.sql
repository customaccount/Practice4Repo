CREATE TABLE [dbo].[DebugTable]
(
	[Id] INT NOT NULL,
	[RandomGuid] nvarchar(max) NOT NULL,
	[DateTime] datetime NOT NULL, 
    [SerializedObject] TEXT NOT NULL, 
    CONSTRAINT [PK_DebugTable] PRIMARY KEY ([Id])
)
