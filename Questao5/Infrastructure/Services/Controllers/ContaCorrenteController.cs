using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private static readonly string[] Nomes = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        private readonly ILogger<ContaCorrenteController> _logger;

        public ContaCorrenteController(ILogger<ContaCorrenteController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetContas")]
        public IEnumerable<ContaCorrente> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new ContaCorrente
            {
                IdContaCorrente = Random.Shared.Next(0, 100),
                Numero = Random.Shared.Next(0, 1000),
                Nome = Nomes[Random.Shared.Next(Nomes.Length)],
                Ativo = Convert.ToBoolean(Random.Shared.Next(0, 1))
            })
            .ToArray();
        }
    }
}
