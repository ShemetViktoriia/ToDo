﻿/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

print('Post deployment // start');

IF (EXISTS(SELECT * FROM [dbo].[ToDoItem]))  
BEGIN  
    DELETE FROM [dbo].[ToDoItem]  
END  
GO

:r .\InsertData_ToDoItem.sql

print('Post deployment // end');