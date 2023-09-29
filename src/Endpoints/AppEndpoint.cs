using Ardalis.Result;
using FastEndpoints;
using FluentValidation.Results;

namespace Endpoints;

public class AppEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
{
    public void AddError(List<ValidationFailure> failures)
    {
        if (failures != null) 
        {
            foreach (var error in failures)
                AddError(error);
        }
    }

    public async Task ResultToSendAsync(Result<TResponse> result)
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                await SendOkAsync(result.Value);
                break;
            case ResultStatus.NotFound:
                await SendNotFoundAsync();
                break;
            case ResultStatus.Unauthorized:
                await SendUnauthorizedAsync();
                break;
            case ResultStatus.Forbidden:
                await SendForbiddenAsync();
                break;
            case ResultStatus.Invalid:
                {
                    foreach (var error in result.ValidationErrors)
                    {
                        AddError(error.ErrorMessage, error.Identifier);
                    }

                    await SendErrorsAsync();
                    break;
                }
            case ResultStatus.Error:
                {
                    foreach (var error in result.Errors)
                    {
                        ThrowError(error);
                    }

                    await SendErrorsAsync();
                    break;
                }
            case ResultStatus.Conflict:
                foreach (var error in result.Errors)
                {
                    ThrowError(error);
                }

                await SendErrorsAsync(409);
                break;
            default:
                throw new NotSupportedException($"Result {result.Status} conversion is not supported.");
        }
    }
}
