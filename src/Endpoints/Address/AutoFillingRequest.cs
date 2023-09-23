using FastEndpoints;

namespace Endpoints.Address;

public class AutoFillingRequest
{
    [QueryParam]
    public string? Text { get; set; }
}
