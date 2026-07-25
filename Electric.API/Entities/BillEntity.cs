using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electric.API.Entities
{
    public class BillEntity : BaseEntity
    {
        // - meterId (Information del Medidor)
        [Column("meter_id")]
        [Required]
        public string? MeterId { get; set; }

        // - dueDate (Fecha de Vencimiento)
        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }
        
        // - totalAmount (Total a Pagar)
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        // - previousReading (Lectura Anterior)
        [Column("previous_reading")]
        public int PreviousReading { get; set; }

        // - currentReading (Lectura Actual)
        [Column("current_reading")]
        public int CurrentReading { get; set; }

        // - previousReadingDate (Fecha Lectura anterior)
        [Column("previous_reading_date")]
        public DateTime PreviousReadingDate { get; set; }

        // - currentReadingDate (Fecha Lectura Actual)
        [Column("current_reading_date")]
        public DateTime CurrentReadingDate { get; set; }

        // - paid (Pagado)
        [Column("paid")]
        public bool Paid { get; set; }

        [ForeignKey(nameof(MeterId))]
        public virtual MeterEntity? Meter { get; set; }
    }
}