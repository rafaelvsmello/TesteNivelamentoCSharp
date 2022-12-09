using Questao5DbFirst.Enums;
using System;
using System.Collections.Generic;

namespace Questao5DbFirst.Models;

public partial class Movimento
{
    public string Idmovimento { get; set; } = null!;

    public string Idcontacorrente { get; set; } = null!;

    public DateTime Datamovimento { get; set; }

    public string Tipomovimento { get; set; } = null!;

    public double Valor { get; set; }

    public virtual Contacorrente IdcontacorrenteNavigation { get; set; } = null!;
}
