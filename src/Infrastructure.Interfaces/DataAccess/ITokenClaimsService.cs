namespace Infrastructure.Interfaces.DataAccess;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}
