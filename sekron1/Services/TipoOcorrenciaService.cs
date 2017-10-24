using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{
    public class TipoOcorrenciaService : ApiController, ITipoOcorrencia
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_tipoocorrencia Add(tb_tipoocorrencia tipoOcorrencia)
        {
            tb_tipoocorrencia ocr = db.tb_tipoocorrencia.Add(tipoOcorrencia);
            db.SaveChanges();
            return ocr;
        }

        public tb_tipoocorrencia Get(long id)
        {
            tb_tipoocorrencia ocr = db.tb_tipoocorrencia.Find(id);
            return ocr;
        }

        public IEnumerable<tb_tipoocorrencia> GetAll()
        {
            return db.tb_tipoocorrencia.ToList();
        }

        public tb_tipoocorrencia Remove(long id)
        {
            tb_tipoocorrencia ocr = db.tb_tipoocorrencia.Find(id);
            db.tb_tipoocorrencia.Remove(ocr);
            db.SaveChanges();
            return ocr;
        }

        public string Update(tb_tipoocorrencia tipoOcorrencia)
        {
            string retorno = "";

            var ExistingOcr = db.tb_tipoocorrencia.Where(s => s.codTipoOcorrencia == tipoOcorrencia.codTipoOcorrencia).FirstOrDefault<tb_tipoocorrencia>();

            if(ExistingOcr != null)
            {
                ExistingOcr.descricao = tipoOcorrencia.descricao;
                db.SaveChanges();
                retorno = "Dados do tipo da ocorrencia alterados com sucesso";
            }else
            {
                retorno = "Tipo de ocorrencia não encontrada";
            }
            return retorno;
        }
    }
}