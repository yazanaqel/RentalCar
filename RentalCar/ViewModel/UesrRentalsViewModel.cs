using RentalCar.Data;
using RentalCar.Models;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.ViewModel;

public class UesrRentalsViewModel
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

	public Guid CarId { get; set; }
}
