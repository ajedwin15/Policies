using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InsurancePolicies.Domain.Entities.Document
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        private DateTime? createdDate;

        [BsonElement("createdDate")]
        public DateTime? CreatedDate
        {
            get
            {
                if (createdDate == null)
                {
                    createdDate = DateTime.UtcNow;
                }
                return createdDate;
            }
            private set => createdDate = value;
        }
    }
}