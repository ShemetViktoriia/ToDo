/****** Object:  Table [dbo].[ToDoItem] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ToDoItem] (
[Id] INT IDENTITY,
[Description] NVARCHAR(500) NOT NULL,
[AddedAt] DateTime NOT NULL,
[AddedBy] [nvarchar](128) NOT NULL,
[WasDone] [bit] NOT NULL,
[WasDoneAt] DateTime NULL,
[DueDate] DateTime NULL
CONSTRAINT [PK_dbo.ToDoItem] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)

GO

ALTER TABLE [dbo].[ToDoItem]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ToDoItem_dbo.AspNetUsers_AddedBy] FOREIGN KEY([AddedBy])
REFERENCES [dbo].[AspNetUsers]([Id])
GO

ALTER TABLE [dbo].[ToDoItem] CHECK CONSTRAINT [FK_dbo.ToDoItem_dbo.AspNetUsers_AddedBy]
GO