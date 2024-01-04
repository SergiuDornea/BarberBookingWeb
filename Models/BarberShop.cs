namespace BarberBookingWeb.Models
{
    public class BarberShop
    {

        public int ID { get; set; }

        public string Nume { get; set; }
        public string Adresa { get; set; }

        public string Email { get; set; }
        public int Telefon { get; set; }

        public ICollection<Barber>? Barbers { get; set; }
    }
}
