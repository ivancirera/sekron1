using sekron1.infra;
using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace sekron1.Services
{
    public class ServicoService : ApiController, IServico
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_servico Add(tb_servico servico)
        {
            tb_servico serv = db.tb_servico.Add(servico);
            db.SaveChanges();
            return serv;
        }

        public tb_servico Get(long id)
        {
            tb_servico serv = db.tb_servico.Find(id);
            return serv;
        }

        public IEnumerable<tb_servico> GetAll()
        {
            return db.tb_servico.ToList();
        }

        public tb_servico Remove(long id)
        {
            tb_servico serv = db.tb_servico.Find(id);
            db.tb_servico.Remove(serv);
            db.SaveChanges();
            return serv;
        }

        public string Update(tb_servico servico)
        {
            string retorno = "";

            var existingServico = db.tb_servico.Where(s => s.codServico == servico.codServico).FirstOrDefault<tb_servico>();

            if(existingServico != null)
            {

                existingServico.codUsuario = servico.codUsuario;
                existingServico.nome = servico.nome;
                existingServico.descricao = servico.descricao;
                existingServico.data = servico.data;
                existingServico.dataValidade = servico.dataValidade;
                existingServico.valor = servico.valor;

                db.SaveChanges();

                retorno = "Serviço alterado com sucesso";
            }else
            {
                retorno = "Serviço não encontrado";
            }

            return retorno;
        }
    }
}