using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chess
{
    public class Utils
    {
        public static Dictionary<string, byte[]> UnZip(byte[] file)
        {
            MemoryStream ms = new MemoryStream(file);
            ZipInputStream zipInputStream = new ZipInputStream(ms);
            Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
            ZipEntry zipEntry = zipInputStream.GetNextEntry();
            //Ciclo sui file contenuti nello zip e li aggiungo al buffer
            while (zipEntry != null)
            {
                byte[] buffer = new byte[4096];
                string fileName = zipEntry.Name;
                //Scompatto il file nel buffer.
                //Lo Using chiuderà tutti gli stream aperti anche nel caso di eccezioni
                using (MemoryStream msOut = new MemoryStream())
                {
                    StreamUtils.Copy(zipInputStream, msOut, buffer);
                    //Aggiungo il file solo se non ne esiste già uno con lo stesso nome
                    if (!files.ContainsKey(fileName))
                    {
                        files.Add(fileName, msOut.ToArray());
                    }
                }
                zipEntry = zipInputStream.GetNextEntry();
            }

            return files;
        }

        public static Dictionary<string, byte[]> UnZipFile(byte[] file)
        {
            MemoryStream ms = new MemoryStream(file);
            ZipInputStream zipInputStream = new ZipInputStream(ms);
            Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
            ZipEntry zipEntry = zipInputStream.GetNextEntry();
            //Ciclo sui file contenuti nello zip e li aggiungo al buffer
            while (zipEntry != null)
            {
                byte[] buffer = new byte[4096];
                string fileName = zipEntry.Name;
                //Scompatto il file nel buffer.
                //Lo Using chiuderà tutti gli stream aperti anche nel caso di eccezioni
                using (MemoryStream msOut = new MemoryStream())
                {
                    StreamUtils.Copy(zipInputStream, msOut, buffer);
                    //Aggiungo il file solo se non ne esiste già uno con lo stesso nome
                    if (!files.ContainsKey(fileName))
                    {
                        files.Add(fileName, msOut.ToArray());
                    }
                }
                zipEntry = zipInputStream.GetNextEntry();
            }

            return files;
        }
    }
}
