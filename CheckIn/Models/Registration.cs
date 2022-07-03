using System.ComponentModel.DataAnnotations;

namespace CheckIn.Models
{
    public class Registration
    {
        public int Id { get; set; }
        [Display(Name = "Entrada")]
        public DateTime? Entry { get; set; }
        [Display(Name = "Salida")]
        public DateTime? Exit { get; set; }

        [Display(Name = "N. Empleado")]
        public int? EmployeeId { get; set; }

        [Display(Name = "Nombre")]
        public Employee? Employee { get; set; }
    }
}
