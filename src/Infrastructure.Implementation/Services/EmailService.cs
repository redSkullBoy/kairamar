using Domain.Entities.Model;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Infrastructure.Implementation.Services;

internal class EmailService : IEmailSender
{
    private readonly IDbContext _dbContext;

    public EmailService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        //var newMail = new Email
        //{
        //    Address = email,
        //    Subject = subject,
        //    Body = body
        //};

        //_dbContext.Emails.Add(newMail);

        //await _dbContext.SaveChangesAsync();
    }
}
