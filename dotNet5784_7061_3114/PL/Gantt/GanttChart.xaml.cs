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

namespace PL.Gantt;
    /// <summary>
    /// Interaction logic for GanttChart.xaml
    /// </summary>

    public partial class GanttChart : Window
    {
        static readonly BlApi.IBl e_bl = BlApi.Factory.Get();

        public IEnumerable<BO.Task> AllTasks
        {
            get { return (IEnumerable<BO.Task>)GetValue(AllTasksProperty); }
            set { SetValue(AllTasksProperty, value); }
        }

        public static readonly DependencyProperty AllTasksProperty =
            DependencyProperty.Register("AllTasks", typeof(IEnumerable<BO.Task>), typeof(GanttChart), new PropertyMetadata(null));

        public DateTime ProjectStartDate
        {
            get { return (DateTime)GetValue(ProjectStartDateProperty); }
            set { SetValue(ProjectStartDateProperty, value); }
        }

        public static readonly DependencyProperty ProjectStartDateProperty =
            DependencyProperty.Register("ProjectStartDate", typeof(DateTime), typeof(GanttChart), new PropertyMetadata(null));

        public GanttChart()
        {
            InitializeComponent();
            AllTasks = e_bl.Task.BringAllFieldTaskList();
            ProjectStartDate = e_bl.Project.ReturnStartProjectDate();
        }

    private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Check if the event sender is a Rectangle and if its DataContext is a Task object
        if (sender is Rectangle rectangle && rectangle.DataContext is BO.Task task)
        {
            // Retrieve the task details from the data source based on its ID
            var singleTaskInList = e_bl.Task.ReadAll(x => x.Id == task.Id);

            // Check if the task is found in the data source
            if (singleTaskInList != null && singleTaskInList.Any())
            {
                // Get the first task from the retrieved list (assuming the ID is unique)
                BO.TaskInList taskInList = singleTaskInList.First();

                // Retrieve the dependencies of the task from the data source
                var dependencies = e_bl.Task.Read(x => x.Id == task.Id).Dependencies;

                // Create a string representation of the task's dependencies
                string dependenciesText = dependencies.Any() ? string.Join("\n", dependencies.Select(dep => dep.ToString())) : "No dependencies";

                // Create a message with the task details and its dependencies
                string message = $"Task: {taskInList.ToString()}\n\nDependencies:\n{dependenciesText}";

                // Show the message to the user using a message box
                MessageBox.Show(message);
            }
            else
            {
                // If the task is not found, display an error message
                MessageBox.Show("Task not found.");
            }
        }
    }



}
