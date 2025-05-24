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
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Haslo { get; set; } = string.Empty;

        [Required]
        public string Imie { get; set; } = string.Empty;

        [Required]
        public string Nazwisko { get; set; } = string.Empty;

        [ForeignKey("Uprawnienia")]
        public int? Id_Uprawnienia { get; set; }

        public virtual Uprawnienia? Uprawnienia { get; set; }
    }
}
