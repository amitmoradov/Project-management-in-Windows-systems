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
/// Interaction logic for SingleEngineerWindow.xaml
/// </summary>
public partial class SingleEngineerWindow : Window
{
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();

    public BO.Engineer Engineer
    {
        get { return (BO.Engineer)GetValue(EngineerProperty); }
        set { SetValue(EngineerProperty, value); }
    }

    public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(SingleEngineerWindow), new PropertyMetadata(null));


    //Boolean variable to know whether to call the CREATE or UPDATE function
    bool isUpdateEngineer = false;
    public SingleEngineerWindow(int id = 0)
    {
        isUpdateEngineer = (id != 0);
        InitializeComponent();
        if (id == 0)
        {
            Engineer = new BO.Engineer();
        }
        else
        {
            try
            {

                Engineer = e_bl.Engineer.Read(id)!;
            }
            catch(BO.BlReadNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

   
    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (isUpdateEngineer)
        {
            try
            {
                e_bl.Engineer.Update(Engineer);
            }
            catch (BO.BlReadNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlIncorrectDatailException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        else
        {
            try
            {
                e_bl.Engineer.Create(Engineer);
                MessageBox.Show("Registration has been successfully completed");
               
            }
            catch (BO.BlAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlIncorrectDatailException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       this.Close();
    }
}
