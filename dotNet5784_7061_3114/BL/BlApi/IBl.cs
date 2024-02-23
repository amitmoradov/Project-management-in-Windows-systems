


namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IProject Project { get; }
    /// <summary>
    /// Represents the current date and time .
    /// </summary>
    #region Properties

    public DateTime Clock { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Advance time of clock by hour .
    /// </summary>
    public void AdvanceTimeByHour();

    /// <summary>
    ///Advance time of clock by day .
    /// </summary>
    public void AdvanceTimeByDay();

    /// <summary>
    /// Initialize Clock Time .
    /// </summary>
    public void InitializeClockTime();

    #endregion
}

