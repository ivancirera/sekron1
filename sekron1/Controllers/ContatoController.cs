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
using System.Web.Http.Description;

namespace sekron1.Controllers
{
    public class ContatoController : ApiController
    {

        static readonly IContato contatoService = new ContatoService();

        public IEnumerable<tb_contato> TodosContatos()
        {
            return contatoService.GetAll();
        }
        
        public HttpResponseMessage listarContatoId(long id)
        {
            tb_contato resultadoContatos = contatoService.Get(id);

            if(resultadoContatos == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Contato não encontrado");
            }
            return Request.CreateResponse(HttpStatusCode.OK, resultadoContatos);
        }

        public HttpResponseMessage deletarContato(long id)
        {
            tb_contato deleteContato = contatoService.Get(id);

            if(deleteContato == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Contato não encontrado");
            }else
            {
                contatoService.Remove(deleteContato.codContato);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Contato deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }

        public HttpResponseMessage NovoContato(JObject contato)
        {
            var contatoAdd = contato.ToObject<tb_contato>();

            if(contatoAdd == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Contato nulo ou vazio");
            }
            if(contatoAdd.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario nulo ou vazio");
            }
            if(contatoAdd.nome == null || contatoAdd.nome == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nome nulo ou vazio");
            }
            if(contatoAdd.email == null || contatoAdd.email == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email nulo ou vazio");
            }
            if(contatoAdd.telefone == null || contatoAdd.telefone == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Telefone nulo ou vazio");
            }

            contatoAdd = contatoService.Add(contatoAdd);

            return Request.CreateResponse(HttpStatusCode.OK, contatoAdd);
        }

        public HttpResponseMessage AlterarContato(JObject contato)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var contatoMudar = contato.ToObject<tb_contato>();

            if(contatoMudar.codContato == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodContato nulo ou zero");
            }

            if (contatoMudar.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario nulo ou vazio");
            }
            if (contatoMudar.nome == null || contatoMudar.nome == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nome nulo ou vazio");
            }
            if (contatoMudar.email == null || contatoMudar.email == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email nulo ou vazio");
            }
            if (contatoMudar.telefone == null || contatoMudar.telefone == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Telefone nulo ou vazio");
            }

            string resultado = contatoService.Update(contatoMudar);

            if(resultado.Equals("Dados alterados com sucesso"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, contatoMudar);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }
        }

        public IEnumerable<tb_contato> ListarContatosUsuario(long id)
        {
            return contatoService.ListarContatoUsuario(id).ToList();
        }
    }
}
