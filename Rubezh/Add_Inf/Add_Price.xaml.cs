using Rubezh.UC_Information;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Rubezh.Add_Inf
{
    /// <summary>
    /// Логика взаимодействия для Add_Price.xaml
    /// </summary>
    public partial class Add_Price : Window
    {
        private Rubezh_dbEntities _context;
        private UC_Price _uc;

        public Add_Price(Rubezh_dbEntities dbEntities, UC_Price uC_Inf)
        {
            InitializeComponent();
            this._context = dbEntities;
            this._uc = uC_Inf;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|Excel Files (*.xlsx)|*.xlsx";

            if (openFileDialog.ShowDialog() == true)
            {
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите добавить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    string fileName = openFileDialog.SafeFileName;
                    byte[] fileContent = File.ReadAllBytes(openFileDialog.FileName);
                    var newFile = new Rubezh_Price_List
                    {
                        Rubezh_Price_List_Name = Name.Text,
                        Rubezh_Price_List_File = fileContent,
                    };
                    _context.Rubezh_Price_List.Add(newFile);
                    _context.SaveChanges();
                    _uc.Update_Product();
                    this.Close();
                }
            
            }
        }
    }
}
