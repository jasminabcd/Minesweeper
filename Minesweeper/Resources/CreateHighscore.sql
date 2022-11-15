

CREATE TABLE Highscore(
	Id int primary key,
	Duration int not null,
	playerName varchar(max),
	PlayDate DateTime2,
	Difficulty varchar(32)
);