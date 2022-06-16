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

        [HttpGet]
        [Route("GetParentPasswd")]
        public IActionResult GetParent(string email, string password)
        {

            Parents parent = _parentsRepository.GetParent(email, password);
            return parent != null ? Ok(parent) : NotFound(parent);
        }

        [HttpGet]
        [Route("GetAllParents")]
        public IActionResult GetAllParents()
        {
            return Ok(_parentsRepository.GetAllParents());
        }
    }
}
