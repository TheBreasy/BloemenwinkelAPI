using System.Collections.Generic;
using System.Linq;
using BloemenwinkelAPI.Database;
using BloemenwinkelAPI.Model;
using BloemenwinkelAPI.Model.Domain;

namespace BloemenwinkelAPI.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly BloemenwinkelDatabaseContext _context;

        public StoreRepository(BloemenwinkelDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Store> GetAllStores()
        {
            return _context.Store.ToList();
        }

        public Store GetOneStoreById(int id)
        {
            return _context.Store.Find(id);
        }

        public void Delete(int id)
        {
            var store = _context.Store.Find(id);
            if (store == null)
            {
                throw new NotFoundException();
            }

            _context.Store.Remove(store);
            _context.SaveChanges();
        }

        public Store Insert(string name)
        {
            var store = new Store
            {
                Name = name
            };
            _context.Store.Add(store);
            _context.SaveChanges();
            return store;
        }

        public Store Update(int id, string name)
        {
            var store = _context.Store.Find(id);
            if (store == null)
            {
                throw new NotFoundException();
            }
            store.Name = name;
            _context.SaveChanges();
            return store;
        }
    }
}
