using PL.Engineer;
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

namespace PL.ADMIN;

/// <summary>
/// Interaction logic for Admin.xaml
/// </summary>
public partial class Admin : Window
{
    // Access to BO .
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    public Admin()
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
        //Update the window after init.
        this.Close();
        new Admin().ShowDialog();
    }

    private void ResetDataBase(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Would you like to Reset data?", "Reset DataBase", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            e_bl.Project.resetAllDB();
        }
        //Update the window after reset.
        this.Close();
        new Admin().ShowDialog();
    }

    private void btnEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineersListWindow().ShowDialog();
    }

    private void btnTasks_Click(object sender, RoutedEventArgs e)
    {
        new Task.TaskInListWindow().ShowDialog();
    }
    private void Viewdependencies(object sender, RoutedEventArgs e)
    {
        // Call with out the id of task , to add/delete from out of task window .
            new Dependency.SingelDependencyWindow(0).ShowDialog();
    }

    private void StartProject_Click(object sender, RoutedEventArgs e)
    {
        //To update the window after insert start date of project
        //(The command closes the current window and after you enter the changing date,
        //the window returns updated, that is, without the option to click this button)
        this.Close();
        new Project.StartProjectDateWindow().ShowDialog();
    }

    private void btnGanttChart_Click(object sender, RoutedEventArgs e)
    {
        new Gantt.GanttChart().ShowDialog();
    }
}
