using System;
using System.Collections.Generic;

namespace Questao5DbFirst.Models;

public partial class Contacorrente
{
    public string Idcontacorrente { get; set; } = null!;

    public long Numero { get; set; }

    public string Nome { get; set; } = null!;

    public bool Ativo { get; set; }

    public virtual ICollection<Movimento> Movimentos { get; } = new List<Movimento>();
}
