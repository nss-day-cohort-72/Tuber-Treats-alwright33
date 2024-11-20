namespace TuberTreats.Models.DTOs;

public class TuberOrderDTO
{
    public int Id { get; set; }
    public DateTime OrderPlacedOnDate { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int? TuberDriverId { get; set; }
    public string TuberDriverName { get; set; }
    public DateTime? DeliveredOnDate { get; set; }
    public List<TuberToppingDTO> Toppings { get; set; } = new List<TuberToppingDTO>();
}
