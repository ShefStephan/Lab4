using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TurtleWPF.AppViewModel;
using TurtleWPF.DataBase;
using TurtleWPF.CommandOperations;


namespace TurtleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Turtle turtle;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel(new DBAppReader(), new DBAppWriter(), new CommandInvoker(turtle));  // Устанавливаем контекст данных для привязки
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
