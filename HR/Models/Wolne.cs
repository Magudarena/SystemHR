using System;
using System.ComponentModel.DataAnnotations;

namespace SystemHR.Models
{
    public class Wolne
    {
        public int Id { get; set; }

        public int Id_Urlop { get; set; }

        public int Id_Pracownik { get; set; }

        public DateTime Poczatek_Wolnego { get; set; }

        public DateTime Koniec_Wolnego { get; set; }

    
        [Required]
        public Urlop Urlop { get; set; } = null!;

        [Required]
        public Pracownik Pracownik { get; set; } = null!;
    }
}
