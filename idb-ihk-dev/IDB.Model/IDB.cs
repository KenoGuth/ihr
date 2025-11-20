using System.ComponentModel.DataAnnotations;

namespace IDB.Model
{
    public class IDB
    {
        public int Id { get; set; }
     
        [Required]
        [StringLength(250)]
        public string? Table_Name { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        public string? Menu_allowed { get; set; }
    }





}