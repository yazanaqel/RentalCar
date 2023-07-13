using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace RentalCar.Services.EmailService;

public class EmailService : IEmailService
{
	private readonly IConfiguration configuration;

	public EmailService(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	public async Task SendEmailAsync(string To, string Subject, string Body)
	{
		var email = new MimeMessage();
		email.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailUsername").Value));
		email.To.Add(MailboxAddress.Parse(To));
		email.Subject = Subject;
		email.Body = new TextPart(TextFormat.Html) { Text = Body };

		using var smptClient = new SmtpClient();
		smptClient.Connect(configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
		smptClient.Authenticate(configuration.GetSection("EmailUsername").Value, configuration.GetSection("EmailPassword").Value);
		smptClient.Send(email);
		smptClient.Disconnect(true);

	}
}
