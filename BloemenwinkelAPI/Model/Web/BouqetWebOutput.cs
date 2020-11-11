namespace BloemenwinkelAPI.Model.Web
{
    public class BouqetWebOutput
    {
        public BouqetWebOutput(int id, int storeId, string name, string description, double price)
        {
            Id = id;
            StoreId = storeId;
            Name = name;
            Description = description;
            Price = price;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StoreId { get; set; }
    }
    
}
