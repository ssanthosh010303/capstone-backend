/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 31/07/2024
 */
using Azure;
using Azure.Communication.Email;
using Azure.Security.KeyVault.Secrets;

using WebApi.Exceptions;

namespace WebApi.Services;

public interface IEmailService
{
    Task SendEmailAsync(
        List<string> recipients, string subject, string htmlBody
    );
}

public class EmailService(
    IConfiguration config, SecretClient secretClient
) : IEmailService
{
    private readonly EmailClient _emailClient = new(
        secretClient.GetSecret("AzureEmailServiceConnectionString").Value.Value
    );
    private readonly string _sender = config["EMAIL_SENDER"]
        ?? throw new ArgumentNullException("InvalidEmailConfiguration");

    public async Task SendEmailAsync(
        List<string> recipients, string subject, string htmlBody
    )
    {
        var emailContent = new EmailContent(subject)
        {
            Html = htmlBody,

        };

        var emailMessage = new EmailMessage(
            _sender,
            new EmailRecipients(recipients
                .ConvertAll(email => new EmailAddress(email))
            ),
            emailContent
        );

        try
        {
            await _emailClient.SendAsync(WaitUntil.Started, emailMessage);
        }
        catch (RequestFailedException ex)
        {
            throw new ServiceException("EmailRequestFailed", ex);
        }
    }
}
