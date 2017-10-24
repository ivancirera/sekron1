using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace sekron1.Controllers
{
    public class DownloadController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage DownloadPhoto(String arquivo)
        {
            Byte[] b = System.IO.File.ReadAllBytes(arquivo);   // You can use your own method over here.         
            return Request.CreateResponse(HttpStatusCode.OK, b);
        }
    }
}
