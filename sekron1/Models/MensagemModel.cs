using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sekron1.Controllers
{
    public class MensagemModel
    {
        public string message { get; set; }
        public string tickerText { get; set; }
        public string contentTitle { get; set; }
        public string tipoMensagem { get; set; }

    }
}