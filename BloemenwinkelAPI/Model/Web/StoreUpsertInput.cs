using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Web
{
    public class StoreUpsertInput
    {
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }
    }
}

