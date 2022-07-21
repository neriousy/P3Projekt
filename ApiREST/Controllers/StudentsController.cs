using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository<Students> _studentsRepository;

        public StudentsController(IStudentsRepository<Students> studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpPost]
        [Route("PostGetStudent")]
        public async Task<IActionResult> PostGetStudent([FromForm] string email, [FromForm] string password)
        {
            Students resp = await _studentsRepository.GetStudentAsync(email, password);

            if (resp == null)
            {
                return NotFound(resp);
            }
            return Ok(resp);
        }

        [HttpPost]
        [Route("GetStudentsByClass")]
        public async Task<IActionResult> GetStudentsByclass([FromForm] Guid uuid)
        {
            ICollection<Students> resp = await _studentsRepository.GetStudentsByClassAsync(uuid);
            foreach (var Stud in resp)
            {
                Stud.Email = null;
                Stud.Passwd = null;
            }
            if (resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }

        [HttpPost]
        [Route("GetStudentByUuid")]
        public async Task<IActionResult> GetStudentByUuid([FromForm] Guid uuid)
        {
            Students resp = await _studentsRepository.GetStudentByUuidAsync(uuid);

            resp.Email = null;
            resp.Passwd = null;

            if (resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }

        [HttpPost]
        [Route("GetStudenNameByUuid")]
        public async Task<IActionResult> GetStudentNameByUuid([FromForm] Guid uuid)
        {
            Students resp = await _studentsRepository.GetStudentByUuidAsync(uuid);
            String name;

            if (resp == null)
            {
                return NotFound(resp);
            }

            name = resp.ToString();
            return Ok(name);
        }

    }
}
