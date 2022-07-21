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
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository<Teachers> _teacherRepository;

        public TeachersController(ITeacherRepository<Teachers> teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpPost]
        [Route("PostGetTeacher")]
        public async Task<IActionResult> PostGetTeacher([FromForm] string email, [FromForm] string password)
        {
            Teachers resp = await _teacherRepository.GetTeacherAsync(email, password);
            
            if(resp == null)
            {
                return NotFound(resp);
            }
            return Ok(resp);
        }

        [HttpPost]
        [Route("GetTeacherNameByUuid")]
        public async Task<IActionResult> GetTeacherNameByUuid([FromForm] Guid uuid)
        {
            Teachers resp = await _teacherRepository.GetTeacherNameByUuidAsync(uuid);
            String name;
            if(resp == null)
            {
                return NotFound(resp);
            }

            name = resp.ToString();

            return Ok(name);
        }


    }
}
