
using System.Collections.ObjectModel;

namespace Ardalis.Result;
public class PaginatedResult<T> : Result<IReadOnlyCollection<T>>
{
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedResult(IReadOnlyCollection<T> value, int count, int pageNumber, int pageSize, string successMessage)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Value = value;
        SuccessMessage = successMessage;
    }

    protected PaginatedResult(ResultStatus status) : base(status) 
    {

    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Represents a successful operation and accepts a values as the result of the operation
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static PaginatedResult<T> Success(IReadOnlyCollection<T> value, int count, int pageNumber, int pageSize)
    {
        return new PaginatedResult<T>(value, count, pageNumber, pageSize, string.Empty);
    }

    /// <summary>
    /// Represents a successful operation and accepts a values as the result of the operation
    /// Sets the SuccessMessage property to the provided value
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <param name="successMessage">Sets the SuccessMessage property</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static PaginatedResult<T> Success(IReadOnlyCollection<T> value, int count, int pageNumber, int pageSize, string successMessage)
    {
        return new PaginatedResult<T>(value, pageSize, count, pageNumber, successMessage);
    }

    /// <summary>
    /// Represents an error that occurred during the execution of the service.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">A list of string error messages.</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> Error(params string[] errorMessages)
    {
        PaginatedResult<T> result = new PaginatedResult<T>(ResultStatus.Error);

        if (errorMessages != null && errorMessages.Length > 0)
            result.Errors = new ObservableCollection<string>(errorMessages);

        return result;
    }

    /// <summary>
    /// Represents a validation error that prevents the underlying service from completing.
    /// </summary>
    /// <param name="validationError">The validation error encountered</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static PaginatedResult<T> Invalid(ValidationError validationError)
    {
        return new PaginatedResult<T>(ResultStatus.Invalid) { ValidationErrors = { validationError } };
    }

    /// <summary>
    /// Represents validation errors that prevent the underlying service from completing.
    /// </summary>
    /// <param name="validationErrors">A list of validation errors encountered</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> Invalid(params ValidationError[] validationErrors)
    {
        PaginatedResult<T> result = new PaginatedResult<T>(ResultStatus.Invalid);

        if (validationErrors != null && validationErrors.Length > 0)
            result.ValidationErrors = new ObservableCollection<ValidationError>(validationErrors);

        return result;
    }

    /// <summary>
    /// Represents validation errors that prevent the underlying service from completing.
    /// </summary>
    /// <param name="validationErrors">A list of validation errors encountered</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> Invalid(List<ValidationError> validationErrors)
    {
        return Invalid(validationErrors.ToArray());
    }

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> NotFound()
    {
        return new PaginatedResult<T>(ResultStatus.NotFound);
    }

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">A list of string error messages.</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> NotFound(params string[] errorMessages)
    {
        PaginatedResult<T> result = new PaginatedResult<T>(ResultStatus.NotFound);

        if (errorMessages != null || errorMessages.Length > 0)
            result.Errors = new ObservableCollection<string>(errorMessages);

        return result;
    }

    /// <summary>
    /// The parameters to the call were correct, but the user does not have permission to perform some action.
    /// See also HTTP 403 Forbidden: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> Forbidden()
    {
        return new PaginatedResult<T>(ResultStatus.Forbidden);
    }

    /// <summary>
    /// This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to authenticate but failed.
    /// See also HTTP 401 Unauthorized: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> Unauthorized()
    {
        return new PaginatedResult<T>(ResultStatus.Unauthorized);
    }

    /// <summary>
    /// Represents a situation where a service is in conflict due to the current state of a resource,
    /// such as an edit conflict between multiple concurrent updates.
    /// See also HTTP 409 Conflict: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> Conflict()
    {
        return new PaginatedResult<T>(ResultStatus.Conflict);
    }

    /// <summary>
    /// Represents a situation where a service is in conflict due to the current state of a resource,
    /// such as an edit conflict between multiple concurrent updates.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// See also HTTP 409 Conflict: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <param name="errorMessages">A list of string error messages.</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> Conflict(params string[] errorMessages)
    {
        PaginatedResult<T> result = new PaginatedResult<T>(ResultStatus.Conflict);

        if (errorMessages != null || errorMessages.Length > 0)
            result.Errors = new ObservableCollection<string>(errorMessages);

        return result;
    }

    /// <summary>
    /// Represents a critical error that occurred during the execution of the service.
    /// Everything provided by the user was valid, but the service was unable to complete due to an exception.
    /// See also HTTP 500 Internal Server Error: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#5xx_server_errors
    /// </summary>
    /// <param name="errorMessages">A list of string error messages.</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static new PaginatedResult<T> CriticalError(params string[] errorMessages)
    {
        PaginatedResult<T> result = new PaginatedResult<T>(ResultStatus.CriticalError);

        if (errorMessages != null || errorMessages.Length > 0)
            result.Errors = new ObservableCollection<string>(errorMessages);

        return result;
    }

    /// <summary>
    /// Represents a situation where a service is unavailable, such as when the underlying data store is unavailable.
    /// Errors may be transient, so the caller may wish to retry the operation.
    /// See also HTTP 503 Service Unavailable: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#5xx_server_errors
    /// </summary>
    /// <param name="errorMessages">A list of string error messages</param>
    /// <returns></returns>
    public static new PaginatedResult<T> Unavailable(params string[] errorMessages)
    {
        PaginatedResult<T> result = new PaginatedResult<T>(ResultStatus.Unavailable);

        if (errorMessages != null || errorMessages.Length > 0)
            result.Errors = new ObservableCollection<string>(errorMessages);

        return result;
    }
}
