namespace Electric.API.Dtos.Bills
{
    public class BillDto
    {
        public string? Id { get; set; }
        public string? MeterId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PreviousReading { get; set; }
        public int CurrentReading { get; set; }
        public DateTime PreviousReadingDate { get; set; }
        public DateTime CurrentReadingDate { get; set; } = DateTime.Now;
        public bool Paid { get; set; } = false;
    }
}