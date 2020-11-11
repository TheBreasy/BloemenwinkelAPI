using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Domain
{
    public class Order : BaseDatabaseClass
    {
        [Required]
        public int StoreId { get; set; }
        public int BouqetId { get; set; }
        public int Amount { get; set; }
    }
}