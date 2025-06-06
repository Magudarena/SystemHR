using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class Wolne
    {
        public int Id { get; set; }
        public int Id_Urlop { get; set; }
        public int Id_Pracownik { get; set; }
        public DateTime Poczatek_Wolnego { get; set; }
        public DateTime Koniec_Wolnego { get; set; }

        // Navigation properties
        public Urlop Urlop { get; set; }
        public Pracownik Pracownik { get; set; }
    }
}
