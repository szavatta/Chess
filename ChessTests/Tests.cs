using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            var torre = new Torre(0, 0, Colore.Nero, true);
            bool ret = torre.Mossa(new Pos { Riga = 0, Colonna = 5 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var torre = new Torre(0, 0, Colore.Nero, true);
            bool ret = torre.Mossa(new Pos { Riga = 1, Colonna = 5 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_non_oltrepassa_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var torre = new Torre(0, 0, Colore.Nero, true);
            var cavallo = new Torre(0, 4, Colore.Nero, true);
            bool ret = torre.Mossa(new Pos { Riga = 0, Colonna = 5 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_mangia()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var torre = new Torre(0, 0, Colore.Nero, true);
            var cavallo = new Torre(0, 4, Colore.Bianco, true);
            bool ret = torre.Mossa(new Pos { Riga = 0, Colonna = 4 });
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
            bool ret = alfiere.Mossa(new Pos { Riga = 7, Colonna = 7 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var alfiere = new Alfiere(5, 5, Colore.Nero, true);
            bool ret = alfiere.Mossa(new Pos { Riga = 7, Colonna = 6 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_non_oltrepassa_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var alfiere = new Alfiere(2, 2, Colore.Nero, true);
            var torre = new Torre(5, 5, Colore.Nero, true);
            bool ret = alfiere.Mossa(new Pos { Riga = 7, Colonna = 7 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_con_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var alfiere = new Alfiere(5, 5, Colore.Nero, true);
            var torre = new Torre(6, 6, Colore.Nero, true);
            bool ret = alfiere.Mossa(new Pos { Riga = 6, Colonna = 6 });
            Assert.IsFalse(ret);
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
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 6 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var cavallo = new Cavallo(5, 5, Colore.Nero, true);
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 7 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_oltrepassa_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var cavallo = new Cavallo(5, 5, Colore.Nero, true);
            var torre = new Torre(6, 5, Colore.Nero, true);
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 6 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test_mangia()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var cavallo = new Cavallo(5, 5, Colore.Nero, true);
            var cavallo2 = new Cavallo(7, 6, Colore.Bianco, true);
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 6 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test_con_pezzo_stesso_colore()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var cavallo = new Cavallo(5, 5, Colore.Nero, true);
            var cavallo2 = new Cavallo(7, 6, Colore.Nero, true);
            bool ret = cavallo.Mossa(new Pos { Riga = 7, Colonna = 6 });
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
            bool ret = re.Mossa(new Pos { Riga = 6, Colonna = 6 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(5, 5, Colore.Nero, true);
            bool ret = re.Mossa(new Pos { Riga = 6, Colonna = 7 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_con_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(5, 5, Colore.Nero, true);
            var torre = new Torre(6, 6, Colore.Nero, true);
            bool ret = re.Mossa(new Pos { Riga = 6, Colonna = 6 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_Arrocco1()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(0, 3, Colore.Nero, true);
            var torre = new Torre(0, 7, Colore.Nero, true);
            bool ret = re.Mossa(new Pos(0, 5));
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test_Arrocco2()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(0, 3, Colore.Nero, true);
            var torre = new Torre(0, 0, Colore.Nero, true);
            bool ret = re.Mossa(new Pos(0, 1));
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test_Arrocco3()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(0, 3, Colore.Nero, true);
            new Torre(0, 0, Colore.Nero, true);
            new Torre(0, 7, Colore.Nero, true);
            new Torre(5, 4, Colore.Bianco, true);
            Assert.IsFalse(re.Mossa(new Pos(0, 5)));
            Assert.IsTrue(re.Mossa(new Pos(0, 1)));
            Assert.AreEqual(Tipo.Torre, Chess.GetScacchiera().Where(q => q.Posizione.Equals(new Pos(0, 2))).FirstOrDefault()?.Tipo);
        }

        [TestMethod()]
        public void Test_Arrocco_non_valido()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(0, 3, Colore.Nero, true);
            new Torre(0, 0, Colore.Nero, true);
            new Cavallo(0, 1, Colore.Nero, true);
            Assert.IsFalse(re.Mossa(new Pos(0, 1)));
        }

        [TestMethod()]
        public void Test_Re_con_Re()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(0, 3, Colore.Nero, true);
            var re2 = new Re(0, 5, Colore.Bianco, true);
            bool ret = re.Mossa(new Pos { Riga = 0, Colonna = 4 });
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
            bool ret = regina.Mossa(new Pos { Riga = 5, Colonna = 6 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_corretta_verticale()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var regina = new Regina(2, 2, Colore.Nero, true);
            bool ret = regina.Mossa(new Pos { Riga = 2, Colonna = 6 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_sbagliata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var regina = new Regina(5, 5, Colore.Nero, true);
            bool ret = regina.Mossa(new Pos { Riga = 6, Colonna = 7 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test_con_un_pezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var regina = new Regina(5, 5, Colore.Nero, true);
            var torre = new Torre(6, 6, Colore.Nero, true);
            bool ret = regina.Mossa(new Pos { Riga = 6, Colonna = 6 });
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
            var pedone = new Pedone(1, 2, Colore.Nero, true);
            bool ret = pedone.Mossa(new Pos { Riga = 2, Colonna = 2 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_errata_1passo_nero()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(1, 2, Colore.Nero, true);
            bool ret = pedone.Mossa(new Pos { Riga = 0, Colonna = 2 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Mossa_corretta_1passo_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(6, 2, Colore.Bianco, true);
            bool ret = pedone.Mossa(new Pos { Riga = 5, Colonna = 2 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_errata_1passo_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(6, 2, Colore.Bianco, true);
            bool ret = pedone.Mossa(new Pos { Riga = 7, Colonna = 2 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Mossa_corretta_2passi_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(6, 2, Colore.Bianco, true);
            bool ret = pedone.Mossa(new Pos { Riga = 4, Colonna = 2 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_errata_2passi_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(6, 2, Colore.Bianco, true);
            pedone.NumMosse++;
            bool ret = pedone.Mossa(new Pos { Riga = 4, Colonna = 2 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Mossa_errata_2passi_con_pezzo_in_mezzo()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(6, 2, Colore.Bianco, true);
            var torre = new Torre(5, 2, Colore.Nero, true);
            bool ret = pedone.Mossa(new Pos (4, 2));
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Mossa_con_mangiata()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedone = new Pedone(6, 2, Colore.Bianco, true);
            var torre = new Torre(5, 3, Colore.Nero, true);
            bool ret = pedone.Mossa(new Pos { Riga = 5, Colonna = 3 });
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Mossa_en_passant_bianco()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedoneb = new Pedone(6, 2, Colore.Bianco, true);
            var pedonen = new Pedone(3, 3, Colore.Nero, true);
            bool ret1 = pedoneb.Mossa(new Pos { Riga = 4, Colonna = 2 });
            bool ret2 = pedonen.Mossa(new Pos { Riga = 4, Colonna = 3 });
            bool ret3 = pedoneb.Mossa(new Pos { Riga = 3, Colonna = 3 });
            Assert.IsTrue(ret1 && ret2 && ret3);
        }

        [TestMethod()]
        public void Mossa_en_passant_nero()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var pedonen = new Pedone(1, 3, Colore.Nero, true);
            var pedoneb = new Pedone(4, 2, Colore.Bianco, true);
            bool ret1 = pedonen.Mossa(new Pos { Riga = 3, Colonna = 3 });
            bool ret2 = pedoneb.Mossa(new Pos { Riga = 3, Colonna = 2 });
            bool ret3 = pedonen.Mossa(new Pos { Riga = 4, Colonna = 2 });
            Assert.IsTrue(ret1 && ret2 && ret3);
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
            var re = new Re(0, 0, Colore.Nero, true);
            var torre = new Torre(0, 2, Colore.Nero, true);
            var torre2 = new Torre(0, 4, Colore.Bianco, true);
            bool ret = re.IsSottoScacco();
            Assert.IsFalse(ret);
            Assert.AreEqual(3, Chess.GetScacchiera().Count, "Non ci sono 3 pezzi");
        }

        [TestMethod()]
        public void Test2()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(0, 0, Colore.Nero, true);
            var torre = new Torre(1, 2, Colore.Nero, true);
            var regina = new Regina(0, 4, Colore.Bianco, true);
            bool ret = re.IsSottoScacco();
            Assert.IsTrue(ret);
        }

        [TestMethod()]
        public void Test3()
        {
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(0, 0, Colore.Nero, true);
            var torre = new Torre(0, 2, Colore.Nero, true);
            var regina = new Regina(0, 4, Colore.Bianco, true);
            bool ret = torre.Mossa(new Pos { Riga = 1, Colonna = 2 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test4()
        {
            //Il pedone muove e mette sotto scacco il re
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(1, 0, Colore.Nero, true);
            var pedone = new Pedone(1, 2, Colore.Nero, true);
            var regina = new Regina(1, 4, Colore.Bianco, true);
            bool ret = pedone.Mossa(new Pos { Riga = 2, Colonna = 2 });
            Assert.IsFalse(ret);
        }

        [TestMethod()]
        public void Test5()
        {
            //La torre muove e mette sotto scacco il re
            Chess chess = new Chess();
            chess.NuovaScacchiera();
            var re = new Re(1, 0, Colore.Nero, true);
            var torre = new Torre(1, 2, Colore.Nero, true);
            var regina = new Regina(1, 4, Colore.Bianco, true);
            bool ret = torre.Mossa(new Pos { Riga = 2, Colonna = 2 });
            Assert.IsFalse(ret);
        }

    }

    [TestClass()]
    public class GenericTests
    {
        [TestMethod()]
        public void Mosse_varie()
        {
            Chess chess = new Chess();
            chess.NuovaPartita();

            Assert.IsTrue(chess.MuoviPezzo(new Pos(0, 1), new Pos(2, 0)), "1");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(1, 1), new Pos(3, 1)), "2");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(6, 0), new Pos(4, 0)), "3");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(0, 6), new Pos(2, 7)), "4");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(4, 0), new Pos(3, 0)), "5");
            Assert.IsTrue(chess.MuoviPezzo(new Pos(3, 1), new Pos(4, 0)), "2");
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

    }

}