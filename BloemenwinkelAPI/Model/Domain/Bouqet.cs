using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Domain
{
    public class Bouqet : BaseDatabaseClass
    {
        [Required]
        public Store Store { get; set; }
        
        public int StoreId { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}