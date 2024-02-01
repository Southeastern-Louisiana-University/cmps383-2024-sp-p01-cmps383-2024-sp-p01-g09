namespace Selu383.SP24.Api.Features
{
    public class HotelDto
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Address { get; set; } = string.Empty; 
    }
}
