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

    public async Task ResultToSendAsync(IResult result)
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                if (typeof(Result).IsInstanceOfType(result))
                    await SendOkAsync();
                else
                    await SendOkAsync((TResponse)result.GetValue());

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
                result.ValidationErrors.ForEach(e =>
                    ValidationFailures.Add(new(e.Identifier, e.ErrorMessage)));

                await HttpContext.Response.SendErrorsAsync(ValidationFailures);
                break;
            case ResultStatus.Error:
                {
                    foreach (var error in result.Errors)
                    {
                        AddError(error);
                    }

                    await SendErrorsAsync();
                    break;
                }
            case ResultStatus.Conflict:
                foreach (var error in result.Errors)
                {
                    AddError(error);
                }

                await SendErrorsAsync(409);
                break;
            default:
                throw new NotSupportedException($"Result {result.Status} conversion is not supported.");
        }
    }
}
