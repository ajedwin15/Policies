using InsurancePolicies.Domain.Entities.Document;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace src.InsurancePolicies.Domain.Entities.Security
{
    [BsonCollection("Login"), BsonIgnoreExtraElements]
    public class User: Document
    {        
        [BsonElement("userName")]
        public string? UserName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
        
        public string? Role { get; set; }        
    }
}