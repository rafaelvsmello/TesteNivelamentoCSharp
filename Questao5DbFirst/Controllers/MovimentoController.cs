using Microsoft.AspNetCore.Mvc;
using Questao5DbFirst.Enums;
using System.Net;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Questao5DbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentoController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ContaCorrenteController _contaCorrenteController;

        public MovimentoController(DatabaseContext context)
        {
            _context = context;
            _contaCorrenteController = new ContaCorrenteController(_context);
        }        

        [HttpGet("listarMovimentosPorId")]
        public async Task<List<Movimento>> ListarMovimentosPorId(string idContaCorrente)
        {
            return await _context.Movimentos.Where(x => x.Idcontacorrente == idContaCorrente).ToListAsync();
        }

        [HttpPost("registrarMovimento")]
        public async Task<ActionResult<Movimento>> RegistrarMovimento(string idMovimento, string idContaCorrente, string tipoMovimento, double valor)
        {
            var conta = await _contaCorrenteController.ObterContaPorId(idContaCorrente);

            if (conta is null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Conta {idContaCorrente} não cadastrada"),
                    ReasonPhrase = EStatusErro.INVALID_ACCOUNT.ToString()
                };

                return BadRequest(resp);
            }           

            if (!conta.Ativo)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Conta {idContaCorrente} inativa"),
                    ReasonPhrase = EStatusErro.INACTIVE_ACCOUNT.ToString()
                };

                return BadRequest(resp);
            }

            if (valor <= 0)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Apenas valores positivos são permitidos"),
                    ReasonPhrase = EStatusErro.INVALID_VALUE.ToString()
                };

                return BadRequest(resp);
            }

            if (tipoMovimento.ToUpper() != "C" && tipoMovimento.ToUpper() != "D")
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Apenas valores positivos são permitidos"),
                    ReasonPhrase = EStatusErro.INVALID_TYPE.ToString()
                };

                return BadRequest(resp);
            }

            var movimento = new Movimento
            {
                Idmovimento = idMovimento,
                Idcontacorrente = idContaCorrente,
                Datamovimento = DateTime.Now,
                Tipomovimento = tipoMovimento.ToUpper(),
                Valor = valor
            };

            _context.Movimentos.Add(movimento);
            _context.SaveChanges();

            return Ok();
        }

    }
}
