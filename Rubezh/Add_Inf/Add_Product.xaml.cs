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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rubezh.Add_Inf
{
    /// <summary>
    /// Логика взаимодействия для Add_Product.xaml
    /// </summary>
    public partial class Add_Product : Window
    {
        private Rubezh_dbEntities _context;
        private UC_Inf _uc;
        public Rubezh_Company Branch { get; set; }
        public string BranchName { get; set; } // новое свойство
        private string _branchName;

        public Add_Product(string branchName, Rubezh_dbEntities dbEntities, UC_Inf uC_Inf)
        {
            InitializeComponent();
            _branchName = branchName;
            this._context = dbEntities;
            this._uc = uC_Inf;  
            _branchName = branchName;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void Image_Download_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
            }
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите добавить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    _context.Rubezh_Catalog.Add(new Rubezh_Catalog()
                    {
                        Rubezh_Catalog_Name_Company = _branchName,
                        Rubezh_Catalog_Description = Descrip.Text,
                        Rubezh_Catalog_Image = imageBytes,
                        Rubezh_Catalog_Name = Name.Text,
                        Rubezh_Catalog_Feature = Char.Text,
                        Rubezh_Catalog_Link = Link.Text,
                    });
                    _context.SaveChanges();
                    _uc.Update_Product();
                    this.Close();
                }
            }
            
        }

        
    }
}
