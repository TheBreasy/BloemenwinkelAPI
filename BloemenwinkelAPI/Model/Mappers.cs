using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Model.Web;
using Microsoft.AspNetCore.Components;

namespace BloemenwinkelAPI.Model
{
    public static class Mappers
    {
        public static StoreWebOutput Convert(this Store input)
        {
            return new StoreWebOutput(input.Id, input.Name);
        }

        public static BouqetWebOutput Convert(this Bouqet input)
        {
            return new BouqetWebOutput(input.Id, input.StoreId, input.Name, input.Description, input.Price);
        }

        public static OrderWebOutput Convert(this Order input)
        {
            return new OrderWebOutput(input.Id, input.StoreId, input.BouqetId, input.Amount);
        }
    }
}
