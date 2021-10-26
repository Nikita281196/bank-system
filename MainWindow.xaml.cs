using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Homework11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Client> clients;
        Manager manager;
        Consultant consultant;
        
        public MainWindow()
        {
            InitializeComponent();           
        }

        /// <summary>
        /// Метод, если нажата кнопка Консультант
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Consultant_Checked(object sender, RoutedEventArgs e)
        {
            clients = new ObservableCollection<Client>();
            using (StreamReader sr = new StreamReader(@"Notes.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string[] vs = sr.ReadLine().Split(',');                    
                    consultant = new Consultant(vs[0], vs[1], vs[2], long.Parse(vs[3]), vs[4], vs[5], vs[6], vs[7], vs[8]);

                    clients.Add(new Client(consultant.Surname,
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
            dbClients.ItemsSource = clients;
        }

        /// <summary>
        /// Метод если нажата кнопка менеджер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_Checked(object sender, RoutedEventArgs e)
        {
            clients = new ObservableCollection<Client>();
            using (StreamReader sr = new StreamReader(@"Notes.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string[] vs = sr.ReadLine().Split(',');
                    manager = new Manager(vs[0], vs[1], vs[2], long.Parse(vs[3]), vs[4], vs[5], vs[6], vs[7], vs[8]);
                    
                    clients.Add(new Client(manager.Surname,
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
            dbClients.ItemsSource = clients;
        }

        /// <summary>
        /// Метод добавления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Client_Button(object sender, RoutedEventArgs e)
        {
            if (ManagerCheck.IsChecked==true)
            {
                string temp = String.Format(
                        Surname.Text,
                        NameClient.Text,
                        Patronymic.Text,
                        PhoneNumber.Text,
                        PassportData.Text);
                if (temp != String.Empty)
                {
                    clients.Add(new Client(Surname.Text,
                        NameClient.Text,
                        Patronymic.Text,
                        long.Parse(PhoneNumber.Text),
                        PassportData.Text));
                    manager.Save(clients);
                }
                else
                    MessageBox.Show("Заполните хотя бы одно поле", "Внимание", MessageBoxButton.OK);
            }
            else if (ConsultantCheck.IsChecked==true)
                MessageBox.Show("Выберите верный уровень доступа", "Внимание", MessageBoxButton.OK);            
            else
                MessageBox.Show("Не выбран уровень доступа", "Внимание", MessageBoxButton.OK);           
        }

        /// <summary>
        /// Метод изменения данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Client_Change_Button(object sender, RoutedEventArgs e)
        {
            if (ManagerCheck.IsChecked==true)
            {
                try
                {
                    CheckFieldForManager();
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
            else if (ConsultantCheck.IsChecked == true)
            {
                string temp = String.Format(Surname.Text, NameClient.Text, Patronymic.Text, PassportData.Text);
                if (temp != String.Empty)
                {
                    MessageBox.Show("Недостоточно прав на изменение!\n" +
                        "Вы можете изменить только номер телефона",
                        "Внимание", MessageBoxButton.OK);
                }
                else
                {
                    try
                    {
                        CheckFieldForConsultant();
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
            }
            else
            {
                MessageBox.Show("Выберите уровень доступа",
                                "Внимание",
                                MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Изменение данных менеджером
        /// </summary>
        public void ManagerDataUpdate()
        {
            string[] DataBeforeChange;
            string[] DataAfterChange;
            DataBeforeChange = new string[] { clients[dbClients.SelectedIndex].Surname, clients[dbClients.SelectedIndex].Name,
                                              clients[dbClients.SelectedIndex].Patronymic, clients[dbClients.SelectedIndex].PhoneNumber.ToString(), 
                                              clients[dbClients.SelectedIndex].PassData.ToString()};
            DataAfterChange = new string[]{ Surname.Text, NameClient.Text, Patronymic.Text,
                                            PhoneNumber.Text, PassportData.Text};
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
            manager.ManagerChange(clients, dbClients.SelectedIndex, Surname.Text, NameClient.Text, Patronymic.Text, 
                long.Parse(PhoneNumber.Text), PassportData.Text, $"{Date} {Time}", DataChanged, TypeOfChange,"Менеджер");
        }

        /// <summary>
        /// Изменение данных консультантом
        /// </summary>
        public void ConsultantDataUpdate()
        {
            if (clients[dbClients.SelectedIndex].PhoneNumber.ToString()==String.Empty)
            {
                MessageBox.Show("Вы не можете изменить пустое поле",
                                "Внимание",
                                MessageBoxButton.OK);
            }
            else
            {
                string PhoneNumberBeforeChange = clients[dbClients.SelectedIndex].PhoneNumber.ToString();
                string PhoneNumberAfterChange = PhoneNumber.Text;
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
                consultant.ConsultantChange(clients, dbClients.SelectedIndex, long.Parse(PhoneNumber.Text),
                    $"{Date} {Time}", DataChanged, TypeOfChange, "Консультант");
            }
            
        }

        /// <summary>
        /// Проверка данных на корректный тип у менеджера
        /// </summary>
        public void CheckFieldForManager()
        {
            long check;
            if (PassportData.Text == String.Empty & PhoneNumber.Text == String.Empty)
                MessageBox.Show("Заполните пустые поля: \nНомер телефона\nПаспортные данные");
            else if (!long.TryParse(PassportData.Text, out check) & !long.TryParse(PhoneNumber.Text, out check))
                MessageBox.Show("Неверное число в поле ввода!");
            else
            {
                ManagerDataUpdate();
                manager.Save(clients);
            }
        }

        /// <summary>
        /// Проверка данных на корректный тип у консультанта
        /// </summary>
        public void CheckFieldForConsultant()
        {
            long check;
            if (PhoneNumber.Text == String.Empty)
                MessageBox.Show("Заполните пустое поле: \nНомер телефона");
            else if (!long.TryParse(PhoneNumber.Text, out check))
                MessageBox.Show("Неверное число в поле ввода!");
            else
            {
                ConsultantDataUpdate();
                consultant.Save(clients);
            }
        }

    }
}
