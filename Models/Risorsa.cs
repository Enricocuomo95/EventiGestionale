using System;
using System.Collections.Generic;

namespace gestioneEventi.Models;

public partial class Risorsa
{
    public int IdRisorsa { get; set; }

    public int EventoRif { get; set; }

    public string Nome { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int Numero { get; set; }

    public int Costo { get; set; }

    public virtual Evento EventoRifNavigation { get; set; } = null!;
}
