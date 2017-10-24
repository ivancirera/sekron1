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
    public class EMedicaController : ApiController
    {

        public static readonly EmedicaService emedicaService = new EmedicaService();

        public IEnumerable<tb_emedica> ListarTodasEmedica()
        {
            return emedicaService.GetAll();
        }

        public HttpResponseMessage ListarEmedicaId(long id)
        {
            tb_emedica data = emedicaService.Get(id);
            if(data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Registro não encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        public HttpResponseMessage DeletarEmedica(long id)
        {
            tb_emedica data = emedicaService.Get(id);

            if(data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Registro não encontrado");
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

        public HttpResponseMessage AdicionarEmedica(JObject emedica)
        {

            var data = emedica.ToObject<tb_emedica>();

            if(data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Dados nulos, preencha algum campo para salvar");
            }

            if(data.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario nulo ou igual a zero");
            }

            //if(data.tipoSanguineo == null || data.tipoSanguineo == "")
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo sanguineo nulo ou vazio");
            //}

            //if(data.peso == null || data.peso == "")
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Peso nulo ou vazio");
            //}

            //if(data.altura == null || data.altura == "")
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Altura nula ou vazia");
            //}

            data = emedicaService.Add(data);

            return Request.CreateResponse(HttpStatusCode.OK, data);

        }

        public HttpResponseMessage AtualizarEmedica(JObject emedica)
        {

            var dados = emedica.ToObject<tb_emedica>();

            if (dados == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Dados nulos");
            }

            if (dados.codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario nulo ou igual a zero");
            }

            //if (dados.tipoSanguineo == null || dados.tipoSanguineo == "")
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tipo sanguineo nulo ou vazio");
            //}

            //if (dados.peso == null || dados.peso == "")
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Peso nulo ou vazio");
            //}

            //if (dados.altura == null || dados.altura == "")
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Altura nula ou vazia");
            //}

            string retorno = emedicaService.Update(dados);

            if(retorno == "Dados do usuario alterados com sucesso")
            {
                tb_emedica updatedEmedica = emedicaService.Get(dados.codUsuario);
                return Request.CreateResponse(HttpStatusCode.OK, updatedEmedica);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, retorno);
            }
        }

        public HttpResponseMessage VerificarCadastroEmedica(long codUsuario)
        {
            tb_emedica verifica = emedicaService.Verify(codUsuario);

            if(verifica != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, verifica);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cadastro não encontrado");
            }
        }
    }
}
