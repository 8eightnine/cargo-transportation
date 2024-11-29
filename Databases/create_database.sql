PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

CREATE TABLE IF NOT EXISTS Bank (
    ID             INTEGER PRIMARY KEY AUTOINCREMENT,
    Name           INTEGER NOT NULL,
    PaymentAccount TEXT    NOT NULL
);

CREATE TABLE IF NOT EXISTS Car_Brand (
    ID   INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT    NOT NULL
);

CREATE TABLE IF NOT EXISTS Car_List (
    ID          INTEGER       PRIMARY KEY AUTOINCREMENT
                              NOT NULL,
    StateNumber TEXT          NOT NULL
                              UNIQUE,
    BrandID     INTEGER       NOT NULL
                              REFERENCES Car_Brand (ID),
    Model       TEXT          NOT NULL,
    WeightLimit INTEGER       NOT NULL,
    Usage       INTEGER       NOT NULL
                              REFERENCES Usage_List (ID),
    IssueDate   TEXT (10, 10) NOT NULL,
    RepairDate  TEXT (10, 10) NOT NULL,
    Mileage     INTEGER       NOT NULL,
    Photo       TEXT          NOT NULL,
    Trip        INTEGER       REFERENCES Trip (ID) 
);

CREATE TABLE IF NOT EXISTS Cargo (
    ID     INTEGER PRIMARY KEY AUTOINCREMENT
                   NOT NULL,
    Name   TEXT    NOT NULL,
    Unit   TEXT    NOT NULL,
    Weight INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS Cargo_List (
    TripID        INTEGER NOT NULL
                          REFERENCES Trip (ID),
    CargoID       INTEGER REFERENCES Cargo (ID) 
                          NOT NULL,
    InsuranceCost INTEGER NOT NULL,
    Quantity      INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS Category_List (
    ID    INTEGER PRIMARY KEY AUTOINCREMENT
                  NOT NULL,
    Value INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS Class_List (
    ID   INTEGER NOT NULL
                 PRIMARY KEY AUTOINCREMENT,
    Name TEXT    NOT NULL
);

CREATE TABLE IF NOT EXISTS Client (
    ID           INTEGER       PRIMARY KEY AUTOINCREMENT
                               NOT NULL,
    FullName     TEXT          NOT NULL,
    PhoneNumber  TEXT (11, 11) NOT NULL,
    PhysPersonID INTEGER       REFERENCES Phys_Person (ID) MATCH [FULL],
    CompanyID    INTEGER       REFERENCES Company_Person (ID) 
);

CREATE TABLE IF NOT EXISTS Company_Person (
    ID          INTEGER PRIMARY KEY AUTOINCREMENT
                        NOT NULL,
    ClientName  TEXT    NOT NULL,
    NameOfCEO   TEXT    NOT NULL,
    Address     TEXT    NOT NULL,
    PhoneNumber TEXT    NOT NULL,
    BankID      INTEGER NOT NULL
                        REFERENCES Bank (ID),
    BankSSN     TEXT    NOT NULL
);

CREATE TABLE IF NOT EXISTS Driver (
    ID          INTEGER       PRIMARY KEY AUTOINCREMENT
                              NOT NULL,
    FullName    TEXT          NOT NULL
                              UNIQUE ON CONFLICT FAIL,
    TableNumber INTEGER       NOT NULL
                              UNIQUE ON CONFLICT FAIL,
    DateOfBirth TEXT (10, 10) NOT NULL,
    Experience  INTEGER       NOT NULL,
    Category    INTEGER       NOT NULL
                              REFERENCES Category_List (ID),
    Class                     NOT NULL
                              REFERENCES Class_List (ID) 
);

CREATE TABLE IF NOT EXISTS [Order] (
    ID               INTEGER       PRIMARY KEY AUTOINCREMENT
                                   NOT NULL,
    Date             TEXT (10, 10) NOT NULL,
    Sender           TEXT          NOT NULL,
    SenderAddress    TEXT          NOT NULL,
    Client           INTEGER       NOT NULL
                                   REFERENCES Client (ID),
    RecipientAddress TEXT          NOT NULL,
    TripLength       INTEGER       NOT NULL,
    Cost             INTEGER       NOT NULL,
    CargoList        INTEGER,
    Trip             INTEGER       REFERENCES Trip (ID) 
                                   NOT NULL
);

CREATE TABLE IF NOT EXISTS Phys_Person (
    ID             INTEGER       PRIMARY KEY AUTOINCREMENT
                                 NOT NULL,
    FullName       TEXT          NOT NULL,
    PhoneNumber    TEXT (11, 11) NOT NULL,
    PassportInfo   TEXT          NOT NULL,
    PassportDate   TEXT (10, 10) NOT NULL,
    PassportIssuer TEXT          NOT NULL
);

CREATE TABLE IF NOT EXISTS Trip (
    ID          INTEGER       PRIMARY KEY AUTOINCREMENT
                              NOT NULL,
    Car         INTEGER       NOT NULL
                              REFERENCES Car_List (ID),
    ArrivalDate TEXT (10, 10) NOT NULL
);

CREATE TABLE IF NOT EXISTS Trip_List (
    ID       INTEGER PRIMARY KEY AUTOINCREMENT,
    DriverID INTEGER REFERENCES Driver (ID) 
);

CREATE TABLE IF NOT EXISTS Usage_List (
    ID   INTEGER PRIMARY KEY AUTOINCREMENT,
    Info TEXT    NOT NULL
);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
