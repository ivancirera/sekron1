using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sekron1.Models;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{
    public class OcorrenciaMapsService : ApiController, IOcorrenciaMaps
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public List<OcorrenciaMapsModel> listOcorrencias(string dataAtual)
        {
            List<OcorrenciaMapsModel> listaOcorrenciaResult = new List<OcorrenciaMapsModel>();

            List<tb_tipoocorrencia> listaTipoOcorrencia = new List<tb_tipoocorrencia>();
            List<tb_ocorrencia> listaOcorrencia = new List<tb_ocorrencia>();

            listaTipoOcorrencia = db.tb_tipoocorrencia.ToList();
            listaOcorrencia = db.tb_ocorrencia.ToList();

            for (int i = 0; i < listaOcorrencia.Count; i++)
            {
                if(listaOcorrencia.ElementAt(i).data == dataAtual)
                {

                    OcorrenciaMapsModel ocrMaps = new OcorrenciaMapsModel();

                    ocrMaps.latitude = listaOcorrencia.ElementAt(i).latitude;
                    ocrMaps.longitude = listaOcorrencia.ElementAt(i).longitude;

                    long codTipoOcorrencia = listaOcorrencia.ElementAt(i).codTipoOcorrencia;

                    tb_tipoocorrencia tipoOcr = db.tb_tipoocorrencia.Where(s => s.codTipoOcorrencia == codTipoOcorrencia).FirstOrDefault<tb_tipoocorrencia>();

                    ocrMaps.titulo = tipoOcr.descricao;
                    ocrMaps.data = listaOcorrencia.ElementAt(i).data;

                    listaOcorrenciaResult.Add(ocrMaps);
                }
            }
            return listaOcorrenciaResult;
        }
    }
}