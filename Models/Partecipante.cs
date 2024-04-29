using System;
using System.Collections.Generic;

namespace gestioneEventi.Models;

public partial class Partecipante
{
    public int IdPartecipante { get; set; }

    public string Telefono { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public virtual ICollection<Evento> EventoRifs { get; set; } = new List<Evento>();
}
