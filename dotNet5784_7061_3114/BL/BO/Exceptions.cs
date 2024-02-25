
using DO;

namespace BO;

/// <summary>
/// The item is already exists
/// </summary>
[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string msg, DO.DalDoesExistException ex) : base(msg, ex) { }
    public BlAlreadyExistsException(string message) : base(message) { }
}

/// <summary>
/// The datails is not correct
/// </summary>
[Serializable]
public class BlIncorrectDatailException : Exception
{
    public BlIncorrectDatailException(string msg) : base(msg) { }
}

/// <summary>
/// The entity cannot be deleted
/// </summary>
[Serializable]
public class BlEntityCanNotRemoveException : Exception
{
    public BlEntityCanNotRemoveException(string msg) : base(msg) { }
}

/// <summary>
/// The entity is not exist
/// </summary>
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string msg): base(msg) { }
    public BlDoesNotExistException(string msg, DO.DalDoesNotExistException ex) : base(msg, ex) { }
}

/// <summary>
/// 
/// </summary>
[Serializable]
public class BlReadNotFoundException : Exception
{
    public BlReadNotFoundException(string msg) : base(msg) { }
}

[Serializable]
public class BlCannotDeletedException : Exception
{
    public BlCannotDeletedException(string msg, DO.DalCannotDeleted ex) : base(msg, ex) { }
    public BlCannotDeletedException(string msg): base(msg) { }
}

[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}

/// <summary>
/// Already planed the plan.
/// </summary>
[Serializable]
public class BlAlreadyPalnedException : Exception
{
    public BlAlreadyPalnedException(string msg) : base(msg) { }
}

[Serializable]
public class BlStartDateAlreadyExistsException : Exception
{
    public BlStartDateAlreadyExistsException(string msg) : base(msg) { }
}

[Serializable]
public class BlCompleteDateAlreadyExistsException : Exception
{
    public BlCompleteDateAlreadyExistsException(string msg) : base(msg) { }
}

[Serializable]
public class BlCannotUpdateException : Exception
{
    public BlCannotUpdateException(string msg) : base(msg) { }
}

[Serializable]
public class BlEngineerWorkingOnTask : Exception
{
    public BlEngineerWorkingOnTask(string msg) : base(msg) { }
}

[Serializable]
public class BlEngineerIsNotTheAllowedLevel : Exception
{
    public BlEngineerIsNotTheAllowedLevel(string msg) : base(msg) { }
}

[Serializable]
public class BlEngineerWorkingOnAnotherTask : Exception
{
    public BlEngineerWorkingOnAnotherTask(string msg) : base(msg) { }
}

[Serializable]
public class BlCannotAddDependencyException : Exception
{
    public BlCannotAddDependencyException(string msg) : base(msg) { }
}

[Serializable]
public class BlDependencyAlreadyExistException : Exception
{
    public BlDependencyAlreadyExistException(string msg, DO.DalDoesExistException ex) : base(msg, ex) { }
    public BlDependencyAlreadyExistException(string msg) : base(msg) { }
}