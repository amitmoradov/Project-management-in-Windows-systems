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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for AllPotentialTasks.xaml
    /// </summary>


    public partial class AllPotentialTasks : Window
    {
        static readonly BlApi.IBl e_bl = BlApi.Factory.Get();

        public IEnumerable<BO.TaskInList> PotentialTasks
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(PotentialTasksProperty); }
            set { SetValue(PotentialTasksProperty, value); }
        }

        public static readonly DependencyProperty PotentialTasksProperty =
            DependencyProperty.Register("PotentialTasks", typeof(IEnumerable<BO.TaskInList>), typeof(AllPotentialTasks), new PropertyMetadata(null));

        public AllPotentialTasks(BO.EngineerExperience? level)
        {
            InitializeComponent();
            PotentialTasks = e_bl.Task.ReadAll(x => (int)x.Copmliexity <= (int)level);
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        { 
                BO.TaskInList? task = (sender as ListView)?.SelectedItem as BO.TaskInList;

                new SingleTaskWindow(task!.Id).ShowDialog();
        }
    }
}
