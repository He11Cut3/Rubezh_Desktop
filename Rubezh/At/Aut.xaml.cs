using Rubezh.UC_Information;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rubezh.At
{
    /// <summary>
    /// Логика взаимодействия для Aut.xaml
    /// </summary>
    public partial class Aut : Window
    {
        private string text = String.Empty;
        Rubezh_dbEntities _context = new Rubezh_dbEntities();

        public Aut()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void pnlControlBar_MouseLeftButtonDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DragMove();
        }
        private void Avt_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;
            var user = _context.Rubezh_Users.FirstOrDefault(u => u.Rubezh_Users_Login == login && u.Rubezh_Users_Password == password);
            if (user == null)
            {
                System.Windows.MessageBox.Show("Пользователь не найден");
            }
            else
            {

                Main win_Main = new Main();
                this.Close();
                win_Main.ShowDialog();
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;
            // Проверяем, есть ли пользователь с таким же логином
            var existingUser = _context.Rubezh_Users.FirstOrDefault(u => u.Rubezh_Users_Login == login);
            if (existingUser != null)
            {
                // Пользователь с таким же логином уже существует
                System.Windows.MessageBox.Show("Пользователь уже существует");
            }
            else
            {
                var newUser = new Rubezh_Users
                {
                    Rubezh_Users_Login = login,
                    Rubezh_Users_Password = password,
                    Rubezh_Users_Level = "-",
                };
                _context.Rubezh_Users.Add(newUser);
                _context.SaveChanges();
                System.Windows.MessageBox.Show("Пользователь успешно добавлен");
            }
        }
    }
}
