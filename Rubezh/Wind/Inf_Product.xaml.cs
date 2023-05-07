using Rubezh.UC_Information;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
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

namespace Rubezh.Wind
{
    /// <summary>
    /// Логика взаимодействия для Inf_Product.xaml
    /// </summary>
    public partial class Inf_Product : Window
    {
        public Rubezh_Company Branch { get; set; }
        public string BranchName { get; set; } // новое свойство
        private string _branchName;

        private UC_Inf _uc;
        private Rubezh_Catalog _rubezh;

        Rubezh_dbEntities _context = new Rubezh_dbEntities();
        List<Rubezh_Catalog> _list = new List<Rubezh_Catalog>();

        public Inf_Product(string branchName, Rubezh_dbEntities dbEntities, UC_Inf uC_Inf, object o)
        {
            InitializeComponent();
            this._context = dbEntities;
            _branchName = branchName;
            _rubezh = (o as Button).DataContext as Rubezh_Catalog;
            this._uc = uC_Inf;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(_rubezh.Rubezh_Catalog_Image);
            bitmapImage.EndInit();
            Image_S.Source = bitmapImage;
            Name.Text = "Имя: " + _rubezh.Rubezh_Catalog_Name;
            Description.Text = "Описание: " + _rubezh.Rubezh_Catalog_Description;
            Char.Text = "Характеристики: " + _rubezh.Rubezh_Catalog_Feature;
            string Lin= _rubezh.Rubezh_Catalog_Link;

            Hyperlink link = new Hyperlink(new Run(Lin));
            link.NavigateUri = new Uri(Lin);
            link.RequestNavigate += (sender, e) =>
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            };
            Link.Inlines.Add(link);


        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
    }
}
