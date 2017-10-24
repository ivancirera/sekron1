using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{
    public class TipoAnexoService : ApiController, ITipoAnexo
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_tipoanexo Add(tb_tipoanexo tipoAnexo)
        {
            tb_tipoanexo tpAnx = db.tb_tipoanexo.Add(tipoAnexo);
            db.SaveChanges();
            return tpAnx;
        }

        public tb_tipoanexo Get(long id)
        {
            tb_tipoanexo tpAnx = db.tb_tipoanexo.Find(id);
            return tpAnx;
        }

        public IEnumerable<tb_tipoanexo> GetAll()
        {
            return db.tb_tipoanexo.ToList();
        }

        public tb_tipoanexo Remove(long id)
        {
            tb_tipoanexo tpAnx = db.tb_tipoanexo.Find(id);
            db.tb_tipoanexo.Remove(tpAnx);
            db.SaveChanges();
            return tpAnx;
        }

        public string Update(tb_tipoanexo tipoAnexo)
        {
            string retorno = "";

            var ExistingTpAnx = db.tb_tipoanexo.Where(s => s.codTipoAnexo == tipoAnexo.codTipoAnexo).FirstOrDefault<tb_tipoanexo>();

            if(ExistingTpAnx != null)
            {
                ExistingTpAnx.descricao = tipoAnexo.descricao;
                db.SaveChanges();
                retorno = "Tipo Anexo alterado com sucesso";
            }
            else
            {
                retorno = "Tipo de anexo não encontrado";
            }

            return retorno;
        }
    }
}