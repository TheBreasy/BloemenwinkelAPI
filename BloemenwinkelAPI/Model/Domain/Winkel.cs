using System.Collections.Generic;

namespace BloemenwinkelAPI.API.Models.Domain
{
    public class Winkel : BaseDatabaseClass
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Regio { get; set; }
    }
}