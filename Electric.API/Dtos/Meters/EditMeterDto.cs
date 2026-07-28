namespace Electric.API.Dtos.Meters
{
    public class EditMeterDto : CreateMeterDto
    {
        public bool IsActive { get; set; }
    }
}