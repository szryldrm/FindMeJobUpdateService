using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using FindMeJobUpdateService.Core.Entities;
using FindMeJobUpdateService.Core.Settings.MongoDbSettings;

namespace FindMeJobUpdateService.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBaseMongo<TEntity> : IEntityRepositoryMongo<TEntity>
        where TEntity : class, IDocument, new()
    {

        private readonly IMongoCollection<TEntity> _collection;

        public EfEntityRepositoryBaseMongo(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public bool Add(TEntity entity)
        {
            try
            {
                _collection.InsertOne(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            return _collection.ReplaceOne(filter: g => g._id == entity._id, replacement: entity).ModifiedCount > 0;
        }

        public bool Delete(string id)
        {
            FilterDefinition<TEntity> data = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
            return _collection.DeleteOne(data).DeletedCount > 0;
        }

        public TEntity Get(FilterDefinition<TEntity> filter)
        {
            return _collection.Find(filter).FirstOrDefault();
        }

        public List<TEntity> GetList(FilterDefinition<TEntity> filter = null)
        {

            if (filter == null)
            {
                return _collection.Find<TEntity>(_ => true).ToList<TEntity>();
            }

            return _collection.Find<TEntity>(filter).ToList<TEntity>();
        }

        public List<TEntity> GetProjectionList(ProjectionDefinition<TEntity> projection, FilterDefinition<TEntity> filter = null)
        {
            if (filter == null)
            {
                return _collection.Find<TEntity>(_ => true).Project<TEntity>(projection).ToList();
            }
            return _collection.Find<TEntity>(filter).Project<TEntity>(projection).ToList();

        }

        public bool UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null)
        {
            return _collection.UpdateOne(filter, update, options).ModifiedCount > 0;
        }
    }
}
