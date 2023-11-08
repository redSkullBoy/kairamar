using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Implementation.Services;

public class CurrentUserService : ICurrentUserService
{
    private bool _isAuthenticated;
    private string _id = string.Empty;
    private string _email = string.Empty;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _isAuthenticated = httpContextAccessor!.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        if (_isAuthenticated)
        {
            _id = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            _email = httpContextAccessor.HttpContext.User.Identity.Name!;
        }
    }

    public string Id => _id;

    public bool IsAuthenticated => _isAuthenticated;

    public string Email => _email;
}
