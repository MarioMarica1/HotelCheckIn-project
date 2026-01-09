namespace Hosta_Hotel.Entities;

public record Room
{
    public int RoomNumber { get; set; }
    public string Status { get; set; } = "Free"; 
    public string RoomType { get; set; } = "Single";
    public int PricePerNight { get; set; }
}