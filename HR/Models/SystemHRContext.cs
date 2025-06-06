using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class SystemHRContext : DbContext
    {
        public SystemHRContext(DbContextOptions<SystemHRContext> options)
            : base(options)
        {
        }


        public DbSet<ListaPracownikow> ListaPracownikow { get; set; }
        public DbSet<ListaUrlopow> Urlop { get; set; }
        public DbSet<Pracownik> Pracownik { get; set; }
        public DbSet<Urlop> NowyUrlop { get; set; }
        public DbSet<Kategoria> Kategoria { get; set; }
        public DbSet<UrlopPerPracownik> UrlopPerPracownik { get; set; }
        public DbSet<BranieWolnego> Wolne { get; set; }
        public DbSet<PracownikHR> Uzytkownicy { get; set; }
        public DbSet<Uprawnienia> Uprawnienia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Uprawnienia>(entity =>
            {
                entity.ToTable("uprawnienia");
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<PracownikHR>(entity =>
            {
                entity.ToTable("pracownikhr");
                entity.HasKey(u => u.Id);
                entity.HasOne(u => u.Uprawnienia)
                      .WithMany(p => p.Uzytkownicy)
                      .HasForeignKey(u => u.Id_Uprawnienia);
            });

            modelBuilder.Entity<BranieWolnego>(entity =>
            {
                entity.ToTable("Wolne"); // Nazwa tabeli w bazie danych
                entity.HasKey(w => w.Id); // Definicja klucza głównego
            });

            modelBuilder.Entity<UrlopPerPracownik>(entity =>
            {
                entity.HasNoKey(); // Widok jest bezkluczowy
                entity.ToView("UrlopPerPracownik"); // Mapowanie do widoku
            });

            // Mapowanie dla klasy NowyUrlop
            modelBuilder.Entity<Urlop>().ToTable("urlop"); // Tabela używana do zapisu urlopów

            // Wymuszenie nazwy tabeli "ListaPracownikow" dla klasy Pracownik
            modelBuilder.Entity<ListaPracownikow>().HasNoKey().ToTable("ListaPracownikow");

            // Wymuszenie nazwy tabeli "ListaUrlopow" dla klasy Urlop
            modelBuilder.Entity<ListaUrlopow>().HasNoKey().ToTable("ListaUrlopow");

            // Tabela używana do zapisu Pracowników
            modelBuilder.Entity<Pracownik>().ToTable("Pracownik").HasKey(k => k.Id);

            modelBuilder.Entity<Kategoria>().ToTable("kategoria");
        }
    }
}
