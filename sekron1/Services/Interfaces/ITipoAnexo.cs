using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface ITipoAnexo
    {

        IEnumerable<tb_tipoanexo> GetAll();
        tb_tipoanexo Get(long id);
        tb_tipoanexo Add(tb_tipoanexo tipoAnexo);
        tb_tipoanexo Remove(long id);
        string Update(tb_tipoanexo tipoAnexo);

    }
}
