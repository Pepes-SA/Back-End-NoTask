CREATE TABLE Notes (
  Id INT PRIMARY KEY AUTO_INCREMENT,
  Title VARCHAR(255),
  Text TEXT,
  CreationDate DATETIME,
  Status ENUM("Activo", "Archivado","Desactivado"),
  IdNoteCategory INT,
  FOREIGN KEY (IdNoteCategory) REFERENCES NoteCategory(Id)
)

CREATE TABLE NoteCategory (
  Id INT PRIMARY KEY AUTO_INCREMENT,
  Name VARCHAR(255),
  CreationDate DATETIME,
  Status ENUM("Activo", "Desactivado")
)


DROP TABLE `Notes`

DROP TABLE `NoteCategory`


INSERT INTO `Notes` (`Title`, `Text`,`CreationDate`,`Status`) VALUES ('NOta2', 'Lorem ipsum dolor sit amet consectetur adip',"2023-12-01 12:01:01", "Desactivado")


INSERT INTO `Notes`(`Title`,`Text`,`CreationDate`,`Status`) VALUES('NOta1','Lorem imsunlsajfkldjsjasldf','2024-05-01 16:01:01','Activo');