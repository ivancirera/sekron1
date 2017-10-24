using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services
{
    interface IUsuario
    {

        IEnumerable<tb_usuario> GetAll();
        tb_usuario Get(long id);
        tb_usuario Add(tb_usuario usuario);
        tb_usuario Remove(long id);
        string Update(tb_usuario usuario);
        tb_usuario UpdateNotification(long id, bool red, bool orange, bool yellow);
        tb_usuario SalvarConfiguracoes(long codUsuario, string cepCasa, string enderecoCasa, string cepTrabalho, string enderecoTrabalho);

    }
}
