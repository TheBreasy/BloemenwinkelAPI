using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Web
{
    public class BouqetUpsertInput
    {
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
