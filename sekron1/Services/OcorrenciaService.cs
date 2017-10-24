using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{
    public class OcorrenciaService : ApiController, IOcorrencia
    {

        dbSekronEntities1 db = new dbSekronEntities1();

        public tb_ocorrencia Add(tb_ocorrencia ocorrencia)
        {
            tb_ocorrencia ocr = db.tb_ocorrencia.Add(ocorrencia);
            db.SaveChanges();
            return ocr;
        }

        public tb_ocorrencia Get(long id)
        {
            tb_ocorrencia ocr = db.tb_ocorrencia.Find(id);
            return ocr;
        }

        public IEnumerable<tb_ocorrencia> GetAll()
        {
            return db.tb_ocorrencia.ToList();
        }

        public tb_ocorrencia Remove(long id)
        {
            tb_ocorrencia ocr = db.tb_ocorrencia.Find(id);
            db.tb_ocorrencia.Remove(ocr);
            db.SaveChanges();
            return ocr;
        }

        public string Update(tb_ocorrencia ocorrencia)
        {
            string retorno = "";

            var existingOcr = db.tb_ocorrencia.Where(s => s.codOcorrencia == ocorrencia.codOcorrencia).FirstOrDefault<tb_ocorrencia>();

            if(existingOcr != null)
            {
                existingOcr.codTipoOcorrencia = ocorrencia.codTipoOcorrencia;
                existingOcr.codUsuario = ocorrencia.codUsuario;
                existingOcr.latitude = ocorrencia.latitude;
                existingOcr.longitude = ocorrencia.longitude;
                existingOcr.descricaoOcorrencia = ocorrencia.descricaoOcorrencia;
                existingOcr.data = ocorrencia.data;
                existingOcr.hora = ocorrencia.hora;
                existingOcr.enderecoOcorrencia = ocorrencia.enderecoOcorrencia;
                existingOcr.placaVeiculo = ocorrencia.placaVeiculo;

                db.SaveChanges();

                retorno = "Ocorrencia alterada com sucesso";

            }else
            {
                retorno = "Ocorrencia não encontrada";
            }

            return retorno;
        }
    }
}