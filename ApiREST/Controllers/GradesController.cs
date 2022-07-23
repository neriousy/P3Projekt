using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiREST;

namespace ApiREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradesRepository<Grades> _gradesRepository;

        public GradesController(IGradesRepository<Grades> gradesRepository)
        {
            _gradesRepository = gradesRepository;
        }
        /// <summary>
        /// Pobierz oceny studenta
        /// </summary>
        /// <param name="uuid">Uuid studenta ktorego chcemy </param>
        /// <returns>Zwraca 200 oraz liste ocen przy poprawnym uuid lub 404 przy niepoprawnym uuid</returns>
        [HttpPost]
        [Route("GetGradesStudent")]
        public async Task<IActionResult> GetGradesStudent([FromForm] Guid uuid)
        {
            ICollection<Grades> oceny = await _gradesRepository.GetGradesStudent(uuid);
            //IEnumerable<Grades> selection = oceny.Select(g => new Grades {Grade = g.Grade, Weight = g.Weight, Grade_id = g.Grade_id, Date = g.Date, Desc = g.Desc, Subjects = g.Subjects, Teachers = g.Teachers});
            
            return oceny != null ? Ok(oceny) : NotFound(oceny);
        }

        /// <summary>
        /// Pobierz oceny z danego przedmiotu
        /// </summary>
        /// <param name="uuid">Uuid przedmiotu</param>
        /// <returns>Zwraca 200 oraz liste ocen przy poprawnym uuid lub 404 przy niepoprawnym uuid</returns>
        [HttpGet]
        [Route("GetGradesSubject")]
        public async Task<IActionResult> GetGradesSubject([FromForm] Guid uuid)
        {
            ICollection<Grades> oceny = await _gradesRepository.GetGradesSubject(uuid);

            return oceny != null ? Ok(oceny) : NotFound(oceny);
        }
        /// <summary>
        /// Wstaw ocene
        /// </summary>
        /// <param name="Grade">Ocena</param>
        /// <param name="Weight">Waga oceny</param>
        /// <param name="Desc">Opis oceny</param>
        /// <param name="Student_id">Uuid studenta ktoremu ocene wystawiamy</param>
        /// <param name="Subject_id">Uuid przedmiotu z ktorego wystawiamy ucene</param>
        /// <param name="Teacher_id">Uuid nauczyciela ktory wystawia ocene</param>
        /// <returns>Zwraca uuid oceny przy poprawnym wstawieniu lub kod 400 przy niepoprawnym </returns>
        [HttpPost]
        [Route("InsertGrade")]
        public async Task<IActionResult> InsertGrade([FromForm]string Grade, [FromForm] int Weight, [FromForm] string Desc, [FromForm] Guid Student_id, [FromForm] Guid Subject_id, [FromForm] Guid Teacher_id)
        {
            Grades grade = new Grades(Grade, Weight, Desc, Student_id, Subject_id, Teacher_id);
            Guid uuid = await _gradesRepository.InsertGrade(grade);
            if (uuid !=  Guid.Empty)
            {
                return CreatedAtAction(nameof(grade), new { id = grade.Grade_id });
            }
            return BadRequest();

        }
        /// <summary>
        /// Edytuj ocene
        /// </summary>
        /// <param name="uuid">Uuid oceny ktora chcemy zmienic</param>
        /// <param name="Grade">Ocena</param>
        /// <param name="Weight">Waga oceny</param>
        /// <param name="Desc">Opis oceny</param>
        /// <param name="Teacher_id">Uuid nauczyciela ktory wystawil ocene</param>
        /// <returns></returns>
        [HttpPut]
        [Route("EditGrade")]
        public async Task<IActionResult> EditGrade([FromForm] Guid uuid,[FromForm] string Grade, [FromForm] int Weight, [FromForm] string Desc, [FromForm] Guid Teacher_id)
        {
            int result = await _gradesRepository.EditGrade(uuid, Grade, Weight, Desc, Teacher_id);
            if(result == 0)
            {
                return NotFound();
            }else if(result == -1){
                return Unauthorized();
            }

            return NoContent();
        }
        /// <summary>
        /// Usun ocene
        /// </summary>
        /// <param name="uuid">Uuid oceny ktora chcemy usunac</param>
        /// <returns>Zwraca 200 przy poprawnym usunieciu lub 404 gdy uuid jest niepoprawne</returns>
        [HttpDelete]
        [Route("DeleteGrade")]
        public async Task<IActionResult> DeleteGrade([FromForm] Guid uuid)
        {
            int result = await _gradesRepository.DeleteGrade(uuid);

            if(result == 0)
            {
                return NotFound();
            }

            return Ok(uuid);
        }
    }
}
