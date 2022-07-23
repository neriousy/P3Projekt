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
        /// <summary>
        /// Zaloguj sie do studenta
        /// </summary>
        /// <param name="email">Email studenta</param>
        /// <param name="password">Haslo studenta</param>
        /// <returns>Zwraca obiekt studenta przy poprawnych danych. Zwraca kod 404 przy niepoprawnych danych</returns>
        [HttpPost]
        [Route("PostGetStudent")]
        public async Task<IActionResult> PostGetStudent([FromForm] string email, [FromForm] string password)
        {
            Students resp = await _studentsRepository.GetStudentAsync(email, password);

            if (resp == null)
            {
                return NotFound(resp);
            }

            resp.Email = null; ;
            resp.Passwd = null;

            return Ok(resp);
        }
        /// <summary>
        /// Pobierz liste studentow danej klasy
        /// </summary>
        /// <param name="uuid">Uuid klaasy</param>
        /// <returns>Zwraca liste uczniow danej klasy(kod 200) lub 404 przy niepoprawnym uuid</returns>
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
        /// <summary>
        /// Zwraca studenta za pomoca uuid
        /// </summary>
        /// <param name="uuid">uuid studenta</param>
        /// <returns>Zwraca obiekt studenta(kod 200). Przy niepoprawnym uuid zwraca kod 404</returns>
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
        /// <summary>
        /// Pobierz imie i nazwisko ucznia
        /// </summary>
        /// <param name="uuid">Uuid ucznia</param>
        /// <returns>Zwraca imie i nazwisko ucznia(kod 200). Przy niepoprawnym uuid zwraca kod 404</returns>
        [HttpPost]
        [Route("GetStudentNameByUuid")]
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
