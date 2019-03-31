use hotel;

-- Tabla para habitaciones
create table tb_rooms(
	id_room int auto_increment,
    id_type int,
    id_status int,
    name varchar(50) not null,
    date_register datetime default now(),
    date_update datetime default null,
    index(id_type),
    index(id_status),
    foreign key(id_type) references tb_types(id_type),
    foreign key(id_status) references tb_status(id_status),
    primary key(id_room)
) engine = innodb;

-- alter table tb_rooms add id_status int;
-- alter table tb_rooms add index(id_status);
-- alter table tb_rooms add constraint foreign key(id_status) references tb_status(id_status);