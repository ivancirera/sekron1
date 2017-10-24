using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface ISubOcorrencia
    {

        IEnumerable<tb_subocorrencia> GetAll();
        tb_subocorrencia Get(long id);
        tb_subocorrencia Add(tb_subocorrencia subOcorrencia);
        tb_subocorrencia Remove(long id);
        string Update(tb_subocorrencia subOcorrencia);

    }
}
