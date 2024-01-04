using System.ComponentModel.DataAnnotations;

namespace BarberBookingWeb.Models
{
    public class Barber
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Parola { get; set; }
        public string Email { get; set; }

        public ICollection<Serviciu>? Servicii { get; set; }
        public ICollection<Programare>? Programari { get; set; }


        public int? BarberShopID { get; set; }
        [Display(Name = "Lucrezaza la: ")]
        public BarberShop? BarberShop { get; set; }

    }
}
