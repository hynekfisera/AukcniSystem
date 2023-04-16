namespace AukcniSystem.Models
{
    public class Aukce
    {
        public int AukceId { get; set; }
        public string? AutorId { get; set; }
        public Klient? Autor { get; set; }
        public int KategorieId { get; set; }
        public Kategorie? Kategorie { get; set; }
        public string? Nazev { get; set; }
        public string? Popis { get; set; }
        public double Cena { get; set; }
        public string? Foto { get; set; }
        public DateTime? Datum { get; set; }
        public bool PrihozeniPoCastce { get; set; }
        public double MinimalniPrihoz { get; set; }
        public bool Schvalena { get; set; }
        public int DobaTrvani { get; set; }
        public IEnumerable<Prihoz> Prihozy { get; set; } = new List<Prihoz>();
    }
}
