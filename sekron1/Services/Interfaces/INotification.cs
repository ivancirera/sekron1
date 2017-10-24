using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekron1.Services.Interfaces
{
    interface INotification
    {
        List<string> getUidsFirebaseAll();
        List<string> getUidsFirebaseYellow();
        List<string> getUidsFirebaseOrange();
        List<string> getUidsFirebaseRed();

    }
}
