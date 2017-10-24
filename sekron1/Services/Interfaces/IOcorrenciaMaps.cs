using sekron1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace sekron1.Services.Interfaces
{
    interface IOcorrenciaMaps
    {

        List<OcorrenciaMapsModel> listOcorrencias(string dataAtual);  

    }
}
