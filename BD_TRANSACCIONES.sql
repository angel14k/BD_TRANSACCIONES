
-- Este script se encargara de crear la base de datos
create database BD_TRANSACCIONES

-- Después seleccionamos en el mismo script la BD
use BD_TRANSACCIONES

-- Creamos al usuario sql
-- Aqui crearemos un usuario para esa base de datos y le daremos acceso.

CREATE LOGIN usuario_transacciones
with PASSWORD = 'usuario_transacciones123';

create USER usuario_transacciones
for LOGIN usuario_transacciones;

alter role db_datareader
add MEMBER usuario_transacciones;

alter role db_datawriter
add MEMBER usuario_transacciones;

-- aqui el usuario tendrá permisos para leer y escribir datos en BD_TRANSACCIONES


-- Creación de base de datos

create table MONEDA
(
pk_id_moneda_in int primary key,
tipo_moneda_ch char(3),
fecha_creacion_dt datetime,
fecha_modificacion_dt datetime
)

create table CUENTA
(
pk_id_cuenta_in int primary key,
fk_id_moneda_in int,
numero_cuenta_vc varchar(30),
saldo_de decimal(12,2),
fecha_creacion_dt datetime,
fecha_modificacion_dt datetime,
foreign key (fk_id_moneda_in) references MONEDA(pk_id_moneda_in)
)

create table MOVIMIENTO
(
pk_id_movimiento_in int primary key,
fk_id_cuenta_in int,
tipo_movimiento_vc varchar(30),
monto_de decimal(12,2),
fecha_dt datetime,
fecha_creacion_dt datetime,
fecha_modificacion_dt datetime,
foreign key (fk_id_cuenta_in) references CUENTA(pk_id_cuenta_in)
)

create table TIPO_CAMBIO
(
pk_id_tipo_cambio_in int primary key,
fk_id_moneda_origen_in int,
fk_id_moneda_destino_in int,
valor_de decimal(12,2),
fecha_dt datetime,
fecha_creacion_dt datetime,
fecha_modificacion_dt datetime,
foreign key (fk_id_moneda_origen_in) references MONEDA(pk_id_moneda_in),
foreign key (fk_id_moneda_destino_in) references MONEDA(pk_id_moneda_in)
)



-- Insertando datos

INSERT INTO MONEDA
(
pk_id_moneda_in,tipo_moneda_ch,fecha_creacion_dt,fecha_modificacion_dt
)
values
(1, 'BOB', GETDATE(), GETDATE()),
(2, 'USD', GETDATE(), GETDATE()),
(3, 'CLP', GETDATE(), GETDATE());

select * from MONEDA


INSERT INTO CUENTA
(
pk_id_cuenta_in, fk_id_moneda_in, numero_cuenta_vc, saldo_de, 
fecha_creacion_dt, fecha_modificacion_dt
)
values
(1, 1, '100001', 5000.00, GETDATE(), GETDATE()),
(2, 2, '100002', 1000.00, GETDATE(), GETDATE()),
(3, 3, '100003', 500000.00, GETDATE(), GETDATE());

select * from CUENTA

INSERT INTO MOVIMIENTO
(
pk_id_movimiento_in, fk_id_cuenta_in, tipo_movimiento_vc, monto_de,
fecha_dt, fecha_creacion_dt, fecha_modificacion_dt
)
values 
(1, 1, 'ABONO', 1000.00, GETDATE(), GETDATE(), GETDATE()),
(2, 1, 'RETIRO', 500.00, GETDATE(), GETDATE(), GETDATE()),
(3, 2, 'ABONO', 200.00, GETDATE(), GETDATE(), GETDATE());

	select * from MOVIMIENTO

INSERT INTO TIPO_CAMBIO
(
pk_id_tipo_cambio_in,  fk_id_moneda_origen_in, fk_id_moneda_destino_in,
valor_de, fecha_dt, fecha_creacion_dt, fecha_modificacion_dt
)
values
(1, 1, 2, 6.96, GETDATE(), GETDATE(), GETDATE()),
(2, 2, 1, 6.96, GETDATE(), GETDATE(), GETDATE()),
(3, 1, 3, 120.50, GETDATE(), GETDATE(), GETDATE());

select * from TIPO_CAMBIO

/*Que moneda utiliza cada cuenta y cuanto saldo tiene*/

select
		C.pk_id_cuenta_in,
		C.numero_cuenta_vc,
		C.saldo_de,
		M.tipo_moneda_ch
from CUENTA C
inner join MONEDA M
	on C.fk_id_moneda_in = M.pk_id_moneda_in


/*Que movimientos tiene cada cuenta y en qué moneda está esa cuenta*/

select
	C.pk_id_cuenta_in,
	C.numero_cuenta_vc,
	M.tipo_moneda_ch,
	MV.tipo_movimiento_vc,
	MV.monto_de,
	MV.fecha_dt
from CUENTA C
inner join MONEDA M
	on C.fk_id_moneda_in = M.pk_id_moneda_in
inner Join MOVIMIENTO MV
	on C.pk_id_cuenta_in = MV.fk_id_cuenta_in


/*tipo de cambio + moneda dos veces*/

/*¿Cuál es la moneda de origen, cuál es la moneda de destino y cuál es el valor del cambio?*/

select
	TC.pk_id_tipo_cambio_in,
	MO.tipo_moneda_ch AS moneda_origen,
	MD.tipo_moneda_ch AS moneda_destino,
	TC.valor_de,
	TC.fecha_dt
from TIPO_CAMBIO TC
inner join MONEDA MO
	on TC.fk_id_moneda_origen_in = MO.pk_id_moneda_in
inner join MONEDA MD
	on TC.fk_id_moneda_destino_in = MD.pk_id_moneda_in


	select * from MONEDA