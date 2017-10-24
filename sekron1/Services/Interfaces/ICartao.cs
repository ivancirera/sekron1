using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services
{
    interface ICartao
    {

        IEnumerable<tb_cartao> GetAll();
        tb_cartao Get(long id);
        tb_cartao Add(tb_cartao cartao);
        tb_cartao Remove(long id);
        string Update(tb_cartao cartao);
        tb_cartao BuscaCartaoUsuario(long codUsuario);
        IEnumerable<tb_cartao> GetAllCardsById(long codUsuario);

    }
}
