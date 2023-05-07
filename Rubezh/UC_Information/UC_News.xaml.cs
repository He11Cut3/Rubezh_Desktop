using Rubezh.Add_Inf;
using Rubezh.Wind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для UC_News.xaml
    /// </summary>
    public partial class UC_News : UserControl
    {
        public Rubezh_Company Branch { get; set; }
        public string BranchName { get; set; } // новое свойство
        private string _branchName;

        Rubezh_dbEntities _context = new Rubezh_dbEntities();
        List<Rubezh_News> _list = new List<Rubezh_News>();

        public UC_News(string branchName, Rubezh_dbEntities dbEntities)
        {
            InitializeComponent();
            this._context = dbEntities;
            _branchName = branchName;
            LV_Product_.ItemsSource = _context.Rubezh_News.OrderBy(t => t.Rubezh_News_id).ToList();

        }
        public void Update_Product()
        {
            _list = _context.Rubezh_News.ToList();
            LV_Product_.ItemsSource = _list;
        }


        private void New_News_Click(object sender, RoutedEventArgs e)
        {
            Add_News add_News = new Add_News(_context, this);
            add_News.ShowDialog();  
        }

        private void Inf_News_Click(object sender, RoutedEventArgs e)
        {
            Inf_News inf_News = new Inf_News(_branchName, _context, this, sender); 
            inf_News.ShowDialog();
        }

        private void Inf_Del_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext as Rubezh_News;
            _context.Rubezh_News.Remove(item);
            _context.SaveChanges();
            Update_Product();

        }
    }
}
