using InsurancePolicies.Domain.Entities.Document;

namespace src.InsurancePolicies.Infrastructure.Repositories.Mongo
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll();

        Task<TDocument> GetById(string id);

        Task<TDocument> InsertDocument(TDocument document);

        Task<TDocument> UpdateDocument(TDocument document);

        Task<TDocument> DeleteById(string id);
    }
}