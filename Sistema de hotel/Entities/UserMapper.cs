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
    class UserMapper
    {
        #region Propiedades
        private DBConnect _db;
        #endregion

        public UserMapper()
        {
            // Inicializamos la base de datos
            _db = new DBConnect();

            // Inicializamos el maper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                AutoMapper.Mappers.MapperRegistry.Mappers.Add(new AutoMapper.DataReaderMapper.DataReaderMapper(AutoMapper.Mapper.Engine));

                cfg.CreateMap(typeof(IDataReader), typeof(UsersModel));
            });
        }

        #region GetUsers
        public IEnumerable<UsersModel> GetUsers()
        {
            IEnumerable<UsersModel> list = null;
            MySqlDataReader rdr = null;

            try
            {
                // Obtenemos el reader
                string query = "SELECT * FROM users";
                rdr = _db.Select(query, new MySqlParameter[] { });

                // Mapeamos los resultados
                if (rdr.HasRows)
                {
                    list = Mapper.Map<IDataReader, IEnumerable<UsersModel>>(rdr);
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

        #region GetUser
        public UsersModel GetUser(int Id)
        {
            IEnumerable<UsersModel> list = null;
            MySqlDataReader rdr = null;

            try
            {
                // Obtenemos el reader
                string query = "SELECT * FROM users WHERE id_user = @IdUSer";
                rdr = _db.Select(query, new MySqlParameter[] {
                   new MySqlParameter("@IdUser", Id)
                });

                // Mapeamos los resultados
                list = Mapper.Map<IDataReader, IEnumerable<UsersModel>>(rdr);
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
        public int CreateUser(UsersModel user)
        {
            int Id = -1;
            try
            {
                string query = "INSERT INTO users (UserName,Name,LastName,Correo,Pass) VALUES (@UserName,@Name,@LastName,@Correo,@Pass)";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@UserName", user.UserName),
                        new MySqlParameter("@Name", user.Name),
                        new MySqlParameter("@LastName", user.LastName),
                        new MySqlParameter("@Correo", user.Correo),
                        new MySqlParameter("@Pass", user.Pass)
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
        public int EditUser(UsersModel user)
        {
            int Id = -1;

            try
            {
                string query = "UPDATE users SET username=@UserName,name=@Name,lastname@LastName,correo=@Correo,pass=@Pass WHERE id_user=@IdUser";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@IdUser", user.IdUser),
                        new MySqlParameter("@UserName", user.UserName),
                        new MySqlParameter("@Name", user.Name),
                        new MySqlParameter("@LastName", user.LastName),
                        new MySqlParameter("@Correo", user.Correo),
                        new MySqlParameter("@Pass", user.Pass)
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
        public int DeleteUser(int Id)
        {
            try
            {
                string query = "DELETE FROM users WHERE id_user=@IdUser";
                Id = _db.Action(query,
                    new MySqlParameter[]{
                        new MySqlParameter("@IdUser", Id)
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
