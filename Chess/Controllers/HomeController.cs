using Chess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Chess chess = new Chess();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult NuovaPartita()
        {
            chess.NuovaPartita();
            return Json(Chess.GetScacchiera());
        }
        public JsonResult NuovaScacchiera()
        {
            chess.NuovaScacchiera();
            return Json(Chess.GetScacchiera());
        }

        public JsonResult MosseDisponibili(int riga, int colonna)
        {
            List<Pos> ret = new List<Pos>();
            Pezzo pezzo = Chess.GetScacchiera().Where(q => q.Posizione.Riga == riga && q.Posizione.Colonna == colonna).FirstOrDefault();
            if (pezzo != null)
                ret = pezzo.MosseDisponibili();

            return Json(ret);
        }

        public JsonResult MuoviPezzo(int initRiga, int initColonna, int finRiga, int finColonna)
        {
            chess.MuoviPezzo(new Pos { Riga = initRiga, Colonna = initColonna }, new Pos { Riga = finRiga, Colonna = finColonna });
            return Json(Chess.GetScacchiera());
        }

        public JsonResult EliminaPezzo(int riga, int colonna)
        {
            Pezzo pezzo = Chess.GetScacchiera().Where(q => q.Posizione.Riga == riga && q.Posizione.Colonna == colonna).FirstOrDefault();
            if (pezzo != null)
                Chess.GetScacchiera().Remove(pezzo);

            return Json(Chess.GetScacchiera());
        }

        public JsonResult InseriscePezzo(int tipo, int colore, int riga, int colonna)
        {
            if (tipo == 0)
                new Pedone(riga, colonna, (Colore)colore, true);
            else if (tipo == 1)
                new Torre(riga, colonna, (Colore)colore, true);
            else if (tipo == 2)
                new Cavallo(riga, colonna, (Colore)colore, true);
            else if (tipo == 3)
                new Alfiere(riga, colonna, (Colore)colore, true);
            else if (tipo == 4)
                new Re(riga, colonna, (Colore)colore, true);
            else if (tipo == 5)
                new Regina(riga, colonna, (Colore)colore, true);

            return Json(Chess.GetScacchiera());
        }


    }
}
