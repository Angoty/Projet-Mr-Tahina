CREATE DATABASE rubylalana;
\c rubylalana;

CREATE TABLE lalana(
    id SERIAL PRIMARY KEY,
    routes VARCHAR(8),
    longueur DOUBLE PRECISION,
    largeur DOUBLE PRECISION,
    niveau INTEGER
);

CREATE TABLE prestataire(
    idPrest SERIAL PRIMARY KEY,
    nomPrest VARCHAR(40),
    prixM3 DOUBLE PRECISION,
    vit DOUBLE PRECISION,
    emp DOUBLE PRECISION,
    anc DATE
);

CREATE TABLE donnes(
    idLalana INTEGER,
    qualite VARCHAR(50),
    FOREIGN KEY (idLalana) REFERENCES lalana(id)
);

select *,(4*vit+2*emp+(1/2)*anc)/prixM3 qualite  from prest order by (4*vit+2*emp+(1/2)*anc)/prixM3;

