using BloemenwinkelAPI.Model.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloemenwinkelAPI.Repositories
{
    public interface IBouqetRepository
    {
        Task<IEnumerable<Bouqet>> GetAllBouqets(int storeId);
        Task<Bouqet> GetOneBouqetById(int storeId, int bouqetId);
        Task Delete(int storeId, int bouqetId);
        Task<Bouqet> Insert(int storeId, string name, double price, string description);
        Task<Bouqet> Update(int storeId, int bouqetId, string name, double price);
    }
}