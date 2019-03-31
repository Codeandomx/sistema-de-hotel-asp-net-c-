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
    class StatusMapper
    {
        #region Propiedades
        private DBConnect _db;
        #endregion

        public StatusMapper()
        {
            // Inicializamos la base de datos
            _db = new DBConnect();

            // Inicializamos el maper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                AutoMapper.Mappers.MapperRegistry.Mappers.Add(new AutoMapper.DataReaderMapper.DataReaderMapper(AutoMapper.Mapper.Engine));

                cfg.CreateMap(typeof(IDataReader), typeof(StatusModel));
            });
        }

        #region GetStatus
        public IEnumerable<StatusModel> GetStatus()
        {
            IEnumerable<StatusModel> list = null;
            MySqlDataReader rdr = null;

            try
            {
                // Obtenemos el reader
                string query = @"
                    SELECT t0.id_status,t0.name,t0.date_register,t0,date_update
                    FROM tb_status t0";
                rdr = _db.Select(query, new MySqlParameter[] { });

                // Mapeamos los resultados
                if (rdr.HasRows)
                {
                    list = Mapper.Map<IDataReader, IEnumerable<StatusModel>>(rdr);
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

        #region GetStatus
        public StatusModel GetStatus(int Id)
        {
            IEnumerable<StatusModel> list = null;
            MySqlDataReader rdr = null;

            try
            {
                // Obtenemos el reader
                string query = @"
                    SELECT t0.id_status,t0.name,t0.date_register,t0,date_update
                    FROM tb_status t0
                    WHERE id_status = @IdStatus";
                rdr = _db.Select(query, new MySqlParameter[] {
                   new MySqlParameter("@IdStatus", Id)
                });

                // Mapeamos los resultados
                list = Mapper.Map<IDataReader, IEnumerable<StatusModel>>(rdr);
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
        public int CreateStatus(string Name)
        {
            int Id = -1;
            try
            {
                string query = "INSERT INTO tb_status (name) VALUES (@Name)";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@Name", Name)
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
        public int EditStatus(int IdStatus, string Name)
        {
            int Id = -1;

            try
            {
                string query = "UPDATE tb_status SET name=@Name,date_update=NOW() WHERE id_status=@IdStatus";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@IdStatus", IdStatus),
                        new MySqlParameter("@Name", Name)
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
        public int DeleteStatus(int Id)
        {
            try
            {
                string query = "DELETE FROM tb_status WHERE id_status=@IdStatus";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@IdStatus", Id)
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
