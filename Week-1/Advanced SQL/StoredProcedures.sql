-- ==========================================
-- Module : Stored Procedures
-- Database : StoredProcedureDB
-- MySQL 8.0
-- ==========================================

DROP DATABASE IF EXISTS StoredProcedureDB;
CREATE DATABASE StoredProcedureDB;
USE StoredProcedureDB;

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
    EmployeeID INT AUTO_INCREMENT PRIMARY KEY,
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

INSERT INTO Employees
(
    FirstName,
    LastName,
    DepartmentID,
    Salary,
    JoinDate
)
VALUES
('John','Doe',1,5000,'2020-01-15'),
('Jane','Smith',2,6000,'2019-03-22'),
('Michael','Johnson',3,7000,'2018-07-30'),
('Emily','Davis',4,5500,'2021-11-05');

-- ==========================================
-- Exercise 1
-- Get Employees by Department
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sp_GetEmployeesByDepartment $$

CREATE PROCEDURE sp_GetEmployeesByDepartment
(
    IN pDepartmentID INT
)
BEGIN

    SELECT *
    FROM Employees
    WHERE DepartmentID = pDepartmentID;

END $$

DELIMITER ;

-- ==========================================
-- Exercise 2
-- Modify Procedure
-- ==========================================

DROP PROCEDURE sp_GetEmployeesByDepartment;

DELIMITER $$

CREATE PROCEDURE sp_GetEmployeesByDepartment
(
    IN pDepartmentID INT
)
BEGIN

    SELECT
        EmployeeID,
        FirstName,
        LastName,
        Salary
    FROM Employees
    WHERE DepartmentID = pDepartmentID;

END $$

DELIMITER ;

-- ==========================================
-- Exercise 3
-- Insert Employee
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sp_InsertEmployee $$

CREATE PROCEDURE sp_InsertEmployee
(
    IN pFirstName VARCHAR(50),
    IN pLastName VARCHAR(50),
    IN pDepartmentID INT,
    IN pSalary DECIMAL(10,2),
    IN pJoinDate DATE
)
BEGIN

    INSERT INTO Employees
    (
        FirstName,
        LastName,
        DepartmentID,
        Salary,
        JoinDate
    )
    VALUES
    (
        pFirstName,
        pLastName,
        pDepartmentID,
        pSalary,
        pJoinDate
    );

END $$

DELIMITER ;

-- ==========================================
-- Exercise 4
-- Execute Procedure
-- ==========================================

CALL sp_GetEmployeesByDepartment(1);

-- ==========================================
-- Exercise 5
-- Employee Count
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sp_GetEmployeeCount $$

CREATE PROCEDURE sp_GetEmployeeCount
(
    IN pDepartmentID INT
)
BEGIN

    SELECT COUNT(*) AS EmployeeCount
    FROM Employees
    WHERE DepartmentID = pDepartmentID;

END $$

DELIMITER ;

CALL sp_GetEmployeeCount(1);

-- ==========================================
-- Exercise 6
-- OUT Parameter
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sp_TotalSalary $$

CREATE PROCEDURE sp_TotalSalary
(
    IN pDepartmentID INT,
    OUT pTotalSalary DECIMAL(12,2)
)
BEGIN

    SELECT IFNULL(SUM(Salary),0)
    INTO pTotalSalary
    FROM Employees
    WHERE DepartmentID = pDepartmentID;

END $$

DELIMITER ;

CALL sp_TotalSalary(1,@SalaryTotal);

SELECT @SalaryTotal AS TotalSalary;

-- ==========================================
-- Exercise 7
-- Update Salary
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sp_UpdateEmployeeSalary $$

CREATE PROCEDURE sp_UpdateEmployeeSalary
(
    IN pEmployeeID INT,
    IN pNewSalary DECIMAL(10,2)
)
BEGIN

    UPDATE Employees
    SET Salary = pNewSalary
    WHERE EmployeeID = pEmployeeID;

END $$

DELIMITER ;

CALL sp_UpdateEmployeeSalary(1,5500);

-- ==========================================
-- Exercise 8
-- Give Bonus
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sp_GiveBonus $$

CREATE PROCEDURE sp_GiveBonus
(
    IN pDepartmentID INT,
    IN pBonusAmount DECIMAL(10,2)
)
BEGIN

    UPDATE Employees
    SET Salary = Salary + pBonusAmount
    WHERE DepartmentID = pDepartmentID;

END $$

DELIMITER ;

CALL sp_GiveBonus(1,500);

-- ==========================================
-- Exercise 9
-- Transaction Procedure
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sp_UpdateSalaryTransaction $$

CREATE PROCEDURE sp_UpdateSalaryTransaction
(
    IN pEmployeeID INT,
    IN pSalary DECIMAL(10,2)
)
BEGIN

    START TRANSACTION;

    UPDATE Employees
    SET Salary = pSalary
    WHERE EmployeeID = pEmployeeID;

    COMMIT;

END $$

DELIMITER ;

CALL sp_UpdateSalaryTransaction(2,7200);

-- ==========================================
-- Exercise 10
-- Dynamic SQL
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sp_DynamicEmployeeSearch $$

CREATE PROCEDURE sp_DynamicEmployeeSearch
(
    IN pColumnName VARCHAR(50),
    IN pValue VARCHAR(100)
)
BEGIN

    SET @sql = CONCAT(
        'SELECT * FROM Employees WHERE ',
        pColumnName,
        ' = ?'
    );

    PREPARE stmt FROM @sql;
    SET @val = pValue;
    EXECUTE stmt USING @val;
    DEALLOCATE PREPARE stmt;

END $$

DELIMITER ;

CALL sp_DynamicEmployeeSearch(
    'FirstName',
    'John'
);

-- ==========================================
-- Exercise 11
-- Error Handling
-- ==========================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sp_SafeSalaryUpdate $$

CREATE PROCEDURE sp_SafeSalaryUpdate
(
    IN pEmployeeID INT,
    IN pSalary DECIMAL(10,2)
)
BEGIN

    DECLARE v_error TEXT;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN

        GET DIAGNOSTICS CONDITION 1
            v_error = MESSAGE_TEXT;

        SELECT CONCAT(
            'Error: ',
            v_error
        ) AS ErrorMessage;

        ROLLBACK;

    END;

    START TRANSACTION;

    UPDATE Employees
    SET Salary = pSalary
    WHERE EmployeeID = pEmployeeID;

    COMMIT;

    SELECT 'Salary Updated Successfully' AS Message;

END $$

DELIMITER ;

CALL sp_SafeSalaryUpdate(1,6000);

-- ==========================================
-- Delete Procedure
-- ==========================================

DROP PROCEDURE IF EXISTS sp_GetEmployeesByDepartment;