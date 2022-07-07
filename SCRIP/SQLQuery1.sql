create table Parqueadero (EstacioTotal numeric, EspacioDisponible numeric)

create table Estacionamiento (NumeroEspacio varchar(2) primary key, NumerodePiso varchar(2), Estado varchar(6))

create table Tarifa (TipoVehiculo numeric primary key, NombreTipodeVehiculo varchar(30), Fecha varchar(21), ValorMinimo numeric, ValorNormal numeric, TiempoMinimo numeric)

create table Vehiculo (Placa varchar(5) primary key, Tipo varchar(15), ModeloyMarca varchar(35), AplicaConvenio varchar(5))

drop table TiquetParticular

create table TiquetParticular (Codigo varchar(5) primary key,
HoraEntrada varchar(100),
HoraSalida varchar(100),
EstadoTiquet varchar(5),
ValorExtra numeric, 
Total numeric,
ValorMinimo numeric,
IdTarifa numeric,
IdVehiculo varchar (6),
IdEstacionamiento varchar(2),
CONSTRAINT fk_Tarifa FOREIGN KEY (IdTarifa) REFERENCES Tarifa (TipoVehiculo), CONSTRAINT fk_Estacionamiento FOREIGN KEY (IdEstacionamiento) REFERENCES Estacionamiento (NumeroEspacio))

create table Login(Usuario Varchar(8) primary key, Contraseña Varchar(8))

insert Login(Usuario,Contraseña) values('admin','123')

delete Parqueadero
delete TiquetParticular

select * from TiquetParticular
select * from Tarifa