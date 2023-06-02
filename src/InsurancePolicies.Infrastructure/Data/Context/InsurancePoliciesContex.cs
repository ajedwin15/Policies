using Microsoft.Extensions.Options;
using MongoDB.Driver;
using src.InsurancePolicies.Domain.Entities;
using src.InsurancePolicies.Infrastructure.Data.Interface;

namespace src.InsurancePolicies.Infrastructure.Data.Context
{
    public class InsurancePoliciesContex : IInsurancePoliciesContex
    {
        private readonly IMongoDatabase _collection;
        
        public InsurancePoliciesContex (IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _collection = client.GetDatabase(options.Value.DatabaseName);  
        }

        public IMongoCollection<Policies> Policies => _collection.GetCollection<Policies>("Policies");
    }
}