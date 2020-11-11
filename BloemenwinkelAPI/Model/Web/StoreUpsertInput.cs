using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Web
{
    public class StoreUpsertInput
    {
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Region { get; set; }
    }
}

