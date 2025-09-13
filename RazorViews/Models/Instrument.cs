namespace RazorViews.Models
{
    public class Instrument
    {
        public int Id { get; set; }
        public int PersonId { get; set; } // Foreign key to Person
        public string Name { get; set; }
        public InstrumentType Type { get; set; } // e.g., String, Percussion, Wind
        public string Brand { get; set; }
        public decimal Price { get; set; }
    }

    public enum InstrumentType
    {
        String,
        Percussion,
        Wind,
        Keyboard,
        Electronic
    }
}
