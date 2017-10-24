using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface ITipoOcorrencia
    {

        IEnumerable<tb_tipoocorrencia> GetAll();
        tb_tipoocorrencia Get(long id);
        tb_tipoocorrencia Add(tb_tipoocorrencia tipoOcorrencia);
        tb_tipoocorrencia Remove(long id);
        string Update(tb_tipoocorrencia tipoOcorrencia);

    }
}
