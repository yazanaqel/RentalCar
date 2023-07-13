namespace RentalCar.Services.EmailService;

public interface IEmailService
{
	public Task SendEmailAsync(string To, string Subject, string Body);
}
