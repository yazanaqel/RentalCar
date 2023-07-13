using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models;

public class Car
{
    [Key]
    public Guid Id { get; set; }
    public string EngineCapacity { get; set; }=String.Empty;
    public byte[]? Photo { get; set; }
    public string Color { get; set; } = String.Empty;
    public decimal DailyFare { get; set; }
    

    public CarType CarType { get; set; }
    

}
