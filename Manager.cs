using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework11
{
    class Manager:Consultant,InterfaceForManager
    {
        #region Конструктор              
        public Manager(string Surname, string Name, string Patronymic, long PhoneNumber, string PassData, 
            string DateTime,string DataChange,string TypeOfChanged,string WhoChanged) :
            base(Surname, Name, Patronymic, PhoneNumber, PassData, DateTime,DataChange,TypeOfChanged,WhoChanged)
        {

        }
        #endregion

        /// <summary>
        /// Метод изменения данных у менеджера
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="index"></param>
        /// <param name="Surname"></param>
        /// <param name="Name"></param>
        /// <param name="Patronymic"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="PassData"></param>
        /// <param name="DateTime"></param>
        /// <param name="DataChanged"></param>
        /// <param name="TypeOfChanged"></param>
        /// <param name="WhoChanged"></param>
        public void ManagerChange(ObservableCollection<Client> clients, int index, string Surname, string Name, string 
            Patronymic, long PhoneNumber, string PassData, string DateTime, string DataChanged, string TypeOfChanged, string WhoChanged)
        {
            Client client = clients[index];
            client.Surname = Surname;
            client.Name = Name;
            client.Patronymic = Patronymic;
            client.PhoneNumber = PhoneNumber;
            client.PassData = PassData;
            client.DateTime = DateTime;
            client.DataChanged = DataChanged;
            client.TypeOfChanged = TypeOfChanged;
            client.WhoChanged = WhoChanged;
            clients[index] = client;
        }
        
    }
}
