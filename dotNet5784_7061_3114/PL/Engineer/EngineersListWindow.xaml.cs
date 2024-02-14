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
public partial class EngineesListrWindow : Window
{
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.All;

    public EngineesListrWindow()
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
        DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineesListrWindow), new PropertyMetadata(null));

    /// <summary>
    /// Filter the list of engineer by level.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cbExpirenceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        EngineerList = (Experience == BO.EngineerExperience.All) ?
        e_bl?.Engineer.ReadAll()! : e_bl?.Engineer.ReadAll(item => (int)item.Level == (int)Experience)!;

    }

    private void AddEngineer(object sender, RoutedEventArgs e)
    {
        new SingleEngineerWindow().ShowDialog();
        //To refresh window after Create
        EngineerList = e_bl?.Engineer.ReadAll()!;
    }

    private void UpdateListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;

        new SingleEngineerWindow(engineer!.Id).ShowDialog();
        //To refresh window after Update
        EngineerList = e_bl?.Engineer.ReadAll()!;

    }
}
