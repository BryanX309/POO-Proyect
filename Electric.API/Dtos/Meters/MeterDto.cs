namespace Electric.API.Dtos.Meters
{
    public class MeterDto
    {
        public string? Id { get; set; }
        public string? SupplyKey { get; set; }
        public string? ClientId { get; set; }
        public string? ConsumptionType { get; set; }
        public decimal Rate { get; set; }
        public string? ComercialSector { get; set; }
        public bool IsActive { get; set; } = true;
    }
}