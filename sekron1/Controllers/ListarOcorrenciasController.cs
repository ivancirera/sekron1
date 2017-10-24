using sekron1.Models;
using sekron1.Services;
using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sekron1.Controllers
{
    public class ListarOcorrenciasController : ApiController
    {

        static readonly IOcorrenciaMaps ocorrenciaMapsService = new OcorrenciaMapsService();

        public HttpResponseMessage listarOcorrencias(string data)
        {
            List<OcorrenciaMapsModel> result = ocorrenciaMapsService.listOcorrencias(data);

            if(result == null)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Não foram encontradas ocorrencias");
                
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
