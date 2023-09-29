using Infrastructure.Interfaces.Services;

namespace Infrastructure.Implementation.Services;

internal class CurrentUserService : ICurrentUserService
{
    public string Id => throw new NotImplementedException();

    public bool IsAuthenticated => throw new NotImplementedException();

    public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
