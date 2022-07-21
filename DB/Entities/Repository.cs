using Crociera.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crociera.DB
{
    public class Repository
    {
        private CrocieraDBContext CrocieraDBContext;

        public Repository(CrocieraDBContext CrocieraDBContext)
        {
            this.CrocieraDBContext = CrocieraDBContext;
        }
        public List<Locali> GetLocaliByID(string CodLocale)
        {
            //select * from Locali
            List<Locali> result = this.CrocieraDBContext.Locali.Where(l => l.CodLocale == CodLocale).ToList();
            return result;
        }
        public List<Eventi> GetEventiByID(string CodEvento)
        {
            //select * from Eventi
            List<Eventi> result = this.CrocieraDBContext.Eventi.Where(e => e.CodEvento == CodEvento).ToList();
            return result;
        }
        public List<Repliche> GetRepliche()
        {
            //select * from Repliche
            List<Repliche> result = this.CrocieraDBContext.Repliche.ToList();
            return result;
        }
        public List<Prenotazioni> GetPrenotazioni(string CodReplica)
        {
            //select * from Prenotazioni
            List<Prenotazioni> result = this.CrocieraDBContext.Prenotazioni.Where(p => p.CodReplica == CodReplica).ToList();
            return result;
        }
    }

}