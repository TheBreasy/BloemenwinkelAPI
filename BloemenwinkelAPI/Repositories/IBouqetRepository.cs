using BloemenwinkelAPI.Model.Domain;
using System.Collections.Generic;

namespace BloemenwinkelAPI.Repositories
{
    public interface IBouqetRepository
    {
        IEnumerable<Bouqet> GetAllBouqets(int storeId);
        Bouqet GetOneBouqetById(int storeId, int bouqetId);
        void Delete(int storeId, int bouqetId);
        Bouqet Insert(int storeId, string name, double price, string description);
        Bouqet Update(int storeId, int bouqetId, string name, double price);
    }
}