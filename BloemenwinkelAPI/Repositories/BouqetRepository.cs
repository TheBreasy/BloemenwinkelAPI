using System.Collections.Generic;
using System.Linq;
using BloemenwinkelAPI.Database;
using BloemenwinkelAPI.Model.Domain;


namespace BloemenwinkelAPI.Repositories
{
    public class BouqetRepository : IBouqetRepository
    {
        private readonly BloemenwinkelDatabaseContext _context;
        
        public BouqetRepository(BloemenwinkelDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Bouqet> GetAllBouqets()
        {
            return _context.Bouqet.ToList();
        }

        public Bouqet GetOneBouqetById(int id)
        {
            return _context.Bouqet.Find(id);
        }

        public void Delete(int id)
        {
            var bouqet = _context.Bouqet.Find(id);
            if (bouqet == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Bouqet.Remove(bouqet);
            _context.SaveChanges();
        }

        public Bouqet Insert(string name)
        {
            var Bouqet = new Bouqet
            {
                Name = name
            };
            _context.Bouqet.Add(Bouqet);
            _context.SaveChanges();
            return Bouqet;
        }

        public Bouqet Update(int id, string name)
        {
            var bouqet = _context.Bouqet.Find(id);
            if (bouqet == null)
            {
                throw new KeyNotFoundException();
            }

            bouqet.Name = name;
            _context.SaveChanges();
            return bouqet;
        }
    }
}
