using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.API.Models.Domain
{
    public class BaseDatabaseClass
    {
        [Key]
        public int Id { get; set; }
    }
}