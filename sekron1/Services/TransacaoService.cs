using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{
    public class TransacaoService : ApiController, ITransacao
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_transacao Add(tb_transacao transacao)
        {
            tb_transacao trans = db.tb_transacao.Add(transacao);
            db.SaveChanges();
            return trans;
        }

        public tb_transacao Get(long id)
        {
            tb_transacao trans = db.tb_transacao.Find(id);
            return trans;
        }

        public IEnumerable<tb_transacao> GetAll()
        {
            return db.tb_transacao.ToList();
        }

        public tb_transacao Remove(long id)
        {
            tb_transacao trans = db.tb_transacao.Find(id);
            db.tb_transacao.Remove(trans);
            db.SaveChanges();
            return trans;
        }

        public string Update(tb_transacao transacao)
        {
            string retorno = "";

            var existingTrans = db.tb_transacao.Where(s => s.codTransacao == transacao.codTransacao).FirstOrDefault<tb_transacao>();

            if(existingTrans != null)
            {
                existingTrans.codCartao = transacao.codCartao;
                existingTrans.codServico = transacao.codServico;
                existingTrans.dataTransacao = transacao.dataTransacao;
                existingTrans.status = transacao.status;

                db.SaveChanges();

                retorno = "Transação alterada com sucesso";
            }else
            {
                retorno = "Transação não encontrada";
            }

            return retorno;
        }
    }
}