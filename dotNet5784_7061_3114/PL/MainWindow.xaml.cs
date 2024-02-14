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
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Data_Initialization(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Would you like to create Initial data?", "Data Initialization", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            DalTest.Initialization.Do();
        }
    }

    private void btnEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineesListrWindow().Show();
    }
}