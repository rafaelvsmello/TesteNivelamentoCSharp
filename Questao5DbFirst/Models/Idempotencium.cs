using System;
using System.Collections.Generic;

namespace Questao5DbFirst.Models;

public partial class Idempotencium
{
    public string ChaveIdempotencia { get; set; } = null!;

    public string? Requisicao { get; set; }

    public string? Resultado { get; set; }
}
