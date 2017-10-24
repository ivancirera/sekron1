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
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace sekron1.Controllers
{
    public class TipoOcorrenciaController : ApiController
    {

        static readonly ITipoOcorrencia tipoOcorrenciaService = new TipoOcorrenciaService();

        public IEnumerable<tb_tipoocorrencia> TodosTiposOcorrencias()
        {
            return tipoOcorrenciaService.GetAll();
        }

        public HttpResponseMessage ListarTipoOcorrenciaId(long codTipoOcorrencia)
        {
            tb_tipoocorrencia TipoOcr = tipoOcorrenciaService.Get(codTipoOcorrencia);
            if(TipoOcr == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo correncia não encontrado");
            }
            return Request.CreateResponse(HttpStatusCode.OK, TipoOcr);
        }

        public HttpResponseMessage NovoTipoOcorrencia(JObject ocorrencia)
        {
            var tipoOcr = ocorrencia.ToObject<tb_tipoocorrencia>();

            if(tipoOcr.descricao == null || tipoOcr.descricao== "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Descrição nula ou vazia");
            }

            tipoOcr = tipoOcorrenciaService.Add(tipoOcr);

            return Request.CreateResponse(HttpStatusCode.OK, tipoOcr);
        }

        public HttpResponseMessage AlterarTipoOcorrencia(JObject ocorrencia)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var TipoOcr = ocorrencia.ToObject<tb_tipoocorrencia>();

            if(TipoOcr == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo ocorrencia nula");
            }

            if(TipoOcr.descricao == null || TipoOcr.descricao == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Descrição da ocorrencia nula ou vazia");
            }

            string resultado = tipoOcorrenciaService.Update(TipoOcr);

            if(resultado == "Dados do tipo da ocorrencia alterados com sucesso")
            {
                tb_tipoocorrencia updatedTipoOcorrencia = tipoOcorrenciaService.Get(TipoOcr.codTipoOcorrencia);
                return Request.CreateResponse(HttpStatusCode.OK, updatedTipoOcorrencia);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }
        }

        public HttpResponseMessage DeletarTipoOcorrencia(long codTipoOcorrencia)
        {
            tb_tipoocorrencia TipoOcrDelete = tipoOcorrenciaService.Get(codTipoOcorrencia);

            if(TipoOcrDelete == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo ocorrencia não encontrado");
            }else
            {
                tipoOcorrenciaService.Remove(TipoOcrDelete.codTipoOcorrencia);
                return Request.CreateResponse(HttpStatusCode.OK, TipoOcrDelete);
            }
        }
    }
}