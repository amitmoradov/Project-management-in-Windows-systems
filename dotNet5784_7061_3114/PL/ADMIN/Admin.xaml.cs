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
    }
}
