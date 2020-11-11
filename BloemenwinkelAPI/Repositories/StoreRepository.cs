using System.Collections.Generic;
using System.Threading.Tasks;
using BloemenwinkelAPI.Database;
using BloemenwinkelAPI.Model;
using BloemenwinkelAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloemenwinkelAPI.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly BloemenwinkelDatabaseContext _context;

        public StoreRepository(BloemenwinkelDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Store>> GetAllStores()
        {
            return await _context.Store.ToListAsync();
        }

        public async Task<Store> GetOneStoreById(int id)
        {
            return await _context.Store.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var store = await _context.Store.FindAsync(id);
            if (store == null)
            {
                throw new NotFoundException();
            }

            _context.Store.Remove(store);
            await _context.SaveChangesAsync();
        }

        public async Task<Store> Insert(string name)
        {
            var store =  new Store
            {
                Name = name
            };
            await _context.Store.AddAsync(store);
            await _context.SaveChangesAsync();
            return store;
        }
        
        public async Task<Store> Update(int id, string name, string address, string region)
        {
            var store = await _context.Store.FindAsync(id);
            if (store == null)
            {
                throw new NotFoundException();
            }
            store.Name = name;
            store.Address = address;
            store.Region = region;
            await _context.SaveChangesAsync();
            return store;
        }
    }
}
