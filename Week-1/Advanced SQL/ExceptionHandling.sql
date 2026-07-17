-- ==========================================
-- Module : Exception Handling
-- Database : ExceptionDB
-- MySQL 8.0
-- ==========================================

DROP DATABASE IF EXISTS ExceptionDB;
CREATE DATABASE ExceptionDB;
USE ExceptionDB;

-- ==========================================
-- Departments
-- ==========================================

CREATE TABLE Departments
(
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100) NOT NULL
);

-- ==========================================
-- Employees
-- ==========================================

CREATE TABLE Employees
(
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100) UNIQUE,
    Salary DECIMAL(10,2),
    DepartmentID INT,

    CONSTRAINT FK_Department
    FOREIGN KEY (DepartmentID)
    REFERENCES Departments(DepartmentID)
);

-- ==========================================
-- Audit Log
-- ==========================================

CREATE TABLE AuditLog
(
    LogID INT AUTO_INCREMENT PRIMARY KEY,
    ActionName VARCHAR(100),
    ErrorMessage VARCHAR(1000),
    ErrorTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ==========================================
-- Sample Data
-- ==========================================

INSERT INTO Departments VALUES
(1,'HR'),
(2,'IT'),
(3,'Finance');

INSERT INTO Employees VALUES
(1,'John','Doe','john@gmail.com',5000,1),
(2,'Jane','Smith','jane@gmail.com',6500,2);

SELECT * FROM Departments;

SELECT * FROM Employees;
-- ==========================================
-- Exercise 2
-- AddEmployee Procedure
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS AddEmployee $$

CREATE PROCEDURE AddEmployee
(
    IN pEmployeeID INT,
    IN pFirstName VARCHAR(50),
    IN pLastName VARCHAR(50),
    IN pEmail VARCHAR(100),
    IN pSalary DECIMAL(10,2),
    IN pDepartmentID INT
)
BEGIN

    DECLARE v_error TEXT;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN

        ROLLBACK;

        GET DIAGNOSTICS CONDITION 1
            v_error = MESSAGE_TEXT;

        INSERT INTO AuditLog
        (
            ActionName,
            ErrorMessage
        )
        VALUES
        (
            'Add Employee',
            v_error
        );

    END;

    START TRANSACTION;

    -- Salary Validation

    IF pSalary <= 0 THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Salary must be greater than zero';

    END IF;

    -- Department Validation

    IF NOT EXISTS
    (
        SELECT *
        FROM Departments
        WHERE DepartmentID = pDepartmentID
    ) THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT='Department does not exist';

    END IF;

    INSERT INTO Employees
    (
        EmployeeID,
        FirstName,
        LastName,
        Email,
        Salary,
        DepartmentID
    )
    VALUES
    (
        pEmployeeID,
        pFirstName,
        pLastName,
        pEmail,
        pSalary,
        pDepartmentID
    );

    COMMIT;

END $$

DELIMITER ;
DELIMITER $$

DROP PROCEDURE IF EXISTS TransferEmployee $$

CREATE PROCEDURE TransferEmployee
(
    IN pEmployeeID INT,
    IN pDepartmentID INT
)
BEGIN

    DECLARE v_error TEXT;
    DECLARE v_count INT DEFAULT 0;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN

        ROLLBACK;

        GET DIAGNOSTICS CONDITION 1
            v_error = MESSAGE_TEXT;

        INSERT INTO AuditLog
        (
            ActionName,
            ErrorMessage
        )
        VALUES
        (
            'Transfer Employee',
            v_error
        );

        RESIGNAL;

    END;

    START TRANSACTION;

    -- Check Employee

    SELECT COUNT(*)
    INTO v_count
    FROM Employees
    WHERE EmployeeID = pEmployeeID;

    IF v_count = 0 THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Employee does not exist';

    END IF;

    -- Check Department

    SELECT COUNT(*)
    INTO v_count
    FROM Departments
    WHERE DepartmentID = pDepartmentID;

    IF v_count = 0 THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Department does not exist';

    END IF;

    UPDATE Employees
    SET DepartmentID = pDepartmentID
    WHERE EmployeeID = pEmployeeID;

    COMMIT;

END $$

DELIMITER ;