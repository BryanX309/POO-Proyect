namespace Electric.API.Dtos.Meters
{
    public class MeterDto
    {
        public string? Id { get; set; }
        public int SupplyKey { get; set; }
        public string? ClientId { get; set; }
        public string? ConsumptionType { get; set; }
        public string? Rate { get; set; }
        public string? ComercialSector { get; set; }
        public bool IsActive { get; set; } = true;
    }
}