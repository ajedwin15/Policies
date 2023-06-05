using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using src.InsurancePolicies.Domain.Domain.Polices;
using src.InsurancePolicies.Domain.Entities;
using src.InsurancePolicies.Infrastructure.Data.Interface;
using src.InsurancePolicies.Infrastructure.Repositories.Mongo;

namespace src.InsurancePolicies.Application.Polices
{
    public class PolicesApplication : IPolicesApplication
    {        
        private readonly IInsurancePoliciesContex _insurancePoliciesContex;
        private readonly IMongoRepository<Policies> _mongoRepository;
        private readonly IPolicesDomain _policesDomain;

        public PolicesApplication(IMongoRepository<Policies> mongoRepository, IPolicesDomain policesDomain, IInsurancePoliciesContex insurancePoliciesContex)
        {
            _insurancePoliciesContex = insurancePoliciesContex;
            _mongoRepository = mongoRepository;
            _policesDomain = policesDomain;
        }

        public async Task<Policies> CreatePolicies(Policies polices)
        {
            var valid = _policesDomain.ValidatePolices(polices);

            if(!valid) return null;

            return await _mongoRepository.InsertDocument(polices);
        }
        
        public async Task<Policies> FilterPolicies(string filter)
        {
            BsonRegularExpression expReg = new BsonRegularExpression(Regex.Escape(filter), "i");
            var filterUser = Builders<Policies>.Filter.Regex(x => x.VehiclePlateNumber, expReg) | Builders<Policies>.Filter.Regex(x => x.PolicyNumber, expReg);
            var police = await _insurancePoliciesContex.Policies.Find(filterUser).SingleOrDefaultAsync();

            return police;            
        }
    }
}