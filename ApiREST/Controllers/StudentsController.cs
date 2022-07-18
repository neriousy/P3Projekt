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
            
            if(resp == null)
            {
                return NotFound(resp);
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("GetStudentsByClass")]
        public async Task<IActionResult> GetStudentsByclass(Guid uuid)
        {
            ICollection<Students> resp = await _studentsRepository.GetStudentsByClassAsync(uuid);
            foreach(var Stud in resp) {
                Stud.Email = null;
                Stud.Passwd = null;
            }
            if(resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }


    }
}
