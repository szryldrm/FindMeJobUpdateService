using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FindMeJobUpdateService.Core.Entities
{
    public interface IDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }
}