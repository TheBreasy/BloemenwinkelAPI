using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloemenwinkelAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BloemenwinkelAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders(int storeId);
        Task<IEnumerable<Order>> GetBestSellingBouqets();
        Task<Order> GetOneOrderById(int storeId, int bouqetId);
        Task Delete(int storeId, int orderId);
        Task<Order> Insert(int storeId, int bouqetId, int amount);
        Task<Order> Update(int storeId, int bouqetId, int orderId, int amount);

    }
}
