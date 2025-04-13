using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window, INotifyPropertyChanged
    {
        private User user;
        private MediaPlayer player;
        private HttpClient client;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string? prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public User User { get => user; set { user = value; Signal(); } }
        public LoginForm()
        {
            InitializeComponent();
            User = new();
            DataContext = this;
            player = new MediaPlayer();
            player.MediaFailed += (s, e) => MessageBox.Show("Error");
            var uri = new Uri(Environment.CurrentDirectory + "\\sound.mp3");
            player.Open(uri);
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5105/api/Login/");
        }
        private async void EnterClick(object sender, RoutedEventArgs e)
        {
            var resp = await client.PostAsJsonAsync("Login", User);
            var result = await resp.Content.ReadFromJsonAsync<bool>();
            if (result)
            {
                new MainWindow().Show();
                Close();
            }
            else AutoLog(new object(), new RoutedEventArgs());
            
        }

        private async void RegClick(object sender, RoutedEventArgs e)
        {
            if (await DataBase.Instance.Registraition(User))
            {
                MessageBox.Show("Успешно!");
            }
            else MessageBox.Show("Произошла ошибка!");
        }

        private void ExitClick(object sender, RoutedEventArgs e)
            => Close();

        private void AutoLog(object sender, RoutedEventArgs e)
        {

            player.Play();
            Thread.Sleep(10000);
            /*
            new MainWindow().Show();
            Close();
            */
        }
    }
}
