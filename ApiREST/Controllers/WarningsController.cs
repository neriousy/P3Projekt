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
    public class WarningsController : ControllerBase
    {
        private readonly IWarningsRepository<Warnings> _warningsRepository;

        public WarningsController(IWarningsRepository<Warnings> warningsRepository)
        {
            _warningsRepository = warningsRepository;
        }

        /// <summary>
        /// Pobierz uwagi ucznia
        /// </summary>
        /// <param name="uuid">Uuid ucznia</param>
        /// <returns>Zwraca liste uwag ucznia (kod 200) lub kod 404 przy niepoprawnym uuid</returns>
        [HttpPost]
        [Route("GetStudentWarnings")]
        public async Task<IActionResult> GetStudentWarnings([FromForm] Guid uuid)
        {
            ICollection<Warnings> resp = await _warningsRepository.GetStudentWarningsAsync(uuid);

            if(resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }
    }
}
