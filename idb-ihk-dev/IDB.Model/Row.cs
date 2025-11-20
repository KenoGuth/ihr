using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDB.Model
{
    public class Row
    {
        public int Id { get; set; }
        public List<Cell> Cells { get; set; } = new List<Cell>();
    }
}
