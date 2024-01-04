namespace BarberBookingWeb.Models
{
    public class BarberShop
    {

        public int ID { get; set; }
        public string Adresa { get; set; }

        public string Email { get; set; }
        public int Telefon { get; set; }

        public List<Barber> Barbers { get; set; }
    }
}
