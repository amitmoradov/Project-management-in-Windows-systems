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

namespace PL.Project;

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
        DateTime? selectedDate = datePicker.SelectedDate;
        if (selectedDate.HasValue)
        {
            e_bl.Project.SaveStartProjectDate(selectedDate.Value);
            MessageBox.Show("Project start date initialized.");
            e_bl.Project.SaveChangeOfStatus("ScheduleDetermination");
            this.Close();

            //To update the old window of ADMIN - that the button will not be active
            new ADMIN.Admin().ShowDialog();
        }
        else
        {
            MessageBox.Show("Please select a valid start date.");
        }
    }
}