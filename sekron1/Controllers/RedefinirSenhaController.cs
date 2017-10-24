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
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace sekron1.Controllers
{
    public class RedefinirSenhaController : ApiController
    {

        static readonly IRedefinirSenha redefinirSenhaService = new RedefinirSenhaService();
        static readonly ILogin loginService = new LoginService();

        //listar todos

        public IEnumerable<tb_redefinirsenha> TodosRedefinir()
        {
            return redefinirSenhaService.GetAll();
        }

        public HttpResponseMessage ListarRedefinirId(long codRedefinir)
        {
            tb_redefinirsenha resultado = redefinirSenhaService.Get(codRedefinir);
            if(resultado == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Não encontrado");
            }else
            {
                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
        }

        public HttpResponseMessage DeletarRedefinirSenha(long codRedefinir)
        {
            tb_redefinirsenha resposta = redefinirSenhaService.Get(codRedefinir);

            if (resposta == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Não encontrado");
            }
            else
            {
                redefinirSenhaService.Remove(resposta.codRedefinir);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;

            }
        }

        public HttpResponseMessage NovoRedefinirSenha(JObject obj)
        {

            var objAdd = obj.ToObject<tb_redefinirsenha>();

            if(objAdd == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Redefinir senha nulo");
            }


            bool exists = redefinirSenhaService.verificaEmail(objAdd.email);

            if (exists)
            {
                redefinirSenhaService.Update(objAdd);
            }
            else
            {
                redefinirSenhaService.Add(objAdd);
            }

            return Request.CreateResponse(HttpStatusCode.OK, objAdd);
        }

        public HttpResponseMessage AtualizarRedefinirSenha(JObject obj)
        {
            var objUpd = obj.ToObject<tb_redefinirsenha>();

            string response = redefinirSenhaService.Update(objUpd);

            if(response == "Redefinido com sucesso")
            {
                return Request.CreateResponse(HttpStatusCode.OK, objUpd);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Não encontrado");
            }
        }

        public HttpResponseMessage RedefinirSenha(string email)
        {
            bool verificaEmail = redefinirSenhaService.verificaEmail(email);

            if(verificaEmail == true)
            {
                string senhaGerada = gerarSenha(6);

                tb_redefinirsenha redsenha = new tb_redefinirsenha();
                redsenha.email = email;
                redsenha.senhaProvisoria = senhaGerada;

                redefinirSenhaService.Add(redsenha);

                EnviarSenhaEmail(email, senhaGerada);

                return Request.CreateResponse(HttpStatusCode.OK, "Senha provisória enviada por email");

            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email não encontrado");
        }

        public string gerarSenha(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public HttpResponseMessage EnviarSenhaEmail(string email, string senha)
        {

            if (email == null || email.Equals(""))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email nulo ou vazio");
            }
            if (senha == null || senha == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Token nulo ou vazio");
            }

            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("ivan.cirera@hotmail.com");
            mail.To.Add(email);
            mail.Subject = "Gerada nova senha provisória";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "Senha provisória: " + senha;
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("ivan.cirera@hotmail.com", "kzw31986");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("{\"Message\":\"Email enviado com sucesso\"}")
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        public HttpResponseMessage AtualizaSenha(string email, string senhaProvisoria, string senhaNova)
        {
            bool verificaSenhaProvisoria = redefinirSenhaService.verificaSenhaProvisoria(email, senhaProvisoria);

            string retorno = "";

            if (verificaSenhaProvisoria)
            {
                retorno = redefinirSenhaService.alterarSenha(email, senhaNova);
                if(retorno == "Senha alterada com sucesso")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, retorno);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Senha provisória inválida");
        }
    }
}
