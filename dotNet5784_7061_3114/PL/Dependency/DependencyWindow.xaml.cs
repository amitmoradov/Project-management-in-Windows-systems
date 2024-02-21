using PL.Task;
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

namespace PL.Dependency;

/// <summary>
/// Interaction logic for DependencyWindow.xaml
/// </summary>
public partial class DependencyWindow : Window
{
    // Access to BO .
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();

    ////Boolean variable to know whether to call the CREATE or UPDATE function
    //bool isUpdateTask = false;

    public IEnumerable<BO.TaskInList> Depedencies
    {
        get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("Depedencies", typeof(IEnumerable<BO.TaskInList>), typeof(DependencyWindow), new PropertyMetadata(null));

    public DependencyWindow()
    {
        InitializeComponent();
    }

    private void AddDependency(object sender, RoutedEventArgs e)
    {
        new SingelDependencyWindow().ShowDialog();
    }
    private void DeleteDependency(object sender, RoutedEventArgs e)
    {
        new SingelDependencyWindow().ShowDialog();
    }

}
