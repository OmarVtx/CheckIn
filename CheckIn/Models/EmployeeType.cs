using System.ComponentModel.DataAnnotations;

namespace CheckIn.Models
{
    public class EmployeeType
    {
        public int Id { get; set; }
        
        [Display(Name = "Tipo de empleado")]
        public string? Name { get; set; }
    }
}
