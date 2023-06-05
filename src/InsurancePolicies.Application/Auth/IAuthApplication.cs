using src.InsurancePolicies.Domain.Entities.Security;

namespace src.InsurancePolicies.Application.Auth
{
    public interface IAuthApplication
    {
        Task<string> PostGenerate(CredentialEntity credentialEntity);
        Task<User> PostLogin(CredentialEntity credentialEntity);
        Task<User> Create(User newUser);        
    }
}