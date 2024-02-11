create database lalana;
\c lalana;

create extension postgis;

create table lalanaSimba(
    idL SERIAL PRIMARY KEY,
    nom VARCHAR(4),
    pkD DOUBLE PRECISION,
    pkA DOUBLE PRECISION
);

create table Niveau(
    idN SERIAL PRIMARY KEY,
    nom VARCHAR(10),
    valeur DOUBLE PRECISION
);
INSERT INTO Niveau VALUES(default,'N4',10);
INSERT INTO Niveau VALUES(default,'N3',15);
INSERT INTO Niveau VALUES(default,'N2',20);

create table Util(
    idC SERIAL PRIMARY KEY,
    nomC VARCHAR(30),
    unite VARCHAR(10)
);
INSERT INTO Util VALUES(default,'Petrole','litre');
INSERT INTO Util VALUES(default,'bois','isa');

create table Produit(
    idP SERIAL PRIMARY KEY,
    nomP VARCHAR(30),
    unite VARCHAR(20),
    dureeFabrication DOUBLE PRECISION
);
INSERT INTO Produit VALUES(default,'Goudron','m3');

create table Composant(
    idP INTEGER,
    idC INTEGER,
    qte DOUBLE PRECISION,
    FOREIGN KEY (idP) REFERENCES Produit(idP),
    FOREIGN KEY (idC) REFERENCES Util(idC)
);
INSERT INTO Composant VALUES (1,1,400);
INSERT INTO Composant VALUES (1,2,10);

create table price(
    idC INTEGER,
    prix DOUBLE PRECISION,
    daty DATE,
    FOREIGN KEY (idC) REFERENCES Util(idC)
);
INSERT INTO Price VALUES(1,13500,'2022-01-12');
INSERT INTO Price VALUES(2,10500,'2022-01-01');