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
    public class SubOcorrenciaController : ApiController
    {

        static readonly ISubOcorrencia subOcorrenciaService = new SubOcorrenciaService();

        public IEnumerable<tb_subocorrencia> TodasSubOcorrencias()
        {
            return subOcorrenciaService.GetAll();
        }

        public HttpResponseMessage ListarSubOcorrenciaId(long codSubOcorrencia)
        {
            tb_subocorrencia resultadoSubOcorrencia = subOcorrenciaService.Get(codSubOcorrencia);

            if(resultadoSubOcorrencia == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sub-Ocorrencia não encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, resultadoSubOcorrencia);
        } 

        public HttpResponseMessage DeletarSubOcorrencia(long codSubOcorrencia)
        {
            tb_subocorrencia subOcrDelete = subOcorrenciaService.Get(codSubOcorrencia);

            if(subOcrDelete == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sub-Ocorrencia não encontrada");
            }else
            {
                subOcorrenciaService.Remove(subOcrDelete.codSubocorrencia);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Sub-Ocorrencia deletada com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }

        public HttpResponseMessage NovaSubOcorrencia(JObject subOcorrencia)
        {
            var subOcorrenciaAdd = subOcorrencia.ToObject<tb_subocorrencia>();

            if(subOcorrenciaAdd == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sub-Ocorrencia nula");
            }

            if(subOcorrenciaAdd.codTipoocorrencia == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Código do tipo da ocorrencia nulo ou vazio");
            }

            if (subOcorrenciaAdd.descricao == null || subOcorrenciaAdd.descricao == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Descrição da ocorrencia nula ou vazia");
            }

            subOcorrenciaAdd = subOcorrenciaService.Add(subOcorrenciaAdd);

            return Request.CreateResponse(HttpStatusCode.OK, subOcorrenciaAdd);
        }

        public HttpResponseMessage AlterarSubOcorrencia(JObject subOcorrencia)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var subOcorrenciaMudar = subOcorrencia.ToObject<tb_subocorrencia>();

            if(subOcorrenciaMudar.codTipoocorrencia == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Codigo do tipo da ocorrencia nulo, vazio ou zero");
            }

            if(subOcorrenciaMudar.descricao == null || subOcorrenciaMudar.descricao == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Descrição da subOcorrencia nula ou vazia");
            }

            string resultado = subOcorrenciaService.Update(subOcorrenciaMudar);

            if(resultado == "Sub-Ocorrencia alterada com sucesso")
            {
                return Request.CreateResponse(HttpStatusCode.OK, subOcorrenciaMudar);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }
        }
    }
}
