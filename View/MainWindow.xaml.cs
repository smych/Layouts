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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ver01_TreeView.ViewModel;

namespace ver01_TreeView.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    System.Windows.Controls.ListBoxItem lbi = sender as System.Windows.Controls.ListBoxItem;
        //    if (lbi != null)
        //    {
        //        FileViewModel temp = lbi.DataContext as FileViewModel;
        //        if (temp != null)
        //        {
        //            //...
        //            System.Windows.MessageBox.Show("Выбран узел: " + temp.FileTitle.ToString());
        //        }
        //    }
        //}
    }
}
