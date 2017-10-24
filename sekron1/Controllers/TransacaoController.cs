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
    public class TransacaoController : ApiController
    {

        static readonly ITransacao transacaoService = new TransacaoService();

        public IEnumerable<tb_transacao> TodasTransacoes()
        {
            return transacaoService.GetAll();
        }

        public HttpResponseMessage ListarTransacaoId(long id)
        {
            tb_transacao trans = transacaoService.Get(id);

            if (trans == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Transação não encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, trans);
        }

        public HttpResponseMessage DeletarTransacao(long id)
        {
            tb_transacao trans = transacaoService.Get(id);

            if(trans == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Transação não encontrada");
            }else
            {
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }

        public HttpResponseMessage NovaTransacao(JObject transacao)
        {

            var trans = transacao.ToObject<tb_transacao>();

            if (trans == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Transação nula");
            }
            if(trans.codCartao == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodCartao Nulo ou zero");
            }
            if(trans.codServico == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodServico nulo ou zero");
            }
            if(trans.dataTransacao == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "DataTransacao nula");
            }
            if(trans.status == null || trans.status == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Status nulo ou vazio");
            }

            trans = transacaoService.Add(trans);

            return Request.CreateResponse(HttpStatusCode.OK, trans);

        }

        public HttpResponseMessage AtualizarTransacao(JObject transacao)
        {

            var trans = transacao.ToObject<tb_transacao>();

            if (trans == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Transação nula");
            }
            if (trans.codCartao == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodCartao Nulo ou zero");
            }
            if (trans.codServico == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodServico nulo ou zero");
            }
            if (trans.dataTransacao == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "DataTransacao nula");
            }
            if (trans.status == null || trans.status == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Status nulo ou vazio");
            }

            string retorno = transacaoService.Update(trans);

            if(retorno == "Transação alterada com sucesso")
            {
                tb_transacao updatedTransacao = transacaoService.Get(trans.codTransacao);
                return Request.CreateResponse(HttpStatusCode.OK, updatedTransacao);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, retorno);
            }
        }
    }
}
