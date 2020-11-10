using System.Collections.Generic;
using System.Linq;
using BloemenwinkelAPI.Database;
using BloemenwinkelAPI.Model;
using BloemenwinkelAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloemenwinkelAPI.Repositories
{
    public class BouqetRepository : IBouqetRepository
    {
        private readonly BloemenwinkelDatabaseContext _context;
        
        public BouqetRepository(BloemenwinkelDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Bouqet> GetAllBouqets(int storeId)
        {
            var storeWithBouqets = _context.Store.Include(x => x.Bouqets).FirstOrDefault(x => x.Id == storeId);
            if (storeWithBouqets == null)
            {
                throw new KeyNotFoundException();
            }
            return storeWithBouqets.Bouqets;
        }

        public Bouqet GetOneBouqetById(int storeId, int bouqetId)
        {
            CheckStoreExists(storeId);
            var bouqet = _context.Bouqet.FirstOrDefault(x => x.StoreId == storeId && x.Id == bouqetId);
            if (bouqet == null)
            {
                throw new NotFoundException();
            }
            return bouqet;
        }

        public void Delete(int storeId, int bouqetId)
        {
            var bouqet = _context.Bouqet.Find(storeId, bouqetId);
            _context.Bouqet.Remove(bouqet);
            _context.SaveChanges();
        }

        public Bouqet Insert(int storeId, string name, double price, string description)
        {
            CheckStoreExists(storeId);
            var bouqet = new Bouqet
            {
                Name = name,
                Price = price,
                Description = description
            };
            _context.Bouqet.Add(bouqet);
            _context.SaveChanges();
            return bouqet;
        }

        public Bouqet Update(int storeId, int bouqetId, string name, double price)
        {
            var bouqet = GetOneBouqetById(storeId, bouqetId);
            bouqet.Name = name;
            bouqet.Price = price;
            _context.SaveChanges();
            return bouqet;
        }

        private void CheckStoreExists(int storeId)
        {
            var storeCheck = _context.Store.Find(storeId);
            if (storeCheck == null)
            {
                throw new NotFoundException();
            }
        }
    }
}
