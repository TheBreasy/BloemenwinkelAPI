namespace BloemenwinkelAPI.Model.Web
{
    public class BouqetWebOutput
    {
        public BouqetWebOutput(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
}
