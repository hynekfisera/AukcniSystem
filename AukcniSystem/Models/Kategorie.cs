namespace AukcniSystem.Models
{
    public class Kategorie
    {
        public int KategorieId { get; set; }
        public string? Nazev { get; set; }
        public IEnumerable<Aukce> Aukce { get; set; } = new List<Aukce>();
    }
}
