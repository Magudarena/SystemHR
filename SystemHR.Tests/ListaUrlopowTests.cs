using SystemHR.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace SystemHR.Tests
{
    public class ListaUrlopowTests
    {
        [Fact]
        public void ListaUrlopow_Model_ValidData_ShouldPassValidation()
        {
            var model = new ListaUrlopow
            {
                Id = 1,
                Nr_identyfikacyjny = "1234567890",
                nazwa_wolnego = "Urlop okolicznościowy",
                dane_wolnego = "2025-08-01",
                Identyfikator = "1234567890",
                Kategoria = "Specjalna",
                Dostepne = true
            };

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, results, true);

            Assert.True(isValid);
        }

        [Fact]
        public void ListaUrlopow_Model_InvalidData_ShouldFailValidation()
        {
            var model = new ListaUrlopow();

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, results, true);

            Assert.False(isValid);
            Assert.NotEmpty(results);
        }
    }
}
