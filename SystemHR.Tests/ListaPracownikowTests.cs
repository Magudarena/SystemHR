using SystemHR.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class ListaPracownikowTests
{
    [Fact]
    public void Valid_ListaPracownikow_PassesValidation()
    {
        var model = new ListaPracownikow
        {
            Imie = "Jan",
            Nazwisko = "Kowalski",
            Email = "jan@example.com",
            Telefon = "123456789"
        };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Invalid_ListaPracownikow_FailsValidation()
    {
        var model = new ListaPracownikow();
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
    }
}
