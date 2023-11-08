using Ardalis.Specification;
using Domain.Entities.Enum;
using Domain.Entities.Model;

namespace UseCases.Handlers.Addresses.Specs;

public class FindingMatchesDadataSpec : Specification<Address>
{
    public FindingMatchesDadataSpec(List<string> fiasIds)
    {
        Query.Where(s => fiasIds.Contains(s.FiasId));
    }
}
