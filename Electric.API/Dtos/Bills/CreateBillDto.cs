using System.ComponentModel.DataAnnotations;

namespace Electric.API.Dtos.Bills
{
    public class CreateBillDto
    {
        [Required(ErrorMessage = "El Id del Contador es Requerido")]
        public string? MeterId { get; set; }

        [Range(1, 9999999, ErrorMessage = "La Lectura Actual debe ser un numero mayor a Cero")]
        [Required(ErrorMessage = "La Lectura Actual es Requerida")]
        public int CurrentReading { get; set; }
    }
}