using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using proj_.Models;
using proj_.Utils;
using Microsoft.EntityFrameworkCore;
using System.Windows.Resources;
using System.Diagnostics;

namespace proj_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        public string WindowTitle { get; set; }

        private ObservableCollection<MaterialProxy>? _dataSource;
        public ObservableCollection<MaterialProxy>? DataSource
        {
            get { return _dataSource; }
            set
            {
                if (value != null)
                {
                    _dataSource = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.DataSource)));

                }
            }

        }

        private MaterialProxy? _selectedItem;
        public MaterialProxy? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.SelectedItem)));

            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.UpdateData();

            this.WindowTitle = "Образ плюс | Окно материалов";

        }

        private void UpdateData()
        {
            var matList = new ObservableCollection<MaterialProxy>( DbUtils.db.Materials.Include(m => m.MaterialType).Include(m => m.MaterialProducts).Select(m => new MaterialProxy(m)).ToList());
            
            foreach (var material in matList)
            { 
                material.MaterialAmount = material.Material.MaterialProducts.Sum(mp => mp.MaterialAmount);
            }

            this.DataSource = matList;
            this.SelectedItem = null;
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddWindow();
            window.Owner = this;

            window.ShowDialog();

            this.UpdateData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedItem is MaterialProxy)
            {

                var window = new AddWindow(this.SelectedItem);
                window.Owner = this;

                window.ShowDialog();

            }
            else 
            {
                MessageBox.Show("Необходимо выделить материал для его редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Debug.WriteLine("Some edit stuff");
            this.UpdateData();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = mainListBox.SelectedItem as MaterialProxy;

            if (selectedItem == null)
            {
                MessageBox.Show("Необходимо выделить материал для отображения списка продукции", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var window = new ProductsWindow(selectedItem);
            window.Owner = this;

            window.ShowDialog();

            this.UpdateData();
        }


        
    }
}