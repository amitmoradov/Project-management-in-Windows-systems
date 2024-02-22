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

namespace PL.ADMIN
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnEngineers_Click(object sender, RoutedEventArgs e)
        {
            new EngineesListrWindow().ShowDialog();
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
    }
}
