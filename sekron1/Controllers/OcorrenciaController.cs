using Newtonsoft.Json.Linq;
using sekron1.infra;
using sekron1.Services;
using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace sekron1.Controllers
{
    public class OcorrenciaController : ApiController
    {

        static readonly IOcorrencia ocorrenciaService = new OcorrenciaService();

        public IEnumerable<tb_ocorrencia> ListarTodasOcorrencias()
        {
            return ocorrenciaService.GetAll();
        }

        public HttpResponseMessage ListarOcorrenciaId(long codOcorrencia)
        {
            tb_ocorrencia ocr = ocorrenciaService.Get(codOcorrencia);
            if(ocr == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ocorrencia não encontrada");
            }
            return Request.CreateResponse(HttpStatusCode.OK, ocr);
        }

        public HttpResponseMessage DeletarOcorrencia(long codOcorrencia)
        {
            tb_ocorrencia deleteOcr = ocorrenciaService.Get(codOcorrencia);
            if(deleteOcr == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ocorrencia não encontrada");
            }else
            {
                ocorrenciaService.Remove(deleteOcr.codOcorrencia);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Cartão deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }

        public HttpResponseMessage NovaOcorrencia(JObject Ocorrencia)
        {
            var OcorrenciaAdd = Ocorrencia.ToObject<tb_ocorrencia>();

            if(OcorrenciaAdd == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ocorrencia nula ou vazia");
            }
            if(OcorrenciaAdd.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Codigo do usuario nulo ou vazio");
            }
            if(OcorrenciaAdd.latitude == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Latitude nula ou vazia");
            }
            if(OcorrenciaAdd.longitude == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Longitude nula ou vazia");
            }
           
            if(OcorrenciaAdd.data == null || OcorrenciaAdd.data == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data nula ou vazia");
            }
            if(OcorrenciaAdd.hora == null || OcorrenciaAdd.hora == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Hora nula ou vazia");
            }


            OcorrenciaAdd = ocorrenciaService.Add(OcorrenciaAdd);

            return Request.CreateResponse(HttpStatusCode.OK, OcorrenciaAdd);
        }

        public HttpResponseMessage AlterarOcorrencia (JObject Ocorrencia)
        {
            var OcorrenciaAlterar = Ocorrencia.ToObject<tb_ocorrencia>();

            if (OcorrenciaAlterar == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ocorrencia nula ou vazia");
            }
            if (OcorrenciaAlterar.codTipoOcorrencia == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Codigo tipo ocorrencia nulo ou vazio");
            }
            if (OcorrenciaAlterar.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Codigo do usuario nulo ou vazio");
            }
            if (OcorrenciaAlterar.latitude == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Latitude nula ou vazia");
            }
            if (OcorrenciaAlterar.longitude == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Longitude nula ou vazia");
            }
            if (OcorrenciaAlterar.data == null || OcorrenciaAlterar.data == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data nula ou vazia");
            }
            if (OcorrenciaAlterar.hora == null || OcorrenciaAlterar.hora == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Hora nula ou vazia");
            }

            string resultado = ocorrenciaService.Update(OcorrenciaAlterar);

            if(resultado == "Ocorrencia alterada com sucesso")
            {
                return Request.CreateResponse(HttpStatusCode.OK, OcorrenciaAlterar);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }
        }
    }
}
