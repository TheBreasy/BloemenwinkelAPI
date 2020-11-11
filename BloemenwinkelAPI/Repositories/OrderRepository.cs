using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Order> GetAllOrders(int storeId)
        {
            var orderFromStores = _context.Store.Include(x => x.Orders).FirstOrDefault(x => x.Id == storeId);
            if (orderFromStores == null)
            {
                throw new NotFoundException();
            }
            return orderFromStores.Orders;
        }

        public IEnumerable<Order> GetBestSellingBouqets()
        {
            return _context.Order.ToList();
        }

        public Order GetOneOrderById(int storeId, int orderId)
        {
            CheckStoreExists(storeId);
            var order = _context.Order.FirstOrDefault(x => x.StoreId == storeId && x.Id == orderId);
            if (order == null)
            {
                throw new NotFoundException();
            }
            return order;
        }

        public void Delete(int storeId, int orderId)
        {
            var order = GetOneOrderById(storeId, orderId);
            _context.Order.Remove(order);
            _context.SaveChanges();
        }

        public Order Insert(int storeId, int bouqetId, int amount)
        {
            CheckStoreExists(storeId);
            var order = new Order()
            {
                StoreId = storeId,
                BouqetId = bouqetId,
                Amount = amount
            };
            _context.Order.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order Update(int storeId, int bouqetId, int orderId, int amount)
        {
            var order = GetOneOrderById(storeId, orderId);
            order.StoreId = storeId;
            order.BouqetId = bouqetId;
            order.Amount = amount;
            _context.SaveChanges();
            return order;
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
