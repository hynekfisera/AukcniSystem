using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AukcniSystem.Models;

namespace AukcniSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Aukce> Aukce { get; set; }
        public DbSet<Prihoz> Prihozy { get; set; }
        public DbSet<Kategorie> Kategorie { get; set; }
        public DbSet<Klient> Klienti { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}