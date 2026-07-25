using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electric.API.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")] // Id
        public string? Id { get; set; }

        [Column("created_by_id")] // Creado por Id
        public string? CreatedById { get; set; }        
        
        [Column("created_date")] // Fecha de Creación
        public DateTime CreatedDate { get; set; }
        
        [Column("modified_by_id")] // Modificado por Id
        public string? ModifiedById { get; set; }        
        
        [Column("modified_date")] // Fecha de Modification
        public DateTime ModifiedDate { get; set; }
    }
}