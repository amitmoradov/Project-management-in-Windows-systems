using System.Globalization;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using BO;
namespace PL;
/// <summary>
/// Converts between a field from the layer in PL and BL
/// </summary>

class ConvertIdToContent : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}


class AccsessToDeleteButtun : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value != 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}


/// <summary>
/// the function checks if the received value (the number) is 0. If the value is 0, the function returns true, 
/// allowing the field to be edited. If the value is different from 0,
/// it returns false, and then the field will not be editable.
/// </summary>
class IdToIsEnabledConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}


//For Task : Engineer that work on the task
/// <summary>
///The permissions allowed in the project in step 3
/// </summary>
class scheduleWasPalnnedIsEnabled : IValueConverter
{
    // Access to BO .
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (e_bl.Project.ReturnStatusProject() == "scheduleWasPalnned")
        {  
            
            return true;
            
        }
        else
        {
            return false;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}

//For Task : Engineer that work on the task
/// <summary>
///The permissions are not allowed in the project in step 3
/// </summary>
class scheduleWasPalnnedIsNotEnabled : IValueConverter
{
    // Access to BO .
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (e_bl.Project.ReturnStatusProject() == "scheduleWasPalnned")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}

/// <summary>
/// The permissions allowed in the project in step 2
/// </summary>
class ScheduleDeterminationIsEnabled : IValueConverter
{
    // Access to BO .
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (e_bl.Project.ReturnStatusProject() == "ScheduleDetermination" && value == null)
        {
            return true;
        }
        else if (value is not null)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}


/// <summary>
/// The permissions allowed in the project in step 1
/// </summary>
class planningIsEnabled : IValueConverter
{
    // Access to BO .
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (e_bl.Project.ReturnStatusProject() == "planning")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
   
}

/// <summary>
/// Convert type of RequiredEffortTime to int , for widtt of rectangle in Gantt chart .
/// </summary>
class ConvertRequiredEffortTimeToInt : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TimeSpan requiredEffortTime)
        {
            // כמות הימים
            return (int)requiredEffortTime.TotalDays * 26;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

public class ConvertDateTimeToInt : IValueConverter
{
    // Access to BO .
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            // ממיר את התאריך למספר שמיועד לשימוש כמוקד בתרשים גאנט
            // לדוגמה, ניתן להמיר את התאריך למספר הימים מתחילת השנה
            TimeSpan difference = dateTime - e_bl.Project.ReturnStartProjectDate();
            return difference.Days*26;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

class AccsessTCreateDate : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}

public class ConvertStatusTaskGantt : IValueConverter
{
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value is BO.Task task)
        {
            // Dictionary mapping status to colors
            Dictionary<string, string> statusColors = new Dictionary<string, string>()
            {
                { "Scheduled", "Aqua" },
                { "OnTrack", "Yellow" },
                { "Done", "Green" },            
            };

            // Get the status of the task
            BO.Status status = task.Status;     

            //If the task is late . 
            if (e_bl.Clock > task.ForcastDate && task.Status != BO.Status.Done)
            {
                return "Red";
            }

            // Check if the status is in the dictionary
            if (statusColors.ContainsKey(status.ToString()))
            {
                // Return the color corresponding to the status
                return statusColors[status.ToString()];
            }

        }

        // Return default color if value is not a Task object
        return "Aqua";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}


