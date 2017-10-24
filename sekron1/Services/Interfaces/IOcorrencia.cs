using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface IOcorrencia
    {

        IEnumerable<tb_ocorrencia> GetAll();
        tb_ocorrencia Get(long id);
        tb_ocorrencia Add(tb_ocorrencia ocorrencia);
        tb_ocorrencia Remove(long id);
        string Update(tb_ocorrencia ocorrencia);

    }
}
