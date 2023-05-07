using Rubezh.Add_Inf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rubezh.UC_Information
{
    /// <summary>
    /// Логика взаимодействия для UC_Price.xaml
    /// </summary>
    public partial class UC_Price : UserControl
    {

        Rubezh_dbEntities _context = new Rubezh_dbEntities();
        List<Rubezh_Price_List> _list = new List<Rubezh_Price_List>();
        Rubezh_dbEntities dbEntities;

        public UC_Price(Rubezh_dbEntities dbEntities)
        {
            InitializeComponent();
            this._context = dbEntities;
            LV_Product_.ItemsSource = _context.Rubezh_Price_List.OrderBy(t => t.Rubezh_Price_List_id).ToList();
        }

        public void Update_Product()
        {
            _list = _context.Rubezh_Price_List.ToList();
            LV_Product_.ItemsSource = _list;
        }


        private void New_Price_Click(object sender, RoutedEventArgs e)
        {
            Add_Price add_Price = new Add_Price(_context, this);
            add_Price.ShowDialog();
        }

        private void Inf_Price_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую нажали
            Button button = sender as Button;

            // Получаем элемент списка, связанный с кнопкой
            var item = button.DataContext as Rubezh_Price_List;

            // Получаем данные файла из базы данных

            string fileName = item.Rubezh_Price_List_Name + ".pdf";
            byte[] fileData = item.Rubezh_Price_List_File;

            // Если данные файла есть, то открываем файл
            if (fileData != null && fileData.Length > 0)
            {
                // Получаем путь к рабочему столу
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                // Создаем путь для сохранения файла на рабочем столе
                string filePath = System.IO.Path.Combine(desktopPath, fileName);

                // Сохраняем файл на рабочий стол
                File.WriteAllBytes(filePath, fileData);
            }
        }

        private void Inf_Del_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext as Rubezh_Price_List;
            _context.Rubezh_Price_List.Remove(item);
            _context.SaveChanges();
            Update_Product();

        }
    }
}
