using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{
    public class AnexoService : ApiController, IAnexo
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_anexo Add(tb_anexo anexo)
        {
            tb_anexo anx = db.tb_anexo.Add(anexo);
            db.SaveChanges();
            return anx;
        }

        public tb_anexo Get(long id)
        {
            tb_anexo anx = db.tb_anexo.Find(id);
            return anx;
        }

        public IEnumerable<tb_anexo> GetAll()
        {
            return db.tb_anexo.ToList();
        }

        public tb_anexo Remove(long id)
        {
            tb_anexo anx = db.tb_anexo.Find(id);
            db.tb_anexo.Remove(anx);
            db.SaveChanges();
            return anx;
        }

        public string Update(tb_anexo anexo)
        {
            string retorno = "";

            var existingAnx = db.tb_anexo.Where(s => s.codAnexo == anexo.codAnexo).FirstOrDefault<tb_anexo>();

            if(existingAnx != null)
            {
                existingAnx.codOcorrencia = anexo.codOcorrencia;
                existingAnx.codTipoAnexo = anexo.codTipoAnexo;
                existingAnx.anexo = anexo.anexo;
                existingAnx.tipoAnexo = anexo.tipoAnexo;
                existingAnx.data = anexo.data;
                existingAnx.hora = anexo.hora;

                db.SaveChanges();

                retorno = "Anexo alterado com sucesso";
            }
            else
            {
                retorno = "Anexo não encontrado";
            }

            return retorno;
        }
    }
}