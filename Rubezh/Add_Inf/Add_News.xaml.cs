using Rubezh.UC_Information;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Add_News.xaml
    /// </summary>
    public partial class Add_News : Window
    {
        private Rubezh_dbEntities _context;
        private UC_News _uc;

        public Add_News(Rubezh_dbEntities dbEntities, UC_News uC_Inf)
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
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите добавить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                _context.Rubezh_News.Add(new Rubezh_News()
                {
                    Rubezh_News_Name = Name.Text,
                    Rubezh_News_Date = DateTime.Now.ToString("dd.MM.yyyy"),
                    Rubezh_News_Text = Text_inf.Text,
                });
                _context.SaveChanges();
                _uc.Update_Product();
                this.Close();
            }
        }
    }
}
