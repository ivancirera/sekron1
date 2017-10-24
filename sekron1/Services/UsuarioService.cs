using sekron1.infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace sekron1.Services
{
    public class UsuarioService : ApiController, IUsuario
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public tb_usuario Add(tb_usuario usuario)
        {
            tb_usuario user = db.tb_usuario.Add(usuario);
            db.SaveChanges();
            return user;
        }

        public tb_usuario Get(long id)
        {
            tb_usuario user = db.tb_usuario.Find(id);
            return user;
        }

        public IEnumerable<tb_usuario> GetAll()
        {
            return db.tb_usuario.ToList();
        }

        public tb_usuario Remove(long id)
        {
            tb_usuario user = db.tb_usuario.Find(id);
            db.tb_usuario.Remove(user);
            db.SaveChanges();
            return user;
        }

        public tb_usuario SalvarConfiguracoes(long codUsuario, string cepCasa, string enderecoCasa, string cepTrabalho, string enderecoTrabalho)
        {
            tb_usuario user = db.tb_usuario.Find(codUsuario);

            if (user != null)
            {
                user.cepCasa = cepCasa;
                user.enderecoCasa = enderecoCasa;
                user.cepTrabalho = cepTrabalho;
                user.enderecoTrabalho = enderecoTrabalho;
                Update(user);
            }

            return user;
        }

        public string Update(tb_usuario usuario)
        {
            string retorno = "";
            tb_usuario existingUser = db.tb_usuario.Where(s => s.codUsuario == usuario.codUsuario).FirstOrDefault<tb_usuario>();

            if (existingUser != null)
            {
                existingUser.codLogin = usuario.codLogin;
                existingUser.nome = usuario.nome;
                existingUser.dataNascimento = usuario.dataNascimento;
                existingUser.cpf = usuario.cpf;
                existingUser.rg = usuario.rg;
                existingUser.celular = usuario.celular;
                existingUser.telefone = usuario.telefone;
                existingUser.sexo = usuario.sexo;
                existingUser.cep = usuario.cep;
                existingUser.rua = usuario.rua;
                existingUser.complemento = usuario.complemento;
                existingUser.numero = usuario.numero;
                existingUser.cidade = usuario.cidade;
                existingUser.estado = usuario.estado;
                existingUser.ativo = usuario.ativo;
                existingUser.dataCadastro = usuario.dataCadastro;
                existingUser.pago = usuario.pago;
                existingUser.voluntario = usuario.voluntario;
                existingUser.fotoPerfil = usuario.fotoPerfil;
                existingUser.uidFirebase = usuario.uidFirebase;
                existingUser.notificacaoAmarela = usuario.notificacaoAmarela;
                existingUser.notificacaoLaranja = usuario.notificacaoLaranja;
                existingUser.notificacaoVermelha = usuario.notificacaoVermelha;
                existingUser.enderecoCasa = usuario.enderecoCasa;
                existingUser.enderecoTrabalho = usuario.enderecoTrabalho;
                existingUser.carroMarca = usuario.carroMarca;
                existingUser.carroModelo = usuario.carroModelo;
                existingUser.carroPlaca = usuario.carroPlaca;
                existingUser.carroFoto = usuario.carroFoto;
                existingUser.carroCor = usuario.carroCor;
                

                db.SaveChanges();

                retorno = "Dados do usuario alterados com sucesso";

            }
            else
            {
                retorno = "Usuario não encontrado";
            }

            return retorno;
        }

        public tb_usuario UpdateNotification(long id, bool red, bool orange, bool yellow)
        {
            tb_usuario userNot = db.tb_usuario.Where(s => s.codUsuario == id).FirstOrDefault<tb_usuario>();

            userNot.notificacaoVermelha = red;
            userNot.notificacaoLaranja = orange;
            userNot.notificacaoAmarela = yellow;

            db.SaveChanges();

            return userNot;

        }
    }
}