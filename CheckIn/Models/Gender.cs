using System.ComponentModel.DataAnnotations;

namespace CheckIn.Models
{
    public class Gender
    {
        public int Id { get; set; }
        [Display(Name = "Genero")]
        public string? Name { get; set; }
    }
}
