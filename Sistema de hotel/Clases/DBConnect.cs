using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_hotel.Clases
{
    class DBConnect
    {
        #region Propiedades
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        #endregion

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        #region Inicializacion
        // Inicializamos los valores
        private void Initialize()
        {
            server = "127.0.0.1";
            database = "hotel";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "server=" + server + ";" + "user id=" + uid + ";" + "password=" + password + ";" + "database=" +
            database + ";";

            conn = new MySqlConnection(connectionString);
        }
        #endregion

        #region OpenConnection
        // abrimos la conexión a la base de datos
        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                Console.WriteLine("versión de MySQL: {0}", conn.ServerVersion);
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        #endregion

        #region CloseConnection
        // Cerramos la conexión
        public bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion

        #region Action
        // Insertar, Editar, Eliminar
        public int Action(string query, MySqlParameter[] parameters)
        {
            /*
            new MySqlParameter[]{
                new MySqlParameter("@inUserId", 123),
                new MySqlParameter("@OtroParametro", "OtroParametro")
            } 
             */
            int Id = -1;

            try
            {
                // Abrimos la conexion
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.Prepare();

                    cmd.Parameters.AddRange(parameters);

                    // Ejecutamos el comando
                    Id = cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }

            return Id;
        }
        #endregion

        #region Select
        //Select statement
        public MySqlDataReader Select(string query, MySqlParameter[] parameters)
        {
            MySqlDataReader rdr = null;

            try
            {
                // Abrimos la conexion
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.Prepare();

                    if (parameters.Count() > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    // Ejecutamos el comando
                    rdr = cmd.ExecuteReader();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }

            return rdr;
        }
        #endregion

        #region Count
        //Count statement
        public int Count(string query, MySqlParameter[] parameters)
        {
            int Count = -1;

            try
            {
                // Abrimos la conexión
                if (this.OpenConnection() == true)
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.Prepare();

                    if (parameters.Count() > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    //ExecuteScalar will return one value
                    Count = int.Parse(cmd.ExecuteScalar() + "");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }

            return Count;
        }
        #endregion

        #region BackUp
        // Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\MySqlBackup" + year + "-" + month + "-" + day +
                "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to backup! " + ex.Message);
            }
        }
        #endregion

        #region Restore
        // Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to Restore!: " + ex.Message);
            }
        }
        #endregion
    }
}
