using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class PracownikHR
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Haslo { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [ForeignKey("Uprawnienia")]
        public int? Id_Uprawnienia { get; set; }

        public virtual Uprawnienia? Uprawnienia { get; set; }



    }
}