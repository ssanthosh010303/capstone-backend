/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 31/07/2024
 */
using System.Text.RegularExpressions;
using WebApi.Exceptions;

namespace WebApi.Email;

public enum EmailTemplateTypes
{
    Welcome,
    ForgotPassword,
    ResetPasswordSuccessful,
    ChangeEmail,
    GrievanceAssignmentWillingness,
    GrievanceCreatedEmployee,
    GrievanceCreatedManager,
    GrievanceAssigned,
    GrievanceResolved,
    GrievanceReopened,
    GrievanceEscalated,
    MeetingScheduled
}

public interface IEmailTemplateLoader
{
    (string subject, string body) GetTemplate(
        EmailTemplateTypes templateType, params string[] placeholders
    );
}

public class EmailTemplateLoader : IEmailTemplateLoader
{
    private readonly Dictionary<EmailTemplateTypes, (string subject, string body)> _templates;

    public EmailTemplateLoader()
    {
        _templates = [];

        LoadTemplates("./Email/Templates/");
    }

    private void LoadTemplates(string templateDirectory)
    {
        foreach (EmailTemplateTypes templateType in Enum.GetValues(typeof(EmailTemplateTypes)))
        {
            var subjectFilePath = Path.Combine(
                templateDirectory,
                $"{templateType}.txt"
            );
            var bodyFilePath = Path.Combine(
                templateDirectory,
                $"{templateType}.html"
            );

            if (File.Exists(subjectFilePath) && File.Exists(bodyFilePath))
            {
                _templates[templateType] = (
                    File.ReadAllText(subjectFilePath).TrimEnd(),
                    File.ReadAllText(bodyFilePath)
                );
            }
            else
            {
                throw new ServiceException("EmailTemplateMissing");
            }
        }
    }

    public (string subject, string body) GetTemplate(
        EmailTemplateTypes templateType, params string[] placeholders
    )
    {
        (string subject, string body) = _templates[templateType];

        return (subject, ReplacePlaceholders(body, placeholders));
    }

    private static string ReplacePlaceholders(string template, params string[] args)
    {
        string pattern = @"\[(\d+)\]";
        string result = Regex.Replace(template, pattern, match =>
        {
            int index;
            if (int.TryParse(match.Groups[1].Value, out index) && index < args.Length)
            {
                return args[index]; // Index In-range
            }
            return match.Value;
        });

        return result;
    }
}
