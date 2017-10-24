using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface IServico
    {

        IEnumerable<tb_servico> GetAll();
        tb_servico Get(long id);
        tb_servico Add(tb_servico servico);
        tb_servico Remove(long id);
        string Update(tb_servico servico);

    }
}
