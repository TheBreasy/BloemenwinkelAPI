using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Model.Domain
{
    public class BaseDatabaseClass
    {
        [Key]
        public int Id { get; set; }
    }
}