using System;
using System.Collections.Generic;

namespace gestioneEventi.Models;

public partial class Evento
{
    public int IdEvento { get; set; }

    public string Nome { get; set; } = null!;

    public string Desrizione { get; set; } = null!;

    public DateOnly DataEvento { get; set; }

    public string Luogo { get; set; } = null!;

    public int CapMax { get; set; }

    public virtual ICollection<Risorsa> Risorsas { get; set; } = new List<Risorsa>();

    public virtual ICollection<Partecipante> PartecipanteRifs { get; set; } = new List<Partecipante>();

    public string CSVString()
    {
        return ($"{IdEvento},{Nome},{Desrizione},{DataEvento},{Luogo},{CapMax}");
    }
}
