using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Domain
{
    public class Store : BaseDatabaseClass
    {
        [Required]
        [MaxLength(1024)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Region { get; set; }

        public IEnumerable<Bouqet> Bouqets { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}