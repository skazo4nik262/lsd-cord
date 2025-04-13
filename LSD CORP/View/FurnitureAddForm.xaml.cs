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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace LSD_CORP.View
{
    /// <summary>
    /// Логика взаимодействия для FurnitureAddForm.xaml
    /// </summary>
    public partial class FurnitureAddForm : Window, INotifyPropertyChanged
    {
        private Furniture furniture;
        private List<Material> materials;
        private List<Client> clients;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string? prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public Furniture Furniture { get => furniture; set { furniture = value; Signal(); } }
        public List<Material> Materials { get => materials; set { materials = value; Signal(); } }
        public List<Client> Clients { get => clients; set { clients = value; Signal(); } }
        public FurnitureAddForm()
        {
            BaseForConstruct();
        }
        public FurnitureAddForm(Furniture furn)
        {
            BaseForConstruct();
            Furniture = furn;
        }

        private async void BaseForConstruct()
        {
            InitializeComponent();
            Materials = await DataBase.Instance.GetAllMaterials();
            Clients = await DataBase.Instance.GetAllClients();
            Furniture = new();
            DataContext = this;
        }

        private async void SaveClick(object sender, RoutedEventArgs e)
        {
            Furniture.SetMatCli();

            if (await DataBase.Instance.SearchFurById(Furniture.Id) == Furniture)
                if (await DataBase.Instance.UpdFur(Furniture))
                    BackClick(sender, e);
            if (await DataBase.Instance.AddFur(Furniture))
                BackClick(sender, e);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
