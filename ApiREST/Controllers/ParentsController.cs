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
    public class ParentsController : ControllerBase
    {
        private readonly IParentsRepository<Parents> _parentsRepository;

        public ParentsController(IParentsRepository<Parents> parentsRepository)
        {
            _parentsRepository = parentsRepository;
        }
        /// <summary>
        /// Zaloguj sie do rodzica
        /// </summary>
        /// <param name="email">Email rodzica</param>
        /// <param name="password">Haslo rodzica</param>
        /// <returns>Zwraca obiekt rodzica przy poprawnych danych. Zwraca kod 404 przy niepoprawnych danych</returns>
        [HttpPost]
        [Route("PostGetParent")]
        public async Task<IActionResult> GetParent([FromForm] string email, [FromForm] string password)
        {
            Parents parent = await _parentsRepository.GetParentAsync(email, password);
            if(parent == null)
            {
                return NotFound(parent);
            }

            parent.Passwd = null;
            parent.Email = null;

            return Ok(parent);
        }
    }
}
