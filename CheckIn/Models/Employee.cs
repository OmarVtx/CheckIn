using System.ComponentModel.DataAnnotations;

namespace CheckIn.Models
{
    public class Employee
    {
        [Display(Name = "N. Empleado")]
        public int Id { get; set; }
        [Display(Name = "Nombre completo")]
        [Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Edad")]
        public int Age
        {
            get
            {
                DateTime currentDate = DateTime.Today;
                int age = currentDate.Year - Birthday.Year;
                if (currentDate < Birthday.AddYears(age))
                    return --age;
                else
                    return age;
            }
        }

        [Required]
        [Display(Name = "N. Celular")]
        public string? PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        public string? Address { get; set; }

        [Display(Name = "Genero")]
        public int? GenderId { get; set; }
        [Display(Name = "Tipo de empleado")]
        public int? EmployeeTypeId { get; set; }
        [Display(Name = "Almacen")]
        public int? WarehouseId { get; set; }
        [Display(Name = "Area de trabajo")]
        public int? WorkAreaId { get; set; }

        [Display(Name = "Genero")]
        public Gender? Gender { get; set; }
        [Display(Name = "Tipo de empleado")]
        public EmployeeType? EmployeeType { get; set; }
        [Display(Name = "Almacen")]
        public Warehouse? Warehouse { get; set; }
        [Display(Name = "Area de trabajo")]
        public WorkArea? WorkArea { get; set; }

    }
}
