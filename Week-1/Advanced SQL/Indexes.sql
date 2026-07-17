-- ==========================================
-- Module : Indexes
-- Database : IndexDB
-- MySQL 8.0
-- ==========================================

DROP DATABASE IF EXISTS IndexDB;
CREATE DATABASE IndexDB;
USE IndexDB;

-- ==========================================
-- TABLE CREATION
-- ==========================================

DROP TABLE IF EXISTS Employees;

CREATE TABLE Employees
(
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50),
    Salary DECIMAL(10,2)
);

INSERT INTO Employees
(EmployeeID, FirstName, LastName, Department, Salary)
VALUES
(1,'John','Doe','HR',5000),
(2,'Jane','Smith','IT',7000),
(3,'Bob','Johnson','Finance',6500),
(4,'Emily','Davis','IT',8000),
(5,'Michael','Brown','HR',5500);

-- ==========================================
-- Exercise 1
-- Primary Key (Clustered Index Equivalent)
-- ==========================================

-- In MySQL InnoDB, the PRIMARY KEY automatically acts as
-- the clustered index. No additional command is required.

SHOW INDEX FROM Employees;

-- ==========================================
-- Exercise 2
-- Create Secondary Index
-- ==========================================

CREATE INDEX IX_LastName
ON Employees(LastName);

-- ==========================================
-- Exercise 3
-- Create Unique Index
-- ==========================================

CREATE UNIQUE INDEX IX_UniqueName
ON Employees(FirstName, LastName);

-- ==========================================
-- Exercise 4
-- Create Composite Index
-- ==========================================

CREATE INDEX IX_DepartmentSalary
ON Employees(Department, Salary);

-- ==========================================
-- Exercise 5
-- View Existing Indexes
-- ==========================================

SHOW INDEX FROM Employees;

-- Alternative:
SELECT
    INDEX_NAME,
    COLUMN_NAME,
    NON_UNIQUE
FROM INFORMATION_SCHEMA.STATISTICS
WHERE TABLE_SCHEMA = 'IndexDB'
  AND TABLE_NAME = 'Employees';

-- ==========================================
-- Exercise 6
-- Test Index Performance
-- ==========================================

EXPLAIN
SELECT *
FROM Employees
WHERE LastName = 'Smith';

SELECT *
FROM Employees
WHERE LastName = 'Smith';

-- ==========================================
-- Exercise 7
-- Drop Index
-- ==========================================

DROP INDEX IX_LastName
ON Employees;

SHOW INDEX FROM Employees;