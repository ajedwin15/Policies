using MongoDB.Bson.Serialization.Attributes;

namespace src.InsurancePolicies.Domain.Entities.Security
{
    [BsonCollection("Login")]
    public class CredentialEntity
    {
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }        
    }
}