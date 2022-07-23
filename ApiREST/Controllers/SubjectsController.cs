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
        /// <summary>
        /// Pobierz przedmiot
        /// </summary>
        /// <param name="uuid">Uuid przedmiotu</param>
        /// <returns>Zwraca obiekt Subjects (kod 200) lub 404 przy niepoprawnym uuid</returns>
        [HttpPost]
        [Route("GetSubject")]
        public async Task<IActionResult> GetSubject([FromForm] Guid uuid)
        {
            Subjects resp = await _subjectsRepository.GetSubjectAsync(uuid);

            if (resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }
        /// <summary>
        /// Pobierz nazwe przedmiotu za pomoca uuid
        /// </summary>
        /// <param name="uuid">Uuid przedmiotu</param>
        /// <returns>Zwraca nazwe przedmiotu (kod 200) lub 404 przy niepoprawnym uuid</returns>
        [HttpPost]
        [Route("GetSubjectNameByUuid")]
        public async Task<IActionResult> GetSubjectNameByUuid([FromForm] Guid uuid)
        {
            Subjects resp = await _subjectsRepository.GetSubjectAsync(uuid);
            String name;
            if(resp == null)
            {
                return NotFound(resp);
            }

            name = resp.Subjectname;
            return Ok(name);
        }


    }
}
