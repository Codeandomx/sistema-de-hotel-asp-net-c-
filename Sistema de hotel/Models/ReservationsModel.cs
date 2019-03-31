using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_hotel.Models
{
    class ReservationsModel
    {
        // principales
        public int? id_reservation { get; set; }
        public DateTime? date_register { get; set; }
        public DateTime? date_update { get; set; }
        public int? id_room { get; set; }
        public string name { get; set; }
        public double? price { get; set; }
        public double? total { get; set; }
        public bool? active { get; set; }

        // secundarios
        public string room_name { get; set; }
        public int? id_type { get; set; }
        public int? id_status { get; set; }
        public string type_name { get; set; }
        public string status_name { get; set; }
    }
}
