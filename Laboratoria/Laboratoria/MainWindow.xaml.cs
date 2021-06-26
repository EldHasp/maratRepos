using Laboratoria.Model;
using Laboratoria.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;

namespace Laboratoria
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool StateClosed = true;

        private readonly MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel = new MainViewModel(new UsersModel());

            this.SourceInitialized += Window1_SourceInitialized;
            // задаем размеры основного окна
            Rect rec = SystemParameters.WorkArea;
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                return;
            }
            if (this.WindowState == WindowState.Normal)
            {
                this.Width = rec.Size.Width;
                this.Height = rec.Size.Height;
                this.Top = rec.Top;
                this.Left = rec.Left;
                return;
            }
            /////////////////////////////
        }
        // запрет перетаскивания
        private void Window1_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            HwndSource source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(WndProc);
        }

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            switch (msg)
            {
                case WM_SYSCOMMAND:
                    int command = wParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        handled = true;
                    }
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
        }


        // закрыть окно
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из программы?", "Выход с программы", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {          
                Application.Current.Shutdown();
            };
        }


        //свернуть программу
        private void ButtonTurn_Click(object sender, RoutedEventArgs e)
        {

            if (this.WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Minimized;
            }
            else if (this.WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }

        }


        private void GridBarTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

              
            }
           
        }


        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }

        //открытия нового окна аккаунтов
        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
            WindowAccaunt Window_Accaunt = new WindowAccaunt();
            Window_Accaunt.DataContext = new AccauntViewModel(viewModel.Model);
            Window_Accaunt.Show();
        }


        //выбор окошка в основной программе
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            //UserControl usc = null;
            //GridMain.Children.Clear();

            //switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            //{
            //    case "ItemBinance":
            //        usc = new UserControlBinance();
            //        GridMain.Children.Add(usc);

            //        if (ButtonCloseMenu.Visibility == Visibility.Visible)
            //        {
            //            ButtonCloseMenu.RaiseEvent(newEventArgs);
            //        }
            //        break;
            //    case "ItemCreate":
            //        //GridMain.Children.Clear();

            //        if (ButtonCloseMenu.Visibility == Visibility.Visible)
            //        {
            //            ButtonCloseMenu.RaiseEvent(newEventArgs);
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
