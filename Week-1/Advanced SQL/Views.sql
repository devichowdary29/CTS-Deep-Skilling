-- ==========================================
-- Module : Views
-- Database : ViewsDB
-- MySQL 8.0
-- ==========================================

DROP DATABASE IF EXISTS ViewsDB;
CREATE DATABASE ViewsDB;
USE ViewsDB;

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
(2,'Finance'),
(3,'IT'),
(4,'Marketing');

INSERT INTO Employees VALUES
(1,'John','Doe',1,5000,'2020-01-15'),
(2,'Jane','Smith',2,6000,'2019-03-22'),
(3,'Michael','Johnson',3,7000,'2018-07-30'),
(4,'Emily','Davis',4,5500,'2021-11-05');

-- ==========================================
-- Exercise 1
-- Create Simple View
-- ==========================================

DROP VIEW IF EXISTS vw_EmployeeBasicInfo;

CREATE VIEW vw_EmployeeBasicInfo AS
SELECT
    E.EmployeeID,
    E.FirstName,
    E.LastName,
    D.DepartmentName
FROM Employees E
JOIN Departments D
ON E.DepartmentID = D.DepartmentID;

SELECT * FROM vw_EmployeeBasicInfo;

-- ==========================================
-- Exercise 2
-- Computed Column Full Name
-- ==========================================

DROP VIEW IF EXISTS vw_EmployeeFullName;

CREATE VIEW vw_EmployeeFullName AS
SELECT
    EmployeeID,
    CONCAT(FirstName, ' ', LastName) AS FullName
FROM Employees;

SELECT * FROM vw_EmployeeFullName;

-- ==========================================
-- Exercise 3
-- Annual Salary
-- ==========================================

DROP VIEW IF EXISTS vw_EmployeeAnnualSalary;

CREATE VIEW vw_EmployeeAnnualSalary AS
SELECT
    EmployeeID,
    FirstName,
    LastName,
    Salary,
    Salary * 12 AS AnnualSalary
FROM Employees;

SELECT * FROM vw_EmployeeAnnualSalary;

-- ==========================================
-- Exercise 4
-- Employee Report
-- ==========================================

DROP VIEW IF EXISTS vw_EmployeeReport;

CREATE VIEW vw_EmployeeReport AS
SELECT
    E.EmployeeID,
    CONCAT(E.FirstName, ' ', E.LastName) AS FullName,
    D.DepartmentName,
    E.Salary,
    E.Salary * 12 AS AnnualSalary,
    (E.Salary * 12) * 0.10 AS Bonus
FROM Employees E
JOIN Departments D
ON E.DepartmentID = D.DepartmentID;

SELECT * FROM vw_EmployeeReport;