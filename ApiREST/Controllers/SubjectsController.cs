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
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsRepository<Subjects> _subjectsRepository;

        public SubjectsController(ISubjectsRepository<Subjects> subjectsRepository)
        {
            _subjectsRepository = subjectsRepository;
        }

        [HttpGet]
        [Route("GetSubject")]
        public async Task<IActionResult> GetSubject(Guid uuid)
        {
            Subjects resp = await _subjectsRepository.GetSubjectAsync(uuid);

            if (resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }
    }
}
