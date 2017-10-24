using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{
    public class EmedicaService : ApiController, IEmedica
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_emedica Add(tb_emedica dados)
        {
            tb_emedica dados1 = db.tb_emedica.Add(dados);
            db.SaveChanges();
            return dados1;
        }

        public tb_emedica Get(long id)
        {
            tb_emedica dados = db.tb_emedica.Where(s => s.codUsuario == id).First<tb_emedica>();
            return dados;
        }

        public IEnumerable<tb_emedica> GetAll()
        {
            return db.tb_emedica.ToList();
        }

        public tb_emedica Remove(long id)
        {
            tb_emedica dados = db.tb_emedica.Find(id);
            db.tb_emedica.Remove(dados);
            db.SaveChanges();
            return dados;
        }

        public string Update(tb_emedica data)
        {
            string retorno = "";

            var existingData = db.tb_emedica.Where(s => s.codUsuario == data.codUsuario).FirstOrDefault<tb_emedica>();

            if(existingData != null)
            {
                existingData.codUsuario = data.codUsuario;
                existingData.cartaoSus = data.cartaoSus;
                existingData.planoSaude = data.planoSaude;
                existingData.numeroPlanoSaude = data.numeroPlanoSaude;
                existingData.problemaSaude = data.problemaSaude;
                existingData.notasMedicas = data.notasMedicas;
                existingData.alergiasReacoes = data.alergiasReacoes;
                existingData.medicamentos = data.medicamentos;
                existingData.tipoSanguineo = data.tipoSanguineo;
                existingData.peso = data.peso;
                existingData.altura = data.altura;

                db.SaveChanges();

                retorno = "Dados do usuario alterados com sucesso";

            }else
            {
                retorno = "Dados não encontrados";
            }

            return retorno;
        }

        public tb_emedica Verify(long id)
        {
            tb_emedica ver = db.tb_emedica.Where(x => x.codUsuario == id).FirstOrDefault<tb_emedica>();
            return ver;
        }
    }
}