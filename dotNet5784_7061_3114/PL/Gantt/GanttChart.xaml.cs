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

namespace PL.Gantt
{
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
            if (sender is Rectangle rectangle)
            {
                if (rectangle.DataContext is BO.Task task)
                {
                    var singleTaskInList = e_bl.Task.ReadAll(x => x.Id == task.Id);
                    if (singleTaskInList != null)
                    {
                        BO.TaskInList taskInList = singleTaskInList.First();
                        MessageBox.Show(taskInList.ToString());
                    }
                }
            }
        }
    }
}