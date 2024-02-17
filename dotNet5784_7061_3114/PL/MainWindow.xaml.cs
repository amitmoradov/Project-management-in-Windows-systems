using PL.Engineer;
//using PL.Engineer;
using System.Text;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Data_Initialization(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Would you like to create Initial data?", "Data Initialization", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            e_bl.Project.InitializeDB();
        }
    }

    private void ResetDataBase(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Would you like to Reset data?", "Reset DataBase", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            e_bl.Project.resetAllDB();  
        }
    }

    private void AdminButton(object sender, RoutedEventArgs e)
    {
        new ADMIN.Admin().ShowDialog();
    }
}