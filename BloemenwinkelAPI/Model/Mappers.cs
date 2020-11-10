﻿using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Model.Web;

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
            return new BouqetWebOutput(input.Id, input.Name);
        }
    }
}
