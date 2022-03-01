using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Chess
{
    public class Chess
    {
        private static List<Pezzo> Scacchiera = new List<Pezzo>();
        private static List<Pezzo> ScacchieraClone = new List<Pezzo>();
        private static List<Mossa> Mosse = new List<Mossa>();

        public static List<Pezzo> GetScacchiera(bool clone = false, Pos posizione = null, Colore? colore = null, Colore? nocolore = null, Tipo? tipo = null)
        {
            if (clone)
                return ScacchieraClone = Scacchiera.Where(q => q.Posizione != null).Select(item => (Pezzo)item.Clone()).ToList();
            else
            {
                var ret = Scacchiera.Where(q => q.Posizione != null).ToList();
                if (posizione != null)
                    ret = ret.Where(q => q.Posizione.Equals(posizione)).ToList();
                if (colore != null)
                    ret = ret.Where(q => q.Colore == colore).ToList();
                if (nocolore != null)
                    ret = ret.Where(q => q.Colore != nocolore).ToList();
                if (tipo != null)
                    ret = ret.Where(q => q.Tipo == tipo).ToList();

                return ret;
            }
        }

        public static Pezzo GetScacchiera(int riga, int colonna)
            => Scacchiera.Where(q => q.Posizione != null && q.Posizione.Riga == riga && q.Posizione.Colonna == colonna).FirstOrDefault();

        public static List<Pezzo> GetScacchieraTotale() => Scacchiera;

        public List<Pezzo> GetScacchieraInizale()
        {
            List<Pezzo> scacchiera = new List<Pezzo> {
                new Torre(8, 1, Colore.Nero),
                new Torre(8, 8, Colore.Nero),
                new Cavallo(8, 2, Colore.Nero),
                new Cavallo(8, 7, Colore.Nero),
                new Alfiere(8, 3, Colore.Nero),
                new Alfiere(8, 6, Colore.Nero),
                new Re(8, 5, Colore.Nero),
                new Regina(8, 4, Colore.Nero),

                new Pedone(7, 1, Colore.Nero),
                new Pedone(7, 2, Colore.Nero),
                new Pedone(7, 3, Colore.Nero),
                new Pedone(7, 4, Colore.Nero),
                new Pedone(7, 5, Colore.Nero),
                new Pedone(7, 6, Colore.Nero),
                new Pedone(7, 7, Colore.Nero),
                new Pedone(7, 8, Colore.Nero),

                new Torre(1, 1, Colore.Bianco),
                new Torre(1, 8, Colore.Bianco),
                new Cavallo(1, 2, Colore.Bianco),
                new Cavallo(1, 7, Colore.Bianco),
                new Alfiere(1, 3, Colore.Bianco),
                new Alfiere(1, 6, Colore.Bianco),
                new Re(1, 5, Colore.Bianco),
                new Regina(1, 4, Colore.Bianco),

                new Pedone(2, 1, Colore.Bianco),
                new Pedone(2, 2, Colore.Bianco),
                new Pedone(2, 3, Colore.Bianco),
                new Pedone(2, 4, Colore.Bianco),
                new Pedone(2, 5, Colore.Bianco),
                new Pedone(2, 6, Colore.Bianco),
                new Pedone(2, 7, Colore.Bianco),
                new Pedone(2, 8, Colore.Bianco)
            };

            return scacchiera;
        }

        public string GetScacchieraString(List<Pezzo> scacchiera = null)
        {
            string ret = "╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗\r\n";
            for (int riga = 8; riga >= 1; riga--)
            {
                ret += "║";
                for (int col = 1; col <= 8; col++)
                {
                    Pezzo pezzo = scacchiera == null ? Chess.GetScacchiera(riga, col) : scacchiera.Where(q => q.Posizione.Equals(new Pos(riga, col))).FirstOrDefault();
                    string let = "";
                    if (pezzo == null)
                        let = " ";
                    else if (pezzo.Tipo == Tipo.Pedone)
                        let = "P";
                    else
                        let = pezzo.Lettera;

                    if (pezzo?.Colore == Colore.Nero && let != " ")
                        let = let.ToLower();

                    ret += " " + let + " " ;

                    ret += "║";
                }
                ret += "\r\n";
                if (riga > 1)
                    ret += "╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣\r\n";
            }
            ret += "╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝\r\n";

            return ret;
        }


        public static List<Pezzo> AggiungePezzo(Pezzo pezzo)
        { 
            Scacchiera.Add(pezzo);
            return Scacchiera;
        }

        public Pezzo GetPezzo(Pos pos)
            => Chess.GetScacchiera(posizione: pos).FirstOrDefault();

        public static List<Pezzo> GetScacchieraClone() => ScacchieraClone.Count() > 0 ? ScacchieraClone : GetScacchiera(true);

        public List<Mossa> GetMosse => Chess.Mosse;

        public List<string> GetStringMosse(string partita)
        {
            List<string> mosse = new List<string>();
            string temp = partita;

            while (partita.IndexOf("[") >= 0)
            {
                temp = temp.Remove(temp.IndexOf("["), temp.IndexOf("]") - temp.IndexOf("[") + 1);
                partita = temp;
            }

            while (partita.IndexOf("{") >= 0)
            {
                temp = temp.Remove(temp.IndexOf("{"), temp.IndexOf("}") - temp.IndexOf("{") + 1);
                partita = temp;
            }
            partita = partita.Replace("  ", " ").Replace("  ", " ");

            string part = partita.Replace("\r\n", " ").Replace("...", "###").Replace(".", ". ").Replace("###", "...").Trim();

            string mossa = "";
            foreach(string item in part.Split(' '))
            {
                if (item.Trim() != "")
                {
                    if (item.EndsWith("..."))
                        continue;

                    if (item.EndsWith("."))
                    {
                        if (!string.IsNullOrEmpty(mossa))
                            mosse.Add(mossa.Trim());
                        mossa = item + " ";
                    }
                    else
                        mossa += item + " ";
                }
            }
            if (!string.IsNullOrEmpty(mossa))
                mosse.Add(mossa);

            return mosse;
        }

        public List<string> GetStringPartite(string spartite)
        {
            List<string> partite = new List<string>();
            string partita = "";
            foreach(string riga in spartite.Split("\r\n"))
            {
                if (!string.IsNullOrEmpty(riga) && !riga.StartsWith("["))
                {
                    partita += riga + "\r\n";
                }
                if(string.IsNullOrEmpty(riga) && !string.IsNullOrEmpty(partita))
                {
                    partite.Add(partita);
                    partita = "";
                }
            }

            return partite;
        }

        public List<Partita> GetPartite(string spartite)
        {
            List<Partita> partite = new List<Partita>();
            Partita partita = null;
            foreach (string riga in spartite.Replace("\r\n", "\temp").Replace("\n", "\temp").Replace("\r", "\temp").Split("\temp"))
            {
                if (riga.StartsWith("["))
                {
                    if (partita == null)
                        partita = new Partita();
                    string tipo = riga.Substring(1, riga.IndexOf(' ')).Trim();
                    int pos = riga.IndexOf('"');
                    string valore = riga.Substring(pos + 1, riga.IndexOf('"', pos + 1) - pos - 1);
                    switch (tipo)
                    {
                        case "Event":
                            partita.Event = valore;
                            break;
                        case "Site":
                            partita.Site = valore;
                            break;
                        case "Date":
                            partita.Date = valore;
                            break;
                        case "Round":
                            partita.Round = valore;
                            break;
                        case "White":
                            partita.White = valore;
                            break;
                        case "Black":
                            partita.Black = valore;
                            break;
                        case "Result":
                            partita.Result = valore;
                            break;
                        case "WhiteElo":
                            partita.WhiteElo = valore;
                            break;
                        case "BlackElo":
                            partita.BlackElo = valore;
                            break;
                        case "ECO":
                            partita.ECO = valore;
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(riga) && !riga.StartsWith("["))
                {
                    if (partita == null)
                        partita = new Partita();

                    if (partita.Mosse == null)
                        partita.Mosse = "";

                    partita.Mosse += riga.Replace("\n"," ") + " ";
                }

                if (string.IsNullOrEmpty(riga) && !string.IsNullOrEmpty(partita?.Mosse))
                {
                    string temp = partita.Mosse;
                    while (partita.Mosse.IndexOf("{") >= 0)
                    {
                        temp = temp.Remove(temp.IndexOf("{"), temp.IndexOf("}") - temp.IndexOf("{") + 1);
                        partita.Mosse = temp;
                    }
                    partita.Mosse = partita.Mosse.Replace("  ", " ").Replace("  ", " ");
                    partite.Add(partita);
                    partita = null;
                }
            }

            if (partita != null)
            {
                string temp = partita.Mosse;
                while (partita.Mosse.IndexOf("{") >= 0)
                {
                    temp = temp.Remove(temp.IndexOf("{"), temp.IndexOf("}") - temp.IndexOf("{") + 1);
                    partita.Mosse = temp;
                }
                partita.Mosse = partita.Mosse.Replace("  ", " ").Replace("  ", " ");

                partite.Add(partita);
            }


            return partite;
        }

        public void NuovaPartita()
        {
            NuovaScacchiera();
            new Chess().InitialPosition();
        }

        public void NuovaScacchiera()
        {
            Scacchiera = new List<Pezzo>();
            ScacchieraClone = new List<Pezzo>();
            Mosse = new List<Mossa>();
        }

        public Mossa MuoviPezzo(Pos da, Pos a)
        {
            Pezzo pezzo = Scacchiera.Where(q => q.Posizione != null && q.Posizione.Equals(da)).FirstOrDefault();
            Mossa mossa = pezzo?.Mossa(a);

            return mossa;
        }

        public Tipo? GetTipo(string lettera)
        {
            Tipo? tipo = null;
            switch (lettera)
            {
                case "T":
                case "R":
                    tipo = Tipo.Torre;
                    break;
                case "C":
                case "N":
                    tipo = Tipo.Cavallo;
                    break;
                case "A":
                case "B":
                    tipo = Tipo.Alfiere;
                    break;
                case "D":
                case "Q":
                    tipo = Tipo.Regina;
                    break;
                case "K":
                    tipo = Tipo.Re;
                    break;

                default:
                    break;
            }

            return tipo;

        }

        public bool MuoviPezzo(string sMossa)
        {
            bool ret = true;

            try
            {
                if (string.IsNullOrEmpty(sMossa))
                    return true;

                var sMosse = sMossa.Trim().Split(' ').ToList();
                if (sMosse.Count == 3 || sMosse[0].Last() == '.')
                    sMosse = sMosse.Skip(1).ToList();

                Colore? colore = null;
                foreach (string sm in sMosse)
                {
                    if (sm == "1-0" || sm == "0-1" || sm == "1/2-1/2" || sm == "*")
                    {
                        //Fine partita
                        return true;
                    }

                    int riga = 0;
                    int colonna = 0;
                    Tipo tipo = Tipo.Pedone;
                    colore = colore == null ? Colore.Bianco : Colore.Nero;
                    int? colda = null;
                    int? rigda = null;
                    Tipo? promozione = null;
                    bool scacco = false;

                    string sm1 = sm;

                    if (sm1.EndsWith("!!") || sm1.EndsWith("??") || sm1.EndsWith("!?") || sm1.EndsWith("?!"))
                    {
                        sm1 = sm1.Remove(sm1.Length - 2, 2);
                    }
                    else if (sm1.EndsWith("!") || sm1.EndsWith("?"))
                    {
                        sm1 = sm1.Remove(sm1.Length - 1, 1);
                    }

                    if (sm1.Last() == '+') //Scacco
                    {
                        sm1 = sm1.Remove(sm1.Length - 1, 1);
                        scacco = true;
                    }
                    else if (sm1.Last() == '#') //Scacco matto
                    {
                        sm1 = sm1.Remove(sm1.Length - 1, 1);
                        scacco = true;
                    }

                    if (sm1 == "O-O")
                    {
                        tipo = Tipo.Re;
                        riga = colore == Colore.Bianco ? 1 : 8;
                        colonna = 7;
                    }
                    else if (sm1 == "O-O-O")
                    {
                        tipo = Tipo.Re;
                        riga = colore == Colore.Bianco ? 1 : 8;
                        colonna = 3;
                    }
                    else
                    {

                        if (sm1.IndexOf('=') > 0)
                        {
                            promozione = GetTipo(sm1.Split("=")[1]);
                            sm1 = sm1.Split("=")[0];
                        }
                        riga = Convert.ToInt32(sm1.Last().ToString());
                        sm1 = sm1.Remove(sm1.Length - 1, 1);
                        colonna = (int)sm1.Last() - 96;
                        sm1 = sm1.Length > 1 ? sm1.Remove(sm1.Length - 1, 1) : "";

                        if (sm1.Length > 0)
                        {
                            bool isMangiata = false;
                            if (sm1.Last() == 'x')
                            {
                                isMangiata = true;
                                sm1 = sm1.Length > 1 ? sm1.Remove(sm1.Length - 1, 1) : "";
                            }
                            if (sm1[0].ToString() == sm1[0].ToString().ToUpper())
                            {
                                tipo = GetTipo(sm1[0].ToString()).Value;
                                sm1 = sm1.Substring(1);
                            }
                            if (tipo == Tipo.Pedone && isMangiata && sm1.Length > 0)
                            {
                                colda = (int)sm1.Last() - 96;
                                sm1 = sm1.Length > 1 ? sm1.Remove(sm1.Length - 1, 1) : "";
                            }
                            if (sm1.Length > 0)
                            {
                                if (Char.IsDigit(sm1.Last()))
                                    rigda = Convert.ToInt32(sm1.Last().ToString());
                                else
                                    colda = (int)sm1.Last() - 96;

                                sm1 = sm1.Length > 1 ? sm1.Remove(sm1.Length - 1, 1) : "";
                            }
                        }
                    }

                    Pezzo pezzo = null;
                    Pos pos = new Pos(riga, colonna);
                    foreach (Pezzo p in Chess.GetScacchiera(colore: colore, tipo: tipo))
                    {
                        if (pezzo == null && (colda == null || p.Posizione.Colonna == colda) && (rigda == null || p.Posizione.Riga == rigda) && p.IsMossaValida(pos, true))
                        {
                             pezzo = p;
                        }
                    }

                    if (pezzo == null)
                        throw new Exception("Mossa non valida");

                    Mossa mossa = pezzo.Mossa(pos, sMossa: sm, promozione: promozione);
                    if (mossa == null)
                        throw new Exception("Mossa non valida");

                    if (scacco)
                    {
                        Chess.ScacchieraClone = new List<Pezzo>();
                        Re re = (Re)Chess.GetScacchiera(tipo: Tipo.Re, nocolore: colore).FirstOrDefault();
                        if (re == null || re != null && !re.IsSottoScacco(false))
                            ret = false;
                        //new Chess().GetScacchieraString();
                    }

                }

            }
            catch
            {
                ret = false;
            }

            //Pezzo pezzo = Scacchiera.Where(q => q.Posizione != null && q.Posizione.Equals(da)).FirstOrDefault();
            //Mossa mossa = pezzo?.Mossa(a);

            return ret;
        }

        /// <summary>
        /// Muove il pezzo sulla scacchiera clone.
        /// </summary>
        /// <param name="da">Posizione iniziale</param>
        /// <param name="a">Posizione finale</param>
        /// <returns>Pezzo dopo la mossa</returns>
        public static Pezzo MuoviPezzoClone(Pos da, Pos a)
        {
            var sc = Chess.GetScacchiera(true);
            var pz = sc.Where(q => q.Posizione.Equals(da)).FirstOrDefault();
            var old = sc.Where(q => q.Posizione.Equals(a)).FirstOrDefault();
            if (old == null && pz.Tipo == Tipo.Pedone && da.Colonna != a.Colonna)
                old = sc.Where(q => q.Posizione.Colonna == a.Colonna && q.Posizione.Riga == da.Riga).FirstOrDefault();
            if (old != null)
                sc.Remove(old);

            pz.Posizione = a;
            return pz;
        }

        public void InitialPosition() 
        {
            Scacchiera = new Chess().GetScacchieraInizale();
        }
       
    }
    public enum Colore
    {
        Bianco = 0,
        Nero = 1
    }
    public enum Tipo
    {
        Pedone = 0,
        Torre = 1,
        Cavallo = 2,
        Alfiere = 3,
        Re = 4,
        Regina = 5
    }
    public interface iPezzo 
    {
        public List<Pos> MosseDisponibili(bool testScacco = true);

    }

    [Serializable()]
    public abstract class Pezzo : iPezzo, ICloneable
    {
        public Pos Posizione { get; set; }
        public Colore Colore { get; set; }
        public Tipo Tipo { get; set; }
        public int NumMosse { get; set; }
        public string Lettera { get; set; }


        abstract public List<Pos> MosseDisponibili(bool testScacco = true);

        public virtual Mossa Mossa(Pos pos, bool testScacco = true, string sMossa = null, Tipo? promozione = null)
        {
            Pos posMangiato = null;
            if (!IsMossaValida(pos, testScacco))
                return null;

            if (Tipo == Tipo.Pedone && pos.Colonna != Posizione.Colonna && new Chess().GetPezzo(pos) == null)
            {
                //En passant
                posMangiato = new Pos(Posizione.Riga, pos.Colonna);
            }

            Chess.GetScacchiera().Where(q => q is Pedone).ToList().ForEach(q => ((Pedone)q).EnPassant = false);
            if (Tipo == Tipo.Pedone && Math.Abs(pos.Riga - Posizione.Riga) == 2)
                ((Pedone)this).EnPassant = true;

            Pezzo mangiato = MangiaPezzo(posMangiato != null ? posMangiato : pos);
            Re re = (Re)Chess.GetScacchiera(tipo: Tipo.Re, nocolore: Colore).FirstOrDefault();

            Mossa mossa = new Mossa(this, Posizione, pos, mangiato, sMossa: sMossa);
            mossa.SetStringMossa(promozione);
            mossa.isScacco = sMossa != null && sMossa.Contains("+");
            new Chess().GetMosse.Add(mossa);

            Posizione = pos;
            mossa.scacchiera = Chess.GetScacchieraTotale().Select(item => (Pezzo)item.Clone()).ToList();

            if (promozione != null)
            {
                Pezzo old = new Chess().GetPezzo(Posizione);
                switch (promozione.Value)
                {
                    case Tipo.Pedone:
                        new Pedone(old.Posizione.Riga, old.Posizione.Colonna, old.Colore, true);
                        break;
                    case Tipo.Torre:
                        new Torre(old.Posizione.Riga, old.Posizione.Colonna, old.Colore, true);
                        break;
                    case Tipo.Cavallo:
                        new Cavallo(old.Posizione.Riga, old.Posizione.Colonna, old.Colore, true);
                        break;
                    case Tipo.Alfiere:
                        new Alfiere(old.Posizione.Riga, old.Posizione.Colonna, old.Colore, true);
                        break;
                    case Tipo.Regina:
                        new Regina(old.Posizione.Riga, old.Posizione.Colonna, old.Colore, true);
                        break;
                    default:
                        break;
                }
                old.Posizione = null;

                if (re != null)
                {
                    if (re.IsSottoScacco(false, true))
                    {
                        mossa.isScacco = true;
                        mossa.sMossa += "+";
                    }
                }
            }

            if (re != null && re.IsScaccoMatto())
            {
                mossa.sMossa = mossa.sMossa.Replace("+", "#");
                mossa.isScaccoMatto = true;
            }

            if (mossa.sMossaOk != null && mossa.sMossa != mossa.sMossaOk.Replace("?", "").Replace("!", ""))
            {
                throw new Exception("Non corrisponde sMossa");
            }

            //Verifica lo scacco
            if (re != null && sMossa != null)
            {
                if (((Re)re).IsSottoScacco(false, true))
                {
                    if (!sMossa.Contains("+") && !sMossa.Contains("#"))
                        throw new Exception("Non coincide lo scacco");
                }
                else
                {
                    if (sMossa.Contains("+") || sMossa.Contains("#"))
                        throw new Exception("Non coincide lo scacco");
                }
            }

            NumMosse++;
            return mossa;
        }

        public virtual bool IsMossaValida(Pos pos, bool testScacco = true)
            => MosseDisponibili(testScacco).Contains(pos);
        
        public Pezzo MangiaPezzo(Pos pos)
        {
            Pezzo old = Chess.GetScacchiera(posizione:pos, nocolore: Colore).FirstOrDefault();
            if (old != null)
                old.Posizione = null;
                //Chess.GetScacchiera().Remove(old);

            return old;
        }

        public object Clone()
        {
            var serialized = JsonConvert.SerializeObject(this);
            if (this is Pedone)
                return JsonConvert.DeserializeObject<Pedone>(serialized);
            else if (this is Torre)
                return JsonConvert.DeserializeObject<Torre>(serialized);
            else if (this is Cavallo)
                return JsonConvert.DeserializeObject<Cavallo>(serialized);
            else if (this is Alfiere)
                return JsonConvert.DeserializeObject<Alfiere>(serialized);
            else if (this is Regina)
                return JsonConvert.DeserializeObject<Regina>(serialized);
            else if (this is Re)
                return JsonConvert.DeserializeObject<Re>(serialized);
            else
                return null;
        }

        public static object Cast(object obj, Type t)
        {
            try
            {
                var param = Expression.Parameter(obj.GetType());
                return Expression.Lambda(Expression.Convert(param, t), param)
                     .Compile().DynamicInvoke(obj);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public bool IsCasellaAdiacente(Pos pos)
        {
            return (Math.Abs(pos.Colonna - Posizione.Colonna) <= 1 && Math.Abs(pos.Riga - Posizione.Riga) <= 1);
        }

        public bool IsInternoScacchiera(Pos pos)
        {
            return (pos.Riga <= 8 && pos.Colonna <= 8 && pos.Riga >= 1 && pos.Colonna >= 1);
        }

    }

    public class Partita
    {
        public string Mosse { get; set; }
        public string Event { get; set; }
        public string Site { get; set; }
        public string Date { get; set; }
        public string Round { get; set; }
        public string White { get; set; }
        public string Black { get; set; }
        public string Result { get; set; }
        public string WhiteElo { get; set; }
        public string BlackElo { get; set; }
        public string ECO { get; set; }
    }

    public class Mossa
    {
        public Mossa(Pezzo pezzo, Pos da, Pos a, Pezzo pezzoMangiato = null, bool isArrocco = false, string sMossa = null)
        {
            this.num = new Chess().GetMosse.Count + 1;
            this.pezzo = pezzo;
            this.da = da;
            this.a = a;
            this.pezzoMangiato = pezzoMangiato;
            this.isArrocco = isArrocco;
            this.sMossaOk = sMossa;
        }
        public int num { get; set; }
        public Pezzo pezzo { get; set; }
        public Pos da { get; set; }
        public Pos a { get; set; }
        public Pezzo pezzoMangiato { get; set; }
        public bool isArrocco { get; set; }
        public bool isScacco { get; set; }
        public bool isScaccoMatto { get; set; }
        public string sMossa { get; set; }
        public string sMossaOk { get; set; }
        public List<Pezzo> scacchiera { get; set; }

        public void SetStringMossa(Tipo? promozione = null)
        {
            if (pezzo != null)
            {
                if (isArrocco)
                    this.sMossa = a.Colonna > 4 ? "O-O" : "O-O-O";
                else
                {
                    this.sMossa = pezzo?.Lettera;
                    if (pezzo is Pedone && pezzoMangiato != null)
                    {
                        this.sMossa += (char)(da.Colonna + 96);
                    }
                    else
                    {
                        foreach (Pezzo pz in Chess.GetScacchiera(tipo: pezzo.Tipo, colore: pezzo.Colore).Where(q => !q.Posizione.Equals(da)))
                        {
                            if (pz.IsMossaValida(a, true))
                            {
                                if (pz.Posizione.Colonna != da.Colonna)
                                    this.sMossa += (char)(da.Colonna + 96);
                                else if (pz.Posizione.Riga != da.Riga)
                                    this.sMossa += da.Riga;
                            }
                        }
                    }
                    this.sMossa += (pezzoMangiato == null ? "" : "x");
                    this.sMossa += $"{(char)(a?.Colonna + 96)}{a?.Riga}";
                    //this.sMossa = $"{pezzo?.Lettera}{(char)(da?.Colonna + 96)}{da?.Riga}{(pezzoMangiato == null ? "-" : "x")}{(char)(a?.Colonna + 96)}{a?.Riga}";
                }
            }

            if (pezzo.Tipo != Tipo.Re)
            {
                Pezzo re = Chess.GetScacchiera(tipo: Tipo.Re, nocolore: pezzo.Colore).FirstOrDefault();
                if (re != null)
                {
                    Chess.MuoviPezzoClone(da, a);
                    if (((Re)re).IsSottoScacco(false))
                    {
                        this.isScacco = true;
                        this.sMossa += "+";
                    }
                }
            }

            if (promozione != null)
            {
                this.sMossa += "=" + Chess.GetScacchieraTotale().Where(q => q.Tipo == promozione).FirstOrDefault().Lettera;
            }

        }

    }

    public class Pos
    {
        public Pos() 
        {
        }

        public Pos(int riga, int colonna)
        {
            Riga = riga;
            Colonna = colonna;
        }

        public int Riga { get; set; }
        public int Colonna { get; set; }

        public override bool Equals(object obj)
        {
            return (((Pos)obj).Riga == Riga && ((Pos)obj).Colonna == Colonna);
        }

        public override int GetHashCode() => base.GetHashCode();

        public bool IsAttaccato(Colore colore, bool testRe = false)
        {
            foreach (Pezzo pezzo in Chess.GetScacchieraClone().Where(q => q.Colore != colore && (testRe || !testRe && q.Tipo != Tipo.Re)))
            {
                if (pezzo.IsMossaValida(this, false))
                    return true;
            }

            return false;
        }

    }

    public class Torre : Pezzo
    {
        public Torre(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos(posRiga,posColonna);
            Colore = colore;
            Tipo = Tipo.Torre;
            Lettera = "R";
            if (aggiungeScacchiera)
                Chess.AggiungePezzo(this);
        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            List<Pos> lista = new List<Pos>();
            List<Pezzo> scacchiera = testScacco ? Chess.GetScacchiera() : Chess.GetScacchieraClone();
            Re re = testScacco ? (Re)scacchiera.Where(q => q.Tipo == Tipo.Re && q.Colore == Colore).FirstOrDefault() : null;

            Pos pos = null;
            Pezzo pezzo = null;

            int riga = Posizione.Riga;
            int colonna = Posizione.Colonna;

            for (int i = 0; i < 4; i++)
            {
                riga = Posizione.Riga;
                colonna = Posizione.Colonna;
                do
                {
                    pos = new Pos(riga, colonna);

                    if (re != null)
                        Chess.MuoviPezzoClone(Posizione, pos);

                    if (!pos.Equals(Posizione) && (re == null || re != null && !re.IsSottoScacco(false)))
                    {
                        lista.Add(pos);
                        if (pezzo != null && pezzo?.Colore != Colore)
                            break;
                    }
                    //if (testScacco && pezzo != null) break;

                    if (i == 0) riga++;
                    else if (i == 1) riga--;
                    else if (i == 2) colonna++;
                    else if (i == 3) colonna--;
                    pos = new Pos(riga, colonna);
                    pezzo = scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault();

                } while (IsInternoScacchiera(new Pos(riga, colonna)) && (pezzo == null || pezzo?.Colore != Colore));

            }

            return lista;
        }

    }

    public class Cavallo : Pezzo
    {
        public Cavallo(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos(posRiga, posColonna);
            Colore = colore;
            Tipo = Tipo.Cavallo;
            Lettera = "N";
            if (aggiungeScacchiera)
                Chess.AggiungePezzo(this);
        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            List<Pos> lista = new List<Pos>();
            List<Pezzo> scacchiera = testScacco ? Chess.GetScacchiera() : Chess.GetScacchieraClone();
            Re re = testScacco ? (Re)scacchiera.Where(q => q.Tipo == Tipo.Re && q.Colore == Colore).FirstOrDefault() : null;

            for (int i = 0; i < 8; i++)
            {
                Pos pos = null;
                if (i == 0)
                    pos = new Pos { Riga = Posizione.Riga + 2, Colonna = Posizione.Colonna + 1 };
                else if (i == 1)
                    pos = new Pos { Riga = Posizione.Riga + 2, Colonna = Posizione.Colonna - 1 };
                else if (i == 2)
                    pos = new Pos { Riga = Posizione.Riga + 1, Colonna = Posizione.Colonna + 2 };
                else if (i == 3)
                    pos = new Pos { Riga = Posizione.Riga + 1, Colonna = Posizione.Colonna - 2 };
                else if (i == 4)
                    pos = new Pos { Riga = Posizione.Riga - 2, Colonna = Posizione.Colonna + 1 };
                else if (i == 5)
                    pos = new Pos { Riga = Posizione.Riga - 2, Colonna = Posizione.Colonna - 1};
                else if (i == 6)
                    pos = new Pos { Riga = Posizione.Riga - 1, Colonna = Posizione.Colonna + 2 };
                else if (i == 7)
                    pos = new Pos { Riga = Posizione.Riga - 1, Colonna = Posizione.Colonna - 2 };

                if (re != null)
                    Chess.MuoviPezzoClone(Posizione, pos);

                if (IsInternoScacchiera(pos)
                    && (scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() == null //posizione vuota
                    || scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault()?.Colore != Colore) //mangiata
                    && (re == null || re != null && !re.IsSottoScacco(false))
                    ) 
                    lista.Add(pos);

            }

            return lista;
        }

    }

    public class Alfiere : Pezzo
    {
        public Alfiere(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos(posRiga, posColonna);
            Colore = colore;
            Tipo = Tipo.Alfiere;
            Lettera = "B";
            if (aggiungeScacchiera)
                Chess.AggiungePezzo(this);
        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            List<Pos> lista = new List<Pos>();
            List<Pezzo> scacchiera = testScacco ? Chess.GetScacchiera() : Chess.GetScacchieraClone();
            Re re = testScacco ? (Re)scacchiera.Where(q => q.Tipo == Tipo.Re && q.Colore == Colore).FirstOrDefault() : null;

            Pos pos = null;
            Pezzo pezzo = null;

            int riga = Posizione.Riga;
            int colonna = Posizione.Colonna;

            for (int i = 0; i < 4; i++)
            {
                riga = Posizione.Riga;
                colonna = Posizione.Colonna;
                do
                {
                    pos = new Pos(riga, colonna);

                    if (re != null)
                        Chess.MuoviPezzoClone(Posizione, pos);

                    if (!pos.Equals(Posizione) && (re == null || re != null && !re.IsSottoScacco(false)))
                    {
                        lista.Add(pos);
                        if (pezzo != null && pezzo?.Colore != Colore)
                            break;
                    }
                    if (i == 0) { riga++; colonna++; }
                    else if (i == 1) { riga++; colonna--; }
                    else if (i == 2) { riga--; colonna++; }
                    else if (i == 3) { riga--; colonna--; }
                    pos = new Pos(riga, colonna);
                    pezzo = scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault();

                } while (IsInternoScacchiera(new Pos(riga, colonna)) && (pezzo == null || pezzo?.Colore != Colore));

            }

            return lista;


            //return GetMosseAlfiere(Posizione, testScacco);
        }

    }

    public class Regina : Pezzo
    {
        public Regina(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos(posRiga, posColonna);
            Colore = colore;
            Tipo = Tipo.Regina;
            Lettera = "Q";
            if (aggiungeScacchiera)
                Chess.AggiungePezzo(this);
        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            Torre torre = new Torre(Posizione.Riga, Posizione.Colonna, Colore);
            Alfiere alfiere = new Alfiere(Posizione.Riga, Posizione.Colonna, Colore);
            return torre.MosseDisponibili(testScacco).Concat(alfiere.MosseDisponibili(testScacco)).ToList();
        }

    }

    public class Re : Pezzo
    {
        public Re(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos(posRiga, posColonna);
            Colore = colore;
            Tipo = Tipo.Re;
            Lettera = "K";
            if (aggiungeScacchiera)
                Chess.AggiungePezzo(this);
        }

        public override Mossa Mossa(Pos pos, bool testScacco = true, string sMossa = null, Tipo? promozione = null)
        {
            if (!IsMossaValida(pos))
                return null;

            Pezzo mangiato = MangiaPezzo(pos);

            Mossa mossa = new Mossa(this, Posizione, pos, mangiato, false, sMossa);

            //Muovi arrocco
            if (Math.Abs(Posizione.Colonna - pos.Colonna) == 2)
            {
                Pezzo torre = Chess.GetScacchiera(posizione: new Pos(Posizione.Riga, pos.Colonna < 5 ? 1 : 8), tipo: Tipo.Torre).FirstOrDefault();
                if (torre != null)
                {
                    Posizione = pos;
                    torre.Posizione = new Pos(Posizione.Riga, pos.Colonna < 5 ? 4 : 6);
                }
                mossa.isArrocco = true;
            }

            mossa.SetStringMossa();

            new Chess().GetMosse.Add(mossa);

            NumMosse++;
            Posizione = pos;
            mossa.scacchiera = Chess.GetScacchieraTotale().Select(item => (Pezzo)item.Clone()).ToList();

            return mossa;
        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            List<Pos> lista = new List<Pos>();
            List<Pezzo> scacchiera = testScacco ? Chess.GetScacchiera() : Chess.GetScacchieraClone();
            

            for (int riga = -1; riga <= 1; riga++)
            {
                for (int colonna = -1; colonna <= 1; colonna++)
                {
                    if (riga != 0 || colonna != 0)
                    {
                        Pos pos = new Pos(Posizione.Riga + riga, Posizione.Colonna + colonna);
                        if (IsInternoScacchiera(pos)
                            && (scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() == null //posizione vuota
                                || scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault()?.Colore != Colore)) //mangiata)
                        {
                            Re re = (Re)Chess.MuoviPezzoClone(Posizione, pos);

                            if (re == null || re != null && !re.IsSottoScacco(false))
                            {
                                if (testScacco)
                                {
                                    Re re2 = (Re)scacchiera.Where(q => q.Tipo == Tipo.Re && q.Colore != Colore).FirstOrDefault();
                                    if(re2 == null || !re.IsCasellaAdiacente(re2.Posizione))
                                        lista.Add(pos);
                                }
                            }
                            
                        }
                    }
                }
            }

            //Arrocco
            var chess = new Chess();
            int rigaa = Colore == Colore.Bianco ? 1 : 8;
            if (Posizione.Riga == rigaa && Posizione.Colonna == 5 && NumMosse == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Pos posa = new Pos(rigaa, i == 0 ? 8 : 1);
                    Pezzo torrea = chess.GetPezzo(posa);
                    if (torrea != null && torrea.Tipo == Tipo.Torre && torrea.Colore == Colore && torrea.NumMosse == 0)
                    {
                        List<Pos> lpos = i == 0
                            ? new List<Pos> { new Pos(rigaa, Posizione.Colonna + 1), new Pos(rigaa, Posizione.Colonna + 2) }
                            : new List<Pos> { new Pos(rigaa, Posizione.Colonna - 1), new Pos(rigaa, Posizione.Colonna - 2) };

                        bool isAttaccato = false;
                        Chess.GetScacchiera(true);
                        foreach(Pos posb in lpos)
                        {
                            isAttaccato = posb.IsAttaccato(Colore);
                            if (!isAttaccato)
                                isAttaccato = chess.GetPezzo(posb) != null;
                            if (isAttaccato) break;
                        }
                        if (!isAttaccato)
                            isAttaccato = Posizione.IsAttaccato(Colore);


                        if (!isAttaccato)
                        {
                            lista.Add(lpos[1]);
                            //Chess.MouviPezzoClone(Posizione, posa);
                            //Re re = (Re)Chess.MouviPezzoClone(Posizione, lpos[1]);
                            //if (re == null || re != null && !re.IsSottoScacco(testScacco))
                            //    Verifica che non ci siano pezzi tra re e torre
                            //    if (Chess.GetScacchiera().Where(q => q.Posizione.Riga == rigaa && (i == 0 && q.Posizione.Colonna > Posizione.Colonna && q.Posizione.Colonna < posa.Colonna || i == 1 && q.Posizione.Colonna < Posizione.Colonna && q.Posizione.Colonna > posa.Colonna)).Count() == 0)
                            //        lista.Add(posa);
                        }
                    }
                }

            }


            return lista;
        }

        public bool IsSottoScacco(bool testRe = true, bool clone = false)
        {
            if (clone)
                Chess.GetScacchiera(true);

            var pezzi = Chess.GetScacchieraClone().Where(q => q.Colore != Colore && (testRe || !testRe && q.Tipo != Tipo.Re)).ToList();
            foreach (Pezzo pezzo in pezzi)
            {
                if (pezzo.IsMossaValida(Posizione, false))
                    return true;
            }

            return false;
        }

        public bool IsScaccoMatto(bool testRe = true, bool clone = false)
        {
            foreach (Pezzo pezzo in Chess.GetScacchiera(colore: Colore))
            {
                if (pezzo.MosseDisponibili().Count > 0)
                    return false;
            }

            return true;
        }

    }

    public class Pedone : Pezzo
    {
        public bool EnPassant { get; set; }

        public Pedone(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos(posRiga, posColonna);
            Colore = colore;
            Tipo = Tipo.Pedone;
            Lettera = "";
            if (aggiungeScacchiera)
                Chess.AggiungePezzo(this);
        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            List<Pos> lista = new List<Pos>();
            List<Pezzo> scacchiera = testScacco ? Chess.GetScacchiera() : Chess.GetScacchieraClone();
            Re re = testScacco ? (Re)scacchiera.Where(q => q.Tipo == Tipo.Re && q.Colore == Colore).FirstOrDefault() : null;

            Pos pos = null;

            int molt = Colore == Colore.Bianco ? 1 : -1;
            int riga = Posizione.Riga + molt;
            int colonna = Posizione.Colonna;
            pos = new Pos(riga, colonna);
            if (re != null)
                Chess.MuoviPezzoClone(Posizione, pos);
            
            if (IsInternoScacchiera(pos) && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() == null && (re == null || re != null && !re.IsSottoScacco(false)))
                lista.Add(pos);

            //2 passi se prima mossa
            if (NumMosse == 0)
            {
                riga = Posizione.Riga + (2 * molt);
                colonna = Posizione.Colonna;
                pos = new Pos(riga, colonna);
                Pos pos2 = new Pos(riga - molt, colonna);
                if (re != null)
                    Chess.MuoviPezzoClone(Posizione, pos);
                if (IsInternoScacchiera(pos)
                    && scacchiera.Where(q => q.Posizione.Equals(pos2)).FirstOrDefault() == null
                    && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() == null
                    && (re == null || re != null && !re.IsSottoScacco(false)))
                    lista.Add(pos);
            }

            //con mangiata
            riga = Posizione.Riga + molt;
            colonna = Posizione.Colonna + 1;
            pos = new Pos(riga, colonna);
            if (re != null)
                Chess.MuoviPezzoClone(Posizione, pos);
            if (IsInternoScacchiera(pos) && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() != null && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault().Colore != Colore && (re == null || re != null && !re.IsSottoScacco(false)))
                lista.Add(pos);

            colonna = Posizione.Colonna - 1;
            pos = new Pos(riga, colonna);
            if (re != null)
                Chess.MuoviPezzoClone(Posizione, pos);
            if (IsInternoScacchiera(pos) && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() != null && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault().Colore != Colore && (re == null || re != null && !re.IsSottoScacco(false)))
                lista.Add(pos);

            //En passant
            if (Posizione.Riga == (Colore == Colore.Bianco ? 5 : 4))
            {
                for (int i = -1; i <= 1; i = i + 2)
                {
                    Pos pos1 = pos1 = new Pos(Posizione.Riga, Posizione.Colonna + i);
                    Pezzo p1 = scacchiera.Where(q => q.Posizione.Equals(pos1)).FirstOrDefault();
                    Pos pos1b = new Pos(Posizione.Riga + molt, Posizione.Colonna + i);
                    if (re != null)
                        Chess.MuoviPezzoClone(Posizione, pos1);
                    if (IsInternoScacchiera(pos1)
                        && scacchiera.Where(q => q.Posizione.Equals(pos1b)).FirstOrDefault() == null
                        && p1 != null && p1.NumMosse == 1 && p1.Tipo == Tipo.Pedone && p1.Colore != Colore & ((Pedone)p1).EnPassant
                        && (re == null || re != null && !re.IsSottoScacco(false)))
                    {
                        lista.Add(pos1b);
                    }
                }
            }


            return lista;
        }

        public bool Promozione(Tipo tipo)
        {
            if (Colore == Colore.Bianco && Posizione.Riga != 8 || Colore == Colore.Nero && Posizione.Riga != 1)
                return false;

            Tipo = tipo;

            return true;
        }

    }
}
