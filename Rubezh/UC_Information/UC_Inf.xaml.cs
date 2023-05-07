using Rubezh.Add_Inf;
using Rubezh.Wind;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rubezh.UC_Information
{
    /// <summary>
    /// Логика взаимодействия для UC_Inf.xaml
    /// </summary>
    public partial class UC_Inf : UserControl
    {
        public Rubezh_Company Branch { get; set; }
        public string BranchName { get; set; } // новое свойство
        private string _branchName;
        public static bool HideButtons { get; set; } = false;

        Rubezh_dbEntities _context = new Rubezh_dbEntities();
        List<Rubezh_Catalog> _list = new List<Rubezh_Catalog>();


        public UC_Inf(string branchName, Rubezh_dbEntities dbEntities)
        {
            InitializeComponent();

            

            this._context = dbEntities;
            _branchName = branchName;

            var result = from item in _context.Rubezh_Catalog
                         where item.Rubezh_Catalog_Name_Company.Contains(_branchName)
                         select item;
            LV_Product_.ItemsSource = result.ToList();
        }

        public void Update_Product()
        {
            var result = from item in _context.Rubezh_Catalog
                         where item.Rubezh_Catalog_Name_Company.Contains(_branchName)
                         select item;
            LV_Product_.ItemsSource = result.ToList();
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void New_Product_Click(object sender, RoutedEventArgs e)
        {
            Add_Product add_Product = new Add_Product(_branchName, _context, this);
            add_Product.ShowDialog();
        }

        private void Inf_Product_Click(object sender, RoutedEventArgs e)
        {
            Inf_Product inf_Product = new Inf_Product(_branchName, _context, this, sender);
            inf_Product.ShowDialog();
        }
        private void Inf_Del_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext as Rubezh_Catalog;
            _context.Rubezh_Catalog.Remove(item);
            _context.SaveChanges();
            Update_Product();

        }
    }
}
