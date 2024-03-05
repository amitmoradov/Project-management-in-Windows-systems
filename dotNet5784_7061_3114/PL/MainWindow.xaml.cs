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
    private static readonly BlApi.IBl e_bl = BlApi.Factory.Get();


    // Dependency property for CurrentTime
    public static readonly DependencyProperty CurrentTimeProperty =
        DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(DateTime.Now));

    // Property for accessing CurrentTime
    public DateTime CurrentTime
    {
        get { return (DateTime)GetValue(CurrentTimeProperty); }
        set { SetValue(CurrentTimeProperty, value); }
    }

    public MainWindow()
    {
        InitializeComponent();
        // Initialize CurrentTime
        CurrentTime = e_bl.Project.ReturnVirtualTimeInDal();
    }

    // Button click event handlers for advancing time
    private void AdvanceHour_Click(object sender, RoutedEventArgs e)
    {
        e_bl.AdvanceTimeByHour();
        CurrentTime = e_bl.Clock;
    }

    private void AdvanceDay_Click(object sender, RoutedEventArgs e)
    {
        e_bl.AdvanceTimeByDay();
        CurrentTime = e_bl.Clock;
    }

    private void AdvanceYear_Click(object sender, RoutedEventArgs e)
    {
        e_bl.AdvanceTimeByYear();
        CurrentTime = e_bl.Clock;
    }

    // Button click event handler for resetting time
    private void ResetTime_Click(object sender, RoutedEventArgs e)
    {
        e_bl.InitializeClockTime();
        CurrentTime = e_bl.Clock;
    }


    private void AdminButton(object sender, RoutedEventArgs e)
    {
        new ADMIN.Admin().ShowDialog();
    }

    private void InsertEngineer(object sender, RoutedEventArgs e)
    {
        new Engineer.InsertEngineerWindow().ShowDialog();
    }

    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e_bl.Project.SaveVirtualTimeInDal(e_bl.Clock);
    }
}