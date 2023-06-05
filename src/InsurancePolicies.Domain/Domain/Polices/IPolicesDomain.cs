using src.InsurancePolicies.Domain.Entities;

namespace src.InsurancePolicies.Domain.Domain.Polices
{
    public interface IPolicesDomain
    {
        bool ValidatePolices(Policies polices);
    }
}