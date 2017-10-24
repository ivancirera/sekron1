
using Newtonsoft.Json.Linq;
using sekron1.infra;
using sekron1.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Web.Http;

namespace sekron1.Controllers
{
    public class UsuarioController : ApiController
    {

        static readonly IUsuario usuarioService = new UsuarioService();
        static readonly ILogin loginService = new LoginService();


        //funciona
        public IEnumerable<tb_usuario> ListarTodosUsuarios()
        {
            return usuarioService.GetAll();
        }

        //funciona
        public HttpResponseMessage ListarUsuarioId(long id)
        {
            tb_usuario usuario = usuarioService.Get(id);
            if (usuario == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario não encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        //funciona
        public HttpResponseMessage DeleteUsuario(long id)
        {
            tb_usuario usuario = usuarioService.Get(id);
            if (usuario == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario não encontrado");
            }
            else
            {
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"Message\":\"Usuario deletado com sucesso\"}")
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
        }

        //funciona
        public HttpResponseMessage NovoUsuario(JObject usuario)
        {
            var user = usuario.ToObject<tb_usuario>();

            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario nulo");
            }
            if (user.nome == null || user.nome == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nome do usuario nulo");
            }

            if (user.dataNascimento == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data nascimento nula");
            }

            if (user.cpf == null || user.cpf == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CPF nulo ou vazio");
            }
            if (user.rg == null || user.rg == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "RG nulo ou vazio");
            }
            if (user.celular == null || user.celular == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Celular nulo ou vazio");
            }
            //if (user.telefone == null || user.telefone == "")
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Telefone nulo ou vazio");
            //}
            if (user.sexo == null || user.sexo == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sexo nulo ou vazio");
            }
            if (user.cep == null || user.cep == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CEP nulo ou vazio");
            }
            if (user.rua == null || user.rua == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Rua nula ou vazia");
            }
            if (user.numero == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Numero nulo ou vazio");
            }
            if (user.cidade == null || user.cidade == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cidade nula ou vazia");
            }
            if (user.estado == null || user.estado == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Estado nulo ou vazio");
            }
            if (user.dataCadastro == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data de cadastro nula ou vazia");
            }
            if(user.pago == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Informações de pagante nula");
            }

            user = usuarioService.Add(user);
            string token = GerarToken();
            tb_login userToken = loginService.Get(user.codLogin);

            //atualizar com o novo token
            userToken.token = token;
            loginService.Update(userToken);
            //enviar email com token
            EnviarTokenEmail(userToken.email, token);

            tb_usuario resultado = usuarioService.Get(user.codUsuario);

            return Request.CreateResponse(HttpStatusCode.OK, resultado);
        }

        public HttpResponseMessage AtualizarUsuario(JObject usuario)
        {

            var user = usuario.ToObject<tb_usuario>();

            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario nulo");
            }
            if (user.nome == null || user.nome == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nome do usuario nulo");
            }

            if (user.dataNascimento == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data nascimento nula");
            }

            if (user.cpf == null || user.cpf == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CPF nulo ou vazio");
            }
            if (user.rg == null || user.rg == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "RG nulo ou vazio");
            }
            if (user.celular == null || user.celular == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Celular nulo ou vazio");
            }
            //if (user.telefone == null || user.telefone == "")
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Telefone nulo ou vazio");
            //}
            if (user.sexo == null || user.sexo == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sexo nulo ou vazio");
            }
            if (user.cep == null || user.cep == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CEP nulo ou vazio");
            }
            if (user.rua == null || user.rua == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Rua nula ou vazia");
            }
            if (user.numero == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Numero nulo ou vazio");
            }
            if (user.cidade == null || user.cidade == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cidade nula ou vazia");
            }
            if (user.estado == null || user.estado == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Estado nulo ou vazio");
            }
            if (user.dataCadastro == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data de cadastro nula ou vazia");
            }
            if (user.pago == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Informações de pagante nula");
            }

            string retorno = usuarioService.Update(user);

            if(retorno == "Dados do usuario alterados com sucesso")
            {
                tb_usuario updatedUser = usuarioService.Get(user.codUsuario);
                return Request.CreateResponse(HttpStatusCode.OK, updatedUser);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, retorno);
            }
        }

        public string GerarToken()
        {
            string token = "";

            Random tk = new Random(DateTime.Now.Millisecond);

            token = tk.Next(100000, 999999).ToString();

            return token;
        }

        //funciona
        public HttpResponseMessage ValidarToken(long codLogin, long codUsuario, string token)
        {

            tb_login user1 = loginService.Get(codLogin);
            tb_usuario user2 = usuarioService.Get(codUsuario);

            string tokenUser = user1.token.Trim();
            string tokenSended = token;

            if (tokenUser.Equals(tokenSended))
            {
                //validou ok ativa o usuario
                user2.ativo = true;
                //atualiza a tabela
                usuarioService.Update(user2);
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

        public HttpResponseMessage AtualizarTokenGCM(long codUsuario, string uidFirebase)
        {
            if(codUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Codigo Usuario nulo");
            }
            if(uidFirebase == "" || uidFirebase == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "UidFirebase nulo ou vazio");
            }

            tb_usuario userUpdateToken = usuarioService.Get(codUsuario);
            userUpdateToken.uidFirebase = uidFirebase;

            string result = usuarioService.Update(userUpdateToken);

            if(result == "Usuario não encontrado")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario não encontrado");
            }else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        public HttpResponseMessage atualizarNotificacoes(long codUsuario, bool notificacaoVermelha, bool notificacaoLaranja, bool notificacaoAmarela)
        {
            tb_usuario user = usuarioService.UpdateNotification(codUsuario, notificacaoVermelha, notificacaoLaranja, notificacaoAmarela);

            if(user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario não encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        public HttpResponseMessage SalvarConfiguracoes(long codUsuario, string cepCasa, string enderecoCasa, string cepTrabalho, string enderecoTrabalho)
        {
            tb_usuario user = usuarioService.SalvarConfiguracoes(codUsuario, cepCasa,
                enderecoCasa, cepTrabalho, enderecoTrabalho);

            if(user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuário não encontrado");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Sucesso");
        }

        public HttpResponseMessage EnviarTokenEmailInativo(string email, string senha)
        {
            tb_usuario user2 = loginService.RetornaUsuario(email, senha);
            tb_login user1 = loginService.Get(user2.codLogin);

            string tokenUser = user1.token.Trim();

            EnviarTokenEmail(email, tokenUser);

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("{\"Message\":\"Token foi enviado no email\"}")
            };

            return resp;

        }
    }
}
