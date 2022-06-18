using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly IParentsRepository<Parents> _parentsRepository;

        public ParentsController(IParentsRepository<Parents> parentsRepository)
        {
            _parentsRepository = parentsRepository;
        }

        [HttpPost]
        [Route("PostGetParent")]
        public async Task<IActionResult> GetParent([FromForm] string email, [FromForm] string password)
        {
            Parents parent = await _parentsRepository.GetParentAsync(email, password);
            return parent != null ? Ok(parent) : NotFound(parent);
        }
    }
}
