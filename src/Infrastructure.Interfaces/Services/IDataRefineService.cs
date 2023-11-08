using Ardalis.Result;
using Infrastructure.Interfaces.Dto;

namespace Infrastructure.Interfaces.Services;

public interface IDataRefineService
{
    Task<Result<IList<AddressDataDto>>> AddressAutoFillingAsync(string text, CancellationToken ctn);
}
