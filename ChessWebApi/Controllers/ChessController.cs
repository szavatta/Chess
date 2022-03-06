using Chess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace ChessWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessController : ControllerBase
    {

        public class ApiMossa
        {
            public string mossa { get; set; }
            public Colore colore { get; set; }
        }

        public class ApiScacchiera
        {
            public List<Pezzo> scacchiera { get; set; }
            public List<Mossa> mosse { get; set; }
        }

        public class ApiColore
        {
            public Colore colore { get; set; }
        }

        [HttpGet]
        public bool Get()
        {
            return true;
        }


        [HttpPost]
        [Route("nuovapartita")]
        public List<Pezzo> NuovaPartita()
        {
            Chess.Chess chess = new Chess.Chess();
            chess.NuovaPartita();

            return Chess.Chess.GetScacchiera();
        }

        [HttpPost]
        [Route("mossa")]
        public List<Pezzo> Mossa([FromBody] ApiMossa mossa)
        {
            Chess.Chess chess = new Chess.Chess();

            try
            {
                if (chess.GetMosse.Count() == 0 && mossa.colore != Colore.Bianco ||
                    chess.GetMosse.Count() > 0 && chess.GetMosse.Last().pezzo.Colore == mossa.colore)
                    throw new Exception("Attendere il proprio turno");

                if (Chess.Chess.GetScacchiera().Count == 0)
                    chess.NuovaPartita();

                var esito = chess.MuoviPezzo(mossa.mossa, mossa.colore);
                if (esito.Esito == false)
                    throw new Exception(esito.Messaggio);

            }
            catch(Exception ex)
            {
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotAcceptable));
            }

            return Chess.Chess.GetScacchiera();
        }

        [HttpPost]
        [Route("turno")]
        public ApiScacchiera Turno([FromBody] ApiColore colore)
        {
            Chess.Chess chess = new Chess.Chess();

            if (colore.colore == Colore.Bianco && Chess.Chess.GetScacchiera().Count == 0)
                chess.NuovaPartita();

            if (chess.GetMosse.Count() == 0 && colore.colore == Colore.Bianco || 
                chess.GetMosse.Count() > 0 && chess.GetMosse.LastOrDefault()?.pezzo?.Colore != colore.colore)
                return new ApiScacchiera { scacchiera = Chess.Chess.GetScacchiera(), mosse = chess.GetMosse };

            return null;
        }

        [HttpGet]
        [Route("scacchiera")]
        public String Scacchiera()
        {
            Chess.Chess chess = new Chess.Chess();

            return chess.GetScacchieraString();
        }

    }
}
