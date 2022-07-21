using Crociera.DB;
using Crociera.DB.Entities;
using Crociera.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Crociera.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
        private UserDBContext dbContext;
        private readonly Repository repository;

        public HomeController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            UserDBContext dbContext,
            Repository repository)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }
        public IActionResult Prenotazioni()
        {
            List<PrenotazioneModel> prenotazioneModel = new List<PrenotazioneModel>();

            List<Repliche> Repliche = this.repository.GetRepliche();
            foreach (Repliche repliche in Repliche)
            {
                List<Eventi> ListaEventiByID = this.repository.GetEventiByID(repliche.CodEvento);
                foreach (Eventi eventi in ListaEventiByID)
                {
                    List<Locali> ListaLocaliByID = this.repository.GetLocaliByID(eventi.CodLocale);
                    foreach (Locali locali in ListaLocaliByID)
                    {
                        List<Prenotazioni> Prenotazioni = this.repository.GetPrenotazioni(repliche.CodReplica);
                        foreach (Prenotazioni prenotazioni in Prenotazioni)
                        {
                            prenotazioneModel.Add(new PrenotazioneModel()
                            {
                                NomeEvento = eventi.NomeEvento,
                                CodReplica = repliche.CodReplica,
                                DataEOra = repliche.DataEOra,
                                Annullato = repliche.Annullato,
                                Nome = locali.Nome,
                                Luogo = locali.Luogo,
                                Posti = locali.Posti,
                                CodLocale = locali.CodLocale,
                                Quantita = prenotazioni.Quantita
                            });
                        }
                    }
                }
            }
            return View(prenotazioneModel);
        }

        public IActionResult Eventi()
        {
            List<PrenotazioneModel> prenotazioneModel = new List<PrenotazioneModel>();
            var user = userManager.Users.Where(u => u.Id == userManager.GetUserId(User)).FirstOrDefault(); // Oro colato
            string email = user.Email;

            List<Repliche> Repliche = this.repository.GetRepliche();
            foreach (Repliche repliche in Repliche)
            {
                List<Eventi> ListaEventiByID = this.repository.GetEventiByID(repliche.CodEvento);
                foreach (Eventi eventi in ListaEventiByID)
                {
                    List<Locali> ListaLocaliByID = this.repository.GetLocaliByID(eventi.CodLocale);
                    foreach (Locali locali in ListaLocaliByID)
                    {
                        prenotazioneModel.Add(new PrenotazioneModel()
                        {
                            NomeEvento = eventi.NomeEvento,
                            CodEvento = eventi.CodEvento,
                            CodReplica = repliche.CodReplica,
                            DataEOra = repliche.DataEOra,
                            Annullato = repliche.Annullato,
                            Nome = locali.Nome,
                            Luogo = locali.Luogo,
                            Posti = locali.Posti,
                        });
                    }
                }
            }
            return View(prenotazioneModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                //User user = await userManager.FindByNameAsync(loginModel.UserName);
                User user = await userManager.FindByEmailAsync(loginModel.Email);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, false, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Redirect("Index");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                if (signInManager.IsSignedIn(User))
                {
                    await signInManager.SignOutAsync();
                }
            }
            catch (Exception ex)
            {
            }
            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
