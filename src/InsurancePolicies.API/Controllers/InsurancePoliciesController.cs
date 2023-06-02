using System.Net;
using Microsoft.AspNetCore.Mvc;
using src.InsurancePolicies.Domain.Entities;
using src.InsurancePolicies.Infrastructure.Repositories.Mongo;

namespace src.InsurancePolicies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsurancePoliciesController : ControllerBase
    {
        private readonly IMongoRepository<Policies> _mongoRepository;

        public InsurancePoliciesController(IMongoRepository<Policies> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }


        /// <summary>
        ///  Get all Users
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Policies>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Policies>>> GetPolices()
        {
            var polices = await _mongoRepository.GetAll();
            return Ok(polices);
        }

        /// <summary>
        ///  Get all Users
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Policies>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Policies>> PostPolices([FromBody] Policies policies)
        {
            var resul = await _mongoRepository.InsertDocument(policies);
            return Ok(resul);
        }

    }
}