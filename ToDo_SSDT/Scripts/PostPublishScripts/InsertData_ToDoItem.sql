SET IDENTITY_INSERT ToDoItem ON
GO

INSERT INTO ToDoItem(Id,[Description],AddedAt,AddedBy,WasDone,WasDoneAt,DueDate) VALUES
(1,'Test task1 for ToDo App','2019-12-20 12:00:00',1,'TRUE','2019-12-25 12:00:00',NULL),
(2,'Test task2 for ToDo App','2019-12-20 13:00:00',1,'FALSE',NULL,NULL),
(3,'Test task3 for ToDo App','2019-12-20 14:00:00',1,'TRUE','2019-12-25 12:00:00',NULL),
(4,'Test task4 for ToDo App','2019-12-20 15:00:00',1,'FALSE',NULL,'2019-12-25 12:00:00'),
(5,'Test task5 for ToDo App','2019-12-20 16:00:00',1,'TRUE','2019-12-25 12:00:00',NULL),
(6,'Test task6 for ToDo App','2019-12-20 17:00:00',1,'FALSE',NULL,NULL),
(7,'Test task7 for ToDo App','2019-12-20 18:00:00',1,'TRUE','2019-12-25 12:00:00',NULL),
(8,'Test task8 for ToDo App','2019-12-20 19:00:00',1,'FALSE',NULL,NULL),
(9,'Test task9 for ToDo App','2019-12-20 20:00:00',1,'TRUE','2019-12-25 12:00:00',NULL),
(10,'Test task10 for ToDo App','2019-12-20 21:00:00',1,'FALSE',NULL,'2019-12-25 12:00:00'),
(11,'Test task11 for ToDo App','2019-12-20 22:00:00',1,'TRUE','2019-12-25 12:00:00',NULL),
(12,'Test task12 for ToDo App','2019-12-20 23:00:00',1,'FALSE',NULL,NULL),
(13,'Test task13 for ToDo App','2019-12-20 12:30:00',1,'TRUE','2019-12-25 12:00:00',NULL),
(14,'Test task14 for ToDo App','2019-12-20 13:30:00',1,'FALSE',NULL,'2019-12-25 12:00:00'),
(15,'Test task15 for ToDo App','2019-12-20 14:30:00',1,'TRUE','2019-12-25 12:00:00',NULL)

SET IDENTITY_INSERT ToDoItem OFF
GO