namespace BloemenwinkelAPI.Model
{
    public class BloemenwinkelDatabaseSettings : IBloemenwinkelDatabaseSettings
    {
        public string OrderCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBloemenwinkelDatabaseSettings
    {
        string OrderCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}