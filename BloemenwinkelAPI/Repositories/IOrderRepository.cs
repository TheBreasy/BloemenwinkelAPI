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
        IEnumerable<Order> GetAllOrders(int storeId);
        IEnumerable<Order> GetBestSellingBouqets();
        Order GetOneOrderById(int storeId, int bouqetId);
        void Delete(int storeId, int orderId);
        Order Insert(int storeId, int bouqetId, int amount);
        Order Update(int storeId, int bouqetId, int orderId, int amount);

    }
}
