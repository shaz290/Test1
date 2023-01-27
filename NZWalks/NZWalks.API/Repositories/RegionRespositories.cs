using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRespository
    {
      Task<IEnumerable<Region>>  GetAllAsync();
    }
}
