using System;
using System.ComponentModel.DataAnnotations;

namespace SystemHR.Models
{
    public class UrlopPerPracownik
    {
        public int? Id_Wolne { get; set; }
        public int? Id_Pracownik { get; set; }
        public int? Id_Urlop { get; set; }

        [Required]
        public string Nr_identyfikacyjny { get; set; } = string.Empty;

        [Required]
        public string nazwa_wolnego { get; set; } = string.Empty;

        [Required]
        public string dane_wolnego { get; set; } = string.Empty;

        [Required]
        public string Identyfikator { get; set; } = string.Empty;

        public DateTime? Poczatek_Wolnego { get; set; }
        public DateTime? Koniec_Wolnego { get; set; }
    }
}
