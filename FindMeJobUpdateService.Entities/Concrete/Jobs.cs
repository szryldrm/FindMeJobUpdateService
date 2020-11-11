using FindMeJobUpdateService.Core.Entities;
using FindMeJobUpdateService.Core.Settings.MongoDbSettings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FindMeJobUpdateService.Entities.Concrete
{
    [System.Serializable]
    [BsonCollection("Jobs")]
    public class Jobs : IDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string githubId { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public DateTime created_at { get; set; }
        public string company { get; set; }
        public string company_url { get; set; }
        public string location { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string how_to_apply { get; set; }
        public string company_logo { get; set; }
        public string JobsPage { get; set; }
    }
}
