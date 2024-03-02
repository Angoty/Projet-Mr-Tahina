/* sqlcmd -S KOLOINA\SQLEXPRESS -E */

create table chaine(
	idChaine varchar(5) primary key,
	nomChaine varchar(30),
	prix decimal(10,2),
	signal int
)

create table region(
	idRegion int primary key,
	nomRegion varchar(30),
	signal int
)

create table client(
	idClient varchar(10) primary key,
	nomClient varchar(30),
	localisation int,
	foreign key (localisation) references region(idRegion)
)

create table bouquet(
	idBouquet varchar(5) primary key,
	nomBouquet varchar(30),
	remise int
)

create table compoBouquet(
	idBouquet varchar(5),
	idChaine varchar(5),
	foreign key (idBouquet) references bouquet(idBouquet),
	foreign key (idChaine) references Chaine(idChaine)
)

create table abonnement(
	idClient varchar(10),
	abonnement varchar(50),
	datedebut datetime,
	datefin datetime,
	prix varchar(30),
	foreign key(idClient) references client(idClient)
)

create table remise(
	remise int
);

insert into remise values (1)

insert into chaine values ('C1','CANAL+SPORT',10000,90) 
insert into chaine values ('C2','CANAL+FAMILY',9000,85) 
insert into chaine values ('C3','TF1',8000,75) 
insert into chaine values ('C4','M6',8000,75) 
insert into chaine values ('C5','ANTENNE REUNION',6000,50) 
insert into chaine values ('C6','FRANCE 24',5000,40) 
insert into chaine values ('C7','MTV',7000,65) 
insert into chaine values ('C8','TVM',3000,40) 
insert into chaine values ('C9','MA TV',4000,45) 
insert into chaine values ('C10','USHUAIA',8500,80) 

insert into region values (1,'Antananarivo',95) 
insert into region values (2,'Itasy',75) 
insert into region values (3,'Miandrivazo',50) 
insert into region values (4,'Ranomafana',60) 

insert into client values ('CN1','Rakoto',1) 
insert into client values ('CN2','Rabe',1) 
insert into client values ('CN3','Rasoa',2) 
insert into client values ('CN4','Bob',2) 
insert into client values ('CN5','Jeanne',3) 
insert into client values ('CN6','Jean',4) 

insert into bouquet values ('B1','Tongasoa',3) 
insert into bouquet values ('B2','Premium',5) 
insert into bouquet values ('B3','VIP',8) 

insert into compoBouquet values ('B1','C3') 
insert into compoBouquet values ('B1','C5') 
insert into compoBouquet values ('B1','C6') 
insert into compoBouquet values ('B1','C8') 

insert into compoBouquet values ('B2','C3') 
insert into compoBouquet values ('B2','C4') 
insert into compoBouquet values ('B2','C5') 
insert into compoBouquet values ('B2','C6') 
insert into compoBouquet values ('B2','C8') 
insert into compoBouquet values ('B2','C9') 

insert into compoBouquet values ('B3','C1') 
insert into compoBouquet values ('B3','C2') 
insert into compoBouquet values ('B3','C3')
insert into compoBouquet values ('B3','C4') 
insert into compoBouquet values ('B3','C5') 
insert into compoBouquet values ('B3','C6') 
insert into compoBouquet values ('B3','C7') 
insert into compoBouquet values ('B3','C8') 
insert into compoBouquet values ('B3','C9') 
insert into compoBouquet values ('B3','C10') 

create view v_clientRegion as 
	select c.idClient,c.nomClient,r.nomRegion,r.signal from client c join region r on c.localisation=r.idRegion

create view v_compBouquet as
	select b.idBouquet,b.nomBouquet,b.remise,c.idChaine,c.nomChaine,c.prix,c.signal from bouquet b join compobouquet cb on b.idBouquet=cb.idBouquet join chaine c on cb.idChaine=c.idChaine


insert into chaine values ('C1','TVM',20000,1) 
insert into chaine values ('C2','VIVA',200,1) 
insert into chaine values ('C3','KOLO',20000,1) 
insert into chaine values ('C4','TF1',30000,4) 
insert into chaine values ('C5','CANAL+',30000,5) 
insert into chaine values ('C6','SPORT+',25000,4) 
insert into chaine values ('C7','CINE+',30000,3) 
insert into chaine values ('C8','ACTION',18000,2) 
insert into chaine values ('C9','EUROSPORT',20000,3) 
insert into chaine values ('C10','BEIN',30000,3)
insert into chaine values ('C11','CANAL NBA',40000,3)

insert into region values (1,'Manjakandriana',4) 
insert into region values (2,'Antsirabe',3) 

insert into client values ('CN1','Rakoto',1) 
insert into client values ('CN2','Rabe',2) 