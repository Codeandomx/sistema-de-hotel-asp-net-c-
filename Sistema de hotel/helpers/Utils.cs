using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_hotel.helpers
{
    class Utils
    {
        #region GetColor
        /// <summary>
        /// Obtenemos el color de la habitacion segun el status
        /// </summary>
        /// <param name="id_status"></param>
        /// <returns></returns>
        public static string GetColor(int id_status)
        {
            string result = string.Empty;

            switch (id_status)
            {
                case 1: result = "#4caf50"; break;
                case 2: result = "#c62828"; break;
                case 3: result = "#cddc39"; break;
                case 4: result = "#4527a0"; break;
            }

            return result;
        }
        #endregion
    }
}
