﻿using INTEGRATA_TDPC13.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEGRATA_TDPC13.DB
{
    public class Repository
    {
        private DBContext DBContext;
        //public int[] PostiVillag
        //{
        //    get
        //    {
        //        return this.PostiVillag;
        //    }
        //    set
        //    {
        //        this.PostiVillag = new int[] { 100, 50, 60, 20, 30 };
        //    }
        //}

        public Repository(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        public List<Prenotazione> GetPersons()
        {
            //select * from persons
            List<Prenotazione> result = this.DBContext.Prenotazione.OrderBy(p => p.idUser).ThenBy(p => p.idVillage).ThenBy(p => p.settimana).ToList();
            return result;
        }
        public List<Prenotazione> GetPersons(string id)
        {
            //select * from persons
            List<Prenotazione> result = this.DBContext.Prenotazione.Where(p => p.idUser.Contains(id)).OrderBy(p => p.idVillage).ThenBy(p => p.settimana).ToList();
            return result;
        }

        public Prenotazione GetPersonByID(string id)
        {
            //select * from persons where id = "id"
            Prenotazione result = this.DBContext.Prenotazione.Where(p => p.ID_prenota.ToString() == id).FirstOrDefault();
            return result;
        }
        public List<Prenotazione> GetPersonWithFilter(string filter)
        {
            //select * from persons where nome like "%filter%"
            //or cognome like "%filter%"
            List<Prenotazione> result = this.DBContext.Prenotazione
                .Where(p => p.idVillage.Contains(filter)
                            || p.idUser.Contains(filter)
                            || p.settimana.Contains(filter)
                       ).ToList();
            return result;
        }
        public String InsertPerson(Prenotazione person)
        {
            try
            {
                //this.PostiVillag
                int[] PostiVillag = new int[] { 100, 50, 60, 20, 30 };
                int i = 0;
                if (person.idVillage == "villaggio1")
                {
                    i = 0;
                }
                else if (person.idVillage == "villaggio2")
                {
                    i = 1;
                }
                else if (person.idVillage == "villaggio3")
                {
                    i = 2;
                }
                else if (person.idVillage == "villaggio4")
                {
                    i = 3;
                }
                else if (person.idVillage == "villaggio4")
                {
                    i = 4;
                };
                var sumPrenotati = this.DBContext.Prenotazione.Where(p => p.idVillage == person.idVillage
                                                                            && p.settimana.Contains(person.settimana)
                                                                            ).Sum(p => p.posti);

                if ((sumPrenotati + person.posti) <= PostiVillag[i]) //(sumPrenotati + person.posti) > this.PostiVillag[i]
                {

                    this.DBContext.Prenotazione.Add(person);
                    this.DBContext.SaveChanges();
                    return "Record inserito";
                    //return "Prenotazione inserita" + (sumPrenotati + "+" + person.posti) + " <= " + PostiVillag[i];
                    //return "Prenotazione inserita";
                    //return "Record inserito";
                }
                else
                {
                    return "Posti non disponibili: (prenotati " + (sumPrenotati + " + richiesti " + person.posti) + ")  <=  totali " + PostiVillag[i];
                };
            }
            catch (Exception ex)
            {
                return ex.ToString(); // stampa messaggio errore
            }
        }
        public String DeletePrenotaByID(string id)
        //public Prenotazione DeletePrenotaByID(string id)
        {
            Prenotazione item = this.DBContext.Prenotazione.Where(p => p.ID_prenota.ToString() == id).FirstOrDefault();

            //Prenotazione item = this.DBContext.Prenotazione.Where(p => p.ID_prenota.ToString() == "7D288C18-5849-4C97-DFF2-08DA4EEEDBCE").FirstOrDefault();
            //Prenotazione item = this.DBContext.Prenotazione.Where(p => p.ID_prenota.ToString() == id).Single();


            try
            {
                if (item != null)
                {
                    this.DBContext.Prenotazione.Remove(item);
                    this.DBContext.SaveChanges();
                };
                //return "Record eliminato: " + id;
                return "Record eliminato";
                //return item;
            }
            catch (Exception ex)
            {
                return ex.ToString() + ": " + id; // stampa messaggio errore
                //return item;
            }
        }
    }

}