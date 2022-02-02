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

        public static List<Pezzo> GetScacchiera(bool clone = false) 
        {
            if (clone)
                return ScacchieraClone = Scacchiera.Select(item => (Pezzo)item.Clone()).ToList();
            else
                return Scacchiera;
        }

        public static List<Pezzo> GetScacchieraClone() => ScacchieraClone.Count() > 0 ? ScacchieraClone : GetScacchiera(true);

        public void NuovaPartita(Colore? colore = null)
        {
            Scacchiera = new List<Pezzo>();
            new Chess().InitialPosition(colore);
        }

        public void NuovaScacchiera()
        {
            Scacchiera = new List<Pezzo>();
            ScacchieraClone = new List<Pezzo>();
        }

        public bool MuoviPezzo(Pos iniziale, Pos finale)
        {
            Pezzo pezzo = Scacchiera.Where(q => q.Posizione.Equals(iniziale)).FirstOrDefault();
            return pezzo != null ? pezzo.Mossa(finale) : false;
        }

        public static Pezzo MangiaPezzo(Pos pos)
        {
            Pezzo old = Chess.GetScacchiera().Where(q => q.Posizione.Equals(pos)).FirstOrDefault();
            if (old != null)
                Chess.GetScacchiera().Remove(old);

            return old;
        }

        public static Pezzo MouviPezzoClone(Pos da, Pos a)
        {
            var sc = Chess.GetScacchiera(true);
            var pz = sc.Where(q => q.Posizione.Equals(da)).FirstOrDefault();
            var old = sc.Where(q => q.Posizione.Equals(a)).FirstOrDefault();
            if (old != null)
                sc.Remove(old);

            pz.Posizione = a;
            return pz;
        }

        public void InitialPosition(Colore? colore = null) 
        {
            if (colore == null || colore == Colore.Nero)
            {
                new Torre(0, 0, Colore.Nero, true);
                new Torre(0, 7, Colore.Nero, true);
                new Cavallo(0, 1, Colore.Nero, true);
                new Cavallo(0, 6, Colore.Nero, true);
                new Alfiere(0, 2, Colore.Nero, true);
                new Alfiere(0, 5, Colore.Nero, true);
                new Re(0, 3, Colore.Nero, true);
                new Regina(0, 4, Colore.Nero, true);

                new Pedone(1, 0, Colore.Nero, true);
                new Pedone(1, 1, Colore.Nero, true);
                new Pedone(1, 2, Colore.Nero, true);
                new Pedone(1, 3, Colore.Nero, true);
                new Pedone(1, 4, Colore.Nero, true);
                new Pedone(1, 5, Colore.Nero, true);
                new Pedone(1, 6, Colore.Nero, true);
                new Pedone(1, 7, Colore.Nero, true);
            }

            if (colore == null || colore == Colore.Bianco)
            {
                new Torre(7, 0, Colore.Bianco, true);
                new Torre(7, 7, Colore.Bianco, true);
                new Cavallo(7, 1, Colore.Bianco, true);
                new Cavallo(7, 6, Colore.Bianco, true);
                new Alfiere(7, 2, Colore.Bianco, true);
                new Alfiere(7, 5, Colore.Bianco, true);
                new Re(7, 3, Colore.Bianco, true);
                new Regina(7, 4, Colore.Bianco, true);

                new Pedone(6, 0, Colore.Bianco, true);
                new Pedone(6, 1, Colore.Bianco, true);
                new Pedone(6, 2, Colore.Bianco, true);
                new Pedone(6, 3, Colore.Bianco, true);
                new Pedone(6, 4, Colore.Bianco, true);
                new Pedone(6, 5, Colore.Bianco, true);
                new Pedone(6, 6, Colore.Bianco, true);
                new Pedone(6, 7, Colore.Bianco, true);
            }
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
    public interface iPezzo {
        public Boolean Mossa(Pos pos);
        public List<Pos> MosseDisponibili(bool testScacco = true);

        public bool IsMossaValida(Pos pos, bool testScacco = true);
    }

    [Serializable()]
    public abstract class Pezzo : iPezzo, ICloneable
    {
        public Pezzo()
        {

        }
        abstract public List<Pos> MosseDisponibili(bool testScacco = true);
        abstract public Boolean Mossa(Pos pos);
        abstract public bool IsMossaValida(Pos pos, bool testScacco = true);
        public Pos Posizione { get; set; }
        public Colore Colore { get; set; }
        public Tipo Tipo { get; set; }
        public int NumMosse { get; set; }
        public Pos PosizioneMangiato { get; set; }

        public Pezzo MangiaPezzo(Pos pos)
        {
            Pezzo old = Chess.GetScacchiera().Where(q => q.Posizione.Equals(pos) && q.Colore != Colore).FirstOrDefault();
            if (old != null)
                Chess.GetScacchiera().Remove(old);

            PosizioneMangiato = null;

            return old;
        }



        public List<Pos> GetMosseTorre(Pos posizione, bool testScacco = true)
        {
            List<Pos> lista = new List<Pos>();
            List<Pezzo> scacchiera = testScacco ? Chess.GetScacchiera() : Chess.GetScacchieraClone();
            Re re = testScacco ? (Re)scacchiera.Where(q => q.Tipo == Tipo.Re && q.Colore == Colore).FirstOrDefault() : null;

            Pos pos = null;
            Pezzo pezzo = null;

            int riga = posizione.Riga;
            int colonna = posizione.Colonna;

            for (int i = 0; i < 4; i++)
            {
                riga = posizione.Riga;
                colonna = posizione.Colonna;
                do
                {
                    pos = new Pos { Riga = riga, Colonna = colonna };

                    if (re != null)
                        Chess.MouviPezzoClone(Posizione, pos);

                    if (!pos.Equals(posizione) && (re == null || re != null && !re.IsSottoScacco()))
                    {
                        lista.Add(pos);
                        if (pezzo != null && pezzo?.Colore != Colore)
                            break;
                    }
                    if (i == 0)      riga++;
                    else if (i == 1) riga--;
                    else if (i == 2) colonna++;
                    else if (i == 3) colonna--;
                    pos = new Pos { Riga = riga, Colonna = colonna };
                    pezzo = scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault();

                } while (riga <= 7 && colonna <= 7 && riga >= 0 && colonna >= 0 && (pezzo == null || pezzo?.Colore != Colore));

            }

            return lista;
        }

        public List<Pos> GetMosseAlfiere(Pos posizione, bool testScacco = true)
        {
            List<Pos> lista = new List<Pos>();
            List<Pezzo> scacchiera = testScacco ? Chess.GetScacchiera() : Chess.GetScacchieraClone();
            Re re = testScacco ? (Re)scacchiera.Where(q => q.Tipo == Tipo.Re && q.Colore == Colore).FirstOrDefault() : null;

            Pos pos = null;
            Pezzo pezzo = null;

            int riga = posizione.Riga;
            int colonna = posizione.Colonna;

            for (int i = 0; i < 4; i++)
            {
                riga = posizione.Riga;
                colonna = posizione.Colonna;
                do
                {
                    pos = new Pos { Riga = riga, Colonna = colonna };

                    if (re != null)
                        Chess.MouviPezzoClone(Posizione, pos);

                    if (!pos.Equals(posizione) && (re == null || re != null && !re.IsSottoScacco()))
                    {
                        lista.Add(pos);
                        if (pezzo != null && pezzo?.Colore != Colore)
                            break;
                    }
                    if (i == 0) { riga++; colonna++; }
                    else if (i == 1) { riga++; colonna--; }
                    else if (i == 2) { riga--; colonna++; }
                    else if (i == 3) { riga--; colonna--; }
                    pos = new Pos { Riga = riga, Colonna = colonna };
                    pezzo = scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault();

                } while (riga <= 7 && colonna <= 7 && riga >= 0 && colonna >= 0 && (pezzo == null || pezzo?.Colore != Colore));

            }


            return lista;
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Torre : Pezzo
    {
        public Torre(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos { Riga = posRiga, Colonna = posColonna };
            Colore = colore;
            Tipo = Tipo.Torre;
            if (aggiungeScacchiera)
                Chess.GetScacchiera().Add(this);
        }

        public override bool Mossa(Pos pos)
        {
            if (!IsMossaValida(pos))
                return false;

            MangiaPezzo(PosizioneMangiato != null ? PosizioneMangiato : pos);

            Posizione = pos;
            NumMosse++;
            return true;
        }

        public override bool IsMossaValida(Pos pos, bool testScacco = true)
        {
            bool ret = true;
            ret = MosseDisponibili(testScacco).Contains(pos);
            

            return ret;

        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            return GetMosseTorre(Posizione, testScacco);
        }


    }

    public class Cavallo : Pezzo
    {
        public Cavallo(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos { Riga = posRiga, Colonna = posColonna };
            Colore = colore;
            Tipo = Tipo.Cavallo;
            if (aggiungeScacchiera)
                Chess.GetScacchiera().Add(this);
        }
        public override bool IsMossaValida(Pos pos, bool testScacco = true) 
        {
            return MosseDisponibili(testScacco).Contains(pos);
        }

        public override bool Mossa(Pos pos)
        {
            if (!IsMossaValida(pos))
                return false;

            MangiaPezzo(PosizioneMangiato != null ? PosizioneMangiato : pos);

            NumMosse++;
            Posizione = pos;
            return true;
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
                    Chess.MouviPezzoClone(Posizione, pos);

                if (pos.Riga >= 0 && pos.Riga <= 7 && pos.Colonna >= 0 && pos.Colonna <= 7
                    && (scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() == null //posizione vuota
                    || scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault()?.Colore != Colore) //mangiata
                    && (re == null || re != null && !re.IsSottoScacco())
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
            Posizione = new Pos { Riga = posRiga, Colonna = posColonna };
            Colore = colore;
            Tipo = Tipo.Alfiere;
            if (aggiungeScacchiera)
                Chess.GetScacchiera().Add(this);
        }

        public override bool Mossa(Pos pos)
        {
            if (!IsMossaValida(pos))
                return false;

            MangiaPezzo(PosizioneMangiato != null ? PosizioneMangiato : pos);

            NumMosse++;
            Posizione = pos;
            return true;
        }

        public override bool IsMossaValida(Pos pos, bool testScacco = true)
        {
            bool ret = MosseDisponibili(testScacco).Contains(pos);

            return ret;
        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            return GetMosseAlfiere(Posizione, testScacco);
        }

    }

    public class Regina : Pezzo
    {
        public Regina(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos { Riga = posRiga, Colonna = posColonna };
            Colore = colore;
            Tipo = Tipo.Regina;
            if (aggiungeScacchiera)
                Chess.GetScacchiera().Add(this);
        }

        public override bool Mossa(Pos pos)
        {
            if (!IsMossaValida(pos))
                return false;

            MangiaPezzo(PosizioneMangiato != null ? PosizioneMangiato : pos);

            NumMosse++;
            Posizione = pos;
            return true;
        }

        public override bool IsMossaValida(Pos pos, bool testScacco = true)
        {
            return MosseDisponibili(testScacco).Contains(pos);
        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            return GetMosseTorre(Posizione, testScacco).Concat(GetMosseAlfiere(Posizione, testScacco)).ToList();
        }

    }

    public class Re : Pezzo
    {
        public Re(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos { Riga = posRiga, Colonna = posColonna };
            Colore = colore;
            Tipo = Tipo.Re;
            if (aggiungeScacchiera)
                Chess.GetScacchiera().Add(this);
        }

        public override bool Mossa(Pos pos)
        {
            if (!IsMossaValida(pos))
                return false;

            MangiaPezzo(PosizioneMangiato != null ? PosizioneMangiato : pos);

            //Muovi arrocco
            Pezzo torre = Chess.GetScacchiera().Where(q => q.Posizione.Equals(pos) && q.Colore == Colore).FirstOrDefault();
            if (torre != null)
            {
                var temp = torre.Posizione;
                torre.Posizione = Posizione;
                Posizione = temp;
            }

            NumMosse++;
            Posizione = pos;
            return true;
        }

        public override bool IsMossaValida(Pos pos, bool testScacco = true)
        {
            bool ret = MosseDisponibili(testScacco).Contains(pos);

            return ret;

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
                        Pos pos = new Pos { Riga = Posizione.Riga + riga, Colonna = Posizione.Colonna + colonna };
                        if (pos.Riga >= 0 && pos.Riga <= 7 && pos.Colonna >= 0 && pos.Colonna <= 7
                            && (scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() == null //posizione vuota
                                || scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault()?.Colore != Colore)) //mangiata)
                        {
                            Re re = (Re)Chess.MouviPezzoClone(Posizione, pos);

                            if (re == null || re != null && !re.IsSottoScacco(testScacco))
                            {
                                lista.Add(pos);
                            }
                        }
                    }
                }
            }

            //Arrocco
            int rigaa = Colore == Colore.Bianco ? 7 : 0;
            if (Posizione.Riga == rigaa && Posizione.Colonna == 3 && NumMosse == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Pos posa = new Pos { Riga = rigaa, Colonna = i == 0 ? 7 : 0 };
                    Pezzo torrea = Chess.GetScacchiera().Where(q => q.Posizione.Equals(posa)).FirstOrDefault();
                    Chess.MouviPezzoClone(Posizione, posa);
                    Re re = (Re)Chess.MouviPezzoClone(Posizione, posa);
                    if (torrea != null && torrea.Tipo == Tipo.Torre && torrea.Colore == Colore && torrea.NumMosse == 0 && (re == null || re != null && !re.IsSottoScacco(testScacco)))
                    {
                        //Verifica che non ci siano pezzi tra re e torre
                        if (Chess.GetScacchiera().Where(q => q.Posizione.Riga == rigaa && (i == 0 && q.Posizione.Colonna > Posizione.Colonna && q.Posizione.Colonna < posa.Colonna || i == 1 && q.Posizione.Colonna < Posizione.Colonna && q.Posizione.Colonna > posa.Colonna)).Count() == 0)
                            lista.Add(posa);
                    }
                }

            }


            return lista;
        }

        public bool IsSottoScacco(bool testRe = true)
        {
            foreach (Pezzo pezzo in Chess.GetScacchieraClone().Where(q => q.Colore != Colore && (testRe || !testRe && q.Tipo != Tipo.Re)))
            {
                if (pezzo.IsMossaValida(Posizione, false))
                    return true;
            }

            return false;
        }

    }

    public class Pedone : Pezzo
    {
        public Pedone(int posRiga, int posColonna, Colore colore, bool aggiungeScacchiera = false)
        {
            Posizione = new Pos { Riga = posRiga, Colonna = posColonna };
            Colore = colore;
            Tipo = Tipo.Pedone;
            if (aggiungeScacchiera)
                Chess.GetScacchiera().Add(this);
        }

        public override bool Mossa(Pos pos)
        {
            if (!IsMossaValida(pos))
                return false;

            MangiaPezzo(PosizioneMangiato != null ? PosizioneMangiato : pos);

            NumMosse++;
            Posizione = pos;
            return true;
        }

        public override bool IsMossaValida(Pos pos, bool testScacco = true)
        {
            bool ret = MosseDisponibili(testScacco).Contains(pos);

            return ret;
        }

        public override List<Pos> MosseDisponibili(bool testScacco = true)
        {
            List<Pos> lista = new List<Pos>();
            List<Pezzo> scacchiera = testScacco ? Chess.GetScacchiera() : Chess.GetScacchieraClone();
            Re re = testScacco ? (Re)scacchiera.Where(q => q.Tipo == Tipo.Re && q.Colore == Colore).FirstOrDefault() : null;

            Pos pos = null;

            int molt = Colore == Colore.Bianco ? - 1 : 1;
            int riga = Posizione.Riga + molt;
            int colonna = Posizione.Colonna;
            pos = new Pos { Riga = riga, Colonna = colonna };
            if (re != null)
                Chess.MouviPezzoClone(Posizione, pos);
            
            if (pos.Riga >= 0 && pos.Riga <= 7 && pos.Colonna >= 0 && pos.Colonna <= 7 && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() == null && (re == null || re != null && !re.IsSottoScacco()))
                lista.Add(pos);

            //2 passi se prima mossa
            if (NumMosse == 0)
            {
                riga = Posizione.Riga + (2 * molt);
                colonna = Posizione.Colonna;
                pos = new Pos { Riga = riga, Colonna = colonna };
                Pos pos2 = new Pos { Riga = riga - molt, Colonna = colonna };
                if (re != null)
                    Chess.MouviPezzoClone(Posizione, pos);
                if (pos.Riga >= 0 && pos.Riga <= 7 && pos.Colonna >= 0 && pos.Colonna <= 7
                    && scacchiera.Where(q => q.Posizione.Equals(pos2)).FirstOrDefault() == null
                    && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() == null
                    && (re == null || re != null && !re.IsSottoScacco()))
                    lista.Add(pos);
            }

            //con mangiata
            riga = Posizione.Riga + molt;
            colonna = Posizione.Colonna + 1;
            pos = new Pos { Riga = riga, Colonna = colonna };
            if (re != null)
                Chess.MouviPezzoClone(Posizione, pos);
            if (pos.Riga >= 0 && pos.Riga <= 7 && pos.Colonna >= 0 && pos.Colonna <= 7 && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() != null && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault().Colore != Colore && (re == null || re != null && !re.IsSottoScacco()))
                lista.Add(pos);

            colonna = Posizione.Colonna - 1;
            pos = new Pos { Riga = riga, Colonna = colonna };
            if (re != null)
                Chess.MouviPezzoClone(Posizione, pos);
            if (pos.Riga >= 0 && pos.Riga <= 7 && pos.Colonna >= 0 && pos.Colonna <= 7 && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault() != null && scacchiera.Where(q => q.Posizione.Equals(pos)).FirstOrDefault().Colore != Colore && (re == null || re != null && !re.IsSottoScacco()))
                lista.Add(pos);

            //En passant
            if (Posizione.Riga == (Colore == Colore.Bianco ? 4 : 3) && NumMosse == 1)
            {
                for (int i = -1; i <= 1; i = i + 2)
                {
                    Pos pos1 = pos1 = new Pos { Riga = Posizione.Riga, Colonna = Posizione.Colonna + i };
                    Pezzo p1 = scacchiera.Where(q => q.Posizione.Equals(pos1)).FirstOrDefault();
                    Pos pos1b = new Pos { Riga = Posizione.Riga + molt, Colonna = Posizione.Colonna + i };
                    if (pos.Riga >= 0 && pos.Riga <= 7 && pos.Colonna >= 0 && pos.Colonna <= 7
                        && scacchiera.Where(q => q.Posizione.Equals(pos1b)).FirstOrDefault() == null
                        && p1 != null && p1.Tipo == Tipo.Pedone && p1.Colore != Colore
                        && (re == null || re != null && !re.IsSottoScacco()))
                    {
                        lista.Add(pos1b);
                        PosizioneMangiato = pos1;
                    }
                }
            }


            return lista;
        }

    }
}
