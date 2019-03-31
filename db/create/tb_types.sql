use hotel;

-- Tabla para los tipos de habitaciones
create table tb_types(
	id_type int auto_increment,
    name varchar(50) not null,
    date_register datetime default now(),
    date_update datetime default null,
    primary key(id_type)
) engine = innodb;