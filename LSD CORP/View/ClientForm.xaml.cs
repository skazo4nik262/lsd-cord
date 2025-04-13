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
    /// Логика взаимодействия для ClientForm.xaml
    /// </summary>
    public partial class ClientForm : Window, INotifyPropertyChanged
    {
        private Client client;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string? prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public Client Client { get => client; set { client = value; Signal(); } }
        public ClientForm()
        {
            InitializeComponent();
            Client = new();
            DataContext = this;
        }

        private async void SaveClick(object sender, RoutedEventArgs e)
        {
            if (await DataBase.Instance.AddClient(Client))
                BackClick(sender, e);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
