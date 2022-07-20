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
        Repository repository;

        public HomeController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            UserDBContext dbContext)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.dbContext = dbContext;
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
            List<Eventi> Eventi = this.repository.GetEventi();
            List<Repliche> Repliche = this.repository.GetRepliche();
            List<Locali> Locali = this.repository.GetLocali();

            List<PrenotazioneModel> prenotazioneModel = new List<PrenotazioneModel>();

            foreach (Eventi evento in Eventi)
                prenotazioneModel.Add(new PrenotazioneModel()
                {
                    NomeEvento = evento.NomeEvento
                });

            foreach (Repliche replica in Repliche)
                prenotazioneModel.Add(new PrenotazioneModel()
                {
                    DataEOra = replica.DataEOra
                });

            foreach (Locali locale in Locali)
                prenotazioneModel.Add(new PrenotazioneModel()
                {
                    Nome = locale.Nome,
                    Luogo = locale.Luogo,
                    Posti = locale.Posti
                });
            return View(prenotazioneModel);
        }
        public IActionResult Eventi()
        {
            return View();
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
