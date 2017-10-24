using sekron1.infra;
using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace sekron1.Services
{
    public class SubOcorrenciaService: ApiController, ISubOcorrencia
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_subocorrencia Add(tb_subocorrencia subOcorrencia)
        {

            tb_subocorrencia Socr = db.tb_subocorrencia.Add(subOcorrencia);
            db.SaveChanges();
            return Socr;

        }

        public tb_subocorrencia Get(long id)
        {
            tb_subocorrencia Socr = db.tb_subocorrencia.Find(id);
            return Socr;
        }

        public IEnumerable<tb_subocorrencia> GetAll()
        {
            return db.tb_subocorrencia.ToList();
        }

        public tb_subocorrencia Remove(long id)
        {
            tb_subocorrencia Socr = db.tb_subocorrencia.Find(id);
            db.tb_subocorrencia.Remove(Socr);
            db.SaveChanges();
            return Socr;
        }

        public string Update(tb_subocorrencia subOcorrencia)
        {
            string retorno = "";

            var existingSocr = db.tb_subocorrencia.Where(x => x.codSubocorrencia == subOcorrencia.codSubocorrencia).FirstOrDefault<tb_subocorrencia>();

            if(existingSocr != null)
            {
                existingSocr.codTipoocorrencia = subOcorrencia.codTipoocorrencia;
                existingSocr.descricao = subOcorrencia.descricao;
                db.SaveChanges();

                retorno = "Sub-Ocorrencia alterada com sucesso";
            }else
            {
                retorno = "Sub-Ocorrencia não encontrada";
            }

            return retorno;

        }
    }
}