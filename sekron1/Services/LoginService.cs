using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace sekron1.Services
{
    public class LoginService : ApiController, ILogin
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_login Add(tb_login login)
        {
            tb_login user = db.tb_login.Add(login);
            db.SaveChanges();
            return user;
        }

        public tb_login Get(long id)
        {
            tb_login user = db.tb_login.Find(id);
            return user;
        }

        public IEnumerable<tb_login> GetAll()
        {
            return db.tb_login.ToList();
        }

        public bool logar(string email, string senha)
        {

            return db.tb_login.Any(user =>
                user.email.Equals(senha, StringComparison.OrdinalIgnoreCase)
                && user.senha.Equals(senha));
        }

        public tb_login Remove(long id)
        {
            tb_login user = db.tb_login.Find(id);
            db.tb_login.Remove(user);
            db.SaveChanges();
            return user;
        }

        public string Update(tb_login login)
        {
            string retorno = "";

            var existingLogin = db.tb_login.Where(s => s.codLogin == login.codLogin).FirstOrDefault<tb_login>();

            if (existingLogin != null)
            {

                existingLogin.email = login.email;
                existingLogin.senha = login.senha;
                existingLogin.token = login.token;
                existingLogin.serialChip = login.serialChip;
                db.SaveChanges();

                retorno = "Dados de login alterados com sucesso";

            }
            else
            {
                retorno = "Login  não encontrado";
            }
            return retorno;
        }

        public string VerificarEmail(tb_login login)
        {
            string result = "";

            var emailBase = db.tb_login.Any(e => e.email == login.email);

            if (emailBase == false)
            {
                result = "liberado";
            }
            else
            {
                result = "Login já existe no banco de dados";
            }
            return result;
        }

        public bool VerificarAtivo(string email, string senha)
        {

            bool ativo = false;

            tb_login login = db.tb_login.Where(s => s.email == email && s.senha == senha).FirstOrDefault<tb_login>();
            if (login != null)
            {
                tb_usuario usuario = db.tb_usuario.Where(k => k.codLogin == login.codLogin).FirstOrDefault<tb_usuario>();
                if (usuario != null)
                {
                    ativo = usuario.ativo;
                }
            }
            return ativo;
        }

        public tb_usuario RetornaUsuario(string email, string senha)
        {
            tb_login currentLogin = db.tb_login.Where(x => x.email == email && x.senha == senha).FirstOrDefault<tb_login>();
            tb_usuario currentUser = db.tb_usuario.Where(z => z.codLogin == currentLogin.codLogin).FirstOrDefault<tb_usuario>();

            return currentUser;    
        }

        public tb_login EsqueciSenha(string email)
        {
            tb_login login = db.tb_login.Where(s => s.email == email).FirstOrDefault<tb_login>();
            return login;
        }

        public bool verificarTelefone(string email, string senha, string telefone)
        {
            tb_login currentUser = db.tb_login.Where(x => x.email == email && x.senha == senha).FirstOrDefault<tb_login>();
            if(currentUser != null)
            {
                if(currentUser.serialChip == telefone)
                {
                    return true;
                }
            }
            return false;
        }

        public void DesativarUsuario(string email, string senha)
        {

            tb_login currentUser = db.tb_login.Where(x => x.email == email && x.senha == senha).FirstOrDefault<tb_login>();
            if (currentUser != null)
            {
                long codLogin = currentUser.codLogin;
                tb_usuario usuario = db.tb_usuario.Where(y => y.codLogin == codLogin).FirstOrDefault<tb_usuario>();
                if (usuario != null)
                {
                    usuario.ativo = false;
                    db.SaveChanges();
                }
            }
        }

        public void AtualizarToken(string email, string token)
        {
           
            tb_login user = db.tb_login.Where(y => y.email == email).FirstOrDefault<tb_login>();
            user.token = token;
            db.SaveChanges();
        }
    }
}