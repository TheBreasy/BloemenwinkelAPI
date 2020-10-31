using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Domain
{
    public class Store : BaseDatabaseClass
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Region { get; set; }
    }
}