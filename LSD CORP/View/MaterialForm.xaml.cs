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
    /// Логика взаимодействия для MaterialForm.xaml
    /// </summary>
    public partial class MaterialForm : Window, INotifyPropertyChanged
    {
        private Material material;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string? prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public Material Material { get => material; set { material = value; Signal(); } }
        public MaterialForm()
        {
            InitializeComponent();
            Material = new();
            DataContext = this;
        }

        private async void SaveClick(object sender, RoutedEventArgs e)
        {
            if (await DataBase.Instance.AddMat(Material))
                BackClick(sender, e);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
