using System.ComponentModel.DataAnnotations;

namespace SystemHR.Models
{
    public class Wracanie
    {
        [Required(ErrorMessage = "Numer Pracownika HR jest wymagany.")]
        public string Nr_identyfikacyjny { get; set; }
    }
}
