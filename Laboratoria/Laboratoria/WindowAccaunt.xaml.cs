using Laboratoria.Class;
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
            InitializeComponent();
            //grid_ValidationsData.DataContext = _validation;
        }

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
      


    }
}
