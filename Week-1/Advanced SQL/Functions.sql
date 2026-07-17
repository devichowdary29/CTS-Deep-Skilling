-- ==========================================
-- Module : Functions
-- Database : FunctionDB
-- MySQL 8.0
-- ==========================================

DROP DATABASE IF EXISTS FunctionDB;
CREATE DATABASE FunctionDB;
USE FunctionDB;

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
(3,'Bob','Johnson',3,5500,'2021-07-01');

-- ==========================================
-- Exercise 1
-- Scalar Function
-- ==========================================

DELIMITER $$

DROP FUNCTION IF EXISTS fn_CalculateAnnualSalary $$

CREATE FUNCTION fn_CalculateAnnualSalary
(
    pSalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
    RETURN pSalary * 12;
END $$

DELIMITER ;

SELECT
EmployeeID,
fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees;

-- ==========================================
-- Exercise 2
-- Table-Valued Function Equivalent
-- ==========================================

-- MySQL does NOT support table-valued functions.
-- Use a VIEW instead.

CREATE OR REPLACE VIEW vw_ITEmployees AS
SELECT *
FROM Employees
WHERE DepartmentID = 2;

SELECT * FROM vw_ITEmployees;

-- ==========================================
-- Exercise 3
-- Bonus Function
-- ==========================================

DELIMITER $$

DROP FUNCTION IF EXISTS fn_CalculateBonus $$

CREATE FUNCTION fn_CalculateBonus
(
    pSalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
    RETURN pSalary * 0.10;
END $$

DELIMITER ;

SELECT
EmployeeID,
fn_CalculateBonus(Salary) AS Bonus
FROM Employees;

-- ==========================================
-- Exercise 4
-- Modify Bonus Function
-- ==========================================

DROP FUNCTION fn_CalculateBonus;

DELIMITER $$

CREATE FUNCTION fn_CalculateBonus
(
    pSalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
    RETURN pSalary * 0.15;
END $$

DELIMITER ;

SELECT
EmployeeID,
fn_CalculateBonus(Salary) AS ModifiedBonus
FROM Employees;

-- ==========================================
-- Exercise 5
-- Annual Salary
-- ==========================================

SELECT
EmployeeID,
fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees;

-- ==========================================
-- Exercise 6
-- Annual Salary of Employee 1
-- ==========================================

SELECT
EmployeeID,
fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees
WHERE EmployeeID = 1;

-- ==========================================
-- Exercise 7
-- Finance Department Employees
-- ==========================================

SELECT *
FROM Employees
WHERE DepartmentID = 3;

-- ==========================================
-- Exercise 8
-- Nested Function
-- ==========================================

DELIMITER $$

DROP FUNCTION IF EXISTS fn_CalculateTotalCompensation $$

CREATE FUNCTION fn_CalculateTotalCompensation
(
    pSalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN

    RETURN
        fn_CalculateAnnualSalary(pSalary)
        +
        fn_CalculateBonus(pSalary);

END $$

DELIMITER ;

SELECT
EmployeeID,
fn_CalculateTotalCompensation(Salary) AS TotalCompensation
FROM Employees;

-- ==========================================
-- Exercise 9
-- Modify Nested Function
-- ==========================================

DROP FUNCTION fn_CalculateTotalCompensation;

DELIMITER $$

CREATE FUNCTION fn_CalculateTotalCompensation
(
    pSalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN

    RETURN
        (pSalary * 12)
        +
        (pSalary * 0.20);

END $$

DELIMITER ;

SELECT
EmployeeID,
fn_CalculateTotalCompensation(Salary) AS UpdatedCompensation
FROM Employees;

-- ==========================================
-- Exercise 10
-- Delete Function
-- ==========================================

DROP FUNCTION IF EXISTS fn_CalculateBonus;