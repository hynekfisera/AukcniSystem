using Microsoft.AspNetCore.Identity;

namespace AukcniSystem.Models
{
    public class Klient : IdentityUser
    {
        public Role Role { get; set; }
        public double Zustatek { get; set; }
        public string? Stat { get; set; }
        public string? Mesto { get; set; }
        public string? Ulice { get; set; }
        public string? CisloPopisne { get; set; }
        public IEnumerable<Aukce> Aukce { get; set; } = new List<Aukce>();
        public IEnumerable<Prihoz> Prihozy { get; set; } = new List<Prihoz>();
    }

    public enum Role
    {
        Admin,
        Ucetni,
        Supervizor,
        Klient
    }
}
