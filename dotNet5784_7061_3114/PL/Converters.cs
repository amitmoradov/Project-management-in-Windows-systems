using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

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
public class EngineerExperienceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int intValue)
        {
            switch (intValue)
            {
                case 0:
                    return BO.EngineerExperience.Beginner;
                case 1:
                    return BO.EngineerExperience.AdvancedBeginner;
                case 2:
                    return BO.EngineerExperience.Intermediate;
                case 3:
                    return BO.EngineerExperience.Advanced;
                case 4:
                    return BO.EngineerExperience.Expert;
                default:
                    return BO.EngineerExperience.Beginner;
            }
        }
        return BO.EngineerExperience.Beginner;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is BO.EngineerExperience experience)
        {
            switch (experience)
            {
                case BO.EngineerExperience.Beginner:
                    return 0;
                case BO.EngineerExperience.AdvancedBeginner:
                    return 1;
                case BO.EngineerExperience.Intermediate:
                    return 2;
                case BO.EngineerExperience.Advanced:
                    return 3;
                case BO.EngineerExperience.Expert:
                    return 4;
                default:
                    return 0;
            }
        }
        return 0;
    }
}