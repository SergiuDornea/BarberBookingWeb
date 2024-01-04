namespace BarberBookingWeb.Models
{
    public class Client
    {

        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Parola { get; set; }
        public string Email { get; set; }

        public List<Programari> Programari { get; set; }

    }
}
