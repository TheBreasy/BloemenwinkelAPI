using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Domain
{
    public class Order : BaseDatabaseClass
    {
        [Required]
        public int Id { get; set; }
    }
}