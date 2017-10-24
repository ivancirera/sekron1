using Newtonsoft.Json.Linq;
using sekron1.infra;
using sekron1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace sekron1.Controllers
{
    public class CartaoController : ApiController
    {

        static readonly ICartao cartaoService = new CartaoService();

        public IEnumerable<tb_cartao> TodosCartoes()
        {
            return cartaoService.GetAll();
        }

        public HttpResponseMessage ListarCartaoId(long id)
        {
            tb_cartao resultadoCartoes = cartaoService.Get(id);

            if(resultadoCartoes == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cartão não encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, resultadoCartoes);
        }

        public HttpResponseMessage DeletarCartao(long id)
        {
            tb_cartao deleteCard = cartaoService.Get(id);

            if(deleteCard == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cartão não encontrado");
            }else
            {
                cartaoService.Remove(deleteCard.codCartao);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Cartão deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }

        public HttpResponseMessage NovoCartao(JObject cartao)
        {

            var cartaoAdd = cartao.ToObject<tb_cartao>();

            if(cartaoAdd == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cartão nulo ou vazio");
            }
            if(cartaoAdd.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario nulo ou zero");
            }
            if (cartaoAdd.numeroCartao == null || cartaoAdd.numeroCartao == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Número do cartão nulo ou vazio");
            }
            if(cartaoAdd.validade == null || cartaoAdd.validade == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Validade nula ou vazia");
            }
            if(cartaoAdd.codSeguranca == null || cartaoAdd.codSeguranca == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Codigo de segurança nulo ou vazio");
            }
            //if(cartaoAdd.bandeira == null || cartaoAdd.bandeira == "")
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bandeira nula ou vazia");
            //}

            cartaoAdd = cartaoService.Add(cartaoAdd);

            return Request.CreateResponse(HttpStatusCode.OK, cartaoAdd);
        }

        public HttpResponseMessage AlterarCartao(JObject cartao)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var cartaoMudar = cartao.ToObject<tb_cartao>();

            if (cartaoMudar == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cartão nulo ou vazio");
            }
            if (cartaoMudar.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario nulo ou zero");
            }
            if (cartaoMudar.numeroCartao == null || cartaoMudar.numeroCartao == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Número do cartão nulo ou vazio");
            }
            if (cartaoMudar.validade == null || cartaoMudar.validade == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Validade nula ou vazia");
            }
            if (cartaoMudar.codSeguranca == null || cartaoMudar.codSeguranca == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Codigo de segurança nulo ou vazio");
            }

            string resultado = cartaoService.Update(cartaoMudar);

            if(resultado == "Dados do cartão alterados com sucesso")
            {
                return Request.CreateResponse(HttpStatusCode.OK, cartaoMudar);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }
        }

        public HttpResponseMessage buscarCartaoPorIdUsuario(long codUsuario)
        {
            tb_cartao responseCard = cartaoService.BuscaCartaoUsuario(codUsuario);

            if(responseCard != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, responseCard);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario não encontrado");

        }

        public HttpResponseMessage buscarTodosOsCartoesUsuario(long codUsuario)
        {
            if(codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Não encontrado");
            }

            var response = cartaoService.GetAllCardsById(codUsuario);

            return  Request.CreateResponse(HttpStatusCode.OK,response);
        }
    }
}
