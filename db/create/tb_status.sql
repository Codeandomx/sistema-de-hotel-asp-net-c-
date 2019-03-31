use hotel;

-- tabla para estados de la habitacion
create table tb_status(
	id_status int auto_increment,
    name varchar(50) not null,
    date_register datetime default now(),
    date_update datetime default null,
    primary key(id_status)
) engine = innodb;