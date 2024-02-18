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
    /// Interaction logic for SingleTaskWindow.xaml
    /// </summary>

   

    public partial class SingleTaskWindow : Window
    {
        // Access to BO .
        static readonly BlApi.IBl e_bl = BlApi.Factory.Get();

        public BO.Task Task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(SingleTaskWindow), new PropertyMetadata(null));
        //Boolean variable to know whether to call the CREATE or UPDATE function
        bool isUpdateTask = false;
        public SingleTaskWindow(int id = 0)
        {
            isUpdateTask = (id != 0);
            InitializeComponent();
            if (id == 0)
            {
                Task = new BO.Task();
            }
            else
            {
                try
                {

                    Task = e_bl.Task.Read(id)!;
                }
                catch (BO.BlReadNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


    }
}
