
--Project.sql
--Name: Queen Sarah Anumu Bih
--ID :2311432
--Date: 30/11/2023

CREATE DATABASE College1en;

GO
 

USE College1en;

GO
 


CREATE TABLE Programs (

    ProgId VARCHAR(5) NOT NULL,

    ProgName VARCHAR(50) NOT NULL,

    PRIMARY KEY (ProgId)

);

GO
 


CREATE TABLE Courses (

    CId VARCHAR(7) NOT NULL,

    CName VARCHAR(50) NOT NULL,

    ProgId VARCHAR(5) NOT NULL,

    PRIMARY KEY (CId),

    FOREIGN KEY (ProgId) REFERENCES Programs(ProgId) 

        ON DELETE CASCADE ON UPDATE CASCADE

);

GO
 


CREATE TABLE Students (

    StId VARCHAR(10) NOT NULL,

    StName VARCHAR(50) NOT NULL,

    ProgId VARCHAR(5) NOT NULL,

    PRIMARY KEY (StId),

    FOREIGN KEY (ProgId) REFERENCES Programs(ProgId) 

        ON DELETE NO ACTION ON UPDATE CASCADE

);

GO
 


CREATE TABLE Enrollments (

    StId VARCHAR(10) NOT NULL,

    CId VARCHAR(7) NOT NULL,

    FinalGrade INT,

    PRIMARY KEY (StId, CId),

    FOREIGN KEY (StId) REFERENCES Students(StId) 

        ON DELETE CASCADE ON UPDATE CASCADE,

    FOREIGN KEY (CId) REFERENCES Courses(CId) 

        ON DELETE NO ACTION ON UPDATE NO ACTION

);

GO
 


INSERT INTO Programs (ProgId, ProgName) VALUES

('P0111', 'Software Engineering'),

('P0112', 'Computer Science'),

('P0113', 'Business Management');

GO
 


INSERT INTO Courses (CId, CName, ProgId) VALUES

('C001111', 'Physics', 'P0111'),

('C001112', 'Computer Architecture', 'P0112'),

('C001113', 'Accounting', 'P0113'),

('C001114', 'Web Development', 'P0111'),

('C001115', 'Database Management', 'P0112'),

('C001116', 'Marketing', 'P0113'),

('C001117', 'Mathematics', 'P0111'),

('C001118', 'Computer Tools', 'P0112'),

('C001119', 'Statistics', 'P0113');

GO
 


INSERT INTO Students (StId, StName, ProgId) VALUES

('S000111111', 'Jomia Getrude', 'P0111'),

('S000222222', 'Queen Sarah', 'P0112'),

('S000333333', 'Praise Oge', 'P0113'),

('S000444444', 'Ingrid Johnson', 'P0111'),

('S000555555', 'Emily Grace', 'P0112'),

('S000666666', 'Kelly Kestine', 'P0113');

GO
 


INSERT INTO Enrollments (StId, CId, FinalGrade) VALUES

('S000111111', 'C001111', 97),

('S000222222', 'C001112', 99),

('S000333333', 'C001113', 98),

('S000444444', 'C001116', NULL),

('S000555555', 'C001115', NULL),

('S000666666', 'C001114', NULL);

GO






