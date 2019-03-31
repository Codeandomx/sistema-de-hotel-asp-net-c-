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
    class ReservationMapper
    {
        #region Propiedades
        private DBConnect _db;
        #endregion

        public ReservationMapper()
        {
            // Inicializamos la base de datos
            _db = new DBConnect();

            // Inicializamos el maper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                AutoMapper.Mappers.MapperRegistry.Mappers.Add(new AutoMapper.DataReaderMapper.DataReaderMapper(AutoMapper.Mapper.Engine));

                cfg.CreateMap(typeof(IDataReader), typeof(ReservationsModel));
            });
        }

        #region GetReservations
        public IEnumerable<ReservationsModel> GetReservations()
        {
            IEnumerable<ReservationsModel> list = null;
            MySqlDataReader rdr = null;

            try
            {
                // Obtenemos el reader
                string query = @"
                    SELECT t0.id_room,t0.id_type,t0.id_status,t0.name as room_name,t3.date_register,t3.date_update,t1.name as type_name,t2.name as status_name,t3.name,t3.active,t3.price,t3.total,t3.id_reservation
                    FROM tb_Reservations t3
                        INNER JOIN tb_rooms t0 ON t3.id_room = t0.id_room AND t3.active = 1
                        INNER JOIN tb_types t1 ON t0.id_type=t1.id_type
                        INNER JOIN tb_status t2 ON t0.id_status=t2.id_status";
                rdr = _db.Select(query, new MySqlParameter[] { });

                // Mapeamos los resultados
                if (rdr.HasRows)
                {
                    list = Mapper.Map<IDataReader, IEnumerable<ReservationsModel>>(rdr);
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

        #region GetReservation
        public ReservationsModel GetReservation(int Id)
        {
            IEnumerable<ReservationsModel> list = null;
            MySqlDataReader rdr = null;

            try
            {
                // Obtenemos el reader
                string query = @"
                    SELECT t0.id_room,t0.id_type,t0.id_status,t0.name as room_name,t3.date_register,t3.date_update,t1.name as type_name,t2.name as status_name,t3.name,t3.active,t3.price,t3.total,t3.id_reservation
                    FROM tb_Reservations t3
                        INNER JOIN tb_rooms t0 ON t3.id_room = t0.id_room AND t3.active = 1
                        INNER JOIN tb_types t1 ON t0.id_type=t1.id_type
                        INNER JOIN tb_status t2 ON t0.id_status=t2.id_status
                WHERE t3.id_room = @IdRoom AND t3.active = 1";
                rdr = _db.Select(query, new MySqlParameter[] {
                   new MySqlParameter("@IdRoom", Id)
                });

                // Mapeamos los resultados
                list = Mapper.Map<IDataReader, IEnumerable<ReservationsModel>>(rdr);
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
        public int CreateReservation(int IdRoom, string Name, double Price)
        {
            int Id = -1;
            try
            {
                string query = "INSERT INTO tb_reservations (id_room,name,price,total) VALUES (@IdRoom,@Name,@Price,@Total)";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@IdRoom", IdRoom),
                        new MySqlParameter("@Name", Name),
                        new MySqlParameter("@Price", Price),
                        new MySqlParameter("@Total", 0.0)
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
        public int EditReservation(ReservationsModel Reservation)
        {
            int Id = -1;

            try
            {
                string query = "UPDATE tb_reservations SET name=@Name,price=@Price,total=@Total,date_update=NOW(),active=0 WHERE id_Reservation=@IdReservation";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@IdReservation", Reservation.id_reservation),
                        new MySqlParameter("@Name", Reservation.name),
                        new MySqlParameter("@Price", Reservation.price),
                        new MySqlParameter("@Total", Reservation.total)
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
        public int DeleteReservation(int Id)
        {
            try
            {
                string query = "DELETE FROM tb_reservations WHERE id_Reservation=@IdReservation";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@IdReservation", Id)
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
