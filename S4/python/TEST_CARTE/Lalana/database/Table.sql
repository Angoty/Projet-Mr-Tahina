create database lalana 
\c 

create table niveau(
    id SERIAL NOT NULL PRIMARY KEY,
    nom VARCHAR(50) NOT NULL ,
    hauteur DECIMAL(7,2) NOT NULL 
);

create TABLE lalanaSimba(
    id SERIAL NOT NULL PRIMARY KEY ,
    ville1 NOT NULL,
    ville2 NOT NULL,
    pk_depart DECIMAL(11,5) NOT NULL ,
    pk_arriver DECIMAL(11,5) NOT NULL ,
    idNiveau integer,
    foreign Key (idNiveau) references niveau(id)
);

create table matiere (
    id SERIAL NOT NULL PRIMARY KEY ,
    nom VARCHAR(50)
);

create table detailMatiere(
    idMatiere integer ,
    valeur DECIMAL(11,5),
    duree DECIMAL(11,5),
    datePrix date , 
    foreign Key (idMatiere) references matiere(id)
);