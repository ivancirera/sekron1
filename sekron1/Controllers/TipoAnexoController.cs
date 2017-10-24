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
    public class TipoAnexoController : ApiController
    {

        static readonly ITipoAnexo tipoAnexoService = new TipoAnexoService();

        public IEnumerable<tb_tipoanexo> TodosTiposAnexo()
        {
            return tipoAnexoService.GetAll();
        }

        public HttpResponseMessage ListarTipoAnexoId(long codTipoAnexo)
        {
            tb_tipoanexo resultAnexos = tipoAnexoService.Get(codTipoAnexo);

            if(resultAnexos == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo anexo não encontrado");
            }else
            {
                return Request.CreateResponse(HttpStatusCode.OK, resultAnexos);
            }
        }

        public HttpResponseMessage DeletarTipoAnexo(long codTipoAnexo)
        {
            tb_tipoanexo deleteTipoAnx = tipoAnexoService.Get(codTipoAnexo);
            if(deleteTipoAnx == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo anexo não encontrado");
            }else
            {
                tipoAnexoService.Remove(deleteTipoAnx.codTipoAnexo);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Tipo anexo deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }

        public HttpResponseMessage NovoTipoAnexo(JObject tipoAnexo)
        {
            var novoTipoAnexo = tipoAnexo.ToObject<tb_tipoanexo>();

            if(novoTipoAnexo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo anexo nulo ou vazio");
            }
            if (novoTipoAnexo.descricao == null || novoTipoAnexo.descricao == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Descrição nula ou vazia");
            }

            novoTipoAnexo = tipoAnexoService.Add(novoTipoAnexo);

            return Request.CreateResponse(HttpStatusCode.OK, novoTipoAnexo);
        }

        public HttpResponseMessage AlterarTipoAnexo(JObject tipoAnexo)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var altTipoAnexo = tipoAnexo.ToObject<tb_tipoanexo>();

            if(altTipoAnexo.descricao == null || altTipoAnexo.descricao == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Descrição nula ou vazia");
            }

            string resultado = tipoAnexoService.Update(altTipoAnexo);
            if(resultado == "Tipo Anexo alterado com sucesso")
            {
                return Request.CreateResponse(HttpStatusCode.OK, altTipoAnexo);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }
        }   
    }
}
