using Microsoft.AspNetCore.Mvc;
using TodoList.Presentation;
using TodoList.Services;

namespace TodoList.Controllers;

    [Route("/api/[controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private readonly IViaCepIntergration _viaCepIntegration;
        
        public CepController(IViaCepIntergration viaCepIntegration)
        {
            _viaCepIntegration = viaCepIntegration;
        }
        
        [HttpGet("{cep}")]
        public async Task<ActionResult<RefitResponse>> GetAddress(string cep)
        {
            var responseData = await _viaCepIntegration.GetDataCep(cep);
            if (responseData == null)
            {
                return BadRequest("Cep não encontrado");
            }

            return Ok(responseData);
        }
        
    }      