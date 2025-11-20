using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDB.Model
{
    public class Cell
    {
        public int Id { get; set; }
        public int Id_row { get; set; }
        public int Id_table { get; set; }
        public int Id_column { get; set; }
        public string? Data_value { get; set; }
        public bool SVS_Verweis { get; set; }
    }
}
