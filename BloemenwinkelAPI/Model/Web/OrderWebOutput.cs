using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Web
{
    public class OrderWebOutput
    {
        public OrderWebOutput(int id, int storeId, int bouqetId, int amount)
        {
            Id = id;
            StoreId = storeId;
            BouqetId = bouqetId;
            Amount = amount;
        }

        public int Id { get; set; }

        [Required]
        public int StoreId { get; set; }
        [Required]
        public int BouqetId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
