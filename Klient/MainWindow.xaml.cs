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
using System.Diagnostics;
using MaterialDesignThemes;

namespace Dziennik_Szkolny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HideMenuButton_Click(object sender, RoutedEventArgs e)
        {
            HideMenuButton.Visibility = Visibility.Collapsed;
            ShowMenuButton.Visibility = Visibility.Visible;
        }

        private void ShowMenuButton_Click(object sender, RoutedEventArgs e)
        {
            HideMenuButton.Visibility = Visibility.Visible;
            ShowMenuButton.Visibility = Visibility.Collapsed;
        }
    }
}
