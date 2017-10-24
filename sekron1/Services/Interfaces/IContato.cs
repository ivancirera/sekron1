using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services
{
    interface IContato
    {

        IEnumerable<tb_contato> GetAll();
        tb_contato Get(long id);
        tb_contato Add(tb_contato contato);
        tb_contato Remove(long id);
        string Update(tb_contato contato);
        IEnumerable<tb_contato> ListarContatoUsuario(long id);
    }
}
