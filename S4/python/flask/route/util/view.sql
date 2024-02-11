CREATE OR REPLACE VIEW global as
    select c.idP,c.idC,u.nomC,c.qte from composant c join util u  on u.idC=c.idC

CREATE OR REPLACE VIEW roadAll as
    select gid idR,roadno rn,linkno,startdesc toerana1,enddesc toerana2,start_km pkD,end_Km pkA,lengthKm long,width larg,condition 
        from road;

CREATE OR REPLACE VIEW roadFair as
    select*from roadAll where condition!='Good';