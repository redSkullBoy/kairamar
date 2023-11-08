namespace Infrastructure.Interfaces.Services;

public interface ICurrentUserService
{
    string Id { get; }
    bool IsAuthenticated { get; }
    string Email { get; }
}
