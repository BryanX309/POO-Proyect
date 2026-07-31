using System.ComponentModel.DataAnnotations;

namespace Electric.API.Dtos.Meters
{
    public class CreateMeterDto
    {
        [RegularExpression(@"^\d{7}$", //largo de código de 7 Dígitos
            ErrorMessage = "Debe contener exactamente 7 dígitos")]
        [Required(ErrorMessage = "Clave de Suministro es Requerida")]
        public string? SupplyKey { get; set; }

        [Required(ErrorMessage = "ID del Cliente es Requerida")]
        public string? ClientId { get; set; }

        [StringLength(20, ErrorMessage = "El Tipo de consumo no puede tener mas de 20 caracteres")]
        [Required(ErrorMessage = "El tipo de consumo es Requerido")]
        public string? ConsumptionType { get; set; }

        [Range(0.01, 999999.99, ErrorMessage = "La Tarifa debe ser mayor que cero")]
        [Required(ErrorMessage = "La Tarifa es Requerida")]
        public decimal Rate { get; set; }

        [StringLength(20, ErrorMessage = "El Sector Comercial no puede tener mas de 20 caracteres")]
        [Required(ErrorMessage = "El Sector Comercial es Requerido")]
        public string? ComercialSector { get; set; }
    }
}