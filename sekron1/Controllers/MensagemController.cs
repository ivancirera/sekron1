using sekron1.Services;
using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Mvc;

namespace sekron1.Controllers
{
    public class MensagemController : Controller
    {

        static readonly INotification notificationService = new NotificationService();

        public ActionResult index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(MensagemModel mensagem)
        {
            if (ModelState.IsValid)
            {

                var tipoMensagem = new SelectList(new[] { "Amarela", "Laranja", "Vermelha" });

                ViewBag.TipoMensagem = tipoMensagem;

                string apiKey = "AAAAS1eUoyg: APA91bGUWaYIq2j6cT9jHW1STepymjlGglr_EfWjuFRW6GvNBQEWoj3ufIsirdvnXf1SmEgnlBxtRXx5WpeQj7LGKs6cpBQWCmWw5 - WwO7vprfKZI_PHxcTtcbscEqRGuBidkGxUznfZ";
                
                string message = mensagem.message;
                string tickerText = mensagem.tickerText;
                string contentTitle = mensagem.contentTitle;
                string tipoMsg = mensagem.tipoMensagem;

                List<string> listaIds = new List<string>();

                if (message == "" || message == null)
                {
                    ViewBag.Error = "A mensagem está vazia";
                    RedirectToAction("Index");
                }
                else if (tickerText == "" || tickerText == null)
                {
                    ViewBag.Error = "Titulo da barra de notificação está vazio";
                    RedirectToAction("Index");
                }
                else if (contentTitle == "" || contentTitle == null)
                {
                    ViewBag.Error = "Título da mensagem está vazio";
                    RedirectToAction("Index");
                }
                else
                {

                    if (tipoMsg == "Selecione")
                    {
                        ViewBag.Error = "Selecione um tipo de notificação antes de enviar";
                        RedirectToAction("Index");
                    }

                    if (tipoMsg == "Todos")
                    {
                        listaIds = notificationService.getUidsFirebaseAll();
                        SendGCMNotification(apiKey, listaIds, message, tickerText, contentTitle, tipoMsg);
                    }

                    if (tipoMsg == "Amarela")
                    {
                        listaIds = notificationService.getUidsFirebaseYellow();
                        SendGCMNotification(apiKey, listaIds, message, tickerText, contentTitle, tipoMsg);
                    }

                    if (tipoMsg == "Laranja")
                    {
                        listaIds = notificationService.getUidsFirebaseOrange();
                        SendGCMNotification(apiKey, listaIds, message, tickerText, contentTitle, tipoMsg);
                    }

                    if (tipoMsg == "Vermelha")
                    {
                        listaIds = notificationService.getUidsFirebaseRed();
                        SendGCMNotification(apiKey, listaIds, message, tickerText, contentTitle, tipoMsg);
                    }
                }
            }

            return View();
        }

        public string SendGCMNotification(string apiKey, List<string> deviceId, string message, string tickerText, string contentTitle, string tipoMensagem)
        {
            string postDataContentType = "application/json";
            apiKey = "AAAAS1eUoyg:APA91bGUWaYIq2j6cT9jHW1STepymjlGglr_EfWjuFRW6GvNBQEWoj3ufIsirdvnXf1SmEgnlBxtRXx5WpeQj7LGKs6cpBQWCmWw5-WwO7vprfKZI_PHxcTtcbscEqRGuBidkGxUznfZ"; // hardcorded
            //deviceId = "eG3S8ihtu9k:APA91bEX8FNjQHJp80I0vfw_9DZOZ1XRtCOEAA5q7rd-HKF_Jh_sYXpbL2R3lIHgbTKnoMUBjPl13w23_Ue4P253Udi-1ivtlxL3-dCan99Jrtc-LyGiXi7kGtsxk8JckVl3cXHE04mp"; // hardcorded

            List<String> deviceIdAux = new List<String>();

            for (int i = 0; i < deviceId.Count; i++)
            {
                if (deviceId[i] != "")
                {
                    deviceIdAux.Add(deviceId[i]);
                }
            }

            string message1 = message;
            string tickerText1 = tickerText;
            string contentTitle1 = contentTitle;
            string tipoMensagem1 = tipoMensagem;
            string destinatario = String.Join<string>("\", \"", deviceIdAux);
            string postData =
            "{ \"registration_ids\": [ \"" + destinatario + "\" ], " +
              "\"data\": {\"tickerText\":\"" + tickerText + "\", " +
                         "\"contentTitle\":\"" + contentTitle + "\", " +
                         "\"messageType\":\"" + tipoMensagem1 + "\", " +
                         "\"message\": \"" + message + "\"}}";

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //
            //  MESSAGE CONTENT
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //
            //  CREATE REQUEST
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;
            Request.ContentType = postDataContentType;
            Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
            Request.ContentLength = byteArray.Length;

            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //
            //  SEND MESSAGE
            try
            {
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    var text = "Unauthorized - need new token";
                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    var text = "Response from web service isn't OK";
                }

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadToEnd();
                Reader.Close();

                return responseLine;
            }
            catch (Exception e)
            {
            }
            return "error";
        }

        public static bool ValidateServerCertificate(
        object sender,
        X509Certificate certificate,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}

