using Microsoft.AspNetCore.Mvc;
using Sistemas.Core.Entities.Models;
using Sistemas.Core.Services.Interfaces;
using System.Collections.Generic;

namespace Sistemas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancosController : ControllerBase
    {
        //Injectamos el objecto
        private readonly IBancoService _bancoService;
        //Constructor
        public BancosController(IBancoService bancoService)
        {
            _bancoService = bancoService;
        }

        //Get api/Bancos
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(
                _bancoService.GetAll());
        }

        //Get api/Bancos/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(
                _bancoService.Get(id)
                );
        }
        [HttpPost]
        public IActionResult Add(Banco model)
        {
            _bancoService.Create(model);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(Banco model)
        {
            _bancoService.Update(model);
            return Ok();
        }
    }
}
