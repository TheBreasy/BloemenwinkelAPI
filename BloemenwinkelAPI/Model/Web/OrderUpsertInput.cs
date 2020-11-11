using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Web
{
    public class OrderUpsertInput
    {
        public int Id { get; set; }

        [Required]
        public int BouqetId { get; set; }
        [Required]
        public int StoreId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
