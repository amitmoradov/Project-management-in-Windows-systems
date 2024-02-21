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

namespace PL.Project
{
    /// <summary>
    /// Interaction logic for StartProjectDateWindow.xaml
    /// </summary>
    public partial class StartProjectDateWindow : Window
    {
        static readonly BlApi.IBl e_bl = BlApi.Factory.Get();

        public StartProjectDateWindow()
        {
            InitializeComponent();
        }

        private void InitializationProjectStartDate_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
