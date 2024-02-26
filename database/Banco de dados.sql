CREATE DATABASE cadastroPessoas;

USE cadastroPessoas;

CREATE TABLE Pessoas(
	Codigo INT IDENTITY(1,1) NOT NULL,
	Nome VARCHAR(50),
	DataNascimento DATE,
	Inativo BIT,
	Nacionalidade SMALLINT,
	RG VARCHAR(20),
	Passaporte VARCHAR(20),
	CONSTRAINT pk PRIMARY KEY (Codigo)
);