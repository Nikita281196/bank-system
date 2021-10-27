using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework11
{
    class Consultant:InterfaceForConsultant
    {
        #region Автосвойства
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public long PhoneNumber { get; set; }
        public string PassData { get; set; }
        public string DateTime { get; set; }
        public string DataChanged { get; set; }
        public string TypeOfChanged { get; set; }
        public string WhoChanged { get; set; }

        public string passData
        {
            get 
            {
                StringBuilder stringBuilder = new StringBuilder(PassData.ToString());
                if (PassData.ToString() != String.Empty)
                {
                    for (int i = 0; i < PassData.ToString().Length; i++)
                    {
                        stringBuilder.Replace(stringBuilder[i], '*');
                    }
                }
                return stringBuilder.ToString();
            }
        }
        #endregion

        #region Конструкторы
        public Consultant(string Surname,
            string Name,
            string Patronymic,
            long PhoneNumber,
            string PassData,
            string DateTime,
            string DataChanged,
            string TypeOfChanged,
            string WhoChanged)         
        {
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.PhoneNumber = PhoneNumber;
            this.PassData = PassData;
            this.DateTime = DateTime;
            this.DataChanged = DataChanged;
            this.TypeOfChanged = TypeOfChanged;
            this.WhoChanged = WhoChanged;
        }
        public Consultant(string Surname, string Name, string Patronymic, long PhoneNumber, string PassData):
            this(Surname,Name,Patronymic,PhoneNumber,PassData,String.Empty,String.Empty,String.Empty,String.Empty)
        {
            
        }
        #endregion

        /// <summary>
        /// Метод изменения данных для консультанта
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="index"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="DateTime"></param>
        /// <param name="DataChanged"></param>
        /// <param name="TypeOfChanged"></param>
        /// <param name="WhoChanged"></param>
        public void ConsultantChange(ObservableCollection<Client> clients, int index, long PhoneNumber, 
            string DateTime, string DataChanged, string TypeOfChanged, string WhoChanged)
        {
            Client client = clients[index];
            client.PhoneNumber = PhoneNumber;
            client.DateTime = DateTime;
            client.DataChanged = DataChanged;
            client.TypeOfChanged = TypeOfChanged;
            client.WhoChanged = WhoChanged;
            clients[index] = client;
        }

        /// <summary>
        /// Метод сохранения данных
        /// </summary>
        /// <param name="clients"></param>
        public void Save(ObservableCollection<Client> clients)
        {
            File.Delete(@"Notes.csv");
            for (int i = 0; i < clients.Count; i++)
            {
                string temp = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                                        clients[i].Surname,
                                        clients[i].Name,
                                        clients[i].Patronymic,
                                        clients[i].PhoneNumber,
                                        clients[i].PassData,
                                        clients[i].DateTime,
                                        clients[i].DataChanged,
                                        clients[i].TypeOfChanged,
                                        clients[i].WhoChanged);
                File.AppendAllText(@"Notes.csv", $"{temp}\n");
            }
        }
    }
}
