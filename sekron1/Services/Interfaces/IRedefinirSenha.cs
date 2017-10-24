using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface IRedefinirSenha
    {

        IEnumerable<tb_redefinirsenha> GetAll();
        tb_redefinirsenha Get(long id);
        tb_redefinirsenha Add(tb_redefinirsenha redefinir);
        tb_redefinirsenha Remove(long id);
        string Update(tb_redefinirsenha redefinir);
        bool verificaEmail(string email);
        bool verificaSenhaProvisoria(string email, string senhaProvisoria);
        string alterarSenha(string email, string senhaNova);

    }
}
