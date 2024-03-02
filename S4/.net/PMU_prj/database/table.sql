create database pmu ;
\c pmu ;
create table cheval(
    id serial primary key,
    nom VARCHAR(30),
    vitesse int,
    endurence DOUBLE PRECISION 
);
create table personne(
    id serial primary key,
    nom VARCHAR(30)
);
create table partie(
    id serial primary key,
    nom VARCHAR(20)
);
create table partie_cheval(
    idPartie int ,
    idCheval int ,
    foreign key (idPartie) references partie(id),
    foreign key (idCheval) references cheval(id)
);
create table tempsVola(
    idPartie int , 
    vola DOUBLE PRECISION ,
    temps DOUBLE PRECISION,
    foreign key (idPartie) references partie(id)
);
create table chevalPersonne(
    idPersonne int ,
    idCheval int , 
    idPartie int , 
    foreign key (idPersonne) references personne(id),
    foreign key (idCheval) references cheval(id),
    foreign key (idPartie) references partie(id)
);
create table chevalTemps(
    idPartie int ,
    idCheval int ,
    temps DOUBLE PRECISION,
    foreign key (idPartie) references partie(id),
    foreign key (idCheval) references cheval(id)
);