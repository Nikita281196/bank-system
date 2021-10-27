using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework11
{
    interface InterfaceForManager
    {
        void ManagerChange(ObservableCollection<Client> clients, int index, string Surname, string Name, string Patronymic,
            long PhoneNumber, string PassData, string DateTime, string DataChanged, string TypeOfChanged, string WhoChanged);
    }
}
