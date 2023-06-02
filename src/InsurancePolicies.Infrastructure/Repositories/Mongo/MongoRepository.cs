using InsurancePolicies.Domain.Entities.Document;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using src.InsurancePolicies.Infrastructure.Data;

namespace src.InsurancePolicies.Infrastructure.Repositories.Mongo
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var db = client.GetDatabase(options.Value.DatabaseName);

            _collection = db.GetCollection<TDocument>(typeof(TDocument).Name);
        }
        
        public async Task<IEnumerable<TDocument>> GetAll()
        {
            return await _collection.Find(p => true).ToListAsync();
        }

        public async Task<TDocument> GetById(string Id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<TDocument> InsertDocument(TDocument document)
        {
            await _collection.InsertOneAsync(document);
            return document; 
        }

        public async Task<TDocument> UpdateDocument(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            
            var updated = await _collection.FindOneAndReplaceAsync(filter, document);
            return updated;
        }

        public async Task<TDocument> DeleteById(string Id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);
            var deleted = await _collection.FindOneAndDeleteAsync(filter);
            return deleted;
        } 
    }
}