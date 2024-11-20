namespace TuberTreats.Models.DTOs;

public class TuberDriverDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> DeliveryOrderIds { get; set; } = new List<int>();
}