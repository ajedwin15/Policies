using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InsurancePolicies.Domain.Entities.Document
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }

        [BsonElement("createdDate")]
        DateTime? CreatedDate { get; }

    }
}