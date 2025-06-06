using System;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class UrlopPerPracownik
    {
        public int? Id_Wolne { get; set; }
        public int? Id_Pracownik { get; set; }
        public int? Id_Urlop { get; set; }
        public string Nr_identyfikacyjny { get; set; }
        public string nazwa_wolnego { get; set; }
        public string dane_wolnego { get; set; }
        public string Identyfikator { get; set; }
        public DateTime? Poczatek_Wolnego { get; set; }
        public DateTime? Koniec_Wolnego { get; set; }
    }

}