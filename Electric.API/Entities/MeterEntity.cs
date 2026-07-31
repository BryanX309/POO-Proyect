using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electric.API.Entities
{
    [Table("meters")]
    public class MeterEntity : BaseEntity
    {
        // - supplyKey (Clave de Suministro)
        [Column("supply_key")]
        public string? SupplyKey { get; set; }

        //Lo mas adecuado seria hacer que ClientId sea una llave Foránea con una tabla de Clientes
        // - clientId (Id Cliente)
        [Column("client_Id")]
        [Required]
        public string? ClientId { get; set; }

        // - consumptionType (Tipo de Consumo)
        [Column("consumption_type")]
        public string? ConsumptionType { get; set; }

        // - rate (Tarifa)
        [Column("rate")]
        public decimal Rate { get; set; }

        // - comercialSector (Sector Comercial)
        [Column("comercial_sector")]
        public string? ComercialSector { get; set; }

        // - isActive (esta Activo)
        [Column("is_active")]
        public bool IsActive { get; set; }

        public virtual IEnumerable<BillEntity>? Bills { get; set; }
    }
}