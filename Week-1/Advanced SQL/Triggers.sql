-- ==========================================
-- Module : Triggers
-- Database : TriggerDB
-- MySQL 8.0
-- ==========================================

DROP DATABASE IF EXISTS TriggerDB;
CREATE DATABASE TriggerDB;
USE TriggerDB;

-- ==========================================
-- TABLES
-- ==========================================

DROP TABLE IF EXISTS EmployeeChanges;
DROP TABLE IF EXISTS Employees;
DROP TABLE IF EXISTS Departments;

CREATE TABLE Departments
(
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100)
);

CREATE TABLE Employees
(
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DepartmentID INT,
    Salary DECIMAL(10,2),
    JoinDate DATE,
    AnnualSalary DECIMAL(12,2),

    FOREIGN KEY (DepartmentID)
        REFERENCES Departments(DepartmentID)
);

CREATE TABLE EmployeeChanges
(
    ChangeID INT AUTO_INCREMENT PRIMARY KEY,
    EmployeeID INT,
    OldSalary DECIMAL(10,2),
    NewSalary DECIMAL(10,2),
    ChangeDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- ==========================================
-- SAMPLE DATA
-- ==========================================

INSERT INTO Departments VALUES
(1,'HR'),
(2,'Finance'),
(3,'IT'),
(4,'Marketing');

INSERT INTO Employees
VALUES
(1,'John','Doe',1,5000,'2022-01-15',60000),
(2,'Jane','Smith',2,6000,'2021-03-22',72000),
(3,'Michael','Johnson',3,7000,'2020-07-30',84000),
(4,'Emily','Davis',4,5500,'2019-11-05',66000);

-- ==========================================
-- Exercise 1
-- AFTER UPDATE Trigger
-- ==========================================

DELIMITER $$

DROP TRIGGER IF EXISTS trg_AfterSalaryUpdate $$

CREATE TRIGGER trg_AfterSalaryUpdate
AFTER UPDATE
ON Employees
FOR EACH ROW
BEGIN

    IF OLD.Salary <> NEW.Salary THEN

        INSERT INTO EmployeeChanges
        (
            EmployeeID,
            OldSalary,
            NewSalary
        )
        VALUES
        (
            OLD.EmployeeID,
            OLD.Salary,
            NEW.Salary
        );

    END IF;

END $$

DELIMITER ;

UPDATE Employees
SET Salary = 7500
WHERE EmployeeID = 1;

SELECT * FROM EmployeeChanges;

-- ==========================================
-- Exercise 2
-- BEFORE DELETE Trigger
-- ==========================================

DELIMITER $$

DROP TRIGGER IF EXISTS trg_PreventDelete $$

CREATE TRIGGER trg_PreventDelete
BEFORE DELETE
ON Employees
FOR EACH ROW
BEGIN

    SIGNAL SQLSTATE '45000'
    SET MESSAGE_TEXT='Employee deletion is not allowed';

END $$

DELIMITER ;

-- Test
-- DELETE FROM Employees WHERE EmployeeID = 1;

-- ==========================================
-- Exercise 3
-- Logon Trigger
-- ==========================================

-- Not supported in MySQL.
-- MySQL has no server LOGON triggers.

-- ==========================================
-- Exercise 4
-- Recreate AFTER UPDATE Trigger
-- ==========================================

DROP TRIGGER trg_AfterSalaryUpdate;

DELIMITER $$

CREATE TRIGGER trg_AfterSalaryUpdate
AFTER UPDATE
ON Employees
FOR EACH ROW
BEGIN

    IF OLD.Salary <> NEW.Salary THEN

        INSERT INTO EmployeeChanges
        (
            EmployeeID,
            OldSalary,
            NewSalary
        )
        VALUES
        (
            OLD.EmployeeID,
            OLD.Salary,
            NEW.Salary
        );

    END IF;

END $$

DELIMITER ;

-- ==========================================
-- Exercise 5
-- Delete Trigger
-- ==========================================

DROP TRIGGER IF EXISTS trg_PreventDelete;

-- ==========================================
-- Exercise 6
-- Annual Salary Trigger
-- ==========================================

DELIMITER $$

DROP TRIGGER IF EXISTS trg_UpdateAnnualSalary $$

CREATE TRIGGER trg_UpdateAnnualSalary
BEFORE UPDATE
ON Employees
FOR EACH ROW
BEGIN

    SET NEW.AnnualSalary = NEW.Salary * 12;

END $$

DELIMITER ;

UPDATE Employees
SET Salary = 8000
WHERE EmployeeID = 2;

SELECT
EmployeeID,
Salary,
AnnualSalary
FROM Employees;