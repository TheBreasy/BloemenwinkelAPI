using BloemenwinkelAPI.Model;
using BloemenwinkelAPI.Model.Domain;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BloemenwinkelAPI.Services
{
    public class Orderservice
    {
        private readonly IMongoCollection<Order> _order;

        public Orderservice(IBloemenwinkelDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _order = database.GetCollection<Order>(settings.OrderCollectionName);
        }

        public List<Order> Get() =>
            _order.Find(order => true).ToList();

        public Order Get(int id) =>
            _order.Find<Order>(order => order.Id == id).FirstOrDefault();

        public Order Create(Order order)
        {
            _order.InsertOne(order);
            return order;
        }

        public void Update(int id, Order orderIn) =>
            _order.ReplaceOne(order => order.Id == id, orderIn);

        public void Remove(Order orderIn) =>
            _order.DeleteOne(order => order.Id == orderIn.Id);

        public void Remove(int id) =>
            _order.DeleteOne(order => order.Id == id);
    }
}