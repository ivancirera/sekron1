using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface ITransacao
    {

        IEnumerable<tb_transacao> GetAll();
        tb_transacao Get(long id);
        tb_transacao Add(tb_transacao transacao);
        tb_transacao Remove(long id);
        string Update(tb_transacao transacao);

    }
}
