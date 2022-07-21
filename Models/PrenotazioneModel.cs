using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Crociera.Models
{
    public class PrenotazioneModel
    {
        public string CodPrenotazione { get; set; }
        public string CodUtente { get; set; }
        public string CodReplica { get; set; }
        public int Quantita { get; set; }
        public string NomeEvento { get; set; }
        public string Nome { get; set; }
        public string Luogo { get; set; }
        public int Posti { get; set; }
        public DateTime DataEOra { get; set; }

    }
}
