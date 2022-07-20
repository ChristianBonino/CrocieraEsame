using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Crociera.DB.Entities
{
    public class Prenotazioni
    {
        [Key]
        public Guid? CodPrenotazione { get; set; }
        public string CodUtente { get; set; }
        public string CodReplica { get; set; }
        public int Quantita { get; set; }
    }
}
