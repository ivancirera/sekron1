using sekron1.infra;
using sekron1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace sekron1.Services
{
    public class NotificationService : ApiController, INotification
    {

        private dbSekronEntities1 db = new dbSekronEntities1();

        public List<string> getUidsFirebaseAll()
        {

            List<tb_usuario> lista = new List<tb_usuario>();
            lista.AddRange(db.tb_usuario.Where(s => s.uidFirebase != null));

            List<string> listaUids = new List<string>();

            for (int i = 0; i < lista.Count; i++)
            {
                listaUids.Add(lista.ElementAt(i).uidFirebase);   
            }

            return listaUids;
        }

        public List<string> getUidsFirebaseOrange()
        {

            List<tb_usuario> lista = new List<tb_usuario>();
            lista.AddRange(db.tb_usuario.Where(s => s.uidFirebase != null && s.notificacaoLaranja == true));

            List<string> listaUids = new List<string>();

            for(int i = 0; i < lista.Count; i++)
            {
                listaUids.Add(lista.ElementAt(i).uidFirebase);
            }

            return listaUids;
        }

        public List<string> getUidsFirebaseRed()
        {
            List<tb_usuario> lista = new List<tb_usuario>();
            lista.AddRange(db.tb_usuario.Where(s => s.uidFirebase != null && s.notificacaoVermelha == true));

            List<string> listaUids = new List<string>();

            for (int i = 0; i < lista.Count; i++)
            {
                listaUids.Add(lista.ElementAt(i).uidFirebase);
            }

            return listaUids;
        }

        public List<string> getUidsFirebaseYellow()
        {
            List<tb_usuario> lista = new List<tb_usuario>();
            lista.AddRange(db.tb_usuario.Where(s => s.uidFirebase != null && s.notificacaoAmarela == true));

            List<string> listaUids = new List<string>();

            for (int i = 0; i < lista.Count; i++)
            {
                listaUids.Add(lista.ElementAt(i).uidFirebase);
            }

            return listaUids;
        }
    }
}