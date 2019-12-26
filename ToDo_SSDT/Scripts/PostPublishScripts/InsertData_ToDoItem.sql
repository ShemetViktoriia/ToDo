SET IDENTITY_INSERT ToDoItem ON
GO

INSERT INTO ToDoItem(Id,[Description],AddedAt,AddedBy,WasDone,WasDoneAt,DueDate) VALUES
(1,'Test task for ToDo App','2019-12-20 12:00:00',1,'TRUE','2019-12-25 12:00:00',NULL)

SET IDENTITY_INSERT ToDoItem OFF
GO