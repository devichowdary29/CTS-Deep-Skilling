-- ==========================================
-- Module: Advanced SQL
-- Database: AdvancedSQLDB
-- MySQL 8.0 Compatible
-- ==========================================

DROP DATABASE IF EXISTS AdvancedSQLDB;
CREATE DATABASE AdvancedSQLDB;
USE AdvancedSQLDB;

-- ==========================================
-- TABLES
-- ==========================================

CREATE TABLE Customers
(
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    Region VARCHAR(50)
);

CREATE TABLE Products
(
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10,2)
);

CREATE TABLE Orders
(
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderDetails
(
    OrderDetailID INT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- ==========================================
-- SAMPLE DATA
-- ==========================================

INSERT INTO Customers VALUES
(1,'Prem','South'),
(2,'Rahul','North'),
(3,'Anil','South');

INSERT INTO Products VALUES
(101,'Laptop','Electronics',50000),
(102,'Mouse','Electronics',500),
(103,'Keyboard','Electronics',1000),
(104,'Chair','Furniture',4000),
(105,'Table','Furniture',6000);

INSERT INTO Orders VALUES
(1,1,'2025-01-10'),
(2,2,'2025-01-11'),
(3,1,'2025-01-12'),
(4,3,'2025-01-13');

INSERT INTO OrderDetails VALUES
(1,1,101,2),
(2,1,102,5),
(3,2,104,3),
(4,3,105,2),
(5,4,101,1);

-- ==========================================
-- Exercise 1
-- Ranking and Window Functions
-- ==========================================

SELECT
    ProductID,
    ProductName,
    Category,
    Price,

    ROW_NUMBER() OVER
    (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS RowNum,

    RANK() OVER
    (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS RankNum,

    DENSE_RANK() OVER
    (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS DenseRankNum

FROM Products;

-- ==========================================
-- Exercise 2
-- GROUPING SETS (Equivalent)
-- ==========================================

SELECT
    C.Region,
    NULL AS Category,
    SUM(OD.Quantity) AS TotalQuantity
FROM Customers C
JOIN Orders O ON C.CustomerID=O.CustomerID
JOIN OrderDetails OD ON O.OrderID=OD.OrderID
JOIN Products P ON P.ProductID=OD.ProductID
GROUP BY C.Region

UNION ALL

SELECT
    NULL,
    P.Category,
    SUM(OD.Quantity)
FROM Customers C
JOIN Orders O ON C.CustomerID=O.CustomerID
JOIN OrderDetails OD ON O.OrderID=OD.OrderID
JOIN Products P ON P.ProductID=OD.ProductID
GROUP BY P.Category

UNION ALL

SELECT
    C.Region,
    P.Category,
    SUM(OD.Quantity)
FROM Customers C
JOIN Orders O ON C.CustomerID=O.CustomerID
JOIN OrderDetails OD ON O.OrderID=OD.OrderID
JOIN Products P ON P.ProductID=OD.ProductID
GROUP BY C.Region,P.Category;

-- ==========================================
-- Exercise 2
-- ROLLUP
-- ==========================================

SELECT
    C.Region,
    P.Category,
    SUM(OD.Quantity) AS TotalQuantity
FROM Customers C
JOIN Orders O ON C.CustomerID=O.CustomerID
JOIN OrderDetails OD ON O.OrderID=OD.OrderID
JOIN Products P ON P.ProductID=OD.ProductID
GROUP BY C.Region,P.Category WITH ROLLUP;

-- ==========================================
-- Exercise 2
-- CUBE (Equivalent)
-- ==========================================

SELECT
    C.Region,
    NULL AS Category,
    SUM(OD.Quantity) AS TotalQuantity
FROM Customers C
JOIN Orders O ON C.CustomerID=O.CustomerID
JOIN OrderDetails OD ON O.OrderID=OD.OrderID
JOIN Products P ON P.ProductID=OD.ProductID
GROUP BY C.Region

UNION ALL

SELECT
    NULL,
    P.Category,
    SUM(OD.Quantity)
FROM Customers C
JOIN Orders O ON C.CustomerID=O.CustomerID
JOIN OrderDetails OD ON O.OrderID=OD.OrderID
JOIN Products P ON P.ProductID=OD.ProductID
GROUP BY P.Category

UNION ALL

SELECT
    C.Region,
    P.Category,
    SUM(OD.Quantity)
FROM Customers C
JOIN Orders O ON C.CustomerID=O.CustomerID
JOIN OrderDetails OD ON O.OrderID=OD.OrderID
JOIN Products P ON P.ProductID=OD.ProductID
GROUP BY C.Region,P.Category

UNION ALL

SELECT
    NULL,
    NULL,
    SUM(OD.Quantity)
FROM Customers C
JOIN Orders O ON C.CustomerID=O.CustomerID
JOIN OrderDetails OD ON O.OrderID=OD.OrderID;

-- ==========================================
-- Exercise 3
-- Recursive CTE
-- ==========================================

WITH RECURSIVE CalendarCTE AS
(
    SELECT DATE('2025-01-01') AS CalendarDate

    UNION ALL

    SELECT DATE_ADD(CalendarDate,INTERVAL 1 DAY)
    FROM CalendarCTE
    WHERE CalendarDate<'2025-01-31'
)

SELECT *
FROM CalendarCTE;

-- ==========================================
-- Exercise 4
-- MERGE Equivalent
-- ==========================================

CREATE TABLE StagingProducts
(
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10,2)
);

INSERT INTO StagingProducts VALUES
(101,'Laptop','Electronics',55000),
(106,'Monitor','Electronics',12000);

INSERT INTO Products(ProductID,ProductName,Category,Price)
SELECT *
FROM StagingProducts
ON DUPLICATE KEY UPDATE
ProductName=VALUES(ProductName),
Category=VALUES(Category),
Price=VALUES(Price);

-- ==========================================
-- Exercise 5
-- PIVOT Equivalent
-- ==========================================

SELECT
    ProductID,

    SUM(CASE WHEN MONTH(O.OrderDate)=1 THEN Quantity ELSE 0 END) AS Jan,
    SUM(CASE WHEN MONTH(O.OrderDate)=2 THEN Quantity ELSE 0 END) AS Feb,
    SUM(CASE WHEN MONTH(O.OrderDate)=3 THEN Quantity ELSE 0 END) AS Mar,
    SUM(CASE WHEN MONTH(O.OrderDate)=4 THEN Quantity ELSE 0 END) AS Apr,
    SUM(CASE WHEN MONTH(O.OrderDate)=5 THEN Quantity ELSE 0 END) AS May,
    SUM(CASE WHEN MONTH(O.OrderDate)=6 THEN Quantity ELSE 0 END) AS Jun,
    SUM(CASE WHEN MONTH(O.OrderDate)=7 THEN Quantity ELSE 0 END) AS Jul,
    SUM(CASE WHEN MONTH(O.OrderDate)=8 THEN Quantity ELSE 0 END) AS Aug,
    SUM(CASE WHEN MONTH(O.OrderDate)=9 THEN Quantity ELSE 0 END) AS Sep,
    SUM(CASE WHEN MONTH(O.OrderDate)=10 THEN Quantity ELSE 0 END) AS Oct,
    SUM(CASE WHEN MONTH(O.OrderDate)=11 THEN Quantity ELSE 0 END) AS Nov,
    SUM(CASE WHEN MONTH(O.OrderDate)=12 THEN Quantity ELSE 0 END) AS `Dec`

FROM Orders O
JOIN OrderDetails OD
ON O.OrderID=OD.OrderID

GROUP BY ProductID;

-- ==========================================
-- Exercise 6
-- UNPIVOT Equivalent
-- ==========================================

SELECT ProductID,
MONTH(OrderDate) AS MonthNo,
Quantity
FROM Orders O
JOIN OrderDetails OD
ON O.OrderID=OD.OrderID;

-- ==========================================
-- Exercise 7
-- CTE
-- ==========================================

WITH CustomerOrderCounts AS
(
    SELECT
        CustomerID,
        COUNT(*) AS OrderCount
    FROM Orders
    GROUP BY CustomerID
)

SELECT
    C.CustomerID,
    C.CustomerName,
    CO.OrderCount
FROM Customers C
JOIN CustomerOrderCounts CO
ON C.CustomerID=CO.CustomerID
WHERE CO.OrderCount>1;