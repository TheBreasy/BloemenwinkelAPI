using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BloemenwinkelAPI.Model.Domain
{
    public class Order : BaseDatabaseClass
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int StoreId { get; set; }
        public int BouqetId { get; set; }
        public int Amount { get; set; }
    }
}