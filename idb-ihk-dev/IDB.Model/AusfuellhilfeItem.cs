

namespace IDB.Model
{
    public class AusfuellhilfeItem
    {
        public int Id { get; set; }
        public int Id_ausfuellhilfe { get; set; }
        public string Text { get; set; }

        public AusfuellhilfeItem()
        {
            Text = string.Empty;
        }
    }
}