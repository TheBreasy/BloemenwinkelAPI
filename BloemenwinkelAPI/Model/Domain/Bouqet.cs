using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Domain
{
    public class Bouqet : BaseDatabaseClass
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}