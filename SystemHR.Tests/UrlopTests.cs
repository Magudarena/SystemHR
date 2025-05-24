using SystemHR.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace SystemHR.Tests
{
    public class UrlopTests
    {
        [Fact]
        public void Urlop_Model_ValidData_ShouldPassValidation()
        {
            var model = new Urlop
            {
                Nr_identyfikacyjny = "1234567890",
                nazwa_wolnego = "Urlop wypoczynkowy",
                dane_wolnego = "2025-07-01 do 2025-07-10",
                Identyfikator = "1234567890123",
                Kategoria = 3,
                Dostepne = true
            };

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, results, true);

            Assert.True(isValid);
        }

        [Fact]
        public void Urlop_Model_MissingRequiredField_ShouldFailValidation()
        {
            var model = new Urlop();

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, results, true);

            Assert.False(isValid);
            Assert.NotEmpty(results);
        }
    }
}
