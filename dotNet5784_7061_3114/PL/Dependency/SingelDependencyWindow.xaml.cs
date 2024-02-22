using BO;
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
/// Interaction logic for SingelDependencyWindow.xaml
/// </summary>
public partial class SingelDependencyWindow : Window
{
    // Access to BO .
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    public IEnumerable<int> AllTasksIds
    {
        get { return (IEnumerable<int>)GetValue(AllTasksIdsProperty); }
        set { SetValue(AllTasksIdsProperty, value); }
    }

    public static readonly DependencyProperty AllTasksIdsProperty =
        DependencyProperty.Register("AllTasksIds", typeof(IEnumerable<int>), typeof(SingelDependencyWindow), new PropertyMetadata(null));



    public int DependentTask
    {
        get { return (int)GetValue(DependentTaskProperty); }
        set { SetValue(DependentTaskProperty, value); }
    }

    public static readonly DependencyProperty DependentTaskProperty =
        DependencyProperty.Register("DependentTask", typeof(int), typeof(SingelDependencyWindow), new PropertyMetadata(null));

    public int DependensOnTask
    {
        get { return (int)GetValue(DependensOnTaskProperty); }
        set { SetValue(DependensOnTaskProperty, value); }
    }

    public static readonly DependencyProperty DependensOnTaskProperty =
        DependencyProperty.Register("DependensOnTask", typeof(int), typeof(SingelDependencyWindow), new PropertyMetadata(null));
    BO.Task editTask = new();
    public SingelDependencyWindow(int taskId)
    {
        AllTasksIds = e_bl.Task.AllTaskSId();
        
        InitializeComponent();
        if (taskId != 0)
        {
            ShowDependeentTaskDetails(taskId);
        }
    }

    private void ShowDependeentTaskDetails(int taskId)
    {
         editTask = e_bl.Task.Read(taskId);
        MessageBox.Show("Information on Task\n"+
            $"Task ID: {editTask.Id}\n" +
            $"Task Description: {editTask.Description}\n" +
            $"Task Status: {editTask.Status}");
    }
    private void AddDependency(object sender, RoutedEventArgs e)
    {
        try
        {
            e_bl.Task.AddDependency(DependentTask, DependensOnTask);
            Close();
        }
        catch (Exception ex) { }
    }
    private void DeleteDependency(object sender, RoutedEventArgs e)
    {
        try
        {
            e_bl.Task.DeleteDependency(DependentTask, DependensOnTask);
            Close();
        }
        catch (BlDoesNotExistException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ShowDependentTaskDescription(object sender, SelectionChangedEventArgs e)
    {
        
        // Show the description of dependent task .
        BO.Task? dependentTask = e_bl.Task.Read(DependentTask);
        MessageBox.Show($"{dependentTask.Description}");
    }

    private void ShowDependensOnTaskDescription(object sender, SelectionChangedEventArgs e)
    {
        // Show the description of dependent task .
        BO.Task? dependensOnTask = e_bl.Task.Read(DependensOnTask);
        MessageBox.Show($"{dependensOnTask.Description}");
    }
}
