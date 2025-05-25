using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace proj_
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public ObservableCollection<MaterialType>? MaterialTypeList { get; set; }
        public ObservableCollection<UnitType>? UnitTypeList { get; set; }

        public string? MaterialName { get; set; }
        public MaterialType? MaterialType { get; set; }
        public decimal? MaterialPrice { get; set; }
        public UnitType? UnitType { get; set; }
        public decimal? MaterialPackAmount { get; set; }
        public decimal? MaterialStorageAmount { get; set; }

        public decimal? MaterialMinAmount { get; set; }


        public string? WindowTitle { get; set; }
        private string Mode { get; set; }

        public AddWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.MaterialTypeList = new ObservableCollection<MaterialType> ( DbUtils.db.MaterialTypes.ToList() );
            this.UnitTypeList = new ObservableCollection<UnitType>(DbUtils.db.UnitTypes.ToList());

            this.MaterialName = "Пусто";
            this.MaterialType = this.MaterialTypeList?.FirstOrDefault();
            this.MaterialPrice = 0.00M;
            this.UnitType = this.UnitTypeList?.FirstOrDefault();
            this.MaterialPackAmount = 0.00M;
            this.MaterialStorageAmount = 0.00M;
            this.MaterialMinAmount = 0.00M;

            this.WindowTitle = "Окно добавления материала";
            this.Mode = "Создание";

        }

        public AddWindow(MaterialProxy selectedMaterial) : this()
        {


            this.WindowTitle = "Окно редактирования материала";
            this.Mode = "Редактирование";

            this.MaterialName = selectedMaterial.Material.MaterialName;
            this.MaterialType = selectedMaterial.Material.MaterialType;
            this.MaterialPrice = selectedMaterial.Material.MaterialPrice;
            this.UnitType = selectedMaterial.Material.UnitType;
            this.MaterialPackAmount = selectedMaterial.Material.MaterialPackAmount;
            this.MaterialStorageAmount = selectedMaterial.Material.MaterialStorageAmount;
            this.MaterialMinAmount = selectedMaterial.Material.MaterialMinAmount;


        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (this.MaterialName == "Пусто" || string.IsNullOrEmpty(this.MaterialName) )
            {
                MessageBox.Show("Проверьте корректность имени", "Ошибка имени", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (this.MaterialPrice < 0)
            {
                MessageBox.Show("Цена не может быть отрицательной", "Ошибка цены", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (this.MaterialMinAmount < 0)
            {
                MessageBox.Show("Минимальное количество не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ( this.Mode == "Создание")
            {
                if ( DbUtils.db.Materials.Where(m => m.MaterialName == this.MaterialName).Any() )
                {
                    MessageBox.Show("Данный материал уже существует", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newMaterial = new Material()
                {
                    MaterialName = this.MaterialName,
                    MaterialPrice = this.MaterialPrice,
                    MaterialTypeId = this.MaterialType.MaterialTypeId,
                    MaterialPackAmount = this.MaterialPackAmount,
                    MaterialStorageAmount = this.MaterialStorageAmount,
                    MaterialMinAmount =this.MaterialMinAmount,
                    UnitTypeId = this.UnitType.UnitTypeId

                };

                Debug.WriteLine($"{this.MaterialMinAmount}");

                try
                {
                    DbUtils.db.Materials.Add(newMaterial);
                    DbUtils.db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Не получилось добавить материал", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            else
            {
                var editedMaterial = DbUtils.db.Materials.Where(m => m.MaterialName == this.MaterialName).FirstOrDefault();

                editedMaterial.MaterialName = this.MaterialName;
                editedMaterial.MaterialPrice = this.MaterialPrice;
                editedMaterial.MaterialTypeId = this.MaterialType.MaterialTypeId;
                editedMaterial.MaterialPackAmount = this.MaterialPackAmount;
                editedMaterial.MaterialStorageAmount = this.MaterialStorageAmount;
                editedMaterial.MaterialMinAmount = this.MaterialMinAmount;
                editedMaterial.UnitTypeId = this.UnitType.UnitTypeId;


                try
                {
                    DbUtils.db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Не получилось обновить данные о материале", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
