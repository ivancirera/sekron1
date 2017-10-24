using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;
using System.Web.Http.Description;

namespace sekron1.Services
{
    public class ContatoService : ApiController, IContato
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_contato Add(tb_contato contato)
        {
            tb_contato cont = db.tb_contato.Add(contato);
            db.SaveChanges();
            return cont; 
        }

        public tb_contato Get(long id)
        {
            tb_contato cont = db.tb_contato.Find(id);
            return cont;
        }

        public IEnumerable<tb_contato> GetAll()
        {
            return db.tb_contato.ToList();
        }

        
        public IEnumerable<tb_contato> ListarContatoUsuario(long codUsuario)
        {

            return db.tb_contato.Where(x => x.codUsuario == codUsuario).ToList().AsEnumerable<tb_contato>();
        }

        public tb_contato Remove(long id)
        {
            tb_contato contRemove = db.tb_contato.Find(id);
            db.tb_contato.Remove(contRemove);
            db.SaveChanges();
            return contRemove;
        }

        public string Update(tb_contato contato)
        {
            string retorno = "";

            var existingContact = db.tb_contato.Where(s => s.codContato == contato.codContato).FirstOrDefault<tb_contato>();

            if(existingContact != null)
            {

                existingContact.codUsuario = contato.codUsuario;
                existingContact.nome = contato.nome;
                existingContact.email = contato.email;
                existingContact.telefone = contato.telefone;
                db.SaveChanges();

                retorno = "Dados alterados com sucesso";

            }else
            {
                retorno = "Contato não encontrado";
            }

            return retorno;
        }
    }
}