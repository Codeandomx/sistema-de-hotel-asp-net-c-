using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_hotel.Models
{
    class StatusModel
    {
        public int? id_status { get; set; }
        public string name { get; set; }
        public DateTime? date_register { get; set; }
        public DateTime? date_update { get; set; }
    }
}
