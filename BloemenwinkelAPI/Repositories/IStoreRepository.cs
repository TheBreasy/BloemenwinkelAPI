using System.Collections.Generic;
using System.Threading.Tasks;
using BloemenwinkelAPI.Model.Domain;

namespace BloemenwinkelAPI.Repositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllStores();
        Task<Store> GetOneStoreById(int id);
        Task Delete(int id);
        Task<Store> Insert(string name);
        Task<Store> Update(int id, string name, string Address, string Region);

    }
}
