using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.InsurancePolicies.Application.Polices;
using src.InsurancePolicies.Domain.Entities;
using src.InsurancePolicies.Infrastructure.Repositories.Mongo;

namespace src.InsurancePolicies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsurancePoliciesController : ControllerBase
    {
        private readonly IMongoRepository<Policies> _mongoRepository;
        private readonly IPolicesApplication _policesApplication;

        public InsurancePoliciesController(IMongoRepository<Policies> mongoRepository, IPolicesApplication policesApplication)
        {
            _mongoRepository = mongoRepository;
            _policesApplication = policesApplication;
        }

        /// <summary>
        ///  This service brings all the policies
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(typeof(IEnumerable<Policies>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Policies>>> GetPolices()
        {
            var polices = await _mongoRepository.GetAll();
            return Ok(polices);
        }

        /// <summary>
        ///  This service filters by policy number or license plate number
        /// </summary>
        [HttpGet("filter")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(typeof(IEnumerable<Policies>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Policies>>> GetPolices(string filter)
        {
            var polices = await _policesApplication.FilterPolicies(filter);
            if(polices == null) return NoContent(); 
            return Ok(polices);
        }

        /// <summary>
        /// This service creates a policy if it is valid
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(typeof(IEnumerable<Policies>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Policies>> PostPolices([FromBody] Policies policies)
        {
            var result = await _policesApplication.CreatePolicies(policies);
            if(result == null) return BadRequest("Error: The policy start date must be earlier than the end date.");
            return Ok(result);
        }

        /// <summary>
        /// This service updates my policy data
        /// </summary>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(typeof(Policies), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePolicies([FromBody] Policies policies)
        {            
            return Ok(await _mongoRepository.UpdateDocument(policies));
        }

        /// <summary>
        /// This service deletes a policy with the id
        /// </summary>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(typeof(Policies), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePoliciesById(string Id)
        {            
            return Ok(await _mongoRepository.DeleteById(Id));
        }
    }
}