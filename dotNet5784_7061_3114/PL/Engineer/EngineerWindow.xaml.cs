using BO;
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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.All;

    public EngineerWindow()
    {
        InitializeComponent();
        EngineerList = e_bl?.Engineer.ReadAll()!;
    }

    // לעבור על ההבנה של מה שרשום פה
    public IEnumerable<BO.Engineer> EngineerList
    {
        get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));

    private void cbExpirenceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        EngineerList = (Experience == BO.EngineerExperience.All) ?
        e_bl?.Engineer.ReadAll()! : e_bl?.Engineer.ReadAll(item => (int)item.Level == (int)Experience)!;

    }

    private void tnAddUpdate_Click(object sender, RoutedEventArgs e)
    {

    }
}
