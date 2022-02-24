using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Chess.Tests
{
    [TestClass()]
    public class TorreTests
    {
        [TestMethod()]
        public void Mossa_corretta()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var torre = new Torre(1, 1, Colore.Nero, true);
            bool ret = torre.Mossa(new Pos { Riga = 1, Colonna = 6 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var torre = new Torre(0, 0, Colore.Nero, true);
            bool ret = torre.Mossa(new Pos { Riga = 1, Colonna = 5 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_non_oltrepassa_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var torre = new Torre(0, 0, Colore.Nero, true);
            var cavallo = new Torre(0, 4, Colore.Nero, true);
            bool ret = torre.Mossa(new Pos { Riga = 0, Colonna = 5 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_mangia()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var torre = new Torre(1, 1, Colore.Nero, true);
            var cavallo = new Torre(1, 5, Colore.Bianco, true);
            bool ret = torre.Mossa(new Pos { Riga = 1, Colonna = 5 }) != null;
            Assert.IsTrue(ret);
        }

    }

    [TestClass()]
    public class AlfiereTests
    {
        [TestMethod()]
        public void Mossa_corretta()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var alfiere = new Alfiere(5, 5, Colore.Nero, true);
            bool ret = alfiere.Mossa(new Pos { Riga = 7, Colonna = 7 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var alfiere = new Alfiere(5, 5, Colore.Nero, true);
            bool ret = alfiere.Mossa(new Pos { Riga = 7, Colonna = 6 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_non_oltrepassa_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var alfiere = new Alfiere(2, 2, Colore.Nero, true);
            var torre = new Torre(5, 5, Colore.Nero, true);
            bool ret = alfiere.Mossa(new Pos { Riga = 7, Colonna = 7 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_con_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var alfiere = new Alfiere(5, 5, Colore.Nero, true);
            new Torre(6, 6, Colore.Nero, true);
            Assert.IsFalse(alfiere.Mossa(new Pos { Riga = 7, Colonna = 7 }) != null);
            new Torre(6, 4, Colore.Bianco, true);
            Assert.IsFalse(alfiere.Mossa(new Pos { Riga = 7, Colonna = 3 }) != null);
            Assert.IsTrue(alfiere.Mossa(new Pos { Riga = 6, Colonna = 4 }) != null);
        }


    }

    [TestClass()]
    public class CavalloTests
    {
        [TestMethod()]
        public void Mossa_corretta()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var cavallo = new Cavallo(5, 5, Colore.Nero, true);
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 6 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var cavallo = new Cavallo(5, 5, Colore.Nero, true);
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 7 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_oltrepassa_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var cavallo = new Cavallo(5, 5, Colore.Nero, true);
            var torre = new Torre(6, 5, Colore.Nero, true);
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 6 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test_mangia()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var cavallo = new Cavallo(5, 5, Colore.Nero, true);
            var cavallo2 = new Cavallo(7, 6, Colore.Bianco, true);
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 6 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test_con_pezzo_stesso_colore()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var cavallo = new Cavallo(5, 5, Colore.Nero, true);
            var cavallo2 = new Cavallo(7, 6, Colore.Nero, true);
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 6 }) != null;
            Assert.IsFalse(ret);
        }

    }

    [TestClass()]
    public class ReTests
    {
        [TestMethod()]
        public void Mossa_corretta()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(5, 5, Colore.Nero, true);
            bool ret = re.Mossa(new Pos { Riga = 6, Colonna = 6 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(5, 5, Colore.Nero, true);
            bool ret = re.Mossa(new Pos { Riga = 6, Colonna = 7 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Re_con_re()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(5, 5, Colore.Nero, true);
            var re2 = new Re(7, 6, Colore.Bianco, true);
            bool ret = re.Mossa(new Pos { Riga = 6, Colonna = 6 }) != null;
            Assert.IsFalse(ret);
            Assert.AreEqual(6, re.MosseDisponibili().Count);
            new Torre(4, 1, Colore.Bianco, true);
            Assert.AreEqual(3, re.MosseDisponibili().Count);
        }

        [TestMethod()]
        public void ScaccoMatto()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(1, 5, Colore.Nero, true);
            new Torre(1, 1, Colore.Bianco, true);
            new Torre(2, 1, Colore.Bianco, true);
            Assert.IsTrue(re.IsScaccoMatto());
        }


        [TestMethod()]
        public void Test_con_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(5, 5, Colore.Nero, true);
            var torre = new Torre(6, 6, Colore.Nero, true);
            bool ret = re.Mossa(new Pos { Riga = 6, Colonna = 6 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_Arrocco1()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(1, 5, Colore.Bianco, true);
            var torre = new Torre(1, 8, Colore.Bianco, true);
            bool ret = re.Mossa(new Pos(1, 7)) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test_Arrocco2()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(1, 5, Colore.Bianco, true);
            var torre = new Torre(1, 1, Colore.Bianco, true);
            bool ret = re.Mossa(new Pos(1, 3)) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test_Arrocco3()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(1, 5, Colore.Bianco, true);
            new Torre(1, 1, Colore.Bianco, true);
            new Torre(1, 8, Colore.Bianco, true);
            new Torre(6, 6, Colore.Nero, true);
            Assert.IsFalse(re.Mossa(new Pos(1, 7)) != null);
            Assert.IsTrue(re.Mossa(new Pos(1, 3)) != null);
            Assert.AreEqual(Tipo.Torre, Chess.GetScacchiera().Where(q => q.Posizione.Equals(new Pos(1, 4))).FirstOrDefault()?.Tipo);
        }

        [TestMethod()]
        public void Test_Arrocco_non_valido()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(0, 3, Colore.Nero, true);
            new Torre(0, 0, Colore.Nero, true);
            new Cavallo(0, 1, Colore.Nero, true);
            Assert.IsFalse(re.Mossa(new Pos(0, 1)) != null);
        }

        [TestMethod()]
        public void Test_Re_con_Re()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(0, 3, Colore.Nero, true);
            var re2 = new Re(0, 5, Colore.Bianco, true);
            bool ret = re.Mossa(new Pos { Riga = 0, Colonna = 4 }) != null;
            Assert.IsFalse(ret);
        }

    }

    [TestClass()]
    public class ReginaTests
    {
        [TestMethod()]
        public void Mossa_corretta_diagonale()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var regina = new Regina(1, 2, Colore.Nero, true);
            bool ret = regina.Mossa(new Pos { Riga = 5, Colonna = 6 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_corretta_verticale()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var regina = new Regina(2, 2, Colore.Nero, true);
            bool ret = regina.Mossa(new Pos { Riga = 2, Colonna = 6 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var regina = new Regina(5, 5, Colore.Nero, true);
            bool ret = regina.Mossa(new Pos { Riga = 6, Colonna = 7 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_con_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var regina = new Regina(5, 5, Colore.Nero, true);
            var torre = new Torre(6, 6, Colore.Nero, true);
            bool ret = regina.Mossa(new Pos { Riga = 6, Colonna = 6 }) != null;
            Assert.IsFalse(ret);
        }

    }

    [TestClass()]
    public class PedoneTests
    {
        [TestMethod()]
        public void Mossa_corretta_1passo_nero()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(7, 2, Colore.Nero, true);
            bool ret = pedone.Mossa(new Pos(6, 2)) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_errata_1passo_nero()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(1, 2, Colore.Nero, true);
            bool ret = pedone.Mossa(new Pos { Riga = 0, Colonna = 2 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Mossa_corretta_1passo_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(2, 2, Colore.Bianco, true);
            bool ret = pedone.Mossa(new Pos { Riga = 3, Colonna = 2 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_errata_1passo_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(6, 2, Colore.Bianco, true);
            bool ret = pedone.Mossa(new Pos { Riga = 5, Colonna = 2 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Mossa_corretta_2passi_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(2, 2, Colore.Bianco, true);
            bool ret = pedone.Mossa(new Pos { Riga = 4, Colonna = 2 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_errata_2passi_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(2, 2, Colore.Bianco, true);
            pedone.NumMosse++;
            bool ret = pedone.Mossa(new Pos { Riga = 4, Colonna = 2 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Mossa_errata_2passi_con_pezzo_in_mezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(6, 2, Colore.Bianco, true);
            var torre = new Torre(5, 2, Colore.Nero, true);
            bool ret = pedone.Mossa(new Pos (4, 2)) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Mossa_con_mangiata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(2, 2, Colore.Bianco, true);
            var torre = new Torre(3, 3, Colore.Nero, true);
            bool ret = pedone.Mossa(new Pos { Riga = 3, Colonna = 3 }) != null;
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_en_passant_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedoneb = new Pedone(4, 2, Colore.Bianco, true);
            var pedonen = new Pedone(7, 3, Colore.Nero, true);
            bool ret1 = pedoneb.Mossa(new Pos { Riga = 5, Colonna = 2 }) != null;
            bool ret2 = pedonen.Mossa(new Pos { Riga = 5, Colonna = 3 }) != null;
            bool ret3 = pedoneb.Mossa(new Pos { Riga = 6, Colonna = 3 }) != null;
            Assert.IsTrue(ret1 && ret2 && ret3);
            Assert.IsTrue(Chess.GetScacchiera().Count == 1);
        }

        [TestMethod()]
        public void Mossa_en_passant_nero()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedonen = new Pedone(5, 3, Colore.Nero, true);
            var pedoneb = new Pedone(2, 2, Colore.Bianco, true);
            bool ret1 = pedonen.Mossa(new Pos { Riga = 4, Colonna = 3 }) != null;
            bool ret2 = pedoneb.Mossa(new Pos { Riga = 4, Colonna = 2 }) != null;
            bool ret3 = pedonen.Mossa(new Pos { Riga = 3, Colonna = 2 }) != null;
            Assert.IsTrue(ret1 && ret2 && ret3);
            Assert.IsTrue(Chess.GetScacchiera().Count == 1);
        }

        [TestMethod()]
        public void Mossa_en_passant_errata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedonen = new Pedone(5, 3, Colore.Nero, true);
            var pedoneb = new Pedone(2, 2, Colore.Bianco, true);
            var torre = new Torre(1, 1, Colore.Bianco, true);
            bool ret1 = pedonen.Mossa(new Pos { Riga = 4, Colonna = 3 }) != null;
            bool ret2 = pedoneb.Mossa(new Pos { Riga = 4, Colonna = 2 }) != null;
            Assert.IsTrue(pedonen.IsMossaValida(new Pos(3,2)));
            torre.Mossa(new Pos(8, 1));
            Assert.IsFalse(pedonen.IsMossaValida(new Pos(3, 2)));
        }
    }

    [TestClass()]
    public class ScaccoTests
    {
        [TestMethod()]
        public void Test1()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(1, 1, Colore.Nero, true);
            var torre = new Torre(1, 3, Colore.Nero, true);
            var torre2 = new Torre(1, 5, Colore.Bianco, true);
            bool ret = re.IsSottoScacco();
            Assert.IsFalse(ret);
            Assert.AreEqual(3, Chess.GetScacchiera().Count, "Non ci sono 3 pezzi");
        }

        [TestMethod()]
        public void Test2()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(1, 1, Colore.Nero, true);
            var torre = new Torre(2, 3, Colore.Nero, true);
            var regina = new Regina(1, 5, Colore.Bianco, true);
            bool ret = re.IsSottoScacco();
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test3()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(1, 1, Colore.Nero, true);
            var torre = new Torre(1, 3, Colore.Nero, true);
            var regina = new Regina(1, 5, Colore.Bianco, true);
            bool ret = torre.Mossa(new Pos { Riga = 2, Colonna = 3 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test4()
        {
            //Il pedone muove e mette sotto scacco il re
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(2, 1, Colore.Nero, true);
            var pedone = new Pedone(2, 3, Colore.Nero, true);
            var regina = new Regina(2, 5, Colore.Bianco, true);
            bool ret = pedone.Mossa(new Pos { Riga = 3, Colonna = 3 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test5()
        {
            //La torre muove e mette sotto scacco il re
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(2, 1, Colore.Nero, true);
            var torre = new Torre(2, 3, Colore.Nero, true);
            var regina = new Regina(2, 5, Colore.Bianco, true);
            bool ret = torre.Mossa(new Pos { Riga = 3, Colonna = 3 }) != null;
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test6()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo(new Pos(7, 5), new Pos(6, 5)) != null);
            Assert.IsTrue(chess.MuoviPezzo(new Pos(2, 6), new Pos(3, 6)) != null);
            Assert.IsTrue(chess.MuoviPezzo(new Pos(8, 4), new Pos(4, 8)) != null);
            Re re = (Re)chess.GetPezzo(new Pos(1, 5));
            Assert.IsTrue(re.IsSottoScacco(false, true),"1");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(2, 7), new Pos(3, 7)) != null);
            Assert.IsFalse(re.IsSottoScacco(false, true),"2");
        }

        [TestMethod()]
        public void Test7()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            chess.MuoviPezzo(new Pos(7, 6), new Pos(6, 6));
            chess.MuoviPezzo(new Pos(2, 5), new Pos(3, 5));
            chess.MuoviPezzo(new Pos(8, 2), new Pos(6, 1));
            chess.MuoviPezzo(new Pos(1, 2), new Pos(3, 1));
            chess.MuoviPezzo(new Pos(6, 1), new Pos(4, 2));
            var mosse = chess.GetMosse;
        }

    }

    [TestClass()]
    public class GenericTests
    {
        public void Mosse_varie()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();

            Assert.IsTrue(chess.MuoviPezzo(new Pos(8, 1), new Pos(8, 0)) != null, "1");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(1, 1), new Pos(3, 1)) != null, "2");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(6, 0), new Pos(4, 0)) != null, "3");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(0, 6), new Pos(2, 7)) != null, "4");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(4, 0), new Pos(3, 0)) != null, "5");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(3, 1), new Pos(4, 0)) != null, "2");
            Assert.AreEqual(Chess.GetScacchiera().Count, 31, "Non ci sono 31 pezzi");
        }

        [TestMethod()]
        public void Test_If()
        {
            int a = 0;
            for (int i = 0; i < 100000000; i++)
            {
                if (i == 1) a = 0;
                else if (i == 2) a = 0;
                else if (i == 3) a = 0;
                else if (i == 4) a = 0;
                else if (i == 5) a = 0;
                else if (i == 6) a = 0;
                else if (i == 7) a = 0;
                else if (i == 8) a = 0;
                else if (i == 9) a = 0;
                else if (i == 10) a = 0;
                else if (i == 11) a = 0;
                else a = 0;
            }
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void Test_Switch()
        {
            int a = 0;
            for (int i = 0; i < 100000000; i++)
            {
                switch (i)
                {
                    case 1: a = 0; break;
                    case 2: a = 0; break;
                    case 3: a = 0; break;
                    case 4: a = 0; break;
                    case 5: a = 0; break;
                    case 6: a = 0; break;
                    case 7: a = 0; break;
                    case 8: a = 0; break;
                    case 9: a = 0; break;
                    case 10: a = 0; break;
                    case 11: a = 0; break;
                    default: a = 0; break;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void Test01()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();

            var lre = Chess.GetScacchiera(tipo: Tipo.Re).FirstOrDefault();
            var scatto = ((Re)lre).IsSottoScacco();
            
        }

    }

    [TestClass()]
    public class PartiteTests
    {
        [TestMethod()]
        public void Test01()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. e4 c5 "));
            Assert.IsTrue(chess.MuoviPezzo("2. Nf3 e6 "));
            Assert.IsTrue(chess.MuoviPezzo("3. c3 d5 "));
            Assert.IsTrue(chess.MuoviPezzo("4. e5 d4 "));
            Assert.IsTrue(chess.MuoviPezzo("5. Bd3 Bd7 "));
            Assert.IsTrue(chess.MuoviPezzo("6. O-O Bc6 "));
            Assert.IsTrue(chess.MuoviPezzo("7. Re1 g5 "));
            Assert.IsTrue(chess.MuoviPezzo("8. h3 h5 "));
            Assert.IsTrue(chess.MuoviPezzo("9. Na3 g4 "));
            Assert.IsTrue(chess.MuoviPezzo("10. Nh2 Nh6 "));
            Assert.IsTrue(chess.MuoviPezzo("11. Be4 d3 "));
            Assert.IsTrue(chess.MuoviPezzo("12. b4 Bxe4 "));
            Assert.IsTrue(chess.MuoviPezzo("13. Rxe4 Qd5 "));
            Assert.IsTrue(chess.MuoviPezzo("14. Qa4+ Nc6 "));
            Assert.IsTrue(chess.MuoviPezzo("15. bxc5 Bxc5 "));
            Assert.IsTrue(chess.MuoviPezzo("16. Nb5 O-O-O "));
            Assert.IsTrue(chess.MuoviPezzo("17. Qc4 g3 "));
            Assert.IsTrue(chess.MuoviPezzo("18. Nf1 gxf2+ "));
            Assert.IsTrue(chess.MuoviPezzo("19. Kh2 Nf5 "));
            Assert.IsTrue(chess.MuoviPezzo("20. Ba3 Bxa3 "));
            Assert.IsTrue(chess.MuoviPezzo("21. Nxa3 Kb8 "));
            Assert.IsTrue(chess.MuoviPezzo("22. Qxd5 Rxd5 "));
            Assert.IsTrue(chess.MuoviPezzo("23. Nc4 b5 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Nce3 Nxe3 "));
            Assert.IsTrue(chess.MuoviPezzo("25. Nxe3 Rxe5 "));
            Assert.IsTrue(chess.MuoviPezzo("26. Rf4 f5 "));
            Assert.IsTrue(chess.MuoviPezzo("27. Kg3 Kc7"));
            Assert.IsTrue(chess.MuoviPezzo("28. a4 a6 "));
            Assert.IsTrue(chess.MuoviPezzo("29. axb5 axb5 "));
            Assert.IsTrue(chess.MuoviPezzo("30. Kxf2 Re4 "));
            Assert.IsTrue(chess.MuoviPezzo("31. Rxe4 fxe4 "));
            Assert.IsTrue(chess.MuoviPezzo("32. Kg3 Rf8 "));
            Assert.IsTrue(chess.MuoviPezzo("33. Re1 Ne7 "));
            Assert.IsTrue(chess.MuoviPezzo("34. Rb1 Kc6 "));
            Assert.IsTrue(chess.MuoviPezzo("35. Rb4 Nf5+ "));
            Assert.IsTrue(chess.MuoviPezzo("36. Kf4 Nd6+ "));
            Assert.IsTrue(chess.MuoviPezzo("37. Ke5 Rf2 "));
            Assert.IsTrue(chess.MuoviPezzo("38. Rd4 Nf7+ "));
            Assert.IsTrue(chess.MuoviPezzo("39. Kxe6 Ng5+ "));
            Assert.IsTrue(chess.MuoviPezzo("40. Ke5 Rxd2 "));
            Assert.IsTrue(chess.MuoviPezzo("41. h4 Re2 "));
            Assert.IsTrue(chess.MuoviPezzo("42. Rd6+ Kb7 "));
            Assert.IsTrue(chess.MuoviPezzo("43. hxg5 Rxe3 "));
            Assert.IsTrue(chess.MuoviPezzo("44. g6 Re2 "));
            Assert.IsTrue(chess.MuoviPezzo("45. g4 Rg2 "));
            Assert.IsTrue(chess.MuoviPezzo("46. gxh5 d2 "));
            Assert.IsTrue(chess.MuoviPezzo("47. Kxe4 "));
            Assert.IsTrue(Chess.GetScacchiera().Count == 9);
        }

        [TestMethod()]
        public void Test02()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. d4 Nf6 "));
            Assert.IsTrue(chess.MuoviPezzo("2. Nf3 g6 "));
            Assert.IsTrue(chess.MuoviPezzo("3. Bf4 d6 "));
            Assert.IsTrue(chess.MuoviPezzo("4. e3 Nh5 "));
            Assert.IsTrue(chess.MuoviPezzo("5. Bg5 h6 "));
            Assert.IsTrue(chess.MuoviPezzo("6. Bh4 g5 "));
            Assert.IsTrue(chess.MuoviPezzo("7. Nfd2 Ng7 "));
            Assert.IsTrue(chess.MuoviPezzo("8. Bg3 Nf5 "));
            Assert.IsTrue(chess.MuoviPezzo("9. c3 Bg7 "));
            Assert.IsTrue(chess.MuoviPezzo("10. Bd3 e6 "));
            Assert.IsTrue(chess.MuoviPezzo("11. Na3 Nd7 "));
            Assert.IsTrue(chess.MuoviPezzo("12. Qf3 Nxg3 "));
            Assert.IsTrue(chess.MuoviPezzo("13. hxg3 a6 "));
            Assert.IsTrue(chess.MuoviPezzo("14. O-O-O Rb8 "));
            Assert.IsTrue(chess.MuoviPezzo("15. Nc2 b5 "));
            Assert.IsTrue(chess.MuoviPezzo("16. Qe2 c5 "));
            Assert.IsTrue(chess.MuoviPezzo("17. f4 d5 "));
            Assert.IsTrue(chess.MuoviPezzo("18. dxc5 Nxc5 "));
            Assert.IsTrue(chess.MuoviPezzo("19. e4 b4 "));
            Assert.IsTrue(chess.MuoviPezzo("20. cxb4 Nxd3+"));
            Assert.IsTrue(chess.MuoviPezzo("21. Qxd3 O-O"));
            Assert.IsTrue(chess.MuoviPezzo("22. exd5 Qxd5 "));
            Assert.IsTrue(chess.MuoviPezzo("23. Qxd5 exd5 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Nf3 a5 "));
            Assert.IsTrue(chess.MuoviPezzo("25. Rxd5 axb4 "));
            Assert.IsTrue(chess.MuoviPezzo("26. fxg5 h5 "));
            Assert.IsTrue(chess.MuoviPezzo("27. Ncd4 Bb7"));
            Assert.IsTrue(chess.MuoviPezzo("28. Rd6 Rfc8+ "));
            Assert.IsTrue(chess.MuoviPezzo("29. Kd2 Rd8 "));
            Assert.IsTrue(chess.MuoviPezzo("30. Rxd8+ Rxd8 "));
            Assert.IsTrue(chess.MuoviPezzo("31. Ke3 Re8+ "));
            Assert.IsTrue(chess.MuoviPezzo("32. Kf2 Ra8 "));
            Assert.IsTrue(chess.MuoviPezzo("33. a3 bxa3"));
            Assert.IsTrue(chess.MuoviPezzo("34. bxa3 Rxa3 "));
            Assert.IsTrue(chess.MuoviPezzo("35. Rxh5 Ra2+ "));
            Assert.IsTrue(chess.MuoviPezzo("36. Kg1 Bxd4+ "));
            Assert.IsTrue(chess.MuoviPezzo("37. Nxd4 Rxg2+ "));
            Assert.IsTrue(chess.MuoviPezzo("38. Kf1 Rxg3 "));
            Assert.IsTrue(chess.MuoviPezzo("39. Kf2 Rg4 "));
            Assert.IsTrue(chess.MuoviPezzo("40. Ke3 Re4+ "));
            Assert.IsTrue(chess.MuoviPezzo("41. Kd3 Re5 "));
            Assert.IsTrue(chess.MuoviPezzo("42. Rh2 Rxg5 "));
            Assert.IsTrue(chess.MuoviPezzo("43. Rb2 Bd5 "));
            Assert.IsTrue(chess.MuoviPezzo("44. Ke3 Re5+ "));
            Assert.IsTrue(chess.MuoviPezzo("45. Kf2 Kg7"));
            Assert.IsTrue(chess.MuoviPezzo("46. Rb5 f6 "));
            Assert.IsTrue(chess.MuoviPezzo("47. Ra5 Kg6 "));
            Assert.IsTrue(chess.MuoviPezzo("48. Ne2 Rf5+ "));
            Assert.IsTrue(chess.MuoviPezzo("49. Ke3 Re5+ "));
            Assert.IsTrue(chess.MuoviPezzo("50. Kf2 Kg5 "));
            Assert.IsTrue(chess.MuoviPezzo("51. Nc3 Be6 "));
            Assert.IsTrue(chess.MuoviPezzo("52. Rxe5+ fxe5 "));
            Assert.IsTrue(chess.MuoviPezzo("53. Ke3 "));
            Assert.IsTrue(Chess.GetScacchiera().Count == 5);
        }

        [TestMethod()]
        public void Test03()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. d4 Nf6 "));
            Assert.IsTrue(chess.MuoviPezzo("2. Bg5 d5 "));
            Assert.IsTrue(chess.MuoviPezzo("3. e3 g6 "));
            Assert.IsTrue(chess.MuoviPezzo("4. Bxf6 exf6 "));
            Assert.IsTrue(chess.MuoviPezzo("5. c4 c6 "));
            Assert.IsTrue(chess.MuoviPezzo("6. Nc3 Bg7 "));
            Assert.IsTrue(chess.MuoviPezzo("7. cxd5 cxd5 "));
            Assert.IsTrue(chess.MuoviPezzo("8. Qb3 O-O "));
            Assert.IsTrue(chess.MuoviPezzo("9. Qxd5 Nc6 "));
            Assert.IsTrue(chess.MuoviPezzo("10. Qxd8 Rxd8 "));
            Assert.IsTrue(chess.MuoviPezzo("11. a3 a6 "));
            Assert.IsTrue(chess.MuoviPezzo("12. Nf3 b5 "));
            Assert.IsTrue(chess.MuoviPezzo("13. Be2 Bb7 "));
            Assert.IsTrue(chess.MuoviPezzo("14. O-O Ne7"));
            Assert.IsTrue(chess.MuoviPezzo("15. Rfc1 Rac8 "));
            Assert.IsTrue(chess.MuoviPezzo("16. Ne1 Nd5 "));
            Assert.IsTrue(chess.MuoviPezzo("17. Nxd5 Bxd5 "));
            Assert.IsTrue(chess.MuoviPezzo("18. Bf3 Bxf3 "));
            Assert.IsTrue(chess.MuoviPezzo("19. Nxf3 Bf8 "));
            Assert.IsTrue(chess.MuoviPezzo("20. Kf1 Bd6"));
            Assert.IsTrue(chess.MuoviPezzo("21. Ke2 Kf8 "));
            Assert.IsTrue(chess.MuoviPezzo("22. Nd2 Ke8 "));
            Assert.IsTrue(chess.MuoviPezzo("23. h3 Kd7 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Kd3 f5 "));
            Assert.IsTrue(chess.MuoviPezzo("25. Rc3 Rxc3+ "));
            Assert.IsTrue(chess.MuoviPezzo("26. bxc3 Rb8 "));
            Assert.IsTrue(chess.MuoviPezzo("27. a4 Rb7 "));
            Assert.IsTrue(chess.MuoviPezzo("28. axb5 axb5 "));
            Assert.IsTrue(chess.MuoviPezzo("29. Ra6 h6 "));
            Assert.IsTrue(chess.MuoviPezzo("30. g4 Kc7 "));
            Assert.IsTrue(chess.MuoviPezzo("31. gxf5 gxf5 "));
            Assert.IsTrue(chess.MuoviPezzo("32. c4 h5 "));
            Assert.IsTrue(chess.MuoviPezzo("33. c5 Be7 "));
            Assert.IsTrue(chess.MuoviPezzo("34. Nb3 Rb8 "));
            Assert.IsTrue(chess.MuoviPezzo("35. d5 Bd8 "));
            Assert.IsTrue(chess.MuoviPezzo("36. Nd4 b4 "));
            Assert.IsTrue(chess.MuoviPezzo("37. Kc4 h4 "));
            Assert.IsTrue(chess.MuoviPezzo("38. Ra7+ Kc8 "));
            Assert.IsTrue(chess.MuoviPezzo("39. d6 b3 "));
            Assert.IsTrue(chess.MuoviPezzo("40. d7# "));

            Pezzo re = Chess.GetScacchiera(tipo: Tipo.Re, colore: Colore.Nero).FirstOrDefault();

            Assert.IsTrue(re.MosseDisponibili().Count == 0);
        }

        [TestMethod()]
        public void Test04()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. e4 e5 "));
            Assert.IsTrue(chess.MuoviPezzo("2. Nf3 Nc6"));
            Assert.IsTrue(chess.MuoviPezzo("3. d4 exd4 "));
            Assert.IsTrue(chess.MuoviPezzo("4. Nxd4 Bc5 "));
            Assert.IsTrue(chess.MuoviPezzo("5. Be3 Qf6 "));
            Assert.IsTrue(chess.MuoviPezzo("6. c3 Nge7 "));
            Assert.IsTrue(chess.MuoviPezzo("7. Bc4 Ne5 "));
            Assert.IsTrue(chess.MuoviPezzo("8. Bb3 Qg6 "));
            Assert.IsTrue(chess.MuoviPezzo("9. O-O d5 "));
            Assert.IsTrue(chess.MuoviPezzo("10. Bf4 Bg4 "));
            Assert.IsTrue(chess.MuoviPezzo("11. Qc2 Bxd4 "));
            Assert.IsTrue(chess.MuoviPezzo("12. cxd4 N5c6 "));
            Assert.IsTrue(chess.MuoviPezzo("13. Qd2 Qxe4 "));
            Assert.IsTrue(chess.MuoviPezzo("14. f3 Bxf3 "));
            Assert.IsTrue(chess.MuoviPezzo("15. gxf3 Qxd4+ "));
            Assert.IsTrue(chess.MuoviPezzo("16. Qxd4 Nxd4 "));
            Assert.IsTrue(chess.MuoviPezzo("17. Bxc7 Kd7 "));
            Assert.IsTrue(chess.MuoviPezzo("18. Bg3 Nxb3 "));
            Assert.IsTrue(chess.MuoviPezzo("19. axb3 Nf5 "));
            Assert.IsTrue(chess.MuoviPezzo("20. Nc3 d4 "));
            Assert.IsTrue(chess.MuoviPezzo("21. Rfd1 Rhd8 "));
            Assert.IsTrue(chess.MuoviPezzo("22. Rd3 Kc6 "));
            Assert.IsTrue(chess.MuoviPezzo("23. Ne2 Rd5 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Rad1 Rad8"));
            Assert.IsTrue(chess.MuoviPezzo("25. Bf2 Kc5 "));
            Assert.IsTrue(chess.MuoviPezzo("26. Rc1+ Kb5"));
            Assert.IsTrue(chess.MuoviPezzo("27. Rc4 b6 "));
            Assert.IsTrue(chess.MuoviPezzo("28. Ng3 Nh4 "));
            Assert.IsTrue(chess.MuoviPezzo("29. Kf1 Ka6 "));
            Assert.IsTrue(chess.MuoviPezzo("30. Rc7 Rf8 "));
            Assert.IsTrue(chess.MuoviPezzo("31. b4 g6 "));
            Assert.IsTrue(chess.MuoviPezzo("32. Ne2 Nf5 "));
            Assert.IsTrue(chess.MuoviPezzo("33. Nxd4 Nxd4 "));
            Assert.IsTrue(chess.MuoviPezzo("34. Rxd4 Rf5 "));
            Assert.IsTrue(chess.MuoviPezzo("35. Rd3 Rf4 "));
            Assert.IsTrue(chess.MuoviPezzo("36. Rb3 Kb5 "));
            Assert.IsTrue(chess.MuoviPezzo("37. Rxa7 Re8 "));
            Assert.IsTrue(chess.MuoviPezzo("38. Rb7 Re6 "));
            Assert.IsTrue(chess.MuoviPezzo("39. Re3 Ref6"));
            Assert.IsTrue(chess.MuoviPezzo("40. Kg2 Rxb4 "));
            Assert.IsTrue(chess.MuoviPezzo("41. b3 Kc6 "));
            Assert.IsTrue(chess.MuoviPezzo("42. Ra7 h5 "));
            Assert.IsTrue(chess.MuoviPezzo("43. Rc3+ Kb5 "));
            Assert.IsTrue(chess.MuoviPezzo("44. Re7 Rf5 "));
            Assert.IsTrue(chess.MuoviPezzo("45. h4 g5 "));
            Assert.IsTrue(chess.MuoviPezzo("46. hxg5 Rxg5+"));
            Assert.IsTrue(chess.MuoviPezzo("47. Kh3 Rf5 "));
            Assert.IsTrue(chess.MuoviPezzo("48. Rb7 Ka6 "));
            Assert.IsTrue(chess.MuoviPezzo("49. Re7 Rbf4 "));
            Assert.IsTrue(chess.MuoviPezzo("50. Ree3 Kb5 "));
            Assert.IsTrue(chess.MuoviPezzo("51. Bg3 Rd4 "));
            Assert.IsTrue(chess.MuoviPezzo("52. Re5+ Rd5"));
            Assert.IsTrue(chess.MuoviPezzo("53. Rxd5+ Rxd5 "));
            Assert.IsTrue(chess.MuoviPezzo("54. Rc4 Rd3 "));
            Assert.IsTrue(chess.MuoviPezzo("55. Rf4 Rxb3 "));
            Assert.IsTrue(chess.MuoviPezzo("56. Rxf7 Kc4 "));
            Assert.IsTrue(chess.MuoviPezzo("57. f4 b5 "));
            Assert.IsTrue(chess.MuoviPezzo("58. f5 Kd5 "));
            Assert.IsTrue(chess.MuoviPezzo("59. Rd7+ Ke4 "));
            Assert.IsTrue(chess.MuoviPezzo("60. f6 Rf3 "));
            Assert.IsTrue(chess.MuoviPezzo("61. f7 b4 "));
            Assert.IsTrue(chess.MuoviPezzo("62. Kg2 b3 "));
            Assert.IsTrue(chess.MuoviPezzo("63. Re7+ "));

            Assert.IsTrue(Chess.GetScacchiera().Count == 8);
        }

        [TestMethod()]
        public void Test05()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. e4 c5 "));
            Assert.IsTrue(chess.MuoviPezzo("2. Nf3 d6"));
            Assert.IsTrue(chess.MuoviPezzo("3. d4 cxd4 "));
            Assert.IsTrue(chess.MuoviPezzo("4. Nxd4 Nf6 "));
            Assert.IsTrue(chess.MuoviPezzo("5. Nc3 a6 "));
            Assert.IsTrue(chess.MuoviPezzo("6. h3 e6 "));
            Assert.IsTrue(chess.MuoviPezzo("7. g4 Be7 "));
            Assert.IsTrue(chess.MuoviPezzo("8. Bg2 Nfd7 "));
            Assert.IsTrue(chess.MuoviPezzo("9. Be3 Nc6 "));
            Assert.IsTrue(chess.MuoviPezzo("10. Qe2 O-O "));
            Assert.IsTrue(chess.MuoviPezzo("11. O-O-O Nxd4 "));
            Assert.IsTrue(chess.MuoviPezzo("12. Bxd4 Qc7 "));
            Assert.IsTrue(chess.MuoviPezzo("13. f4 b5 "));
            Assert.IsTrue(chess.MuoviPezzo("14. g5 b4 "));
            Assert.IsTrue(chess.MuoviPezzo("15. Na4 e5 "));
            Assert.IsTrue(chess.MuoviPezzo("16. Be3 exf4 "));
            Assert.IsTrue(chess.MuoviPezzo("17. Bxf4 Ne5 "));
            Assert.IsTrue(chess.MuoviPezzo("18. Qf2 Rb8 "));
            Assert.IsTrue(chess.MuoviPezzo("19. h4 Bg4 "));
            Assert.IsTrue(chess.MuoviPezzo("20. Rd2 Rfc8 "));
            Assert.IsTrue(chess.MuoviPezzo("21. b3 Qa5"));
            Assert.IsTrue(chess.MuoviPezzo("22. Bh3 Be6 "));
            Assert.IsTrue(chess.MuoviPezzo("23. h5 Nc4 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Re2 Bxg5 "));
            Assert.IsTrue(chess.MuoviPezzo("25. bxc4 Bxc4 "));
            Assert.IsTrue(chess.MuoviPezzo("26. Bxg5 Qxg5+ "));
            Assert.IsTrue(chess.MuoviPezzo("27. Re3 Bd3 "));
            Assert.IsTrue(chess.MuoviPezzo("28. Qg3 Rxc2+ "));
            Assert.IsTrue(chess.MuoviPezzo("29. Kd1 Qxh5+ "));
            Assert.IsTrue(chess.MuoviPezzo("30. Qg4 Qe5 "));
            Assert.IsTrue(chess.MuoviPezzo("31. Rxd3 Rxa2 "));
            Assert.IsTrue(chess.MuoviPezzo("32. Rd2 Qa1+ "));
            Assert.IsTrue(chess.MuoviPezzo("33. Ke2 Rxd2+ "));
            Assert.IsTrue(chess.MuoviPezzo("34. Kxd2 Qxh1 "));
            Assert.IsTrue(chess.MuoviPezzo("35. Nb2 Qh2+ "));
            Assert.IsTrue(chess.MuoviPezzo("36. Bg2 h5 "));
            Assert.IsTrue(chess.MuoviPezzo("37. Qg5 Qe5 "));
            Assert.IsTrue(chess.MuoviPezzo("38. Qxe5 dxe5 "));
            Assert.IsTrue(chess.MuoviPezzo("39. Bf1 a5 "));
            Assert.IsTrue(chess.MuoviPezzo("40. Bc4 h4"));

            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Bianco).Count() == 4);
            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Nero).Count() == 8);
        }

        [TestMethod()]
        public void Test06()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. d4 Nc6 "));
            Assert.IsTrue(chess.MuoviPezzo("2. Nf3 d5 "));
            Assert.IsTrue(chess.MuoviPezzo("3. c4 Bg4 "));
            Assert.IsTrue(chess.MuoviPezzo("4. cxd5 Bxf3 "));
            Assert.IsTrue(chess.MuoviPezzo("5. gxf3 Qxd5 "));
            Assert.IsTrue(chess.MuoviPezzo("6. e3 e5 "));
            Assert.IsTrue(chess.MuoviPezzo("7. Nc3 Bb4 "));
            Assert.IsTrue(chess.MuoviPezzo("8. Bd2 Bxc3 "));
            Assert.IsTrue(chess.MuoviPezzo("9. bxc3 Qd6 "));
            Assert.IsTrue(chess.MuoviPezzo("10. Qb1 O-O-O "));
            Assert.IsTrue(chess.MuoviPezzo("11. f4 exf4 "));
            Assert.IsTrue(chess.MuoviPezzo("12. Qf5+ Kb8 "));
            Assert.IsTrue(chess.MuoviPezzo("13. Qxf4 Qxf4 "));
            Assert.IsTrue(chess.MuoviPezzo("14. exf4 Nf6 "));
            Assert.IsTrue(chess.MuoviPezzo("15. f3 Nd5 "));
            Assert.IsTrue(chess.MuoviPezzo("16. Kf2 Nb6 "));
            Assert.IsTrue(chess.MuoviPezzo("17. f5 Ne7 "));
            Assert.IsTrue(chess.MuoviPezzo("18. Bd3 c5 "));
            Assert.IsTrue(chess.MuoviPezzo("19. Be4 cxd4 "));
            Assert.IsTrue(chess.MuoviPezzo("20. cxd4 Rxd4"));
            Assert.IsTrue(chess.MuoviPezzo("21. Bc3 Ra4 "));
            Assert.IsTrue(chess.MuoviPezzo("22. Bxg7 Re8 "));
            Assert.IsTrue(chess.MuoviPezzo("23. Rhd1 Nc6 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Rd6 Nc4 "));
            Assert.IsTrue(chess.MuoviPezzo("25. Rxc6 Rxe4 "));
            Assert.IsTrue(chess.MuoviPezzo("26. fxe4 bxc6"));
            Assert.IsTrue(chess.MuoviPezzo("27. Rd1 Rxa2+ "));
            Assert.IsTrue(chess.MuoviPezzo("28. Kf3 Kc7 "));
            Assert.IsTrue(chess.MuoviPezzo("29. e5 Rd2 "));
            Assert.IsTrue(chess.MuoviPezzo("30. Rc1 Nb6 "));
            Assert.IsTrue(chess.MuoviPezzo("31. Ra1 Kb7 "));
            Assert.IsTrue(chess.MuoviPezzo("32. h4 c5 "));
            Assert.IsTrue(chess.MuoviPezzo("33. e6 fxe6 "));
            Assert.IsTrue(chess.MuoviPezzo("34. fxe6 Rd8 "));
            Assert.IsTrue(chess.MuoviPezzo("35. Ke4 Re8 "));
            Assert.IsTrue(chess.MuoviPezzo("36. Ke5 a5 "));
            Assert.IsTrue(chess.MuoviPezzo("37. Rd1 Kc6 "));
            Assert.IsTrue(chess.MuoviPezzo("38. Kf6 Nd5+ "));
            Assert.IsTrue(chess.MuoviPezzo("39. Ke5 Ne3"));
            Assert.IsTrue(chess.MuoviPezzo("40. Rd6+ Kb5 "));
            Assert.IsTrue(chess.MuoviPezzo("41. Rd7 a4 "));
            Assert.IsTrue(chess.MuoviPezzo("42. e7 Kc6 "));
            Assert.IsTrue(chess.MuoviPezzo("43. Ke6 Nf5 "));
            Assert.IsTrue(chess.MuoviPezzo("44. Bf6 Nxe7 "));
            Assert.IsTrue(chess.MuoviPezzo("45. Bxe7 Ra8 "));
            Assert.IsTrue(chess.MuoviPezzo("46. Rd6+ Kb5 "));
            Assert.IsTrue(chess.MuoviPezzo("47. Rd5 Rc8 "));
            Assert.IsTrue(chess.MuoviPezzo("48. Rd3 Ra8 "));
            Assert.IsTrue(chess.MuoviPezzo("49. Rc3 c4 "));
            Assert.IsTrue(chess.MuoviPezzo("50. Kf7 Ra7 "));
            Assert.IsTrue(chess.MuoviPezzo("51. h5 Rd7 "));
            Assert.IsTrue(chess.MuoviPezzo("52. h6 Rd3"));
            Assert.IsTrue(chess.MuoviPezzo("53. Rc1 a3 "));
            Assert.IsTrue(chess.MuoviPezzo("54. Kg8 a2 "));
            Assert.IsTrue(chess.MuoviPezzo("55. Bf6 c3 "));
            Assert.IsTrue(chess.MuoviPezzo("56. Kxh7 Kc4 "));
            Assert.IsTrue(chess.MuoviPezzo("57. Kg8 Rg3+ "));
            Assert.IsTrue(chess.MuoviPezzo("58. Bg7 Kb3 "));
            Assert.IsTrue(chess.MuoviPezzo("59. h7 Kb2 "));
            Assert.IsTrue(chess.MuoviPezzo("60. Rh1 Rxg7+ "));
            Assert.IsTrue(chess.MuoviPezzo("61. Kxg7 c2 "));
            Assert.IsTrue(chess.MuoviPezzo("62. h8=Q c1=Q "));
            Assert.IsTrue(chess.MuoviPezzo("63. Kf7+ Qc3 "));
            Assert.IsTrue(chess.MuoviPezzo("64. Rh2+ Kb3 "));
            Assert.IsTrue(chess.MuoviPezzo("65. Qxc3+"));

            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Bianco).Count() == 3);
            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Nero).Count() == 2);
        }

        [TestMethod()]
        public void Test07()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. Nf3 b5 "));
            Assert.IsTrue(chess.MuoviPezzo("2. e4 a6 "));
            Assert.IsTrue(chess.MuoviPezzo("3. d4 e6 "));
            Assert.IsTrue(chess.MuoviPezzo("4. Bd3 Bb7 "));
            Assert.IsTrue(chess.MuoviPezzo("5. O-O c5 "));
            Assert.IsTrue(chess.MuoviPezzo("6. c3 Nf6 "));
            Assert.IsTrue(chess.MuoviPezzo("7. Re1 Qc7 "));
            Assert.IsTrue(chess.MuoviPezzo("8. a4 c4"));
            Assert.IsTrue(chess.MuoviPezzo("9. Bc2 d5 "));
            Assert.IsTrue(chess.MuoviPezzo("10. e5 Ne4 "));
            Assert.IsTrue(chess.MuoviPezzo("11. Nbd2 Nxd2 "));
            Assert.IsTrue(chess.MuoviPezzo("12. Bxd2 h6 "));
            Assert.IsTrue(chess.MuoviPezzo("13. b4 cxb3 "));
            Assert.IsTrue(chess.MuoviPezzo("14. Bxb3 Nd7 "));
            Assert.IsTrue(chess.MuoviPezzo("15. axb5 axb5 "));
            Assert.IsTrue(chess.MuoviPezzo("16. Qe2 Rb8 "));
            Assert.IsTrue(chess.MuoviPezzo("17. Reb1 Bc6 "));
            Assert.IsTrue(chess.MuoviPezzo("18. Bc2 Nb6 "));
            Assert.IsTrue(chess.MuoviPezzo("19. Ne1 Nc4 "));
            Assert.IsTrue(chess.MuoviPezzo("20. Bc1 Be7 "));
            Assert.IsTrue(chess.MuoviPezzo("21. Nd3 Kd7"));
            Assert.IsTrue(chess.MuoviPezzo("22. Qg4 g6 "));
            Assert.IsTrue(chess.MuoviPezzo("23. Qf3 Rh7 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Nb4 Ra8 "));
            Assert.IsTrue(chess.MuoviPezzo("25. g3 Bb7 "));
            Assert.IsTrue(chess.MuoviPezzo("26. Rxa8 Bxa8 "));
            Assert.IsTrue(chess.MuoviPezzo("27. Bd3 Bb7 "));
            Assert.IsTrue(chess.MuoviPezzo("28. h4 h5 "));
            Assert.IsTrue(chess.MuoviPezzo("29. Bg5 Bxg5 "));
            Assert.IsTrue(chess.MuoviPezzo("30. hxg5 Nd2 "));
            Assert.IsTrue(chess.MuoviPezzo("31. Qe3 Nxb1 "));
            Assert.IsTrue(chess.MuoviPezzo("32. Bxb1 Ke7 "));
            Assert.IsTrue(chess.MuoviPezzo("33. Qf3 Rg7 "));
            Assert.IsTrue(chess.MuoviPezzo("34. Nd3 Kf8"));
            Assert.IsTrue(chess.MuoviPezzo("35. Nc5 Kg8 "));
            Assert.IsTrue(chess.MuoviPezzo("36. Qf6 Bc6 "));
            Assert.IsTrue(chess.MuoviPezzo("37. Bd3 Kh7 "));
            Assert.IsTrue(chess.MuoviPezzo("38. Kg2 Rg8 "));
            Assert.IsTrue(chess.MuoviPezzo("39. Be2 Ra8 "));
            Assert.IsTrue(chess.MuoviPezzo("40. Bd3 Be8 "));
            Assert.IsTrue(chess.MuoviPezzo("41. Qf3 Kg8 "));
            Assert.IsTrue(chess.MuoviPezzo("42. Bc2 Ra3 "));
            Assert.IsTrue(chess.MuoviPezzo("43. Bb3 Qa5 "));
            Assert.IsTrue(chess.MuoviPezzo("44. Qf6 Qc7 "));
            Assert.IsTrue(chess.MuoviPezzo("45. Qf4 Qe7 "));
            Assert.IsTrue(chess.MuoviPezzo("46. Qc1 Ra8 "));
            Assert.IsTrue(chess.MuoviPezzo("47. Bc2 Rb8 "));
            Assert.IsTrue(chess.MuoviPezzo("48. Qd2 Qd8 "));
            Assert.IsTrue(chess.MuoviPezzo("49. Bd3 Qa5 "));
            Assert.IsTrue(chess.MuoviPezzo("50. Nb3 Qa3 "));
            Assert.IsTrue(chess.MuoviPezzo("51. Nc5 b4 "));
            Assert.IsTrue(chess.MuoviPezzo("52. cxb4 Qxb4 "));
            Assert.IsTrue(chess.MuoviPezzo("53. Qf4 Qb6 "));
            Assert.IsTrue(chess.MuoviPezzo("54. Qf6 Qd8"));
            Assert.IsTrue(chess.MuoviPezzo("55. Qf4 Rb4 "));
            Assert.IsTrue(chess.MuoviPezzo("56. Qe3 Bb5 "));
            Assert.IsTrue(chess.MuoviPezzo("57. Bxb5 Rxb5 "));
            Assert.IsTrue(chess.MuoviPezzo("58. Qf4 Rb4 "));
            Assert.IsTrue(chess.MuoviPezzo("59. Kh2 Rc4 "));
            Assert.IsTrue(chess.MuoviPezzo("60. Kg2 Qb6 "));
            Assert.IsTrue(chess.MuoviPezzo("61. Nd7 Qxd4 "));
            Assert.IsTrue(chess.MuoviPezzo("62. Qf6 Qe4+ "));
            Assert.IsTrue(chess.MuoviPezzo("63. Kh2 Rc8 "));
            Assert.IsTrue(chess.MuoviPezzo("64. Qe7 Qf5 "));
            Assert.IsTrue(chess.MuoviPezzo("65. Nf6+ Kg7"));

            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Bianco).Count() == 7);
            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Nero).Count() == 8);
        }

        [TestMethod()]
        public void Test08()
        {
            //[Event "UNAM Selected Simul"]
            //[Site "Mexico City MEX"]
            //[Date "2010.11.18"]
            //[Round "?"]
            //[White "Garry Kasparov"]
            //[Black "Tania Guadalupe Sanchez Guzman"]
            //[Result "1-0"]
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. c4 Nf6 "));
            Assert.IsTrue(chess.MuoviPezzo("2. Nc3 e5 "));
            Assert.IsTrue(chess.MuoviPezzo("3. g3 c6 "));
            Assert.IsTrue(chess.MuoviPezzo("4. d4 exd4 "));
            Assert.IsTrue(chess.MuoviPezzo("5. Qxd4 Be7 "));
            Assert.IsTrue(chess.MuoviPezzo("6. Bg2 d6 "));
            Assert.IsTrue(chess.MuoviPezzo("7. Bf4 Qb6 "));
            Assert.IsTrue(chess.MuoviPezzo("8. Qd2 O-O "));
            Assert.IsTrue(chess.MuoviPezzo("9. Nf3 Be6 "));
            Assert.IsTrue(chess.MuoviPezzo("10. b3 d5 "));
            Assert.IsTrue(chess.MuoviPezzo("11. cxd5 Nxd5 "));
            Assert.IsTrue(chess.MuoviPezzo("12. Nxd5 Bxd5 "));
            Assert.IsTrue(chess.MuoviPezzo("13. O-O Bf6 "));
            Assert.IsTrue(chess.MuoviPezzo("14. Be3 Qd8 "));
            Assert.IsTrue(chess.MuoviPezzo("15. Rad1 Qe7 "));
            Assert.IsTrue(chess.MuoviPezzo("16. Rfe1 Nd7 "));
            Assert.IsTrue(chess.MuoviPezzo("17. Bd4 Rfd8 "));
            Assert.IsTrue(chess.MuoviPezzo("18. e4 Be6 "));
            Assert.IsTrue(chess.MuoviPezzo("19. Qf4 Bxd4 "));
            Assert.IsTrue(chess.MuoviPezzo("20. Nxd4 Nf6 "));
            Assert.IsTrue(chess.MuoviPezzo("21. h3 h6 "));
            Assert.IsTrue(chess.MuoviPezzo("22. Qe3 Rd7 "));
            Assert.IsTrue(chess.MuoviPezzo("23. Nxe6 Qxe6 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Rxd7 Qxd7 "));
            Assert.IsTrue(chess.MuoviPezzo("25. e5 Nd5 "));
            Assert.IsTrue(chess.MuoviPezzo("26. Rd1 Nxe3 "));
            Assert.IsTrue(chess.MuoviPezzo("27. Rxd7 Nxg2"));
            Assert.IsTrue(chess.MuoviPezzo("28. Kxg2 b6 "));
            Assert.IsTrue(chess.MuoviPezzo("29. f4 Kf8 "));
            Assert.IsTrue(chess.MuoviPezzo("30. Kf3 a5 "));
            Assert.IsTrue(chess.MuoviPezzo("31. Rb7 Ra6 "));
            Assert.IsTrue(chess.MuoviPezzo("32. Ke4 a4 "));
            Assert.IsTrue(chess.MuoviPezzo("33. b4 a3 "));
            Assert.IsTrue(chess.MuoviPezzo("34. Kd3 c5"));
            Assert.IsTrue(chess.MuoviPezzo("35. bxc5 bxc5 "));
            Assert.IsTrue(chess.MuoviPezzo("36. Kc4 Ra5 "));
            Assert.IsTrue(chess.MuoviPezzo("37. h4 h5 "));
            Assert.IsTrue(chess.MuoviPezzo("38. Rb5 Ra4+ "));
            Assert.IsTrue(chess.MuoviPezzo("39. Kxc5 Ke7 "));
            Assert.IsTrue(chess.MuoviPezzo("40. Kd5 Ra8 "));
            Assert.IsTrue(chess.MuoviPezzo("41. Rb7+ Ke8 "));
            Assert.IsTrue(chess.MuoviPezzo("42. Ke4 g6 "));
            Assert.IsTrue(chess.MuoviPezzo("43. Kf3 Ra4 "));
            Assert.IsTrue(chess.MuoviPezzo("44. g4 hxg4+ "));
            Assert.IsTrue(chess.MuoviPezzo("45. Kxg4 Rd4 "));
            Assert.IsTrue(chess.MuoviPezzo("46. Rb3 Ra4 "));
            Assert.IsTrue(chess.MuoviPezzo("47. Kg5 Ra6 "));
            Assert.IsTrue(chess.MuoviPezzo("48. Rb4 Ke7 "));
            Assert.IsTrue(chess.MuoviPezzo("49. Rb7+ Ke6 "));
            Assert.IsTrue(chess.MuoviPezzo("50. Rc7 Rb6 "));
            Assert.IsTrue(chess.MuoviPezzo("51. Ra7 Rb2 "));
            Assert.IsTrue(chess.MuoviPezzo("52. Ra6+ Ke7 "));
            Assert.IsTrue(chess.MuoviPezzo("53. Rxa3 Rg2+"));
            Assert.IsTrue(chess.MuoviPezzo("54. Kh6 Rh2 "));
            Assert.IsTrue(chess.MuoviPezzo("55. Kg7 Rxh4 "));
            Assert.IsTrue(chess.MuoviPezzo("56. Ra7+ Ke6 "));
            Assert.IsTrue(chess.MuoviPezzo("57. Rxf7 Rh3 "));
            Assert.IsTrue(chess.MuoviPezzo("58. Rf6+ Ke7 "));
            Assert.IsTrue(chess.MuoviPezzo("59. Kxg6 Ra3 "));
            Assert.IsTrue(chess.MuoviPezzo("60. Kf5 Rxa2 "));
            Assert.IsTrue(chess.MuoviPezzo("61. Ke4 1-0"));

            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Bianco).Count() == 4);
            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Nero).Count() == 2);
        }

        [TestMethod()]
        public void Test09()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. e4 g6 "));
            Assert.IsTrue(chess.MuoviPezzo("2. d4 Bg7"));
            Assert.IsTrue(chess.MuoviPezzo("3. Nf3 d6 "));
            Assert.IsTrue(chess.MuoviPezzo("4. Bc4 e6 "));
            Assert.IsTrue(chess.MuoviPezzo("5. Bb3 Ne7 "));
            Assert.IsTrue(chess.MuoviPezzo("6. c3 b6 "));
            Assert.IsTrue(chess.MuoviPezzo("7. Be3 Bb7 "));
            Assert.IsTrue(chess.MuoviPezzo("8. Nbd2 Nd7 "));
            Assert.IsTrue(chess.MuoviPezzo("9. h3 h6 "));
            Assert.IsTrue(chess.MuoviPezzo("10. Qe2 a5 "));
            Assert.IsTrue(chess.MuoviPezzo("11. O-O a4 "));
            Assert.IsTrue(chess.MuoviPezzo("12. Bc2 O-O "));
            Assert.IsTrue(chess.MuoviPezzo("13. Nh2 a3 "));
            Assert.IsTrue(chess.MuoviPezzo("14. b3 c5 "));
            Assert.IsTrue(chess.MuoviPezzo("15. Rad1 cxd4 "));
            Assert.IsTrue(chess.MuoviPezzo("16. cxd4 Nc6 "));
            Assert.IsTrue(chess.MuoviPezzo("17. Ndf3 Nb4 "));
            Assert.IsTrue(chess.MuoviPezzo("18. Bb1 Ba6 "));
            Assert.IsTrue(chess.MuoviPezzo("19. Qd2 Bxf1 "));
            Assert.IsTrue(chess.MuoviPezzo("20. Rxf1 Nc6 "));
            Assert.IsTrue(chess.MuoviPezzo("21. Bxh6 Nf6 "));
            Assert.IsTrue(chess.MuoviPezzo("22. Rd1 e5 "));
            Assert.IsTrue(chess.MuoviPezzo("23. Bxg7 Kxg7 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Nf1 Qe7 "));
            Assert.IsTrue(chess.MuoviPezzo("25. d5 Nd4 "));
            Assert.IsTrue(chess.MuoviPezzo("26. Nxd4 exd4 "));
            Assert.IsTrue(chess.MuoviPezzo("27. Qxd4 Qe5"));
            Assert.IsTrue(chess.MuoviPezzo("28. Qxe5 dxe5 "));
            Assert.IsTrue(chess.MuoviPezzo("29. Ne3 b5 "));
            Assert.IsTrue(chess.MuoviPezzo("30. Bd3 Rab8 "));
            Assert.IsTrue(chess.MuoviPezzo("31. f3 Rfc8 "));
            Assert.IsTrue(chess.MuoviPezzo("32. b4 Ne8 "));
            Assert.IsTrue(chess.MuoviPezzo("33. Nc2 Rc3 "));
            Assert.IsTrue(chess.MuoviPezzo("34. Kf2 Rbc8 "));
            Assert.IsTrue(chess.MuoviPezzo("35. Ne3 Nd6 "));
            Assert.IsTrue(chess.MuoviPezzo("36. Ke2 Rf8 "));
            Assert.IsTrue(chess.MuoviPezzo("37. Kd2 Rc7 "));
            Assert.IsTrue(chess.MuoviPezzo("38. g4 Rh8 "));
            Assert.IsTrue(chess.MuoviPezzo("39. Rh1 g5 "));
            Assert.IsTrue(chess.MuoviPezzo("40. Rh2 Kf6 "));
            Assert.IsTrue(chess.MuoviPezzo("41. Rh1 Ke7 "));
            Assert.IsTrue(chess.MuoviPezzo("42. Nc2 f6 "));
            Assert.IsTrue(chess.MuoviPezzo("43. Rh2 Rcc8 "));
            Assert.IsTrue(chess.MuoviPezzo("44. Bf1 Ra8 "));
            Assert.IsTrue(chess.MuoviPezzo("45. Kc3 Rhc8+ "));
            Assert.IsTrue(chess.MuoviPezzo("46. Kb3 Rab8 "));
            Assert.IsTrue(chess.MuoviPezzo("47. h4 gxh4 "));
            Assert.IsTrue(chess.MuoviPezzo("48. Rxh4 Rh8 "));
            Assert.IsTrue(chess.MuoviPezzo("49. Rxh8 Rxh8 "));
            Assert.IsTrue(chess.MuoviPezzo("50. Nxa3 Rh1 "));
            Assert.IsTrue(chess.MuoviPezzo("51. Bxb5 1/2-1/2"));

            Assert.IsTrue(Chess.GetScacchiera().Count() == 14);
        }

        [TestMethod()]
        public void Test10()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            Assert.IsTrue(chess.MuoviPezzo("1. e4 e5 "));
            Assert.IsTrue(chess.MuoviPezzo("2. Nf3 Nf6 "));
            Assert.IsTrue(chess.MuoviPezzo("3. Nxe5 d6 "));
            Assert.IsTrue(chess.MuoviPezzo("4. Nf3 Nxe4 "));
            Assert.IsTrue(chess.MuoviPezzo("5. d4 d5 "));
            Assert.IsTrue(chess.MuoviPezzo("6. Bd3 Nc6 "));
            Assert.IsTrue(chess.MuoviPezzo("7. O-O Be7 "));
            Assert.IsTrue(chess.MuoviPezzo("8. c4 Nb4 "));
            Assert.IsTrue(chess.MuoviPezzo("9. Be2 O-O "));
            Assert.IsTrue(chess.MuoviPezzo("10. a3 Nc6 "));
            Assert.IsTrue(chess.MuoviPezzo("11. cxd5 Qxd5 "));
            Assert.IsTrue(chess.MuoviPezzo("12. Nc3 Nxc3 "));
            Assert.IsTrue(chess.MuoviPezzo("13. bxc3 Bf5 "));
            Assert.IsTrue(chess.MuoviPezzo("14. Re1 Rfe8"));
            Assert.IsTrue(chess.MuoviPezzo("15. Bf4 Rac8 "));
            Assert.IsTrue(chess.MuoviPezzo("16. h3 h6 "));
            Assert.IsTrue(chess.MuoviPezzo("17. Nd2 Qd7 "));
            Assert.IsTrue(chess.MuoviPezzo("18. Nc4 Bd6 "));
            Assert.IsTrue(chess.MuoviPezzo("19. Qd2 Bxf4 "));
            Assert.IsTrue(chess.MuoviPezzo("20. Qxf4 Re4 "));
            Assert.IsTrue(chess.MuoviPezzo("21. Qg3 Rce8 "));
            Assert.IsTrue(chess.MuoviPezzo("22. Ne3 R4e7 "));
            Assert.IsTrue(chess.MuoviPezzo("23. Bb5 a6 "));
            Assert.IsTrue(chess.MuoviPezzo("24. Bc4 Na5 "));
            Assert.IsTrue(chess.MuoviPezzo("25. Ba2 Be6 "));
            Assert.IsTrue(chess.MuoviPezzo("26. d5 Bf5 "));
            Assert.IsTrue(chess.MuoviPezzo("27. Red1 Re5 "));
            Assert.IsTrue(chess.MuoviPezzo("28. Rd4 Qd6 "));
            Assert.IsTrue(chess.MuoviPezzo("29. Kh2 Bg6 "));
            Assert.IsTrue(chess.MuoviPezzo("30. a4 Re4 "));
            Assert.IsTrue(chess.MuoviPezzo("31. Qxd6 cxd6 "));
            Assert.IsTrue(chess.MuoviPezzo("32. Rb4 Rc8 "));
            Assert.IsTrue(chess.MuoviPezzo("33. c4 Kf8 "));
            Assert.IsTrue(chess.MuoviPezzo("34. Rd1 Ke7"));
            Assert.IsTrue(chess.MuoviPezzo("35. g4 Kd7 "));
            Assert.IsTrue(chess.MuoviPezzo("36. Kg3 Rce8 "));
            Assert.IsTrue(chess.MuoviPezzo("37. Rb2 h5 "));
            Assert.IsTrue(chess.MuoviPezzo("38. Bb1 R4e5 "));
            Assert.IsTrue(chess.MuoviPezzo("39. Bxg6 fxg6 "));
            Assert.IsTrue(chess.MuoviPezzo("40. c5 dxc5 "));
            Assert.IsTrue(chess.MuoviPezzo("41. Rb6 Rxe3+ "));
            Assert.IsTrue(chess.MuoviPezzo("42. fxe3 Rxe3+ "));
            Assert.IsTrue(chess.MuoviPezzo("43. Kf4 Rxh3 "));
            Assert.IsTrue(chess.MuoviPezzo("44. Rxg6 Rh4 "));
            Assert.IsTrue(chess.MuoviPezzo("45. Rxg7+ Kd6 "));
            Assert.IsTrue(chess.MuoviPezzo("46. Rg6+ Kd7"));
            Assert.IsTrue(chess.MuoviPezzo("47. Rc1 b6 "));
            Assert.IsTrue(chess.MuoviPezzo("48. Rxb6 Rxg4+ "));
            Assert.IsTrue(chess.MuoviPezzo("49. Kf3 c4 "));
            Assert.IsTrue(chess.MuoviPezzo("50. Rh1 Rg5 "));
            Assert.IsTrue(chess.MuoviPezzo("51. Rxa6 Nb3 "));
            Assert.IsTrue(chess.MuoviPezzo("52. Kf4 Rg4+ "));
            Assert.IsTrue(chess.MuoviPezzo("53. Ke3 c3 "));
            Assert.IsTrue(chess.MuoviPezzo("54. Rc6 h4 "));
            Assert.IsTrue(chess.MuoviPezzo("55. Rb1 Rg3+ "));
            Assert.IsTrue(chess.MuoviPezzo("56. Kf4 Nd4 "));
            Assert.IsTrue(chess.MuoviPezzo("57. Rb7+ Kd8 "));
            Assert.IsTrue(chess.MuoviPezzo("58. Ra6 Kc8 "));
            Assert.IsTrue(chess.MuoviPezzo("59. Rh7 Kb8"));
            Assert.IsTrue(chess.MuoviPezzo("60. Rb6+ Ka8 "));
            Assert.IsTrue(chess.MuoviPezzo("61. Ra6+ Kb8 "));
            Assert.IsTrue(chess.MuoviPezzo("62. Rb6+ Ka8 "));
            Assert.IsTrue(chess.MuoviPezzo("63. Rf6 Rg8 "));
            Assert.IsTrue(chess.MuoviPezzo("64. Ke4 c2 "));
            Assert.IsTrue(chess.MuoviPezzo("65. Rc7 Kb8 "));
            Assert.IsTrue(chess.MuoviPezzo("66. d6 Rc8 "));
            Assert.IsTrue(chess.MuoviPezzo("67. Kxd4 c1=Q "));
            Assert.IsTrue(chess.MuoviPezzo("68. Rxc1 Rxc1 "));
            Assert.IsTrue(chess.MuoviPezzo("69. Kd5 Kc8 "));
            Assert.IsTrue(chess.MuoviPezzo("70. Rf8+ Kd7 "));
            Assert.IsTrue(chess.MuoviPezzo("71. Rf7+ Kd8 "));
            Assert.IsTrue(chess.MuoviPezzo("72. a5 Rd1+ "));
            Assert.IsTrue(chess.MuoviPezzo("73. Kc6 Rc1+ "));
            Assert.IsTrue(chess.MuoviPezzo("74. Kb7 Rb1+ "));
            Assert.IsTrue(chess.MuoviPezzo("75. Ka7 h3 "));
            Assert.IsTrue(chess.MuoviPezzo("76. Rh7 Rb3 1/2-1/2"));

            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Bianco).Count() == 4);
            Assert.IsTrue(Chess.GetScacchiera(colore: Colore.Nero).Count() == 3);
        }

        [TestMethod()]
        public void Test11()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            string partita = @"
            1.e4 e5 2.Nf3 Nc6 3.Bb5 a6 4.Ba4 Nf6 5.O-O Be7 6.Re1 b5 7.Bb3 O-O 8.a4 Bb7
9.d3 d6 10.Nc3 Na5 11.Ba2 b4 12.Ne2 Rb8 13.Ng3 c5 14.Nd2 Bc8 15.Nc4 Bg4 16.f3 Be6
17.Nxa5 Qxa5 18.Bc4 Nd7 19.b3 Nb6 20.Rb1 Rbd8 21.Qe2 d5 22.exd5 Nxd5 23.Bd2 f6
24.f4 Bd6 25.f5 Bf7 26.Qg4 Kh8 27.Re4 Qb6 28.Kh1 Qb7 29.Rbe1 Rfe8 30.Qh4 Bf8
31.Nf1 Nb6 32.Bxf7 Qxf7 33.Be3 Nd5 34.Bg1 Nc3 35.Rc4 Rd4 36.Bxd4 exd4 37.Rxc3 bxc3
38.Re4 Bd6 39.h3 Kg8 40.Nh2 Bxh2 41.Kxh2 Qc7+ 42.Kg1 Re5 43.Qf4 Qe7 44.Kf2 Rxe4
45.dxe4 g5 46.fxg6 hxg6 47.Ke2 f5 48.e5 Qb7 49.Qg5 Qe4+ 50.Kd1 Qc6 51.h4 f4
52.Qxf4 a5 53.Qg4 Kf7 54.Ke2 Qa6+ 55.Kf2 Qe6 56.Qe4 Qf5+ 57.Qxf5+ gxf5 58.h5 d3
59.h6 dxc2 60.e6+ Kxe6 61.h7 c1=Q 62.h8=Q Qd2+ 63.Kg3 Qe3+  0-1
            ";
            List<string> mosse = chess.GetStringMosse(partita);

            foreach (string mossa in mosse)
            {
                Assert.IsTrue(chess.MuoviPezzo(mossa));
            }


            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Bianco).Count() == 4);
            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Nero).Count() == 3);
        }


        [TestMethod()]
        public void Test12()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            string partita = @"
1.e4 e6 2.d4 d5 3.Nd2 Nf6 4.e5 Nfd7 5.f4 c5 6.c3 Nc6 7.Ndf3 cxd4 8.cxd4 f6
9.Bd3 Bb4+ 10.Bd2 Qb6 11.Ne2 fxe5 12.fxe5 O-O 13.a3 Be7 14.Qc2 Rxf3 15.gxf3 Nxd4
16.Nxd4 Qxd4 17.O-O-O Nxe5 18.Bxh7+ Kh8 19.Kb1 Qh4 20.Bc3 Bf6 21.f4 Nc4 22.Bxf6 Qxf6
23.Bd3 b5 24.Qe2 Bd7 25.Rhg1 Be8 26.Rde1 Bf7 27.Rg3 Rc8 28.Reg1 Nd6 29.Rxg7 Nf5
30.R7g5 Rc7 31.Bxf5 exf5 32.Rh5+  1-0

            ";
            List<string> mosse = chess.GetStringMosse(partita);

            foreach (string mossa in mosse)
            {
                Assert.IsTrue(chess.MuoviPezzo(mossa));
            }


            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Bianco).Count() == 4);
            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Nero).Count() == 3);
        }

        [TestMethod()]
        public void Test13()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            string partita = @"
1.e4 e6 2.d4 d5 3.Nd2 c5 4.exd5 Qxd5 5.Ngf3 cxd4 6.Bc4 Qd6 7.O-O Nf6 8.Nb3 Nc6
9.Nbxd4 Nxd4 10.Nxd4 a6 11.Nf3 b5 12.Bd3 Bb7 13.a4 Ng4 14.Re1 Qb6 15.Qe2 Bc5
16.Rf1 b4 17.h3 Nf6 18.Bg5 Nh5 19.Be3 Bxe3 20.Qxe3 Qxe3 21.fxe3 Ng3 22.Rfe1 Ne4
23.Ne5 Nc5 24.Bc4 Ke7 25.a5 Rhd8 26.Red1 Rac8 27.b3 Rc7 28.Rxd8 Kxd8 29.Nd3 Nxd3
30.Bxd3 Rc5 31.Ra4 Kc7 32.Kf2 g6 33.g4 Bc6 34.Rxb4 Rxa5 35.Rf4 f5 36.g5 Rd5
37.Rh4 Rd7 38.Bxa6 Rd2+ 39.Ke1 Rxc2 40.Rxh7+ Kd6 41.Bc4 Bd5 42.Rg7 Rh2 43.Rxg6 Rxh3
44.Kd2 Rg3 45.Rg8 Bxc4 46.bxc4 Kc5 47.g6 Kd6 48.c5+ Kc7 49.g7 Kb7 50.c6+  1-0
            ";
            List<string> mosse = chess.GetStringMosse(partita);

            foreach (string mossa in mosse)
            {
                Assert.IsTrue(chess.MuoviPezzo(mossa));
            }


            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Bianco).Count() == 4);
            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Nero).Count() == 3);
        }

        [TestMethod()]
        public void Test14()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            string partita = @"
1.e4 c6 2.c4 d5 3.exd5 cxd5 4.cxd5 Nf6 5.Nc3 g6 6.Bc4 Bg7 7.Nf3 O-O 8.O-O Nbd7
9.d3 Nb6 10.Qb3 Bf5 11.Re1 h6 12.a4 Nfd7 13.Be3 a5 14.Nd4 Nxc4 15.dxc4 Nc5
16.Qa3 Nd3 17.Nxf5 gxf5 18.Red1 Ne5 19.b3 Ng4 20.Qc1 f4 21.Bd4 Bxd4 22.Rxd4 e5
23.Rd2 Qh4 24.h3 Nf6 25.Qe1 Qg5 26.Ne4 Nxe4 27.Qxe4 f5 28.Qxe5 Rae8 29.h4 Qxh4
30.Qc3 Re4 31.d6 Qg5 32.f3 Re3 33.Qxa5 Rfe8 34.Rf2 Qf6 35.Rd1 R3e5 36.d7  1-0
            ";
            List<string> mosse = chess.GetStringMosse(partita);

            foreach (string mossa in mosse)
            {
                Assert.IsTrue(chess.MuoviPezzo(mossa));
            }


            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Bianco).Count() == 4);
            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Nero).Count() == 3);
        }

        [TestMethod()]
        public void Test15()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            string partita = @"
1.e4 c6 2.c4 d5 3.exd5 cxd5 4.cxd5 Nf6 5.Nc3 Nxd5 6.d4 Nc6 7.Nf3 e6 8.Bd3 Be7
9.O-O O-O 10.Re1 Bf6 11.Be4 Nce7 12.a3 Rb8 13.h4 b6 14.Qd3 g6 15.h5 Bb7 16.Bh6 Re8
17.Rac1 Nxc3 18.bxc3 Bxe4 19.Rxe4 Nf5 20.Bf4 Rc8 21.hxg6 hxg6 22.Be5 Bxe5
23.Nxe5 Qg5 24.Rd1 Kg7 25.Qf3 Rh8 26.Rg4 Qh5 27.Kf1 Nh6 28.Rf4 Qxf3 29.Rxf3 Rhd8
30.Rfd3 f6 31.Nf3 Rd5 32.Rc1 Nf5 33.Ke2 Nd6 34.Nd2 Nb5 35.Kd1 e5 36.a4 Nd6
37.Rb1 e4 38.Re3 f5 39.g3 g5 40.Rb4 Kg6 41.Kc2 Rc6 42.Re1  0-1
            ";
            List<string> mosse = chess.GetStringMosse(partita);

            foreach (string mossa in mosse)
            {
                Assert.IsTrue(chess.MuoviPezzo(mossa));
            }


            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Bianco).Count() == 4);
            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Nero).Count() == 3);
        }

        [TestMethod()]
        public void Test16()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            string partita = @"
1.e4 c5 2.Nf3 d6 3.d4 cxd4 4.Nxd4 Nf6 5.Nc3 a6 6.Bg5 e6 7.f4 Be7 8.Qf3 Qc7
9.O-O-O Nbd7 10.Bd3 b5 11.Rhe1 Bb7 12.Qg3 b4 13.Nd5 exd5 14.e5 dxe5 15.fxe5 O-O-O
16.Nf5 Bc5 17.exf6 Qxg3 18.hxg3 gxf6 19.Bf4 Ne5 20.Bxe5 fxe5 21.Rxe5 Rde8
22.Rxe8+ Rxe8 23.Rh1 Rh8 24.Rh6 a5 25.Kd2 Kb8 26.Rf6 Rf8 27.Rh6 Rh8 28.Rf6  1/2-1/2

            ";
            List<string> mosse = chess.GetStringMosse(partita);

            foreach (string mossa in mosse)
            {
                Assert.IsTrue(chess.MuoviPezzo(mossa));
            }


            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Bianco).Count() == 4);
            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Nero).Count() == 3);
        }


        [TestMethod()]
        public void Test17()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            string partita = @"
1.d4 Nf6 2.c4 e6 3.Nc3 Bb4 4.e3 b6 5.Ne2 Ba6 6.Ng3 Bxc3+ 7.bxc3 d5 8.cxd5 Bxf1
9.Kxf1 Qxd5 10.Qd3 Qd7 11.e4 Nc6 12.Bg5 h6 13.Bxf6 gxf6 14.Nh5 O-O-O 15.Nxf6 Qd6
16.e5 Nxe5 17.Qg3 Nd7 18.Qxd6 cxd6 19.Nxd7 Kxd7 20.Ke2 Rhg8 21.g3 Rc8 22.Kd3 Rg5
23.Rhb1 Rh5 24.Rh1 Rf5 25.f4 h5 26.Raf1 Ra5 27.Rf2 Ra3 28.Rc2 Ke7 29.Rb1 Ra5
30.Rb4 Rg8 31.Rb1 Kf6 32.Ke4 h4 33.Rg1 hxg3 34.hxg3 Rh5 35.g4 Rh3 36.g5+ Ke7
37.d5 Rc8 38.dxe6 fxe6 39.Kd4 b5 40.c4 Rh4 41.c5 dxc5+ 42.Rxc5 Rxf4+ 43.Ke3 Rxc5
44.Kxf4 Rc2 45.a3 Rc3 46.g6 Kf8 47.Rd1 Rxa3 48.Rd7 b4 49.Kg5 Rg3+ 50.Kf6 Rf3+
51.Kxe6 a5 52.Ra7 b3 53.Rxa5 Kg7 54.Rb5 Kxg6 55.Rb4 Rd3 56.Ke5 Kg5 57.Ke4 Rg3
58.Rb5+ Kf6  1/2-1/2
            ";
            List<string> mosse = chess.GetStringMosse(partita);

            foreach (string mossa in mosse)
            {
                Assert.IsTrue(chess.MuoviPezzo(mossa));
            }


            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Bianco).Count() == 4);
            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Nero).Count() == 3);
            chess.GetScacchieraString();
        }

        [TestMethod()]
        public void Test18()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            string partita = @"
1. e4 c5 2. Nf3 e6 3. d4 cxd4 4. Nxd4 Nf6 5. Nc3 Nc6 6. Nxc6 bxc6 7. e5 Nd5 8.
Ne4 Qc7 9. f4 Qb6 10. c4 Bb4+ 11. Ke2 f5 12. exf6 Nxf6 13. Be3 Qd8 14. Nd6+ Bxd6
15. Qxd6 Bb7 16. Rd1 Rc8 17. g4 c5 18. Rg1 Rf8 19. Bg2 Bxg2 20. Rxg2 Rf7 21. Kf1
Qb6 22. Kg1 Rc6 23. Qd3 d5 24. g5 dxc4 25. Qe2 Ng8 26. Qxc4 Ne7 27. Rgd2 Rf5 28.
Qe4 Qc7 29. b3 a6 30. Rd3 Qc8 31. Bc1 g6 32. Bb2 Rd5 33. Rxd5 exd5 34. Rxd5 Re6
35. Re5 Rxe5 36. fxe5 Qd7 37. e6 Qd1+ 38. Kg2 Qd2+ 39. Kh3 Qd5 40. Qxd5 Nxd5 41.
Kg4 Ke7 42. Ba3 Kd6 43. Kf3 Ne7 44. Ke4 Nd5 45. e7 Nxe7 46. b4 Nc6 47. bxc5+ Ke6
48. Bc1 Ne7 49. Bd2 Nc6 50. Bc3 Ne7 51. Kd4 Nf5+ 52. Ke4 Ne7 53. Be5 Nc6 54. Bf4
Nb4 55. Kd4 Nc6+ 56. Ke4 Nb4 57. Bd2 Nc6 58. Bc3 Ne7 1/2-1/2
            ";
            List<string> mosse = chess.GetStringMosse(partita);

            foreach (string mossa in mosse)
            {
                Assert.IsTrue(chess.MuoviPezzo(mossa));
            }

            var c = chess.GetScacchieraString();
        }

        [TestMethod()]
        public void Test19_partita_piu_lunga()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();
            string partita = @"
1. d4 Nf6 2. c4 g6 3. Nc3 Bg7 4. e4 d6 5. Nf3 O-O
6. Be2 Nbd7 7. O-O e5 8. Re1 Re8 9. Bf1 h6 10. d5 Nh7
11. Rb1 f5 12. Nd2 f4 13. b4 g5 14. Nb3 Bf8 15. Be2 Ndf6
16. c5 g4 17. cxd6 cxd6 18. a3 Ng5 19. Bf1 Re7
20. Qd3 Rg7 21. Kh1 Qe8 22. Nd2 g3 23. fxg3 fxg3 24. Qxg3 Nh3
25. Qf3 Qg6 26. Nc4 Bd7 27. Bd3 Ng5 28. Bxg5 Qxg5 29. Ne3 Re8 30. Ne2 Be7
31. Rbd1 Rf8 32. Nf5 Ng4 33. Neg3 h5 34. Kg1 h4 35. Qxg4 Qxg4
36. Nh6+ Kh7 37. Nxg4 hxg3 38. Ne3 gxh2+ 39. Kxh2 Rh8 40. Rh1 Kg6+
41. Kg1 Rc8 42. Be2 Rc3 43. Rd3 Rc1+ 44. Nf1 Bd8 45. Rh8 Bb6+
46. Kh2 Rh7+ 47. Rxh7 Kxh7 48. Nd2 Bg1+ 49. Kh1 Bd4+ 50. Nf1 Bg4
51. Bxg4 Rxf1+ 52. Kh2 Bg1+ 53. Kh3 Re1 54. Bf5+ Kh6 55. Kg4 Re3
56. Rd1 Bh2 57. Rh1 Rg3+ 58. Kh4 Rxg2 59. Kh3 Rg3+ 60. Kxh2 Rxa3
61. Rg1 Ra6 62. Rg6+ Kh5 63. Kg3 Rb6 64. Rg7 Rxb4 65. Bc8 a5
66. Bxb7 a4 67. Bc6 a3 68. Ra7 Rb3+ 69. Kf2 Kg5 70. Ke2 Kf4
71. Ra4 Rh3 72. Kd2 a2 73. Bb5 Rh1 74. Rxa2 Rh2+ 75. Be2 Kxe4
76. Ra5 Kd4 77. Ke1 Rh1+ 78. Kf2 Rc1 79. Bg4 Rc2+ 80. Ke1 e4
81. Be6 Ke5 82. Bg8 Rc8 83. Bf7 Rc7 84. Be6 Rc2 85. Ra8 Rb2
86. Ra6 Rg2 87. Kd1 Rb2 88. Ra5 Rg2 89. Bd7 Rh2 90. Bc6 Kf4
91. Ra8 e3 92. Re8 Kf3 93. Rf8+ Ke4 94. Rf6 Kd3 95. Bb5+ Kd4
96. Rf5 Rh1+ 97. Ke2 Rh2+ 98. Kd1 Rh1+ 99. Kc2 Rh2+ 100. Kc1 Rh1+
101. Kc2 Rh2+ 102. Kd1 Rh1+ 103. Ke2 Rh2+ 104. Kf1 Rb2 105. Be2 Ke4
106. Rh5 Rb1+ 107. Kg2 Rb2 108. Rh4+ Kxd5 109. Kf3 Kc5 110. Kxe3 Rb3+
111. Bd3 d5 112. Rh8 Ra3 113. Re8 Kd6 114. Kd4 Ra4+ 115. Kc3 Ra3+
116. Kd4 Ra4+ 117. Ke3 Ra3 118. Rh8 Ke5 119. Rh5+ Kd6 120. Rg5 Rb3
121. Kd2 Rb8 122. Bf1 Re8 123. Kd3 Re5 124. Rg8 Rh5 125. Bg2 Kc5
126. Rf8 Rh6 127. Bf3 Rd6 128. Re8 Rc6 129. Ra8 Rb6 130. Rd8 Rd6
131. Rf8 Ra6 132. Rf5 Rd6 133. Kc3 Rd8 134. Rg5 Rd6 135. Rh5 Rd8
136. Rf5 Rd6 137. Rf8 Ra6 138. Re8 Rc6 139. Ra8 Rb6 140. Ra5+ Rb5
141. Ra1 Rb8 142. Rd1 Rd8 143. Rd2 Rd7 144. Bg2 Rd8 145. Kd3 Ra8
146. Ke3 Re8+ 147. Kd3 Ra8 148. Kc3 Rd8 149. Bf3 Rd7 150. Kd3 Ra7
151. Bg2 Ra8 152. Rc2+ Kd6 153. Rc3 Ra2 154. Bf3 Ra8 155. Rb3 Ra5
156. Ke3 Ke5 157. Rd3 Rb5 158. Kd2 Rc5 159. Bg2 Ra5 160. Bf3 Rc5
161. Bd1 Rc8 162. Bb3 Rc5 163. Rh3 Kf4 164. Kd3 Ke5 165. Rh5+ Kf4
166. Kd4 Rb5 167. Bxd5 Rb4+ 168. Bc4 Ra4 169. Rh7 Kg5 170. Rf7 Kg6
171. Rf1 Kg5 172. Kc5 Ra5+ 173. Kc6 Ra4 174. Bd5 Rf4 175. Re1 Rf6+
176. Kc5 Rf5 177. Kd4 Kf6 178. Re6+ Kg5 179. Be4 Rf6 180. Re8 Kf4
181. Rh8 Rd6+ 182. Bd5 Rf6 183. Rh1 Kf5 184. Be4+ Ke6 185. Ra1 Kd6
186. Ra5 Re6 187. Bf5 Re1 188. Ra6+ Ke7 189. Be4 Rc1 190. Ke5 Rc5+
191. Bd5 Rc7 192. Rg6 Rd7 193. Rh6 Kd8 194. Be6 Rd2 195. Rh7 Ke8
196. Kf6 Kd8 197. Ke5 Rd1 198. Bd5 Ke8 199. Kd6 Kf8 200. Rf7+ Ke8
201. Rg7 Rf1 202. Rg8+ Rf8 203. Rg7 Rf6+ 204. Be6 Rf2 205. Bd5 Rf6+
206. Ke5 Rf1 207. Kd6 Rf6+ 208. Be6 Rf2 209. Ra7 Kf8 210. Rc7 Rd2+
211. Ke5 Ke8 212. Kf6 Rf2+ 213. Bf5 Rd2 214. Rc1 Rd6+ 215. Be6 Rd2
216. Rh1 Kd8 217. Rh7 Rd1 218. Rg7 Rd2 219. Rg8+ Kc7 220. Rc8+ Kb6
221. Ke5 Kb7 222. Rc3 Kb6 223. Bd5 Rh2 224. Kd6 Rh6+ 225. Be6 Rh5
226. Ra3 Ra5 227. Rg3 Rh5 228. Rg2 Ka5 229. Rg3 Kb6 230. Rg4 Rb5
231. Bd5 Rc5 232. Rg8 Rc2 233. Rb8+ Ka5 234. Bb3 Rc3 235. Kd5 Rc7
236. Kd4 Rd7+ 237. Bd5 Re7 238. Rb2 Re8 239. Rb7 Ka6 240. Rb1 Ka5
241. Bc4 Rd8+ 242. Kc3 Rh8 243. Rb5+ Ka4 244. Rb6 Rh3+ 245. Bd3 Rh5
246. Re6 Rg5 247. Rh6 Rc5+ 247. Bc4 Rg5 249. Ra6+ Ra5 250. Rh6 Rg5
251. Rh4 Ka5 252. Rh2 Rg3+ 253. Kd4 Rg5 254. Bd5 Ka4 255. Kc5 Rg3
256. Ra2+ Ra3 257. Rb2 Rg3 258. Rh2 Rc3+ 259. Bc4 Rg3 260. Rb2 Rg5+
261. Bd5 Rg3 262. Rh2 Rc3+ 263. Bc4 Rg3 264. Rh8 Ka3 265. Ra8+ Kb2
266. Ra2+ Kb1 267. Rf2 Kc1 268. Kd4 Kd1 269. Bd3 Rg7 1/2-1/2
            ";
            List<string> mosse = chess.GetStringMosse(partita);

            foreach (string mossa in mosse)
            {
                Assert.IsTrue(chess.MuoviPezzo(mossa));
            }
            Assert.AreEqual(3, Chess.GetScacchiera(colore: Colore.Bianco).Count());
            Assert.AreEqual(2, Chess.GetScacchiera(colore: Colore.Nero).Count());

            var c = chess.GetScacchieraString();
        }


    }

    [TestClass()]
    public class PartiteMultiTests
    {

        [TestMethod()]
        public void TestMultipartita()
        {
            string partite = Properties.Resources.partite;

            Chess chess = new Chess();

            //partite in errore fino a 2400: 
            List<Partita> lpart = chess.GetPartite(partite);
            lpart = lpart.Skip(2400).ToList();
            int npar = 0;
            foreach (Partita partita in lpart)
            {
                DateTime dataini = DateTime.Now;
                npar++;
                chess.NuovaPartita();

                List<string> lmosse = chess.GetStringMosse(partita.Mosse);
                int nmos = 0;
                foreach (string mossa in lmosse)
                {
                    nmos++;
                    Assert.IsTrue(chess.MuoviPezzo(mossa), $"Event {partita.Event}, Round {partita.Round}, Mossa {nmos}");
                    //chess.GetScacchieraString();
                }
                Trace.WriteLine($"partita {npar} di {lpart.Count}, mosse {lmosse.Count} ({(int)(DateTime.Now - dataini).TotalMilliseconds} millisecondi)");
            }

            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Bianco).Count() == 4);
            //Assert.IsTrue(Chess.GetScacchiera().Where(q => q.Colore == Colore.Nero).Count() == 3);
        }

        [TestMethod()]

        public void Anand()
        {
            string partite = Properties.Resources.Anand;

            Chess chess = new Chess();

            //partite in errore fino a 2400: 
            List<Partita> lpart = chess.GetPartite(partite);
            int npar = 0;
            foreach (Partita partita in lpart)
            {
                DateTime dataini = DateTime.Now;
                npar++;
                chess.NuovaPartita();

                List<string> lmosse = chess.GetStringMosse(partita.Mosse);
                int nmos = 0;
                foreach (string mossa in lmosse)
                {
                    nmos++;
                    Assert.IsTrue(chess.MuoviPezzo(mossa), $"Event {partita.Event}, Round {partita.Round}, Mossa {nmos}");
                    //chess.GetScacchieraString();
                }
                Trace.WriteLine($"partita {npar} di {lpart.Count}, mosse {lmosse.Count} ({(int)(DateTime.Now-dataini).TotalMilliseconds} millisecondi)");
            }
        }

        [TestMethod()]
        public void Kasparov()
        {
            string partite = Properties.Resources.Kasparov;

            Chess chess = new Chess();

            List<Partita> lpart = chess.GetPartite(partite).OrderByDescending(q => q.Mosse.Length).ToList();
            int npar = 0;
            foreach (Partita partita in lpart)
            {
                DateTime dataini = DateTime.Now;
                npar++;
                chess.NuovaPartita();

                List<string> lmosse = chess.GetStringMosse(partita.Mosse);
                int nmos = 0;
                foreach (string mossa in lmosse)
                {
                    nmos++;
                    Assert.IsTrue(chess.MuoviPezzo(mossa), $"Event {partita.Event}, Round {partita.Round}, Mossa {nmos}");
                    //chess.GetScacchieraString();
                }
                Trace.WriteLine($"partita {npar} di {lpart.Count}, mosse {lmosse.Count} ({(int)(DateTime.Now - dataini).TotalMilliseconds} millisecondi)");
            }
        }

    }

}