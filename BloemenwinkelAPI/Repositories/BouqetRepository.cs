using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Bouqet>> GetAllBouqets(int storeId)
        {
            var storeWithBouqets = await _context.Store.Include(x => x.Bouqets).FirstOrDefaultAsync(x => x.Id == storeId);
            if (storeWithBouqets == null)
            {
                throw new KeyNotFoundException();
            }
            return storeWithBouqets.Bouqets;
        }

        public async Task<Bouqet> GetOneBouqetById(int storeId, int bouqetId)
        {
            CheckStoreExists(storeId);
            var bouqet = await _context.Bouqet.FirstOrDefaultAsync(x => x.StoreId == storeId && x.Id == bouqetId);
            if (bouqet == null)
            {
                throw new NotFoundException();
            }
            return bouqet;
        }

        public async Task Delete(int storeId, int bouqetId)
        {
            var bouqet = await _context.Bouqet.FindAsync(storeId, bouqetId);
            _context.Bouqet.Remove(bouqet);
            await _context.SaveChangesAsync();
        }

        public async Task<Bouqet> Insert(int storeId, string name, double price, string description)
        {
            CheckStoreExists(storeId);
            var bouqet = new Bouqet
            {
                Name = name,
                Price = price,
                Description = description
            };
            await _context.Bouqet.AddAsync(bouqet);
            await _context.SaveChangesAsync();
            return bouqet;
        }

        public async Task<Bouqet> Update(int storeId, int bouqetId, string name, double price)
        {
            var bouqet = await GetOneBouqetById(storeId, bouqetId);
            bouqet.Name = name;
            bouqet.Price = price;
            await _context.SaveChangesAsync();
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
