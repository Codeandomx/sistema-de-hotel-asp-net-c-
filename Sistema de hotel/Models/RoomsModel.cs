using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_hotel.Models
{
    class RoomsModel
    {
        // Principales
        public int? id_room { get; set; }
        public int? id_type { get; set; }
        public int? id_status { get; set; }
        public string name { get; set; }
        public DateTime? date_register { get; set; }
        public DateTime? date_update { get; set; }

        // Secundarios
        public string type_name { get; set; }
        public string status_name { get; set; }
    }
}
