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
        public async Task<IActionResult> PostGetStudent([FromForm] string email, [FromForm] string passwd)
        {
            Students resp = await _studentsRepository.GetStudentAsync(email, passwd);

            return Ok(resp);
        }
        [HttpGet]
        [Route("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            ICollection<Students> students = _studentsRepository.GetAllStudents();
            return Ok(students);
        }
    }
}
