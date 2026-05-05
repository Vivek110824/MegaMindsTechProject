-- USER TABLE
CREATE TABLE tblUserRegistration (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Address VARCHAR(255),
    StateId INT,
    CityId INT
);

-- STATE TABLE
CREATE TABLE tblState (
    Id INT PRIMARY KEY,
    StateName VARCHAR(100)
);

INSERT INTO tblState VALUES (1, 'Gujarat'), (2, 'Maharashtra');

-- CITY TABLE
CREATE TABLE tblCity (
    Id INT PRIMARY KEY,
    StateId INT,
    CityName VARCHAR(100)
);

INSERT INTO tblCity VALUES 
(1,1,'Surat'),
(2,1,'Bardoli'),
(3,1,'Baroda'),
(4,2,'Mumbai'),
(5,2,'Pune');

-- STORED PROCEDURES

CREATE PROCEDURE sp_InsertUser
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(20),
    @Address VARCHAR(255),
    @StateId INT,
    @CityId INT
AS
BEGIN
    INSERT INTO tblUserRegistration(Name, Email, Phone, Address, StateId, CityId)
    VALUES (@Name, @Email, @Phone, @Address, @StateId, @CityId)
END

CREATE PROCEDURE sp_GetUsers
AS
BEGIN
    SELECT * FROM tblUserRegistration
END

CREATE PROCEDURE sp_GetUserById
    @Id INT
AS
BEGIN
    SELECT * FROM tblUserRegistration WHERE Id = @Id
END

CREATE PROCEDURE sp_UpdateUser
    @Id INT,
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(20),
    @Address VARCHAR(255),
    @StateId INT,
    @CityId INT
AS
BEGIN
    UPDATE tblUserRegistration
    SET Name=@Name, Email=@Email, Phone=@Phone, Address=@Address,
        StateId=@StateId, CityId=@CityId
    WHERE Id=@Id
END

CREATE PROCEDURE sp_DeleteUser
    @Id INT
AS
BEGIN
    DELETE FROM tblUserRegistration WHERE Id=@Id
END