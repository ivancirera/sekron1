using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services
{
    interface IEmedica
    {

        IEnumerable<tb_emedica> GetAll();
        tb_emedica Get(long id);
        tb_emedica Add(tb_emedica data);
        tb_emedica Remove(long id);
        string Update(tb_emedica data);
        tb_emedica Verify(long id);

    }
}
