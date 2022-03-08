using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Utils
    {
    }

    public class MetodiWebservice
    {
        static IConfiguration conf = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());
        public static string urlws = conf["UrlWebservice"].ToString();


        public static string GetScacchiera()
        {
            string output = "";
            try
            {
                string url = urlws + "/chess/scacchiera";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.ContentType = "application/json";
                webRequest.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var streamReader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                    output = streamReader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {

            }

            return output;
        }

        public static string Mossa(Pos da, Pos a)
        {
            string output = "";
            try
            {
                Colore? colore = Chess.GetScacchiera(posizione: da).FirstOrDefault()?.Colore;
                if (colore == null)
                    throw new Exception("Non trovato pezzo in posizione iniziale");
                string url = urlws + "/chess/mossa";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.ContentType = "application/json";
                webRequest.Method = "POST";
                string body = $"{{" +
                    $"\"mossaDa\": {{ \"Riga\": {da.Riga}, \"Colonna\": {da.Colonna} }}," +
                    $"\"mossaA\": {{ \"Riga\": {a.Riga}, \"Colonna\": {a.Colonna} }}," +
                    $"\"colore\": {(int)colore}" +
                    $"}}";
                var bodyb = Encoding.UTF8.GetBytes(body);
                webRequest.ContentLength = bodyb.Length;
                Stream requestStream = webRequest.GetRequestStream();
                requestStream.Write(bodyb, 0, bodyb.Length);
                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var streamReader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                    output = streamReader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {

            }
            return output;
        }

        public static string Turno(Colore colore)
        {
            string output = "";
            try
            {
                string url = urlws + "/chess/turno";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.ContentType = "application/json";
                webRequest.Method = "POST";
                string body = $"{{\"colore\":{(int)colore}}}";
                var bodyb = Encoding.UTF8.GetBytes(body);
                webRequest.ContentLength = bodyb.Length;
                Stream requestStream = webRequest.GetRequestStream();
                requestStream.Write(bodyb, 0, bodyb.Length);
                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var streamReader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                    output = streamReader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {

            }

            return output;
        }

    }

}
