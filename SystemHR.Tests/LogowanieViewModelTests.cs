using SystemHR.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class LogowanieViewModelTests
{
    [Fact]
    public void Valid_LogowanieViewModel_PassesValidation()
    {
        var model = new LogowanieViewModel
        {
            Email = "email@example.com",
            Haslo = "Secret123",
            RememberMe = true
        };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Invalid_LogowanieViewModel_FailsValidation()
    {
        var model = new LogowanieViewModel();
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
    }
}
