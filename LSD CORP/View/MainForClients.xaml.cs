using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LSD_CORP.View
{
    /// <summary>
    /// Логика взаимодействия для MainForClients.xaml
    /// </summary>
    public partial class MainForClients : Window, INotifyPropertyChanged
    {
        private List<Client> clients;
        private Client selectedClient;

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Client> Clients { get => clients; set { clients = value; Signal(); } }
        public Client SelectedClient { get => selectedClient; set { selectedClient = value; Signal(); } }

        private void Signal([CallerMemberName] string? prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public MainForClients()
        {
            InitializeComponent();
            GetAll();
            this.DataContext = this;
        }

        private async Task GetAll()
        {
            Clients = await DataBase.Instance.GetAllClients();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            new ClientAddForm().Show();
            Close();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            new ClientAddForm(SelectedClient).Show();
            Close();
        }

        private async void DelClick(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show("Вы уверенны?", "It will ban u))", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes || dialogResult == MessageBoxResult.OK)
            {
                await DataBase.Instance.DelClient(SelectedClient);
                Clients = await DataBase.Instance.GetAllClients();
            }
        }

        private async void DupeClick(object sender, RoutedEventArgs e)
        {
            await DataBase.Instance.DupeCl(SelectedClient);
            Clients = await DataBase.Instance.GetAllClients();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}