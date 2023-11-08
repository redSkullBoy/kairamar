using Ardalis.Result;

namespace ApplicationServices.Interfaces;

public interface IAddressService
{
    /// <summary>
    /// Добавляет новые адреса в бд и возвращает ид от всех адресов
    /// </summary>
    /// <param name="text"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Result<IList<int>>> GetAddresIds(string text, CancellationToken cancellationToken);
}
