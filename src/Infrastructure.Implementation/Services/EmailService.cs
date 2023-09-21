using Domain.Entities.AppDb;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Services;

namespace Infrastructure.Implementation.Services;

internal class EmailService : IEmailService
{
    private readonly IDbContext _dbContext;

    public EmailService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var newMail = new Email
        {
            Address = email,
            Subject = subject,
            Body = body
        };

        _dbContext.Emails.Add(newMail);

        await _dbContext.SaveChangesAsync();
    }
}
