using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface IMAtivo
    {

        IEnumerable<tb_mativo> GetAll();
        tb_mativo Get(long id);
        tb_mativo Add(tb_mativo contato);
        tb_mativo Remove(long id);
        string Update(tb_mativo contato);

    }
}
