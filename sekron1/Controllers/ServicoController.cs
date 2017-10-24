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
    public class ServicoController : ApiController
    {

        static readonly IServico servicoService = new ServicoService();

        public IEnumerable<tb_servico> TodosServicos()
        {
            return servicoService.GetAll();
        }

        public HttpResponseMessage ListarServicoId(long id)
        {
            tb_servico servResp = servicoService.Get(id);

            if(servResp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Serviço não encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, servResp);
        }

        public HttpResponseMessage DeletarServico(long id)
        {

            tb_servico servicoDeleted = servicoService.Get(id);

            if(servicoDeleted == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Serviço não encontrado");
            }else
            {
                servicoService.Remove(servicoDeleted.codServico);

                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Serviço deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }

        public HttpResponseMessage NovoServico(JObject servico)
        {

            var servAdd = servico.ToObject<tb_servico>();

            if(servAdd == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Serviço nulo ou vazio");
            }
            if(servAdd.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario nulo ou zero");
            }
            if(servAdd.nome == null || servAdd.nome == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nome nulo ou vazio");
            }
            if(servAdd.descricao == null || servAdd.descricao == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Descrição nula ou vazia");
            }
            if(servAdd.data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data nula ou vazia");
            }
            if(servAdd.dataValidade == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "DataValidade nula ou vazia");
            }
            if(servAdd.valor == null || servAdd.valor == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Valor nulo ou vazio");
            }

            servAdd = servicoService.Add(servAdd);

            return Request.CreateResponse(HttpStatusCode.OK, servAdd);
        }

        public HttpResponseMessage AtualizarServico(JObject servico)
        {

            var servChange = servico.ToObject<tb_servico>();

            if (servChange == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Serviço nulo ou vazio");
            }
            if (servChange.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario nulo ou zero");
            }
            if (servChange.nome == null || servChange.nome == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nome nulo ou vazio");
            }
            if (servChange.descricao == null || servChange.descricao == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Descrição nula ou vazia");
            }
            if (servChange.data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data nula ou vazia");
            }
            if (servChange.dataValidade == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "DataValidade nula ou vazia");
            }
            if (servChange.valor == null || servChange.valor == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Valor nulo ou vazio");
            }

            string resultado = servicoService.Update(servChange);

            if(resultado == "Serviço alterado com sucesso")
            {
                return Request.CreateResponse(HttpStatusCode.OK, servChange);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }
        }
    }
}
