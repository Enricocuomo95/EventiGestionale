use GestioneEventi;

create table Evento(
	 idEvento int identity(1,1),
	 nome varchar(20) not null,
	 desrizione varchar(255) not null,
	 dataEvento date not null,
	 luogo varchar(255) not null,
	 capMax int not null,
	 primary key(idEvento),
	 unique (nome,dataEvento)
);

create table Risorsa(
	idRisorsa int identity(1,1),
	eventoRif int not null,
	nome varchar(20) unique not null,
	tipo varchar(50) not null,
	numero int not null,
	costo int not null,
	primary key (idRisorsa),
	foreign key (eventoRif) references Evento(idEvento) on delete cascade
);

create table partecipante(
	idPartecipante int identity(1,1),
	telefono varchar(10) not null,
	nome varchar(50) not null,
	unique (telefono),
	primary key(idPartecipante)
);

create table composto(
	partecipanteRif int not null,
	eventoRif int not null,
	primary key (partecipanteRif,eventoRif),
	foreign key(partecipanteRif) references Partecipante(idPartecipante) on delete cascade,
	foreign key(eventoRif) references Evento(idEvento) on delete cascade
);
