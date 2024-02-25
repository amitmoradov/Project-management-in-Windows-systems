
namespace BlImplementation;
using BlApi;
using BO;
using System;

internal class BL : IBl
{
    public IEngineer Engineer => new EngineerImplementation(this);

    public ITask Task => new TaskImplementation(this);

    public IProject Project => new ProjectImplementation();

    /// <summary>
    /// Field to ensure that clock be unique if will be more one object of Bl .
    /// </summary>
    private static DateTime s_Clock = DateTime.Now.Date;
    /// <summary>
    /// Return clock time , and set him .
    /// </summary>
    public DateTime Clock { get { return s_Clock; } private set { s_Clock = value; } }

    public void AdvanceTimeByHour()
    {
        Clock = Clock.AddHours(1);
    }

    public void AdvanceTimeByDay()
    {
        Clock = Clock.AddDays(1);
    }

    public void AdvanceTimeByYear()
    {
        // מתקדם בשנה אחת
        Clock = Clock.AddYears(1);
    }

    public void InitializeClockTime()
    {
        Clock = DateTime.Now;
    }

}
