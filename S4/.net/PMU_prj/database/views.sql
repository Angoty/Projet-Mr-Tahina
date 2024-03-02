-- maka ny temps , nom cheval , nom partie
    CREATE OR REPLACE view v_relationCPT as 
        SELECT p.id idPartie ,c.idCheval idCheval ,
                    c.temps temps, p.nom partieNom, cheval.nom chevalNom
        FROM chevalTemps c 
        Join partie p on p.id = c.idPartie
        JOIN cheval on cheval.id = c.idCheval 
        group by p.id , c.idCheval , c.temps ,p.nom,cheval.nom
        order by c.temps desc;

-- maka izay nisy nanao pari
    CREATE OR REPLACE view  v_relationPCPT as 
        SELECT vrCPT.*,p.*
        from chevalPersonne cp 
        join personne p on p.id = cp.idPersonne
        join v_relationCPT vrCPT on vrCPT.idCheval = cp.idCheval
        order by vrCPT.temps desc;

