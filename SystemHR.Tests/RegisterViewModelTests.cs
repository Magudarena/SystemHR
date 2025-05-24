using SystemHR.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class RegisterViewModelTests
{
    [Fact]
    public void Valid_RegisterViewModel_PassesValidation()
    {
        var model = new RegisterViewModel
        {
            Imie = "Anna",
            Nazwisko = "Nowak",
            Email = "test@example.com",
            Haslo = "StrongPass1",
            ConfirmHaslo = "StrongPass1"
        };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Invalid_RegisterViewModel_FailsValidation()
    {
        var model = new RegisterViewModel
        {
            Haslo = "abc",
            ConfirmHaslo = "def"
        };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
    }
}
