using System.Net;
using Microsoft.AspNetCore.Mvc;
using src.InsurancePolicies.Application.Auth;
using src.InsurancePolicies.Domain.Entities.Security;

namespace src.InsurancePolicies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;

        public AuthController(IAuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        /// <summary>
        ///  This service generates a token
        /// </summary>
        [HttpPost("/Token/Generate")]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> PostGenerate(CredentialEntity credentialEntity)
        {
            var result = await _authApplication.PostGenerate(credentialEntity);

            if (result == null) return BadRequest("Identity validation error");

            return result;
        }

        /// <summary>
        /// This service creates a user
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> PostAuth([FromBody] User user)
        {
            var result = await _authApplication.Create(user);

            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}