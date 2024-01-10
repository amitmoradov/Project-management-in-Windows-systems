
namespace DO;

/// <summary>
/// Data of Engineer .
/// </summary>
/// <param name="Id"> Id of the engineer</param>
/// <param name="Email"> Email of the engineer</param>
/// <param name="Cost">Cost to hour .</param>
/// <param name="Name">Name of the engineer .</param>
/// <param name="Level">Level of the engineer .</param>
public record Engineer
(
    int _id,
    double? _cost,
    DO.EngineerExperience? _level,
    string? _email = null,
    string? _name = null,
    bool _active = true,
    bool _canToRemove = true

)
{
    public Engineer () : this(0, 0, null) { }
}
