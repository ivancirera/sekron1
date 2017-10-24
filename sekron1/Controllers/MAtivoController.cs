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
    public class MAtivoController : ApiController
    {

        static readonly IMAtivo mativoService = new MAtivoService();

        public IEnumerable<tb_mativo> TodosContatos()
        {
            return mativoService.GetAll();
        }

        public HttpResponseMessage ListarContatoId(long id)
        {
            tb_mativo resultCont = mativoService.Get(id);

            if (resultCont == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Contato não encontrado");
            }
            return Request.CreateResponse(HttpStatusCode.OK, resultCont);
        }

        public HttpResponseMessage DeletarContato(long id)
        {
            tb_mativo cont = mativoService.Get(id);
            if(cont == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Contato não encontrado");
            }else
            {
                mativoService.Remove(cont.codContato);

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
            var contatoAdd = contato.ToObject<tb_mativo>();

            if(contatoAdd == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Contato nulo");
            }

            if(contatoAdd.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario nulo ou igual a 0");
            }

            if (contatoAdd.nome == null || contatoAdd.nome == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nome nulo ou vazio");
            }

            if(contatoAdd.telefone == null || contatoAdd.telefone == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Telefone nulo ou vazio");
            }

            if(contatoAdd.email == null || contatoAdd.email == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email nulo ou vazio");
            }

            contatoAdd = mativoService.Add(contatoAdd);
            return Request.CreateResponse(HttpStatusCode.OK, contatoAdd);
        }

        public HttpResponseMessage AlterarContato(JObject contato)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var contatoMudar = contato.ToObject<tb_mativo>();

            if (contatoMudar.codContato == 0)
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

            string resultado = mativoService.Update(contatoMudar);

            if (resultado.Equals("Contato alterado com sucesso"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, contatoMudar);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }
        }
    }
}
