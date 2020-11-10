using System.Collections.Generic;
using BloemenwinkelAPI.Model.Domain;

namespace BloemenwinkelAPI.Repositories
{
    public interface IStoreRepository
    {
        IEnumerable<Store> GetAllStores();
        Store GetOneStoreById(int id);
        void Delete(int id);
        Store Insert(string name);
        Store Update(int id, string name);

    }
}
