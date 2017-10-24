using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services
{
    interface ILogin
    {
        IEnumerable<tb_login> GetAll();
        tb_login Get(long id);
        tb_login Add(tb_login login);
        tb_login Remove(long id);
        string Update(tb_login login);
        string VerificarEmail(tb_login login);
        bool logar(string email, string senha);
        bool VerificarAtivo(string email, string senha);
        tb_usuario RetornaUsuario(string email, string senha);
        tb_login EsqueciSenha(string email);
        bool verificarTelefone(string email, string senha, string telefone);
        void DesativarUsuario(string email, string senha);
        void AtualizarToken(string email, string token);

    }
}
