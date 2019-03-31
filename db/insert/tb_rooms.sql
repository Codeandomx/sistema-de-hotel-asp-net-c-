use hotel;

insert into tb_rooms (id_type,id_status,name) values (1,1,'1 S');
insert into tb_rooms (id_type,id_status,name) values (1,1,'2 S');
insert into tb_rooms (id_type,id_status,name) values (1,1,'3 S');
insert into tb_rooms (id_type,id_status,name) values (1,1,'4 S');
insert into tb_rooms (id_type,id_status,name) values (2,1,'1 D');
insert into tb_rooms (id_type,id_status,name) values (2,1,'2 D');
insert into tb_rooms (id_type,id_status,name) values (2,1,'3 D');
insert into tb_rooms (id_type,id_status,name) values (2,1,'4 D');
insert into tb_rooms (id_type,id_status,name) values (3,1,'1 L');
insert into tb_rooms (id_type,id_status,name) values (3,1,'2 L');
insert into tb_rooms (id_type,id_status,name) values (3,1,'3 L');
insert into tb_rooms (id_type,id_status,name) values (3,1,'4 L');

SELECT t0.id_room,t0.id_type,t0.id_status,t0.name,t0.date_register,t0.date_update,t1.name as type_name,t2.name as status_name
FROM tb_rooms t0
	INNER JOIN tb_types t1 ON t0.id_type=t1.id_type
	INNER JOIN tb_status t2 ON t0.id_status=t2.id_status;
    
select * from tb_status;