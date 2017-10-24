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
    public class AnexoController : ApiController
    {

        static readonly IAnexo anexoService = new AnexoService();

        public IEnumerable<tb_anexo> TodosAnexos()
        {
            return anexoService.GetAll();
        }

        public HttpResponseMessage ListarAnexoId(long codAnexo)
        {
            tb_anexo result = anexoService.Get(codAnexo);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Anexo não encontrado");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);            
        }
        public HttpResponseMessage DeletarAnexo(long codAnexo)
        {
            tb_anexo result = anexoService.Get(codAnexo);

            if(result == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Anexo não encontrado");
            }
            else{
                anexoService.Remove(result.codAnexo);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Anexo deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }
        public HttpResponseMessage NovoAnexo(JObject anexo)
        {
            var AnexoAdd = anexo.ToObject<tb_anexo>();

            if(AnexoAdd == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Anexo nulo ou vazio");
            }
            if(AnexoAdd.codOcorrencia == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodOcorrencia nulo ou vazio");
            }
            if(AnexoAdd.codTipoAnexo == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodTipoAnexo nulo ou vazio");
            }
            if(AnexoAdd.anexo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Anexo nulo ou vazio");
            }
            if(AnexoAdd.tipoAnexo == null || AnexoAdd.tipoAnexo == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo anexo nulo ou vazio");
            }
            if(AnexoAdd.data == null || AnexoAdd.data == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data nula ou vazia");
            }
            if(AnexoAdd.hora == null || AnexoAdd.hora == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Hora nula ou vazia");
            }

            AnexoAdd = anexoService.Add(AnexoAdd);

            return Request.CreateResponse(HttpStatusCode.OK, AnexoAdd);
        }

        public HttpResponseMessage AlterarAnexo(JObject anexo)
        {
            var AnexoUpdate = anexo.ToObject<tb_anexo>();

            if (AnexoUpdate == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Anexo nulo ou vazio");
            }
            if (AnexoUpdate.codOcorrencia == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodOcorrencia nulo ou vazio");
            }
            if (AnexoUpdate.codTipoAnexo == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodTipoOcorrencia nulo ou vazio");
            }
            if (AnexoUpdate.anexo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Anexo nulo ou vazio");
            }
            if (AnexoUpdate.tipoAnexo == null || AnexoUpdate.tipoAnexo == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo anexo nulo ou vazio");
            }
            if (AnexoUpdate.data == null || AnexoUpdate.data == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data nula ou vazia");
            }
            if (AnexoUpdate.hora == null || AnexoUpdate.hora == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Hora nula ou vazia");
            }

            string resultado = anexoService.Update(AnexoUpdate);

            if(resultado == "Anexo alterado com sucesso")
            {
                return Request.CreateResponse(HttpStatusCode.OK, AnexoUpdate);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }
        }
    }
}
