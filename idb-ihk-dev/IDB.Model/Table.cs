using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDB.Model
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Row> Rows { get; set; } = new List<Row>();
        public Table() { 
        Name = string.Empty;
        }
    }
}
