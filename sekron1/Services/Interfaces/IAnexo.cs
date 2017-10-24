using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface IAnexo
    {

        IEnumerable<tb_anexo> GetAll();
        tb_anexo Get(long id);
        tb_anexo Add(tb_anexo anexo);
        tb_anexo Remove(long id);
        string Update(tb_anexo anexo);

    }
}
