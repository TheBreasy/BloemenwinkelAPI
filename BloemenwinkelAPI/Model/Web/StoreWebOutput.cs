namespace BloemenwinkelAPI.Model.Web
{
    public class StoreWebOutput
    {
        public StoreWebOutput(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
