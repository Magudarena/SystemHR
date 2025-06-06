using System;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class BranieWolnego
    {
        public int Id { get; set; }
        public int? Id_Urlop { get; set; }
        public int? Id_Pracownik { get; set; }
        public DateTime? Poczatek_Wolnego { get; set; }
        public DateTime? Koniec_Wolnego { get; set; }
    }
}