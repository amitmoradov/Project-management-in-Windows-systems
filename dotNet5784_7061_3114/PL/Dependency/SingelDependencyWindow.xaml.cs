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

namespace PL.Dependency
{
    /// <summary>
    /// Interaction logic for SingelDependencyWindow.xaml
    /// </summary>
    public partial class SingelDependencyWindow : Window
    {
        // Access to BO .
        static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
        IEnumerable<int> allIds = e_bl.Task.AllTaskSId();

        public int dependentTask
        {
            get { return (int)GetValue(dependentTaskProperty); }
            set { SetValue(dependentTaskProperty, value); }
        }

        public static readonly DependencyProperty dependentTaskProperty =
            DependencyProperty.Register("dependentTaskProperty", typeof(int), typeof(SingelDependencyWindow), new PropertyMetadata(null));

        public int dependensOnTask
        {
            get { return (int)GetValue(dependensOnTaskProperty); }
            set { SetValue(dependensOnTaskProperty, value); }
        }

        public static readonly DependencyProperty dependensOnTaskProperty =
            DependencyProperty.Register("dependentTaskProperty", typeof(int), typeof(SingelDependencyWindow), new PropertyMetadata(null));

        public SingelDependencyWindow()
        {
            InitializeComponent();
        }
    }
}
