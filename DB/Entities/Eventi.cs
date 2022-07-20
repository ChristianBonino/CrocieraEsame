using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Crociera.DB.Entities
{
    public class Eventi
    {
        [Key]
        public string CodEvento { get; set; }
        public string CodLocale { get; set; }
        public string NomeEvento { get; set; }
    }
}
