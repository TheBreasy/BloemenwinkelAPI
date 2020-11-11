using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloemenwinkelAPI.Database;
using BloemenwinkelAPI.Model;
using BloemenwinkelAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloemenwinkelAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BloemenwinkelDatabaseContext _context;

        public OrderRepository(BloemenwinkelDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders(int storeId)
        {
            var orderFromStores = await _context.Store.Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == storeId);
            if (orderFromStores == null)
            {
                throw new NotFoundException();
            }
            return orderFromStores.Orders;
        }

        public async Task<IEnumerable<Order>> GetBestSellingBouqets()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<Order> GetOneOrderById(int storeId, int orderId)
        {
            CheckStoreExists(storeId);
            var order = await _context.Order.FirstOrDefaultAsync(x => x.StoreId == storeId && x.Id == orderId);
            if (order == null)
            {
                throw new NotFoundException();
            }
            return order;
        }

        public async Task Delete(int storeId, int orderId)
        {
            var order = await GetOneOrderById(storeId, orderId);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> Insert(int storeId, int bouqetId, int amount)
        {
            CheckStoreExists(storeId);
            var order = new Order()
            {
                StoreId = storeId,
                BouqetId = bouqetId,
                Amount = amount
            };
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Update(int storeId, int bouqetId, int orderId, int amount)
        {
            var order = await GetOneOrderById(storeId, orderId);
            order.StoreId = storeId;
            order.BouqetId = bouqetId;
            order.Amount = amount;
            await _context.SaveChangesAsync();
            return order;
        }

        private void CheckStoreExists(int storeId)
        {
            var storeCheck =  _context.Store.Find(storeId);
            if (storeCheck == null)
            {
                throw new NotFoundException();
            }
        }
    }
}
