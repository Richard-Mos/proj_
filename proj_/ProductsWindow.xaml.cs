using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using proj_.Models;
using proj_.Utils;
using Microsoft.EntityFrameworkCore;

namespace proj_
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {

        public string WindowTitle { get; set; }
        public ObservableCollection<MaterialProduct> DataSource { get; set; }

        public ProductsWindow( MaterialProxy selectedMaterial )
        {
            InitializeComponent();
            this.DataContext = this;

            this.DataSource = new ObservableCollection<MaterialProduct>( DbUtils.db.MaterialProducts
                                                                .Where(mp => mp.MaterialId == selectedMaterial.Material.MaterialId)
                                                                .Include(mp => mp.Product).ToList() );

            this.WindowTitle = $"Продукция из материала {selectedMaterial.Material.MaterialName}";

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
