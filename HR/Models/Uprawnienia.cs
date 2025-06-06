using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class Uprawnienia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nazwa { get; set; }

        public virtual ICollection<PracownikHR>? Uzytkownicy { get; set; }
    }
}
