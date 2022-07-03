using System.ComponentModel.DataAnnotations;

namespace CheckIn.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Almacen")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        public string? Address { get; set; }
    }
}
