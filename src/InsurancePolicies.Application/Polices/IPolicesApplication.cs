using src.InsurancePolicies.Domain.Entities;

namespace src.InsurancePolicies.Application.Polices
{
    public interface IPolicesApplication
    {
        Task<Policies> CreatePolicies(Policies polices);
        Task<Policies> FilterPolicies(string filter);
    }
}