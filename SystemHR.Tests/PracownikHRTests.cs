using SystemHR.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class PracownikHRTests
{
    [Fact]
    public void Valid_PracownikHR_PassesValidation()
    {
        var model = new PracownikHR
        {
            Email = "email@example.com",
            Haslo = "Haslo123",
            Imie = "Jan",
            Nazwisko = "Kowalski"
        };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Invalid_PracownikHR_FailsValidation()
    {
        var model = new PracownikHR();
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
    }
}
