using SystemHR.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class WracanieTests
{
    [Fact]
    public void Valid_Wracanie_PassesValidation()
    {
        var model = new Wracanie
        {
            Nr_identyfikacyjny = "1234567890"
        };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Invalid_Wracanie_FailsValidation()
    {
        var model = new Wracanie(); 
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
    }
}
