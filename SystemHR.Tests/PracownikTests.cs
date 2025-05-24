using SystemHR.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace SystemHR.Tests
{
    public class PracownikTests
    {
        [Fact]
        public void Pracownik_Model_ValidData_ShouldPassValidation()
        {
            var model = new Pracownik
            {
                Id = 1,
                Imie = "Anna",
                Nazwisko = "Kowalska",
                Email = "anna.kowalska@example.com",
                Telefon = "123456789"
            };

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, results, true);

            Assert.True(isValid);
        }

        [Fact]
        public void Pracownik_Model_MissingFields_ShouldFailValidation()
        {
            var model = new Pracownik();

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, results, true);

            Assert.False(isValid);
            Assert.NotEmpty(results);
        }
    }
}
