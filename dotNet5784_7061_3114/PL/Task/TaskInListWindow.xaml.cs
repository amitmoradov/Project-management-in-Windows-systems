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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskInListWindow.xaml
    /// </summary>
    public partial class TaskInListWindow : Window
    {
        static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
        public BO.Status StatusTask { get; set; } = BO.Status.All;
        public TaskInListWindow()
        {
            InitializeComponent();
            TaskInLists= e_bl.Task.ReadAll()!;
           
        }

        public IEnumerable<BO.TaskInList> TaskInLists
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskInLists", typeof(IEnumerable<BO.TaskInList>), typeof(TaskInListWindow), new PropertyMetadata(null));

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? task = (sender as ListView)?.SelectedItem as BO.TaskInList;

            new SingleTaskWindow(task!.Id).ShowDialog();
            //To refresh window after Update
            TaskInLists = e_bl?.Task.ReadAll()!;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            BO.Task task = new();
            new SingleTaskWindow(task.Id).ShowDialog();
            //To refresh window after Create
            TaskInLists = e_bl?.Task.ReadAll()!;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskInLists = (StatusTask == BO.Status.All) ?
          e_bl?.Task.ReadAll()! : e_bl?.Task.ReadAll(item => (int)item.Status == (int)StatusTask)!;
        }
    }
}
