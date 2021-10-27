using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Homework11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Client> clients;
        Repository repositoryforConsultant;
        Repository repositoryforManager;

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
            repositoryforConsultant = new Repository(clients);

            clients = repositoryforConsultant.FillClientForConsultant();
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
            repositoryforManager = new Repository(clients);
          
            clients = repositoryforManager.FillClientForManager();
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
                repositoryforManager.AddClient(Surname.Text, NameClient.Text, Patronymic.Text,
                        PhoneNumber.Text, PassportData.Text);                             
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
                    string[] DataBeforeChange = new string[] { clients[dbClients.SelectedIndex].Surname, clients[dbClients.SelectedIndex].Name,
                                              clients[dbClients.SelectedIndex].Patronymic, clients[dbClients.SelectedIndex].PhoneNumber.ToString(),
                                              clients[dbClients.SelectedIndex].PassData.ToString()};
                    string[] DataAfterChange = new string[]{ Surname.Text, NameClient.Text, Patronymic.Text,
                                            PhoneNumber.Text, PassportData.Text};
                    
                    repositoryforManager.ChangeManager(DataBeforeChange, DataAfterChange, dbClients.SelectedIndex, Surname.Text, NameClient.Text, Patronymic.Text, PhoneNumber.Text, PassportData.Text);
                    
                    
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
                        repositoryforConsultant.ChangeConsultant(clients[dbClients.SelectedIndex].PhoneNumber.ToString(), PhoneNumber.Text, dbClients.SelectedIndex);                       
                        
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
        /// Метод сортировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Client_Sort_Button(object sender, RoutedEventArgs e)
        {
            var sortedClient = clients.OrderBy(i => i.Surname);
            dbClients.ItemsSource = sortedClient;
        }
    }
}
