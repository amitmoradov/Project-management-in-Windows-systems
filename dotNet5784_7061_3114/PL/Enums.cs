using System.Collections;
using System.Collections.Generic;

namespace PL;
public class ExperienceCollection : IEnumerable
{ 
    static readonly IEnumerable<BO.EngineerExperience> e_enums =
(Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();

}

public enum EngineerExperience
{
    Beginner,
    AdvancedBeginner,
    Intermediate,
    Advanced,
    Expert,
    All
}


public class StatusCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> e_enums =
(Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();

}

public enum Status
{
    Unscheduled,// לא מתוכנן
    Scheduled,//מתוכנן
    OnTrack, // בעבודה
    Done,
}

