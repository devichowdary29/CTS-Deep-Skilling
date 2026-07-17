-- ==========================================
-- Module: Cursors
-- Database: CursorDB
-- MySQL 8.0 Compatible
-- ==========================================

DROP DATABASE IF EXISTS CursorDB;
CREATE DATABASE CursorDB;
USE CursorDB;

-- ==========================================
-- TABLES
-- ==========================================

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
    FOREIGN KEY (DepartmentID)
        REFERENCES Departments(DepartmentID)
);

-- ==========================================
-- SAMPLE DATA
-- ==========================================

INSERT INTO Departments VALUES
(1,'HR'),
(2,'IT'),
(3,'Finance');

INSERT INTO Employees VALUES
(1,'John','Doe',1,5000,'2020-01-15'),
(2,'Jane','Smith',2,6000,'2019-03-22'),
(3,'Bob','Johnson',3,5500,'2021-07-30');

-- ==========================================
-- Exercise 1
-- Cursor Example
-- ==========================================

DELIMITER $$

CREATE PROCEDURE EmployeeCursorDemo()
BEGIN

    DECLARE done INT DEFAULT FALSE;

    DECLARE vEmployeeID INT;
    DECLARE vFirstName VARCHAR(50);
    DECLARE vLastName VARCHAR(50);

    DECLARE emp_cursor CURSOR FOR
        SELECT EmployeeID, FirstName, LastName
        FROM Employees;

    DECLARE CONTINUE HANDLER FOR NOT FOUND
        SET done = TRUE;

    OPEN emp_cursor;

    read_loop: LOOP

        FETCH emp_cursor
        INTO
            vEmployeeID,
            vFirstName,
            vLastName;

        IF done THEN
            LEAVE read_loop;
        END IF;

        SELECT CONCAT(
            'Employee ID: ',
            vEmployeeID,
            ' Name: ',
            vFirstName,
            ' ',
            vLastName
        ) AS EmployeeDetails;

    END LOOP;

    CLOSE emp_cursor;

END $$

DELIMITER ;

CALL EmployeeCursorDemo();

-- ==========================================
-- Exercise 2
-- Cursor Types
-- ==========================================

-- MySQL supports only one cursor type:
-- Forward-Only, Read-Only Cursor

SELECT
'MySQL supports only Forward-Only Read-Only cursors inside Stored Procedures.'
AS CursorInfo;