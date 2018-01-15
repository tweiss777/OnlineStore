DROP DATABASE IF EXISTS OnlineStore;
CREATE DATABASE OnlineStore;
USE OnlineStore

CREATE TABLE car(
    vin INT NOT NULL AUTO_INCREMENT,
    make VARCHAR(50) NOT NULL,
    model VARCHAR(50) NOT NULL,
    year INT NOT NULL,
    trimtype VARCHAR(50) NOT NULL,
    color VARCHAR(50),
    PRIMARY KEY(vin)
    );

CREATE TABLE person(
    userID int NOT NULL AUTO_INCREMENT,
    psswrd VARCHAR(50) NOT NULL,
    fname VARCHAR(50) NOT NULL,
    lname VARCHAR(50) NOT NULL,
    addr1 VARCHAR(50) NOT NULL,
    addr2 VARCHAR(50),
    email VARCHAR(50),
    PRIMARY KEY(userID)
);

INSERT INTO person (psswrd,fname,lname,addr1,email) VALUES("abc123","admin","admin","123 sesame st","tweiss747@gmail.com");
INSERT INTO person (psswrd,fname,lname,addr1,email) VALUES("abc123","Jane","Dough","Pecado","JD@pubg.org");
INSERT INTO car(make,model,year,trimtype,color) VALUES("Ford","Bronco",2019,"titanium","white");
