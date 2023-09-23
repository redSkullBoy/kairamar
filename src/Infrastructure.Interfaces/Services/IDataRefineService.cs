using Ardalis.Result;
using Infrastructure.Interfaces.Dto;

namespace Infrastructure.Interfaces.Services;

public interface IDataRefineService
{
    Task<Result<List<AddressDataDto>>> AddressAutoFillingAsync(string text);
}
