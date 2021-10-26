using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework11
{
    interface InterfaceForConsultant
    {       
        void ConsultantChange(ObservableCollection<Client> clients, int index, long PhoneNumber,
            string DateTime,string DataChanged,string TypeOfChanged,string WhoChanged);
        
    }
}
