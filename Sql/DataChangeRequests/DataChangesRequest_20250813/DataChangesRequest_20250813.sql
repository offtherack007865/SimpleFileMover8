-- SQL Server Instance: smg-sql01
USE [SimpleFileMover8];

-- 001 Disable Simple File Mover entry for PointClickCare
SELECT COUNT(*)
WHERE [Pk] = 22;

-- 1 record 

-- BEGIN TRAN
UPDATE [SimpleFileMover8].[dbo].[SimpleFileMover8_Config]
SET [Enabled] = 0
WHERE [Pk] = 22;

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
