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
        /// <summary>
        /// Zaloguj sie do nauczyciela
        /// </summary>
        /// <param name="email">Email nauczyciela</param>
        /// <param name="password">Haslo nauczyciela</param>
        /// <returns>Zwraca obiekt nauczyciela (kod 200) lub 404 przy niepoprawnych danych</returns>
        [HttpPost]
        [Route("PostGetTeacher")]
        public async Task<IActionResult> PostGetTeacher([FromForm] string email, [FromForm] string password)
        {
            Teachers resp = await _teacherRepository.GetTeacherAsync(email, password);
            
            if(resp == null)
            {
                return NotFound(resp);
            }

            resp.Passwd = null;
            resp.Email = null;
            return Ok(resp);
        }
        /// <summary>
        /// Pobierz imie naucyzciela
        /// </summary>
        /// <param name="uuid">Uuid nauczyciela</param>
        /// <returns>Zwraca imie nauczyciela (kod 200) lub kod 404 przy niepoprawnym uuid</returns>
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
