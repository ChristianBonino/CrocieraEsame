using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Crociera.DB.Entities
{
    public class Locali
    {
        [Key]
        public Guid? CodLocale { get; set; }
        public string Nome { get; set; }
        public string Luogo { get; set; }
        public int Posti { get; set; }
    }
}
