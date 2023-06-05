using src.InsurancePolicies.Domain.Entities;

namespace src.InsurancePolicies.Domain.Domain.Polices
{
    public class PolicesDomain : IPolicesDomain
    {
        // This method checks if a given insurance policy is valid based on its start and end dates and compares these dates with the current date.
        public bool ValidatePolices(Policies polices)
        {
            var currentDate = DateTime.UtcNow;

            return !(polices.PolicyStartDate > polices.PolicyEndDate
                     || polices.PolicyStartDate < currentDate
                     || polices.PolicyEndDate < currentDate);
        }
    }
}