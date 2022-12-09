using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Controllers
{

    [ApiController]
    [Route("api/getSaldo")]
    public class MovimentoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Movimento> GetSaldo(int idContaCorrente)
        {
            return Ok();
        }
    }
}
