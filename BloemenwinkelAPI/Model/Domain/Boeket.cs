using System.Collections.Generic;

namespace BloemenwinkelAPI.API.Models.Domain
{
    public class Boeket : BaseDatabaseClass
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Prijs { get; set; }
        [Required]
        public string Beschrijving { get; set; }
    }
}