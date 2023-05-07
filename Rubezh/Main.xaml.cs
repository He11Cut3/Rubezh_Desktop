using Rubezh.Add_Inf;
using Rubezh.At;
using Rubezh.UC_Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Rubezh
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private string _branchName = "";
        Rubezh_dbEntities _context = new Rubezh_dbEntities();
        private string branchName;

        public static bool HideButtons { get; set; } = false;


        public Main()
        {
            InitializeComponent();
            if (HideButtons)
            {
                // Если уровень доступа "-" скрываем кнопки в UserControl
                Add_Comp.Visibility = Visibility.Collapsed;
            }
        }

        private void Add_Comp_Click(object sender, RoutedEventArgs e)
        {
            New_Company new_Company = new New_Company(_context, this);
            new_Company.ShowDialog();
        }

        public void Button_Refr(string branchName) // добавляем параметр branchName
        {
            uc_spawn_stack.Children.Clear();
            var buttonData = _context.Rubezh_Company.ToList();
            foreach (var data in buttonData)
            {
                var button = new System.Windows.Controls.Button
                {
                    Name = "button_" + data.Rubezh_Company_id.ToString(),
                    Content = data.Rubezh_Company_Name,
                    Style = (Style)FindResource("menuButton_uc"),
                    Tag = new UC_Inf(branchName, _context)
                };
                button.Click += Button_Click;
                uc_spawn_stack.Children.Add(button);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                // Получить уникальный идентификатор филиала из имени кнопки
                string buttonId = button.Name.Split('_').Last();

                // Использовать идентификатор, чтобы получить филиал из базы данных
                int branchId = int.Parse(buttonId);
                var branch = _context.Rubezh_Company.FirstOrDefault(b => b.Rubezh_Company_id == branchId);

                // Получить наименование кнопки и сохранить его в переменную branchName
                branchName = button.Content.ToString();

                // Удалить предыдущий UserControl, если он был добавлен ранее
                Product.Children.Clear();

                // Создать новый экземпляр UserControl и добавить его на StackPanel
                var userControl = new UC_Inf(branchName, _context);
                userControl.Branch = branch;
                userControl.BranchName = branchName; // передать значение branchName в UserControl
                userControl.UpdateLayout(); // Обновить визуальный интерфейс пользовательского элемента управления
                Product.Children.Add(userControl);
            }
        }


        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        





        private void btnProduct_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnProduct;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Продукция";
            }
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            Button_Refr(_branchName);
            Price.Children.Clear();
            News.Children.Clear();

        }

        private void btnProduct_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;

        }






        private void btnNews_Click(object sender, RoutedEventArgs e)
        {
            uc_spawn_stack.Children.Clear();
            Price.Children.Clear();
            Product.Children.Clear();
            UC_News uC_News = new UC_News(branchName, _context);
            News.Children.Add(uC_News);
        }

        private void btnNews_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnNews;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Новости";
            }
        }

        private void btnNews_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }









        private void btnPriceList_Click(object sender, RoutedEventArgs e)
        {
            News.Children.Clear();
            Product.Children.Clear();
            uc_spawn_stack.Children.Clear();

            UC_Price uC_Price = new UC_Price(_context);
            Price.Children.Add(uC_Price);
        }

        private void btnPriceList_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnPriceList;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Прайс - лист";
            }
        }

        private void btnPriceList_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        








        // End: Button Close | Restore | Minimize
    }
}
