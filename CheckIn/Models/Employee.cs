using System.ComponentModel.DataAnnotations;

namespace CheckIn.Models
{
    public class Employee
    {
        [Display(Name ="N. Empleado")]
        public int Id { get; set; }
        [Display(Name ="Nombre")]
        [Required]
        public string? Name { get; set; }
        [Required]
        [Display(Name ="Puesto")]
        public string? Type { get; set; }
        [Required]
        [Display(Name = "N. Celular")]
        public string? PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        public string? Address { get; set; }
    }
}
