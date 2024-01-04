using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace BarberBookingWeb.Models
{
    public class Programare
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public string Locatia { get; set; }

        public int? BarberID { get; set; }
        public Barber? Barber { get; set; }

        public int? ClientID { get; set; }
        public Client? Client { get; set; }

        // nume creat dinamic in functie de numele clientului , numele barberului si data la care a fost facuta programarea
        public string Detalii
        {
            get { 
                return $"Clientul {Client.Nume} programat la barberul - {Barber.Nume}  in data de- {Data.ToString("yyyy-MM-dd HH:mm")}";
            }
        }
    }
}
