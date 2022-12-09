using Microsoft.AspNetCore.Mvc;
using Questao5DbFirst.Enums;
using Questao5DbFirst.Models;
using System.Net;
using System.Web.Http;
using System.Linq;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Questao5DbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ContaCorrenteController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("listarContas")]
        public async Task<List<Contacorrente>> ObterContas()
        {
            return await _context.Contacorrentes.ToListAsync();
        }

        [HttpGet("listarContaPorId")]
        public async Task<Contacorrente> ObterContaPorId(string idContaCorrente)
        {
            return await _context.Contacorrentes.FirstOrDefaultAsync(x => x.Idcontacorrente == idContaCorrente);
        }

        [HttpGet("obterSaldo")]
        public async Task<object> ObterSaldo(string idContaCorrente)
        {
            double credito = 0.0, debito = 0.0;
            var conta = await ObterContaPorId(idContaCorrente);

            var movimentoController = new MovimentoController(_context);
            var movimentosConta = movimentoController.ListarMovimentosPorId(conta.Idcontacorrente);

            foreach (var mov in movimentosConta.Result)
            {
                if (mov.Tipomovimento.ToUpper().Equals("C"))
                    credito += mov.Valor;
                else
                    debito += mov.Valor;
            }

            double saldo = credito - debito;

            var result = from con in _context.Contacorrentes
                          .Where(x => x.Idcontacorrente == idContaCorrente)
                          select new { con.Numero, con.Nome, DateTime.Now, saldo };

            return result;            
        }

    }
}
