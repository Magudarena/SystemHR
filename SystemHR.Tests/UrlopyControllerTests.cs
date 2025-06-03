using System;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemHR.Controllers;
using SystemHR.Models;
using Xunit;

namespace SystemHR.Tests
{
    public class UrlopyControllerTests
    {
        private static SystemHRContext NewContext()
        {
            var options = new DbContextOptionsBuilder<SystemHRContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var ctx = new SystemHRContext(options);
            ctx.Database.EnsureCreated();
            return ctx;
        }

        [Fact]
        public void Index_ReturnsViewWithModel()
        {
            using var ctx = NewContext();
            var controller = new Controllers.UrlopyController(ctx);

            var result = controller.Index();

            var view = result.Should().BeOfType<ViewResult>().Subject;
            view.ViewName.Should().Be("ListaUrlopow");
            view.Model.Should().NotBeNull();
        }

        [Fact]
        public void Dodaj_InvalidModel_ReturnsView()
        {
            using var ctx = NewContext();
            var controller = new Controllers.UrlopyController(ctx);
            controller.ModelState.AddModelError("nazwa_wolnego", "Required");

            var urlop = new Urlop();
            var result = controller.Dodaj(urlop);

            result.Should().BeOfType<ViewResult>()
                  .Subject.Model.Should().Be(urlop);
        }

        [Fact]
        public void Dodaj_ValidModel_PersistsAndRedirects()
        {
            using var ctx = NewContext();
            var controller = new Controllers.UrlopyController(ctx);

            var urlop = new Urlop
            {
                Nr_identyfikacyjny = "9876543210",
                nazwa_wolnego = "Urlop testowy",
                dane_wolnego = "Opis",
                Identyfikator = "9876543210987",
                Kategoria = 2,
                Dostepne = true
            };

            var result = controller.Dodaj(urlop);

            result.Should().BeOfType<RedirectToActionResult>()
                  .Subject.ActionName.Should().Be("Index");

            ctx.NowyUrlop.Any(u => u.nazwa_wolnego == "Urlop testowy").Should().BeTrue();
        }
    }
}
