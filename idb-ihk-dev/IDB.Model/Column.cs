using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDB.Model
{
    public class Column
    {
        public int Id { get; set; }
        public int Id_table { get; set; }
        public int? Id_ausfuellhilfe {  get; set; }
        public int Column_no { get; set; }
        [StringLength(250)]
        public string Name { get; set; } 
        public string Data_type { get; set; }
        // private List<string> dataTypes = new List<string> { "Ganze Zahl", "Text", "Datum/Zeit", "Checkbox" };
        public Boolean Is_nullable { get; set; }
        public string? Default_value { get; set; }
        public bool? Svs_data { get; set; }
        public bool Archived { get; set; }
        
        public Column () {
            Name = string.Empty;
            Data_type = string.Empty;
        }
    }
}
