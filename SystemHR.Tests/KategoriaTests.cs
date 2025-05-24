using SystemHR.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class KategoriaTests
{
    [Fact]
    public void Valid_Kategoria_PassesValidation()
    {
        var model = new Kategoria
        {
            Nazwa = "Testowa"
        };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Invalid_Kategoria_FailsValidation()
    {
        var model = new Kategoria();
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
    }
}
