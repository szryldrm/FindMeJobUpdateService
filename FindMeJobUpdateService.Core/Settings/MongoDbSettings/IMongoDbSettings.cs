namespace FindMeJobUpdateService.Core.Settings.MongoDbSettings
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}