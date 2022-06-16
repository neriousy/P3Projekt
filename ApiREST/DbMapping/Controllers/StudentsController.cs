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

        //[HttpPost]
        //[Route("GetStudentPasswd")]
        //public ActionResult<Students> GetStudent(string email, string password)
        //{
        //    Students student =  _studentsRepository.GetStudent(email, password);
        //    if(student == null)
        //    {
        //        return NotFound(student);
        //    }
        //    return Ok(student);
        //}
        
        [HttpPost]
        [Route("PostGetStudent")]
        public IActionResult PostGetStudent([FromForm] string email, [FromForm] string passwd)
        {
            Students resp = _studentsRepository.GetStudent(email, passwd);

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
