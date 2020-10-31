using BloemenwinkelAPI.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloemenwinkelAPI.Repositories
{
    public interface IBouqetRepository
    {
        IEnumerable<Bouqet> GetAllBouqets();
        Bouqet GetOneBouqetById(int id);
        void Delete(int id);
        Bouqet Insert(string name);
        Bouqet Update(int id, string name);
    }
}
