use hotel;

-- tabla para reservaciones
create table tb_reservations(
	id_reservation int auto_increment,
    id_room int,
    name varchar(50) not null,
    date_register datetime default now(),
    date_update datetime default null,
    price double(7,2) default 0.0,
    total double(7,2) default 0.0,
    active bool default 1,
    index(id_room),
    foreign key(id_room) references tb_rooms(id_room),
    primary key(id_reservation)
) engine = innodb;

alter table tb_reservations add active bool default 1;
alter table tb_reservations add price double(7,2) default 0.0;
alter table tb_reservations add total double(7,2) default 0.0;

select * from tb_reservations;

delete from tb_reservations where id_reservation = 1;

SELECT t0.id_room,t0.id_type,t0.id_status,t0.name as room_name,t3.date_register,t3.date_update,t1.name as type_name,t2.name as status_name,t3.name as reservation_name,t3.active,t3.price,t3.total,t3.id_reservation
                    FROM tb_Reservations t3
                        INNER JOIN tb_rooms t0 ON t3.id_room = t0.id_room AND t3.active = 1
                        INNER JOIN tb_types t1 ON t0.id_type=t1.id_type
                        INNER JOIN tb_status t2 ON t0.id_status=t2.id_status
                WHERE t3.id_room = 10 AND t3.active = 1