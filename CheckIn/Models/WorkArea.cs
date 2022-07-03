using System.ComponentModel.DataAnnotations;

namespace CheckIn.Models
{
    public class WorkArea
    {
        public int Id { get; set; }
        [Display(Name = "Area de trabajo")]
        [Required]
        public string? Name { get; set; }
    }
}