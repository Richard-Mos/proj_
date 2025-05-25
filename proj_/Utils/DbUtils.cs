using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using proj_.Models;

namespace proj_.Utils
{
    public static class DbUtils
    {
        public static myDBContext db;

        static DbUtils()
        {
            try
            {
               db = new myDBContext();
            }
            catch
            {
                MessageBox.Show("Не получилось подключиться к базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
