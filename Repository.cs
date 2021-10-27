using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Homework11
{
    
    class Repository
    {

        public ObservableCollection<Client> Сlients { get; set; }
        Manager manager;
        Consultant consultant;


        public Repository(ObservableCollection<Client> Clients)
        {
            this.Сlients = Clients;
        }

        /// <summary>
        /// Метод заполнения коллекции типа Client для менеджера
        /// </summary>
        /// <returns></returns>
        /// 
        public ObservableCollection<Client> FillClientForManager()
        {
            this.Сlients = new ObservableCollection<Client>();
            using (StreamReader sr = new StreamReader(@"Notes.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string[] vs = sr.ReadLine().Split(',');
                    manager = new Manager(vs[0], vs[1], vs[2], long.Parse(vs[3]), vs[4], vs[5], vs[6], vs[7], vs[8]);

                    Сlients.Add(new Client(manager.Surname,
                        manager.Name,
                        manager.Patronymic,
                        manager.PhoneNumber,
                        manager.PassData.ToString(),
                        manager.DateTime,
                        manager.DataChanged,
                        manager.TypeOfChanged,
                        manager.WhoChanged));
                }
            }
            return Сlients;
        }

        /// <summary>
        /// Метод заполнения коллекции типа Client для консультанта
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Client> FillClientForConsultant()
        {
            Сlients = new ObservableCollection<Client>();
            using (StreamReader sr = new StreamReader(@"Notes.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string[] vs = sr.ReadLine().Split(',');
                    consultant = new Consultant(vs[0], vs[1], vs[2], long.Parse(vs[3]), vs[4], vs[5], vs[6], vs[7], vs[8]);

                    Сlients.Add(new Client(consultant.Surname,
                        consultant.Name,
                        consultant.Patronymic,
                        consultant.PhoneNumber,
                        consultant.passData,
                        consultant.DateTime,
                        consultant.DataChanged,
                        consultant.TypeOfChanged,
                        consultant.WhoChanged));
                }
            }
            return Сlients;
        }

        /// <summary>
        /// Метод добавления клиента
        /// </summary>
        /// <param name="Surname"></param>
        /// <param name="NameClient"></param>
        /// <param name="Patronymic"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="PassportData"></param>
        public void AddClient(string Surname,string NameClient,string Patronymic, string PhoneNumber, string PassportData)
        {
            if (CheckFieldForManager(Surname, NameClient, Patronymic, PhoneNumber, PassportData))
            {
                Сlients.Add(new Client(Surname, NameClient, Patronymic,
                    long.Parse(PhoneNumber), PassportData));
                manager.Save(Сlients);
            }
        }

        /// <summary>
        /// Проверка данных на корректный тип у менеджера
        /// </summary>
        public bool CheckFieldForManager(string Surname, string NameClient, string Patronymic, string PhoneNumber, string PassportData)
        {
            bool chechFiled = false;
            long check;
            if (PassportData == String.Empty | PhoneNumber == String.Empty |
                Surname == String.Empty | NameClient == String.Empty | Patronymic == String.Empty)
                MessageBox.Show("Заполните пустые поля");
            else if (!long.TryParse(PassportData, out check) | !long.TryParse(PhoneNumber, out check))
                MessageBox.Show("Неверное число в поле ввода!");
            else
            {
                chechFiled = true;
            }
            return chechFiled;
        }

        /// <summary>
        /// Проверка данных для изменения менеджером
        /// </summary>
        /// <param name="DataBeforeChange"></param>
        /// <param name="DataAfterChange"></param>
        /// <param name="index"></param>
        /// <param name="Surname"></param>
        /// <param name="NameClient"></param>
        /// <param name="Patronymic"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="PassportData"></param>
        public void ChangeManager(string[] DataBeforeChange, string[] DataAfterChange, int index, string Surname, string NameClient, string Patronymic, string PhoneNumber, string PassportData)
        {
            try
            {
                if (CheckFieldForManager(Surname, NameClient, Patronymic, PhoneNumber, PassportData))
                {
                    ManagerDataUpdate(DataBeforeChange, DataAfterChange, index,  Surname,  NameClient,  Patronymic,  PhoneNumber,  PassportData);
                    manager.Save(Сlients);
                }

            }
            catch (Exception error)
            {
                if (error != null)
                {
                    MessageBox.Show("Выберите клиента данные которого хотите изменить",
                    "Внимание", MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Метод изменения данных менеджером
        /// </summary>
        public void ManagerDataUpdate(string[] DataBeforeChange, string[] DataAfterChange,int index, string Surname, string NameClient, string Patronymic, string PhoneNumber, string PassportData)
        {
                                   
            string[] Field = new string[] { "Фамилия", "Имя", "Отчество", "Номер телефона", "Паспортные данные" };
            string[] TypeChange = new string[] { "Удалено", "Изменено" };
            string TypeOfChange = "";
            string DataChanged = "";
            for (int i = 0; i < DataBeforeChange.Length; i++)
            {
                if (DataBeforeChange[i] != DataAfterChange[i])
                {
                    DataChanged += $"{Field[i]} ";
                    if (DataAfterChange[i] == String.Empty) TypeOfChange += $"{TypeChange[0]} ";
                    else TypeOfChange += $"{TypeChange[1]} ";
                }
            }
            string Date = DateTime.Now.ToShortDateString();
            string Time = DateTime.Now.ToShortTimeString();
            manager.ManagerChange(Сlients, index, Surname, NameClient, Patronymic,
                long.Parse(PhoneNumber), PassportData, $"{Date} {Time}", DataChanged, TypeOfChange, "Менеджер");
        }

        /// <summary>
        /// Проверка данных на корректный тип у консультанта
        /// </summary>
        public bool CheckFieldForConsultant(string PhoneNumber)
        {
            bool chechFiled = false;
            long check;
            if (PhoneNumber == String.Empty)
                MessageBox.Show("Заполните пустое поле: \nНомер телефона");
            else if (!long.TryParse(PhoneNumber, out check))
                MessageBox.Show("Неверное число в поле ввода!");
            else
            {
                chechFiled = true;
            }
            return chechFiled;
        }     

        /// <summary>
        /// Проверка данных на изменение консультантом
        /// </summary>
        /// <param name="PhoneNumberBeforeChange"></param>
        /// <param name="PhoneNumberAfterChange"></param>
        /// <param name="index"></param>
        public void ChangeConsultant(string PhoneNumberBeforeChange, string PhoneNumberAfterChange, int index)
        {
            try
            {

                if (CheckFieldForConsultant(PhoneNumberAfterChange))
                {
                    ConsultantDataUpdate(PhoneNumberBeforeChange, PhoneNumberAfterChange, index);
                    consultant.Save(Сlients);
                }

            }
            catch (Exception error)
            {
                if (error != null)
                {
                    MessageBox.Show("Выберите клиента, данные которого вы хотите изменить",
                        "Внимание",
                        MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Метод изменения данных консультантом
        /// </summary>
        public void ConsultantDataUpdate(string PhoneNumberBeforeChange, string PhoneNumberAfterChange, int index)
        {
            if (PhoneNumberBeforeChange == String.Empty)
            {
                MessageBox.Show("Вы не можете изменить пустое поле",
                                "Внимание",
                                MessageBoxButton.OK);
            }
            else
            {
                string Field = "Номер телефона";
                string[] TypeChange = new string[] { "Удалено", "Изменено" };
                string TypeOfChange = "";
                string DataChanged = "";
                if (PhoneNumberBeforeChange != PhoneNumberAfterChange)
                {
                    DataChanged += $"{Field}";
                    if (PhoneNumberAfterChange == String.Empty) TypeOfChange += $"{TypeChange[0]} ";
                    else TypeOfChange += $"{TypeChange[1]} ";
                }
                string Date = DateTime.Now.ToShortDateString();
                string Time = DateTime.Now.ToShortTimeString();
                consultant.ConsultantChange(Сlients, index, long.Parse(PhoneNumberAfterChange),
                    $"{Date} {Time}", DataChanged, TypeOfChange, "Консультант");
            }

        }
    }
}
