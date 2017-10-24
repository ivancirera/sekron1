using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using sekron1.infra;

namespace sekron1.Services
{
    public class CartaoService : ApiController, ICartao
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_cartao Add(tb_cartao cartao)
        {
            tb_cartao card = db.tb_cartao.Add(cartao);
            db.SaveChanges();
            return card;
        }

        public tb_cartao BuscaCartaoUsuario(long codUsuario)
        {
            tb_cartao card = db.tb_cartao.Where(s => s.codUsuario == codUsuario).FirstOrDefault<tb_cartao>();
            return card;
        }

        public tb_cartao Get(long id)
        {
            tb_cartao card = db.tb_cartao.Find(id);
            return card;
        }

        public IEnumerable<tb_cartao> GetAll()
        {
            return db.tb_cartao.ToList();
        }

        public IEnumerable<tb_cartao> GetAllCardsById(long codUsuario)
        {
            return db.tb_cartao.Where(s => s.codUsuario == codUsuario).ToList();
        }

        public tb_cartao Remove(long id)
        {
            tb_cartao card = db.tb_cartao.Find(id);
            db.tb_cartao.Remove(card);
            db.SaveChanges();
            return card;
        }

        public string Update(tb_cartao cartao)
        {
            string retorno = "";

            var existingCard = db.tb_cartao.Where(s => s.codCartao == cartao.codCartao).FirstOrDefault<tb_cartao>();

            if(existingCard != null)
            {
                existingCard.codUsuario = cartao.codUsuario;
                existingCard.numeroCartao = cartao.numeroCartao;
                existingCard.validade = cartao.validade;
                existingCard.codSeguranca = cartao.codSeguranca;
                existingCard.bandeira = cartao.bandeira;
                existingCard.cep = cartao.cep;

                db.SaveChanges();

                retorno = "Dados do cartão alterados com sucesso";
            }else
            {
                retorno = "Cartão não encontrado";
            }

            return retorno;
        }
    }
}