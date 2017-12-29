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

namespace DormitoryGUI
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void NavigateMainPage()
        {
            MainContainer.NavigationService.Navigate(new MainPage(this));
        }

        public void NavigatePage(Page page)
        {
            MainContainer.NavigationService.Navigate(page);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigatePage(new LoginPage(this));
        }

        private void MainContainer_ContentRendered(object sender, EventArgs e)
        {
            MainContainer.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        private void MainContainer_Navigated(object sender, NavigationEventArgs e)
        {
            Title = (e.Content as Page).Title;
        }

        /* private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CollapseButton_Click(object sender, EventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        } */
    }
}
