using BarberBookingWeb.Data;

namespace BarberBookingWeb.Models
{
    public class Client : IUser
    {

        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Parola { get; set; }
        public string Email { get; set; }

        public ICollection<Programare>? Programari { get; set; }

    }
}
