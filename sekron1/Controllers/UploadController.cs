using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

namespace sekron1.Controllers
{
    public class UploadController : ApiController
    {

        [HttpPost()]
        public HttpResponseMessage UploadFotoPerfil()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Shared/Perfil/");

            string returnPath = "";

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {

                    // SAVE THE FILES IN THE FOLDER.
                    hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                    iUploadedCnt = iUploadedCnt + 1;
                    returnPath = sPath + Path.GetFileName(hpf.FileName);

                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
               
                return Request.CreateResponse(HttpStatusCode.OK, returnPath);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro ao realizar upload");
            }
        }

        [HttpPost()]
        public HttpResponseMessage UploadFotoCarro()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Shared/Carro/");

            string returnPath = "";

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // SAVE THE FILES IN THE FOLDER.
                    hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                    iUploadedCnt = iUploadedCnt + 1;
                    returnPath = sPath + Path.GetFileName(hpf.FileName);
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {

                return Request.CreateResponse(HttpStatusCode.OK, returnPath);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro ao realizar upload");
            }
        }

        [HttpPost()]
        public HttpResponseMessage UploadAnexoOcorrencia()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Shared/Ocorrencias/");

            string returnPath = "";

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // SAVE THE FILES IN THE FOLDER.
                    hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                    iUploadedCnt = iUploadedCnt + 1;
                    returnPath = sPath + Path.GetFileName(hpf.FileName);
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, returnPath);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro ao realizar upload");
            }
        }
    }
}