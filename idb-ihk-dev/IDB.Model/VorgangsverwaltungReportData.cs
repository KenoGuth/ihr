using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDB.Model
{
    public class VorgangsverwaltungReportData
    {
        public int RowId { get; set; }
        public string? Aktenzeichen { get; set; }
        public string? Vorgang { get; set; }
        public string? Amt1 { get; set; }
        public string? Amt2 { get; set; }
        public string? Betreff { get; set; }
        public string? Stichworte { get; set; }
        public string? Rechtsgrundlage { get; set; }
        public string? TBNr { get; set; }
        public bool? Ablage { get; set; }
        public bool? TBBeitrag { get; set; }
        public DateTime? Eingangsdatum { get; set; }
        public DateTime? Ablagedatum { get; set; }
        public DateTime? WVDatum { get; set; }
    }
}
