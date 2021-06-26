using Laboratoria.Class;
using Laboratoria.Dto;
using Laboratoria.ViewModels;
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
using System.Windows.Threading;

namespace Laboratoria
{
    /// <summary>
    /// Логика взаимодействия для WindowAccaunt.xaml
    /// </summary>
    public partial class WindowAccaunt : Window
    {

        //private Validations _validation = new Validations();  
        private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        public WindowAccaunt()
        {
            DataContextChanged += (s, e) => viewModels = (AccauntViewModel)e.NewValue;
            InitializeComponent();
            //grid_ValidationsData.DataContext = _validation;
        }
        private AccauntViewModel viewModels;
        // выход  с окошка
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы закончили ввод данных?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        // перетаскивание окна
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void OnLabsFilter(object sender, FilterEventArgs e)
        {
            UserSmenaDto user = (UserSmenaDto)e.Item;
            e.Accepted = user.Position == viewModels.Positons[1];
        }

        private void OnNachsFilter(object sender, FilterEventArgs e)
        {
            UserSmenaDto user = (UserSmenaDto)e.Item;
            e.Accepted = user.Position == viewModels.Positons[0];
        }
    }
}
