namespace IDB.Model
{
    public class Ausfuellhilfe
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string? Notiz { get; set; }

        public Ausfuellhilfe()
        {
            Titel = string.Empty;
        }
    }
}