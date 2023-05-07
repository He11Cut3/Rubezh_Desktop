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
using static System.Net.Mime.MediaTypeNames;

namespace Rubezh.Wind
{
    /// <summary>
    /// Логика взаимодействия для Inf_News.xaml
    /// </summary>
    public partial class Inf_News : Window
    {
        public Rubezh_Company Branch { get; set; }
        public string BranchName { get; set; } // новое свойство
        private string _branchName;

        private UC_News _uc;
        private Rubezh_News _rubezh;

        Rubezh_dbEntities _context = new Rubezh_dbEntities();
        List<Rubezh_News> _list = new List<Rubezh_News>();

        public Inf_News(string branchName, Rubezh_dbEntities dbEntities, UC_News uC_Inf, object o)
        {
            InitializeComponent();
            this._context = dbEntities;
            _branchName = branchName;
            _rubezh = (o as Button).DataContext as Rubezh_News;
            this._uc = uC_Inf;
            Name.Text = "Наименование: " + _rubezh.Rubezh_News_Name;
            Date.Text = "Дата: " + _rubezh.Rubezh_News_Date;
            Text.Text =  _rubezh.Rubezh_News_Text;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
