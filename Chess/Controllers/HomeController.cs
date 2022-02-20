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

        public JsonResult GetScacchiera(int? numMosse = null)
        {
            List<Pezzo> scacchiera = null;
            Chess chess = new Chess();
            if (numMosse == null)
                scacchiera = Chess.GetScacchiera();
            else if (numMosse.Value > 0)
                scacchiera = chess.GetMosse.Where(q => q.num == numMosse.Value).FirstOrDefault()?.scacchiera.Where(q => q.Posizione != null).ToList();
            else
                scacchiera = chess.GetScacchieraInizale();

            return Json(scacchiera);
        }

        public JsonResult GetMosse()
        {
            return Json(new Chess().GetMosse);
        }

        public JsonResult NuovaScacchiera()
        {
            chess.NuovaScacchiera();
            return Json(Chess.GetScacchiera());
        }

        public JsonResult CaricaPartita(string txtPartita)
        {
            chess.NuovaPartita();
            Partita partita = chess.GetPartite(txtPartita).FirstOrDefault();
            List<string> lmosse = chess.GetStringMosse(partita.Mosse);
            foreach (string mossa in lmosse)
            {
                if (!chess.MuoviPezzo(mossa))
                    break;
            }

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
            Mossa mossa = null;
            if (initRiga != finRiga || initColonna != finColonna)
                mossa = chess.MuoviPezzo(new Pos { Riga = initRiga, Colonna = initColonna }, new Pos { Riga = finRiga, Colonna = finColonna });

            var pinit = Chess.GetScacchiera().Where(q => q.Posizione.Riga == initRiga && q.Posizione.Colonna == initColonna).FirstOrDefault();
            var pfin = Chess.GetScacchiera().Where(q => q.Posizione.Riga == finRiga && q.Posizione.Colonna == finColonna).FirstOrDefault();

            string op = "";
            if (pfin == null)
                op = "vuoto";
            else if (pfin?.Colore == pinit?.Colore)
                op = "colore";


            return Json(new { scacchiera = Chess.GetScacchiera(), mosse = chess.GetMosse, mossa = mossa, op = op });
        }

        public JsonResult IsSottoScacco(int riga, int colonna, Colore? colore)
        {
            bool isSottoScacco = false;
            Pezzo re = null;
            if (colore == null)
            {
                Pezzo pezzo = Chess.GetScacchiera().Where(q => q.Posizione.Riga == riga && q.Posizione.Colonna == colonna).FirstOrDefault();
                colore = pezzo?.Colore;
            }
            if (colore != null)
            {
                re = Chess.GetScacchiera().Where(q => q.Tipo == Tipo.Re && q.Colore != colore).FirstOrDefault();
                Chess.GetScacchiera(true);
                isSottoScacco = re is Re && ((Re)re).IsSottoScacco(false);
            }

            if (!isSottoScacco)
            {

            }

            return Json(isSottoScacco ? re.Posizione : null);
        }

        public JsonResult EliminaPezzo(int riga, int colonna)
        {
            var chess = new Chess();
            Pezzo pezzo = Chess.GetScacchiera().Where(q => q.Posizione.Riga == riga && q.Posizione.Colonna == colonna).FirstOrDefault();
            if (pezzo != null)
                Chess.GetScacchieraTotale().Remove(pezzo);

            return Json(new { scacchiera = Chess.GetScacchiera(), mosse = chess.GetMosse });
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

            return Json(new { scacchiera = Chess.GetScacchiera(), mosse = chess.GetMosse });
        }

        public JsonResult GetPezzo(int riga, int colonna)
        {
            Pezzo pezzo = Chess.GetScacchiera().Where(q => q.Posizione.Riga == riga && q.Posizione.Colonna == colonna).FirstOrDefault();
            return Json(pezzo);
        }

        public JsonResult LoadFile()
        {

            return Json(true);
        }


    }
}
