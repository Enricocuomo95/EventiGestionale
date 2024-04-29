using gestioneEventi.Models;
using System.Globalization;
using System.IO;

namespace gestioneEventi
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region INSERT
            using (var ctx = new GestioneEventiContext())
            {
                try
                {
                    Evento evento1 = new Evento() { Nome = "cheBello1", Desrizione = "questosiche è un evento", DataEvento = DateOnly.FromDateTime(DateTime.Now), Luogo = "Roma via sta ceppa", CapMax = 500 };
                    ctx.Eventos.Add(evento1);
                    Evento evento2 = new Evento() { Nome = "cheBello2", Desrizione = "questosiche è un evento", DataEvento = DateOnly.FromDateTime(DateTime.Now), Luogo = "Roma via sta ceppa", CapMax = 500 };
                    ctx.Add(evento2);
                    Evento evento3 = new Evento() { Nome = "cheBello3", Desrizione = "questosiche è un evento", DataEvento = DateOnly.FromDateTime(DateTime.Now), Luogo = "Roma via sta ceppa", CapMax = 500 };
                    ctx.Add(evento3);
                    Evento evento4 = new Evento() { Nome = "cheBello4", Desrizione = "questosiche è un evento", DataEvento = DateOnly.FromDateTime(DateTime.Now), Luogo = "Roma via sta ceppa", CapMax = 500 };

                    ctx.Partecipantes.Add(new Partecipante() { Nome = "pippo", Telefono = "111111111", EventoRifs = { evento1, evento2 } });
                    ctx.Add(new Partecipante() { Nome = "pluto", Telefono = "111111112", EventoRifs = { evento3, evento4 } });
                    ctx.Add(new Partecipante() { Nome = "topolino", Telefono = "11111113", EventoRifs = { evento3, evento4 } });

                    ctx.Risorsas.Add(new Risorsa() { Nome = "torta della nonna", Tipo = "alimento", Numero = 10, Costo = 30, EventoRifNavigation = evento1 });
                    ctx.Add(new Risorsa() { Nome = "patatine", Tipo = "alimento", Numero = 10, Costo = 30, EventoRifNavigation = evento2 });
                    ctx.Add(new Risorsa() { Nome = "luce", Tipo = "strumento", Numero = 10, Costo = 30, EventoRifNavigation = evento3 });
                    ctx.Add(new Risorsa() { Nome = "palco", Tipo = "strumento", Numero = 10, Costo = 30, EventoRifNavigation = evento4 });

                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion

            #region READ
            using (var ctx = new GestioneEventiContext())
            {
                try
                {
                    Console.WriteLine("ora ti stampo tutti gli eventi te te...");
                    ICollection<Evento> listaEventi = ctx.Eventos.ToList();
                    foreach (Evento e in listaEventi)
                    {
                        Console.WriteLine($"ah bello questo è l'evento: {e.Nome}, {e.DataEvento}, {e.Desrizione}");
                        Console.WriteLine("VATTE A FA STO EVENTO");
                    }

                    Console.WriteLine("ora ti stampo tutti i partecipanti te te...");
                    ICollection<Partecipante> listaPartecipanti = ctx.Partecipantes.ToList();
                    foreach (Partecipante p in listaPartecipanti)
                    {
                        Console.WriteLine($"ah bello questo è un partecipante: {p.Nome}, {p.Telefono}");
                        Console.WriteLine("BELLO ZIOOO");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion

            #region Update
            using (var ctx = new GestioneEventiContext())
            {
                try
                {
                    Console.WriteLine("ora ti faccio un update tete...");
                    Console.WriteLine("ti cerco levento evento1 e gli cambio il nome");
                    Evento x = ctx.Eventos.Single(e => e.IdEvento == 1);
                    x.Nome = "partyFigo";
                    ctx.SaveChanges();

                    Console.WriteLine($"tutti i partecipanti a questo evento sono: ");
                    foreach (Partecipante p in x.PartecipanteRifs)
                        Console.WriteLine(p.Nome);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion


            #region Delete
            using (var ctx = new GestioneEventiContext())
            {
                try
                {
                    Console.WriteLine("a bello sto evento è terminato mo to cancello e ba...");
                    var evento = ctx.Eventos.Single(e => e.IdEvento == 4);
                    ctx.Eventos.Remove(evento);
                    ctx.SaveChanges();
                    Console.WriteLine("lho cancellato");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            #endregion
            #region SCRIVI FILE
            using (var ctx = new GestioneEventiContext())
            {
                string path = "C:\\Users\\Utente\\Desktop\\Eventi.txt";
                try
                {


                    if (!File.Exists(path))
                    {
                        StreamWriter sw = File.CreateText(path);
                        foreach (Evento e in ctx.Eventos.ToList())
                            sw.WriteLine(e.CSVString());
                    }

                    else
                    {
                        StreamWriter sw = File.AppendText(path);
                        foreach (Evento e in ctx.Eventos.ToList())
                            sw.WriteLine(e.CSVString());
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            #endregion

            #region LEGGIFILE

            using (var ctx = new GestioneEventiContext())
            {
                string path = "C:\\Users\\Utente\\Desktop\\Eventi.txt";
                try
                {
                    StreamReader sr = new StreamReader(path);
                    string? line;
                    String[] array = new String[6];
                    Evento evento = null;

                    while ((line = sr.ReadLine()) != null)
                    {
                        array = line.Split(",");
                        Console.WriteLine(line);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            #endregion


        }

    }
}
