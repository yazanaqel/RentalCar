using RentalCar.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.Models;

public class Rental
{
	public int Id { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    public DateTime From { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    public DateTime To { get; set; }
    public bool IsWithDriver { get; set; }

	public Location Location { get; set; }

	public string UserId { get; set; }
	public ApplicationUser ApplicationUser { get; set; }

	public Guid CarId { get; set; }
    public Car Car { get; set; }
}
