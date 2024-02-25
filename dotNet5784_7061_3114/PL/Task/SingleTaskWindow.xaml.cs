using BO;
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

        //Boolean variable to know whether to call the CREATE or UPDATE function
        bool isUpdateTask = false;

        public BO.Task Task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(SingleTaskWindow), new PropertyMetadata(null));

        public SingleTaskWindow(int id)
        {

            InitializeComponent();
            // chack if the task is exist
            BO.Task? privuseTask = e_bl.Task.Read(id);

            // If the task is not exist, Create new task
            if (privuseTask == null)
            {
                Task = new BO.Task();
                // boolen = false because we create new task 
                isUpdateTask = false;
            }
            else
            {
                // Copy the field to task for Update it
                Task = privuseTask;
                // boolen = true because we update the old task 
                isUpdateTask = true;
            }
        }

        private void btnAddUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdateTask)
            {
                try
                {
                    e_bl.Task.Update(Task);
                    MessageBox.Show("Registration has been successfully completed");
                }
                catch (BO.BlReadNotFoundException ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlNullPropertyException ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlIncorrectDatailException ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlCannotUpdateException ex)
                { MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error); }

                catch (BO.BlDoesNotExistException ex)
                { MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error); }

                //Tasks can not deleted if I am in step 3
                catch (BO.BlAlreadyPalnedException ex)
                { MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch(BO.BlEngineerIsNotTheAllowedLevel ex)
                {
                    { MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error); }
                }
                catch (BO.BlEngineerWorkingOnAnotherTask ex)
                {
                    { MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error); }
                }
            }

            else
            {
                try
                {
                    //It is allowed to extend a task as long as I am not in stage 3
                    if (e_bl.Project.ReturnStatusProject() != "scheduleWasPalnned")
                    {
                        e_bl.Task.Create(Task);
                        MessageBox.Show("Registration has been successfully completed");                    
                    }

                }
                catch (BO.BlAlreadyExistsException ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlNullPropertyException ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlIncorrectDatailException ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            this.Close();
        }

        private void EditDependenciesButton_Click(object sender, RoutedEventArgs e)
        {
            // Call with task id to show the task details of the dependent task .
            new Dependency.SingelDependencyWindow(Task.Id).ShowDialog();
        }

        private void DeleteDependecy_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Prompt the user with a confirmation message box
            var result = MessageBox.Show("Do you want to delete this dependency?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Check if the user clicked Yes
            if (result == MessageBoxResult.Yes)
            {
                // Check if the sender is a ListView
                if (sender is ListView element)
                {
                    // Check if an item is selected in the ListView
                    if (element.SelectedItem is BO.TaskInList taskInList)
                    {
                        // Find the dependency to remove from the Task's Dependencies list
                        var dependencyToRemove = Task.Dependencies.FirstOrDefault(dependency => dependency.Id == taskInList.Id);

                        // If the dependency is found, delete it
                        if (dependencyToRemove != null)
                        {
                            e_bl.Task.DeleteDependency(Task.Id, dependencyToRemove.Id);
                            MessageBox.Show("The delete of dependency was successfully");
                            Close();
                        }
                    }
                }

                // Close the current window
                this.Close();

                // Open a new window to update the task details
                new SingleTaskWindow(Task.Id).ShowDialog();
            }
        }

    }
}
