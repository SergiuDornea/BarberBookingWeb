using System.ComponentModel.DataAnnotations.Schema;

namespace BarberBookingWeb.Models
{
    public class Serviciu
    {

        public int ID { get; set; }
        public string Tip { get; set; }
        public string Descriere { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Cost { get; set; }

        public int? BarberID { get; set; }
        public Barber? Barber { get; set; }
    }
}
