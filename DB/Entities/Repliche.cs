using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Crociera.DB.Entities
{
    public class Repliche
    {
        [Key]
        public string CodReplica { get; set; }
        public string CodEvento { get; set; }
        public DateTime DataEOra { get; set; }
        public bool Annullato { get; set; }
    }
}
