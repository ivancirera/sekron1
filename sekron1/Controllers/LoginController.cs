using sekron1.infra;
using sekron1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Net.Mail;

namespace sekron1.Controllers
{
    public class LoginController : ApiController
    {
        static readonly ILogin loginService = new LoginService();
        static readonly IUsuario usuarioService = new UsuarioService();

        public IEnumerable<tb_login> TodosLogins()
        {
            return loginService.GetAll();
        }

        
        public HttpResponseMessage Logar(JObject login)
        {

            var user = login.ToObject<tb_login>();

            string userEmail = user.email;
            string userSenha = user.senha;
            string serialChip = user.serialChip;

            LoginSecure loginSecure = new LoginSecure();

            if (userEmail == null || userEmail == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email nulo ou vazio");
            }

            if (userSenha == null || userSenha == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Senha nula ou vazia");
            }

            bool valida = false;
            bool ativo;
            bool verificaTelefone;

            valida = loginSecure.Login(userEmail, userSenha);
            ativo = loginService.VerificarAtivo(user.email, user.senha);
            verificaTelefone = loginService.verificarTelefone(user.email, user.senha, user.serialChip);

            if (valida == true && ativo == true && verificaTelefone == true)
            {

                tb_usuario getUsuario = loginService.RetornaUsuario(user.email, user.senha);
                tb_usuario retorno = usuarioService.Get(getUsuario.codUsuario);

                return Request.CreateResponse(HttpStatusCode.OK, retorno);

            }
            else if(valida == true && ativo == true && verificaTelefone == false)
            {
                loginService.DesativarUsuario(userEmail, userSenha);

                string token = GerarToken();

                loginService.AtualizarToken(userEmail, token);

                EnviarTokenEmail(userEmail, token);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Telefone difere do cadastro");

            }
            else if (valida == true && ativo == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario inativo");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Login ou senha inválidos");
            }
        }


        public HttpResponseMessage listarLoginId(long id)
        {
            tb_login resultLogin = loginService.Get(id);

            if (resultLogin == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario não encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, resultLogin);
        }

        
        public HttpResponseMessage DeleteLogin(long id)
        {
            tb_login userDelete = loginService.Get(id);

            if (userDelete == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario não encontrado");
            }
            else
            {
                loginService.Remove(userDelete.codLogin);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Login deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }

        
        public HttpResponseMessage NovoLogin(JObject login)
        {
            var user = login.ToObject<tb_login>();

            string resultado = "";

            resultado = loginService.VerificarEmail(user);

            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro ao criar o login, login nulo");
            }

            if (user.email == null || user.email == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email nulo");
            }

            if (user.senha == null || user.senha == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Senha nula");
            }

            if (user.token == null || user.token == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Token nulo");
            }

            if (user.serialChip == null || user.serialChip == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Telefone nulo");
            }

            if (resultado == "liberado")
            {
                user = loginService.Add(user);

                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, resultado);
            }

        }

        
        public HttpResponseMessage AlterarLogin(JObject login)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var user = login.ToObject<tb_login>();

            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario nulo");
            }

            if (user.codLogin == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodLogin nulo");
            }

            if (user.email == null || user.email == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email nulo");
            }

            if (user.senha == null || user.senha == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Senha nula");
            }

            if (user.token == null || user.token == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Token nulo");
            }

            if (user.serialChip == null || user.serialChip == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Telefone nulo");
            }

            string resultado = loginService.Update(user);

            if(resultado == "Dados de login alterados com sucesso")
            {
                tb_login updatedLogin = loginService.Get(user.codLogin);
                return Request.CreateResponse(HttpStatusCode.OK, updatedLogin);
            }else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,resultado);
            }
        }

        public HttpResponseMessage EsqueciSenha(string email)
        {
            tb_login login = loginService.EsqueciSenha(email);

            if(login == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email não encontrado");
            }else
            {
                string senha = login.senha;
                string emailx = login.email;

                var resp = EnviarSenhaEmail(emailx, senha);

                return resp;

            }
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

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("gntecmobile@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Senha E190";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "Sua senha: " + senha;
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("gntecmobile@gmail.com", "gntec2608@@#");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("{\"Message\":\"Email enviado com sucesso\"}")
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        public string GerarToken()
        {
            string token = "";

            Random tk = new Random(DateTime.Now.Millisecond);

            token = tk.Next(100000, 999999).ToString();

            return token;
        }

        //funciona
        public HttpResponseMessage ValidarTokenTelefone(string email, string senha, string token, string telefone)
        {

            tb_usuario user2 = loginService.RetornaUsuario(email, senha);
            tb_login user1 = loginService.Get(user2.codLogin);
            
            string tokenUser = user1.token.Trim();
            string tokenSended = token;

            if (tokenUser.Equals(tokenSended))
            {
                //validou ok ativa o usuario
                user2.ativo = true;
                //user2.celular = telefone;
                //atualiza a tabela
                usuarioService.Update(user2);
                user1.serialChip = telefone;
                //loginService.Update(user1);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Token validado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Token inválido");
        }

        public HttpResponseMessage EnviarTokenEmail(string email, string token)
        {


            if (email == null || email.Equals(""))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email nulo ou vazio");
            }
            if (token == null || token == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Token nulo ou vazio");
            }

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("gntecmobile@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Token de validação E190";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "Seu Token de validação foi gerado: " + token;
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("gntecmobile@gmail.com", "gntec2608@@#");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("{\"Message\":\"Email enviado com sucesso\"}")
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        public HttpResponseMessage ReenviarToken(long codLogin)
        {
            if (codLogin == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodUsuario zero");
            }

            tb_login User = loginService.Get(codLogin);

            if (User == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CodLogin não encontrado");
            }

            string token = User.token;

            EnviarTokenEmail(User.email, token);

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("{\"Message\":\"Token foi enviado no email\"}")
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }
    }
}
