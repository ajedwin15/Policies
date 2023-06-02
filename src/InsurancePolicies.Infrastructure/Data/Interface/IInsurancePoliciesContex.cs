using MongoDB.Driver;
using src.InsurancePolicies.Domain.Entities;

namespace src.InsurancePolicies.Infrastructure.Data.Interface
{
    public interface IInsurancePoliciesContex
    {
        IMongoCollection<Policies> Policies { get; }
        
    }
}