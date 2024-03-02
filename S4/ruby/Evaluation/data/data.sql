INSERT INTO prestataire VALUES(default,'Filatex',125000,4,100,'2003-07-15');
INSERT INTO prestataire VALUES(default,'Colas',129000,3,90,'1990-07-12');

INSERT INTO lalana VALUES(default,'RN2',30.5,5,2);
INSERT INTO lalana VALUES(default,'RN1',40.5,6,3);
INSERT INTO lalana VALUES(default,'RN7',60.5,4.5,4);

INSERT INTO donnes VALUES(1,'4*vit+2*emp+(1/2)*anc');
INSERT INTO donnes VALUES(2,'4*(vit+emp)-2*anc');

CREATE OR REPLACE VIEW Prest AS
    SELECT p.idPrest,p.nomPrest,p.prixM3,p.vit,p.emp,EXTRACT(YEAR FROM CURRENT_DATE)-EXTRACT(YEAR FROM p.anc)::DOUBLE PRECISION anc FROM prestataire p;

SELECT qualite from 

alter table prestataire alter column emp type double precision;
