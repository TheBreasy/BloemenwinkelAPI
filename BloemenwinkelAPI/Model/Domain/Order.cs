using System.Collections.Generic;

namespace BloemenwinkelAPI.API.Models.Domain
{
    public class Order : BaseDatabaseClass
    {
        [Required]
        public int Id { get; set; }
    }
}