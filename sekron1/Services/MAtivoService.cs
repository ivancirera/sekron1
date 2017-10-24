using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{
    public class MAtivoService : ApiController, IMAtivo
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_mativo Add(tb_mativo contato)
        {
            tb_mativo cont = db.tb_mativo.Add(contato);
            db.SaveChanges();
            return cont;
        }

        public tb_mativo Get(long id)
        {
            tb_mativo cont = db.tb_mativo.Find(id);
            return cont;
        }

        public IEnumerable<tb_mativo> GetAll()
        {
            return db.tb_mativo.ToList();
        }

        public tb_mativo Remove(long id)
        {
            tb_mativo cont = db.tb_mativo.Find(id);
            db.tb_mativo.Remove(cont);
            db.SaveChanges();
            return cont;
        }

        public string Update(tb_mativo contato)
        {

            string retorno = "";
            var existingCont = db.tb_mativo.Where(s => s.codContato == contato.codContato).FirstOrDefault<tb_mativo>();
            if(existingCont != null)
            {
                existingCont.codContato = contato.codContato;
                existingCont.codUsuario = contato.codUsuario;
                existingCont.nome = contato.nome;
                existingCont.telefone = contato.telefone;
                existingCont.email = contato.email;
                db.SaveChanges();

                retorno = "Contato alterado com sucesso";
            }else
            {
                retorno = "Contato não encontrado";
            }

            return retorno; 
        }
    }
}