using AutoMapper;
using MySql.Data.MySqlClient;
using Sistema_de_hotel.Clases;
using Sistema_de_hotel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_hotel.Entities
{
    class RoomMapper
    {
        #region Propiedades
        private DBConnect _db;
        #endregion

        public RoomMapper()
        {
            // Inicializamos la base de datos
            _db = new DBConnect();

            // Inicializamos el maper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                AutoMapper.Mappers.MapperRegistry.Mappers.Add(new AutoMapper.DataReaderMapper.DataReaderMapper(AutoMapper.Mapper.Engine));

                cfg.CreateMap(typeof(IDataReader), typeof(RoomsModel));
            });
        }

        #region GetRooms
        public IEnumerable<RoomsModel> GetRooms()
        {
            IEnumerable<RoomsModel> list = null;
            MySqlDataReader rdr = null;

            try
            {
                // Obtenemos el reader
                string query = @"
                    SELECT t0.id_room,t0.id_type,t0.id_status,t0.name,t0.date_register,t0.date_update,t1.name as type_name,t2.name as status_name
                    FROM tb_rooms t0
                        INNER JOIN tb_types t1 ON t0.id_type=t1.id_type
                        INNER JOIN tb_status t2 ON t0.id_status=t2.id_status";
                rdr = _db.Select(query, new MySqlParameter[] { });

                // Mapeamos los resultados
                if (rdr.HasRows)
                {
                    list = Mapper.Map<IDataReader, IEnumerable<RoomsModel>>(rdr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Cerramos la conexión
                _db.CloseConnection();
            }

            return list;
        }
        #endregion

        #region GetRoom
        public RoomsModel GetRoom(int Id)
        {
            IEnumerable<RoomsModel> list = null;
            MySqlDataReader rdr = null;

            try
            {
                // Obtenemos el reader
                string query = @"
                    SELECT t0.id_room,t0.id_type,t0.id_status,t0.name,t0.date_register,t0.date_update,t1.name as type_name,t2.name as status_name
                    FROM tb_rooms t0
                        INNER JOIN tb_types t1 ON t0.id_type=t1.id_type
                        INNER JOIN tb_status t2 ON t0.id_status=t2.id_status
                    WHERE id_Room = @IdRoom";
                rdr = _db.Select(query, new MySqlParameter[] {
                   new MySqlParameter("@IdRoom", Id)
                });

                // Mapeamos los resultados
                list = Mapper.Map<IDataReader, IEnumerable<RoomsModel>>(rdr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Cerramos la conexión
                _db.CloseConnection();
            }

            return list.FirstOrDefault();
        }
        #endregion

        #region Create
        public int CreateRoom(RoomsModel Room)
        {
            int Id = -1;
            try
            {
                string query = "INSERT INTO tb_rooms (id_type,id_status,name) VALUES (@IdType,@IdStatus,@Name)";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@RoomName", Room.id_type),
                        new MySqlParameter("@Name", Room.id_status),
                        new MySqlParameter("@LastName", Room.name),
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Cerramos la conexión
                _db.CloseConnection();
            }

            return Id;
        }
        #endregion

        #region Edit
        public int EditRoom(RoomsModel Room)
        {
            int Id = -1;

            try
            {
                string query = "UPDATE tb_rooms SET name=@Name,id_type=@IdType,id_status=@IdStatus,date_update=NOW() WHERE id_room=@IdRoom";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@IdRoom", Room.id_room),
                        new MySqlParameter("@IdType", Room.id_type),
                        new MySqlParameter("@IdStatus", Room.id_status),
                        new MySqlParameter("@Name", Room.name)
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Cerramos la conexión
                _db.CloseConnection();
            }

            return Id;
        }
        #endregion

        #region Delete
        public int DeleteRoom(int Id)
        {
            try
            {
                string query = "DELETE FROM tb_rooms WHERE id_Room=@IdRoom";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@IdRoom", Id)
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Id = -1;
            }
            finally
            {
                // Cerramos la conexión
                _db.CloseConnection();
            }

            return Id;
        }
        #endregion
    }
}
