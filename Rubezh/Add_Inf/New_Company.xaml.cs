using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для New_Company.xaml
    /// </summary>
    public partial class New_Company : Window
    {
        private Rubezh_dbEntities _context;
        private Main _wm;

        public New_Company(Rubezh_dbEntities rubezh_DbEntities, Main main)
        {
            InitializeComponent();
            this._context = rubezh_DbEntities;
            this._wm = main;
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Company_Click(object sender, RoutedEventArgs e)
        {

            if ((MessageBox.Show("Вы уверены, что хотите добавить отдел?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                var branchName = Name.Text;
                var branch = _context.Rubezh_Company.FirstOrDefault(b => b.Rubezh_Company_Name == branchName);

                if (branch != null)
                {
                    MessageBox.Show("Данная компания уже существует");
                }
                else
                {
                    _context.Rubezh_Company.Add(new Rubezh_Company()
                    {
                        Rubezh_Company_Name = Name.Text,
                    });


                    _context.SaveChanges();
                    _wm.Button_Refr(branchName);
                    this.Close();
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
