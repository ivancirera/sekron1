using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sekron1
{
    public class LoginSecure
    {
        private dbSekronEntities1 db = new dbSekronEntities1();

        public bool Login(string email, string senha)
        {

            var teste = db.tb_login.Any(user => user.email.Equals(email) && user.senha.Equals(senha));

            return teste;
        }
    }
}