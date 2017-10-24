using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{

    public class RedefinirSenhaService : ApiController, IRedefinirSenha
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_redefinirsenha Add(tb_redefinirsenha redefinir)
        {
            tb_redefinirsenha obj = db.tb_redefinirsenha.Add(redefinir);
            db.SaveChanges();
            return obj;

        }

        public tb_redefinirsenha Get(long id)
        {
            tb_redefinirsenha obj = db.tb_redefinirsenha.Find(id);
            return obj;
        }

        public IEnumerable<tb_redefinirsenha> GetAll()
        {
            return db.tb_redefinirsenha.ToList();
        }

        public tb_redefinirsenha Remove(long id)
        {
            tb_redefinirsenha obj = db.tb_redefinirsenha.Find(id);
            db.tb_redefinirsenha.Remove(obj);
            db.SaveChanges();
            return obj;
        }

        public string Update(tb_redefinirsenha redefinir)
        {
            string retorno = "";

            var existingRedefinir = db.tb_redefinirsenha.Where(s => s.codRedefinir == redefinir.codRedefinir).FirstOrDefault<tb_redefinirsenha>();

            if (existingRedefinir != null)
            {
                existingRedefinir.email = redefinir.email;
                existingRedefinir.senhaProvisoria = redefinir.senhaProvisoria;

                db.SaveChanges();

                retorno = "Redefinido com sucesso";
            }

            else
            {
                retorno = "Não encontrado";
            }

            return retorno;
        }

        public bool verificaEmail(string email)
        {
            var teste = db.tb_login.Where(a => a.email == email).FirstOrDefault<tb_login>();

            if (teste != null)
            {
                return true;
            }
            return false;
        }

        public bool verificaSenhaProvisoria(string email, string senhaProvisoria)
        {
            var verifica = db.tb_redefinirsenha.Where(x => x.email == email && x.senhaProvisoria == senhaProvisoria).FirstOrDefault<tb_redefinirsenha>();

            if(verifica != null)
            {
                return true;
            }
            return false;
        }

        public string alterarSenha(string email, string senhaNova)
        {
            string retorno = "";

            var alt = db.tb_login.Where(x => x.email == email).FirstOrDefault<tb_login>();
            if(alt != null)
            {
                alt.senha = senhaNova;
                db.SaveChanges();
                retorno = "Senha alterada com sucesso";
            }else
            {
                retorno = "Email não encontrado";
            }

            return retorno;
        }
    }
}