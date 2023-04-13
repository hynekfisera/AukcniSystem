namespace AukcniSystem.Models
{
    public class Prihoz
    {
        public int PrihozId { get; set; }
        public double Castka { get; set; }
        public DateTime Datum { get; set; }
        public string KlientId { get; set; } = null!;
        public Klient Klient { get; set; } = null!;
        public int AukceId { get; set; }
        public Aukce Aukce { get; set; } = null!;
    }
}
